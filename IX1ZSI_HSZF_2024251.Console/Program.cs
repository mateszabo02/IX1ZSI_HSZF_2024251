using ConsoleTools;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.Common.Utilities;
using System.Globalization;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using System.Xml.Serialization;
namespace RealEstate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Adatbázis, szolgáltatások inicializálása
            var host = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
            {
                services.AddScoped<RealEstateDbContext>();
                services.AddSingleton<IPropertyRepository, PropertyRepository>();
                services.AddSingleton<IPropertyService<Property>, PropertyService>();
                services.AddSingleton<ICustomerRepository, CustomerRepository>();
                services.AddSingleton<ICustomerService<Customer>, CustomerService>();
                services.AddSingleton<IContractRepository, ContractRepository>();
                services.AddSingleton<IContractService<Contract>, ContractService>();
            }).Build();
            host.Start();

            using IServiceScope serviceScope = host.Services.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;
            
            var propertyService = serviceProvider.GetService<IPropertyRepository>();
            var customerService = serviceProvider.GetService<ICustomerRepository>();
            var contractService = serviceProvider.GetService<IContractRepository>();

            propertyService.PropertyInserted += ListMatchingCustomers;
            customerService.CustomerInserted += ListMatchingProperties;

            //Összes property listázása az adatbázisból
            void ListAllProperties()
            {
                Console.WriteLine("The properties in the database:");
                foreach(var item in propertyService.ReadAll().ToList())
                {
                    Console.WriteLine(item.ToString());
                }
                Console.ReadKey();
            }

            //Összes customer listázása az adatbázisból
            void ListAllCustomers()
            {
                Console.WriteLine("The customers in the database:");
                foreach (var item in customerService.ReadAll().ToList())
                {
                    Console.WriteLine(item.ToString());
                }
                Console.ReadKey();
            }

            //Összes contract listázása az adatbázisból
            void ListAllContract()
            {
                Console.WriteLine("The contracts in the database:");
                foreach (var item in contractService.ReadAll().ToList())
                {
                    Console.WriteLine(item.ToString());
                }
                Console.ReadKey();
            }

            //Új property hozzáadása
            void AddNewProperty()
            {
                Property property = new Property();
                Console.WriteLine("Enter District: ");
                property.District = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Address: ");
                property.Address = Console.ReadLine();
                Console.WriteLine("Enter Area of property: ");
                property.Area = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                Console.WriteLine("Enter number of Rooms: ");
                property.Rooms = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter SellingPrice");
                try
                {
                    property.SellingPrice = UInt32.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    property.SellingPrice = null;
                }
                Console.WriteLine("Enter RentPrice");
                try
                {
                    property.RentPrice = int.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    property.RentPrice = null;
                }
                propertyService.Create(property);
            }

            //Új customer hozzáadása
            void AddNewCustomer()
            {
                Customer customer = new Customer();
                List<int> pDistricts = new List<int>(); //preferred Districts
                string inputDistrict = "";
                Console.WriteLine("Enter preferred Districts. (Leave empty to finish): ");
                do
                {
                    Console.WriteLine("District: ");
                    inputDistrict = Console.ReadLine();
                    try
                    {
                        int intD = int.Parse(inputDistrict);
                        if(intD < 1 ||  intD > 23)
                        {
                            Console.WriteLine("District is not valid in Budapest!");
                        }
                        else
                        {
                            pDistricts.Add(intD);
                        }
                    }
                    catch(FormatException ex)
                    {
                        if(inputDistrict != "")
                        {
                            Console.WriteLine("Not a valid number");
                        }
                    }
                } while (inputDistrict != "");

                customer.DistrictPreferences = pDistricts;
                Console.WriteLine("Enter the minimum number of Rooms");
                customer.MinRooms = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter the maximum number of Rooms");
                customer.MaxRooms = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter the minimum Area of the property");
                customer.MinArea = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                Console.WriteLine("Enter the maximum Area of the property");
                customer.MaxArea = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                Console.WriteLine("Enter the minimum Price of the property");
                customer.MinPrice = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter the minimum Price of the property");
                customer.MaxPrice = UInt32.Parse(Console.ReadLine());

                Console.WriteLine("Looking for a rent? (true/false) ");
                customer.LookingForRent = bool.Parse(Console.ReadLine());
                Console.WriteLine("Looking for a purchase? (true/false) ");
                customer.LookingForPurchase = bool.Parse(Console.ReadLine());

                customerService.Create(customer);
            }

            //Új contract hozzáadása
            void AddNewContract()
            {
                Contract contract = new Contract();
                Console.Write("Enter the Id of the property: ");
                contract.PropertyId = int.Parse(Console.ReadLine());
                Console.WriteLine($"The data of the property: {propertyService.Read(contract.PropertyId).ToString()}");

                Console.Write("Enter the Id of the seller: ");
                contract.SellerId = int.Parse(Console.ReadLine());
                Console.WriteLine($"The seller's data: {customerService.Read(contract.SellerId).ToString()}");

                Console.Write("Enter the Id of the buyer: ");
                contract.BuyerId = int.Parse(Console.ReadLine());
                Console.WriteLine($"The buyer's data: {customerService.Read(contract.BuyerId).ToString()}");

                Console.Write("Enter the Price: ");
                contract.Price = UInt32.Parse(Console.ReadLine());
                Console.Write("Enter the SignDate: ");
                contract.SignDate = DateTime.Parse(Console.ReadLine());
                Console.Write("Enter the ContractExpiration: ");
                try
                {
                    contract.ContractExpiration = DateTime.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    contract.ContractExpiration = null;
                }
                contractService.Create(contract);

                Property modified = propertyService.Read(contract.PropertyId);
                Customer seller = customerService.Read(contract.SellerId);
                Customer buyer = customerService.Read(contract.BuyerId);
                if(contract.ContractExpiration == null)
                {
                    //The contract is a sale, so the buyer is no longer looking for a purchase
                    buyer.OwnedProperties.Add(modified);
                    buyer.LookingForPurchase = false;
                    seller.OwnedProperties.Remove(modified);
                }
                else
                {
                    buyer.RentedProperties.Add(modified);
                    seller.RentedProperties.Remove(modified);
                }
                modified.RentPrice = null;
                modified.SellingPrice = null;

                propertyService.Update(modified);
                customerService.Update(seller);
                customerService.Update(buyer);

                Console.Write("The contract is saved");
                Console.ReadKey();
            }

            //Property frissítése
            void UpdateProperty()
            {
                Console.Write("Enter the property's Id to update: ");
                int propId = int.Parse(Console.ReadLine());

                Property update = propertyService.Read(propId);
                Console.Write($"Enter the District (Existing: {update.District}): ");
                update.District = int.Parse(Console.ReadLine());
                Console.Write($"Enter the Address (Existing: {update.Address}): ");
                update.Address = Console.ReadLine();
                Console.Write($"Enter the Area (Existing: {update.Area}): ");
                update.Area = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                Console.Write($"Enter the number of Rooms (Existing: {update.Rooms}): ");
                update.Rooms = int.Parse(Console.ReadLine());
                Console.Write($"Enter the SellingPrice (Existing: {update.SellingPrice}): ");
                update.SellingPrice = UInt32.Parse(Console.ReadLine());
                Console.Write($"Enter the RentPrice (Existing: {update.RentPrice}): ");
                update.RentPrice = int.Parse(Console.ReadLine());

                propertyService.Update(update);
                Console.Write("The property is saved!");
                Console.ReadKey();
            }

            //Customer frissítése
            void UpdateCustomer()
            {
                Console.WriteLine("Enter the customer's Id to update:");
                int customerId = int.Parse(Console.ReadLine());

                Customer update = customerService.Read(customerId);

                Console.Write($"Enter the minimum number of rooms (existing: {update.MinRooms}): ");
                update.MinRooms = int.Parse(Console.ReadLine());
                Console.Write($"Enter the maximum number of rooms (existing: {update.MaxRooms}): ");
                update.MaxRooms = int.Parse(Console.ReadLine());

                Console.Write($"Enter the minimum area of property (existing: {update.MinArea}): ");
                update.MinArea = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                Console.Write($"Enter the maximum area of property (existing: {update.MaxArea}): ");
                update.MaxArea = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                Console.Write($"Enter the minimum price of property (existing: {update.MinPrice}): ");
                update.MinPrice = int.Parse(Console.ReadLine());
                Console.Write($"Enter the maximum price of property (existing: {update.MaxPrice}): ");
                update.MaxPrice = UInt32.Parse(Console.ReadLine());


                Console.Write("Looking for rent? (true/false) ");
                update.LookingForRent = bool.Parse(Console.ReadLine());
                Console.Write("Looking for purchase? (true/false) ");
                update.LookingForPurchase = bool.Parse(Console.ReadLine());
                customerService.Update(update);
                Console.Write("The customer saved.");
                Console.ReadKey();
            }

            //Contract frissítése
            void UpdateContract()
            {
                Console.WriteLine("Enter the contract's Id to update:");
                int contractId = int.Parse(Console.ReadLine());

                Contract update = contractService.Read(contractId);

                Console.Write($"Enter the Price (existing: {update.Price}): ");
                update.Price = UInt32.Parse(Console.ReadLine());
                Console.Write($"Enter the SignDate (existing: {update.SignDate}): ");
                update.SignDate = DateTime.Parse(Console.ReadLine());
                Console.Write($"Enter the ConractExpiration (existing: {update.ContractExpiration}): ");
                update.ContractExpiration = DateTime.Parse(Console.ReadLine());

                contractService.Update(update);
                Console.Write("The contract saved.");
                Console.ReadKey();
            }

            void DeleteProperty()
            {
                Console.Write( "The Id of the property to delete: ");
                int propId = int.Parse(Console.ReadLine());

                propertyService.Delete(propId);
                Console.Write("The property is deleted!");
                Console.ReadKey( );
            }
            void DeleteCustomer()
            {
                Console.Write("The Id of the customer to delete: ");
                int custId = int.Parse(Console.ReadLine());

                customerService.Delete(custId);
                Console.Write("The customer is deleted!");
                Console.ReadKey();
            }
            void DeleteContract()
            {
                Console.Write("The Id of the contract to delete: ");
                int contId = int.Parse(Console.ReadLine());

                contractService.Delete(contId);
                Console.Write("The contract is deleted!");
                Console.ReadKey();
            }

            //Properties for sale lekérdezése
            void PropertiesForSale()
            {
                List<Property> properties = new List<Property>();
                Console.WriteLine("Search propertis for Sale. Enter the parameters:");
                Console.Write("District: ");
                int? district = null;
                try
                {
                    district = int.Parse(Console.ReadLine());
                }
                catch (FormatException ex)
                {
                    district = null;
                }

                Console.Write("Min size of Area: ");
                double? minArea = null;
                try
                {
                    minArea = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                }
                catch (FormatException ex)
                {
                    minArea = null;
                }

                Console.Write("Max size of Area: ");
                double? maxArea = null;
                try
                {
                    maxArea = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                }
                catch (FormatException ex)
                {
                    maxArea = null;
                }

                Console.Write("Lowest Price: ");
                int? minPrice = null;
                try
                {
                    minPrice = int.Parse(Console.ReadLine());
                }
                catch (FormatException ex)
                {
                    minPrice = null;
                }

                Console.Write("Highest Price: ");
                int? maxPrice = null;
                try
                {
                    maxPrice = int.Parse(Console.ReadLine());
                }
                catch (FormatException ex)
                {
                    maxPrice = null;
                }

                Console.Write("Minimum number of rooms: ");
                int? minRoomNo = null;
                try
                {
                    minRoomNo = int.Parse(Console.ReadLine());
                }
                catch (FormatException ex)
                {
                    minRoomNo = null;
                }

                Console.Write("Maximum number of rooms: ");
                int? maxRoomNo = null;
                try
                {
                    maxRoomNo = int.Parse(Console.ReadLine());
                }
                catch (FormatException ex)
                {
                    maxRoomNo = null;
                }

                Expression<Func<Property, bool>> districtExpr = (district) switch
                {
                    (not null) => x => x.District == district,
                    (null) => x => true
                };

                Expression<Func<Property, bool>> areaExpr = (minArea, maxArea) switch
                {
                    (null, not null) => x => x.Area <= maxArea,
                    (not null, not null) => x => x.Area >= minArea || x.Area <= maxArea,
                    (not null, null) => x => x.Area >= minArea,
                    (null, null) => x => true
                };

                Expression<Func<Property, bool>> priceExpr = (minPrice, maxPrice) switch
                {
                    (null, not null) => x => x.SellingPrice <= maxPrice,
                    (not null, not null) => x => x.SellingPrice >= minPrice || x.SellingPrice <= maxPrice,
                    (not null, null) => x => x.SellingPrice >= minPrice,
                    (null, null) => x => x.SellingPrice != null
                };

                Expression<Func<Property, bool>> roomExpr = (minRoomNo, maxRoomNo) switch
                {
                    (null, not null) => x => x.Rooms <= maxRoomNo,
                    (not null, not null) => x => x.Rooms >= minRoomNo || x.Rooms <= maxRoomNo,
                    (not null, null) => x => x.Rooms >= minRoomNo,
                    (null, null) => x => true
                };

                var q4 = propertyService
                            .ReadAll()
                            .Where(districtExpr)
                            .Where(areaExpr)
                            .Where(priceExpr)
                            .Where(roomExpr)
                            .OrderBy(x => x.SellingPrice)
                            .ToList();

                Console.WriteLine("Results: ");
                foreach (var item in q4)
                {
                    Console.WriteLine(item.ToString());
                }
                Console.ReadKey();

            }

            //Properties for rent lekérdezése
            void PropertiesForRent()
            {
                List<Property> properties = new List<Property>();

                Console.WriteLine("Search propertis for Rent. Enter the parameters:");
                Console.Write("District: ");
                int? district = null;
                try
                {
                    district = int.Parse(Console.ReadLine());
                }
                catch (FormatException ex)
                {
                    district = null;
                }

                Console.Write("Min size of Area: ");
                double? minArea = null;
                try
                {
                    minArea = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                }
                catch (FormatException ex)
                {
                    minArea = null;
                }

                Console.Write("Max size of Area: ");
                double? maxArea = null;
                try
                {
                    maxArea = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                }
                catch (FormatException ex)
                {
                    maxArea = null;
                }

                Console.Write("Lowest Price: ");
                int? minPrice = null;
                try
                {
                    minPrice = int.Parse(Console.ReadLine());
                }
                catch (FormatException ex)
                {
                    minPrice = null;
                }

                Console.Write("Highest Price: ");
                int? maxPrice = null;
                try
                {
                    maxPrice = int.Parse(Console.ReadLine());
                }
                catch (FormatException ex)
                {
                    maxPrice = null;
                }

                Console.Write("Minimum number of rooms: ");
                int? minRoomNo = null;
                try
                {
                    minRoomNo = int.Parse(Console.ReadLine());
                }
                catch (FormatException ex)
                {
                    minRoomNo = null;
                }

                Console.Write("Maximum number of rooms: ");
                int? maxRoomNo = null;
                try
                {
                    maxRoomNo = int.Parse(Console.ReadLine());
                }
                catch (FormatException ex)
                {
                    maxRoomNo = null;
                }

                Expression<Func<Property, bool>> districtExpr = (district) switch
                {
                    (not null) => x => x.District == district,
                    (null) => x => true
                };

                Expression<Func<Property, bool>> areaExpr = (minArea, maxArea) switch
                {
                    (null, not null) => x => x.Area <= maxArea,
                    (not null, not null) => x => x.Area >= minArea || x.Area <= maxArea,
                    (not null, null) => x => x.Area >= minArea,
                    (null, null) => x => true
                };

                Expression<Func<Property, bool>> priceExpr = (minPrice, maxPrice) switch
                {
                    (null, not null) => x => x.RentPrice <= maxPrice,
                    (not null, not null) => x => x.RentPrice >= minPrice || x.RentPrice <= maxPrice,
                    (not null, null) => x => x.RentPrice >= minPrice,
                    (null, null) => x => x.RentPrice != null
                };

                Expression<Func<Property, bool>> roomExpr = (minRoomNo, maxRoomNo) switch
                {
                    (null, not null) => x => x.Rooms <= maxRoomNo,
                    (not null, not null) => x => x.Rooms >= minRoomNo || x.Rooms <= maxRoomNo,
                    (not null, null) => x => x.Rooms >= minRoomNo,
                    (null, null) => x => true
                };

                var q4 = propertyService
                            .ReadAll()
                            .Where(districtExpr)
                            .Where(areaExpr)
                            .Where(priceExpr)
                            .Where(roomExpr)
                            .OrderBy(x => x.RentPrice)
                            .ToList();

                Console.WriteLine("Results: ");
                foreach (var item in q4)
                {
                    Console.WriteLine(item.ToString());
                }
                Console.ReadKey();
            }

            //Top10 properties
            void Top10()
            {
                List<Property> properties = new List<Property>();

                Console.WriteLine("\r\n██╗░░░██╗███╗░░██╗██████╗░███████╗██████╗░\r\n██║░░░██║████╗░██║██╔══██╗██╔════╝██╔══██╗\r\n██║░░░██║██╔██╗██║██║░░██║█████╗░░██████╔╝\r\n██║░░░██║██║╚████║██║░░██║██╔══╝░░██╔══██╗\r\n╚██████╔╝██║░╚███║██████╔╝███████╗██║░░██║\r\n░╚═════╝░╚═╝░░╚══╝╚═════╝░╚══════╝╚═╝░░╚═╝\r\n\r\n░█████╗░░█████╗░███╗░░██╗░██████╗████████╗██████╗░██╗░░░██╗░█████╗░████████╗██╗░█████╗░███╗░░██╗\r\n██╔══██╗██╔══██╗████╗░██║██╔════╝╚══██╔══╝██╔══██╗██║░░░██║██╔══██╗╚══██╔══╝██║██╔══██╗████╗░██║\r\n██║░░╚═╝██║░░██║██╔██╗██║╚█████╗░░░░██║░░░██████╔╝██║░░░██║██║░░╚═╝░░░██║░░░██║██║░░██║██╔██╗██║\r\n██║░░██╗██║░░██║██║╚████║░╚═══██╗░░░██║░░░██╔══██╗██║░░░██║██║░░██╗░░░██║░░░██║██║░░██║██║╚████║\r\n╚█████╔╝╚█████╔╝██║░╚███║██████╔╝░░░██║░░░██║░░██║╚██████╔╝╚█████╔╝░░░██║░░░██║╚█████╔╝██║░╚███║\r\n░╚════╝░░╚════╝░╚═╝░░╚══╝╚═════╝░░░░╚═╝░░░╚═╝░░╚═╝░╚═════╝░░╚════╝░░░░╚═╝░░░╚═╝░╚════╝░╚═╝░░╚══╝");
                /* TODO */
                Console.ReadKey();

            }

            //Avarage price of properties per district
            void AveragePricePerDistinct()
            {
                List<Property> properties = new List<Property>();

                Console.WriteLine("Search propertis for Rent. Enter the parameters:");

                var groups = propertyService
                            .ReadAll()
                            .Where(x => x.SellingPrice != null)
                            .GroupBy(x => x.District)
                            ;
                foreach (var group in groups)
                {
                    double? tempAvg = group.Average(t => t.SellingPrice / t.Area);

                    Console.WriteLine($"District: {group.Key}, Average selling price: {(tempAvg.HasValue ? double.Round(tempAvg.Value, 2) : "N/A")} ");
                }
                Console.ReadKey();

            }

            //Listing matching properties
            void ListMatchingProperties(Customer customer)
            {
                List<Property> properties = new List<Property>();

                if (customer.LookingForPurchase == true)
                {
                    var q4 = propertyService
                                .ReadAll()
                                .Where(x => customer.DistrictPreferences.Contains(x.District))
                                .Where(x => x.Area >= customer.MinArea || x.Area <= customer.MaxArea)
                                .Where(x => x.SellingPrice >= customer.MinPrice || x.SellingPrice <= customer.MaxPrice)
                                .Where(x => x.Rooms >= customer.MinRooms || x.Rooms <= customer.MaxRooms)
                                .OrderBy(x => x.SellingPrice)
                                .ToList();


                    if (q4.Count > 0)
                    {
                        Console.WriteLine("Matching properties for purchase: ");
                        foreach (var item in q4)
                        {
                            Console.WriteLine(item.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no matching property for purchase: ");
                    }
                }
                if (customer.LookingForRent == true)
                {
                    var q4 = propertyService
                                .ReadAll()
                                .Where(x => customer.DistrictPreferences.Contains(x.District))
                                .Where(x => x.Area >= customer.MinArea || x.Area <= customer.MaxArea)
                                .Where(x => x.RentPrice >= customer.MinPrice || x.RentPrice <= customer.MaxPrice)
                                .Where(x => x.Rooms >= customer.MinRooms || x.Rooms <= customer.MaxRooms)
                                .OrderBy(x => x.RentPrice)
                                .ToList();

                    if (q4.Count > 0)
                    {
                        Console.WriteLine("Matching properties for rent: ");
                        foreach (var item in q4)
                        {
                            Console.WriteLine(item.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no matching property for rent: ");
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();

            }

            //List matching customers
            void ListMatchingCustomers(Property property)
            {
                List<Customer> customers = new List<Customer>();

                var qPurchase = customerService
                .ReadAll()
                .Where(x => x.LookingForPurchase == true)
                .Where(x => x.DistrictPreferences.Contains(property.District))
                .Where(x => (x.MinArea != null && x.MinArea <= property.Area) && (x.MaxArea != null && x.MaxArea >= property.Area))
                .Where(x => (x.MinPrice != null && x.MinPrice <= property.SellingPrice) && (x.MaxPrice != null && x.MaxPrice >= property.SellingPrice))
                .Where(x => (x.MinRooms != null && x.MinRooms <= property.Rooms) && (x.MaxRooms != null && x.MaxRooms >= property.Rooms))
                .OrderByDescending(x => x.MaxPrice)
                .ToList();

                Console.WriteLine();
                if (qPurchase.Count > 0)
                {
                    Console.WriteLine("Matching customers for purchase: ");
                    foreach (var item in qPurchase)
                    {
                        Console.WriteLine(item.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("There is no matching customer for purchase: ");
                }

                var qRent = customerService
                .ReadAll()
                .Where(x => x.LookingForRent == true)
                .Where(x => x.DistrictPreferences.Contains(property.District))
                .Where(x => (x.MinArea != null && x.MinArea <= property.Area) && (x.MaxArea != null && x.MaxArea >= property.Area))
                .Where(x => (x.MinPrice != null && x.MinPrice <= property.RentPrice) && (x.MaxPrice != null && x.MaxPrice >= property.RentPrice))
                .Where(x => (x.MinRooms != null && x.MinRooms <= property.Rooms) && (x.MaxRooms != null && x.MaxRooms >= property.Rooms))
                .OrderByDescending(x => x.MaxPrice)
                .ToList();

                Console.WriteLine();
                if (qRent.Count > 0)
                {
                    Console.WriteLine("Matching customers for rent: ");
                    foreach (var item in qRent)
                    {
                        Console.WriteLine(item.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("There is no matching customer for rent: ");
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();

            }

            //Inport Db from Xml
            void DbFromXml()
            {
                //Property
                XDocument xdoc = XDocument.Load("RealEstate.xml");
                List<Property> properties = new List<Property>();
                Dictionary<int, int> propertyIds = new Dictionary<int, int>();
                Dictionary<int, int> customerIds = new Dictionary<int, int>();
                foreach (var item in xdoc.Descendants("Property"))
                {
                    Property newProperty = new Property();
                    newProperty.Address = item.Element("Address").Value;
                    newProperty.District = int.Parse(item.Element("District").Value);
                    newProperty.Rooms = int.Parse(item.Element("Rooms").Value);
                    newProperty.Area = double.Parse(item.Element("Area").Value, CultureInfo.InvariantCulture);
                    newProperty.SellingPrice = (item.Element("SellingPrice").Value == "") ? null : UInt32.Parse(item.Element("SellingPrice").Value);
                    newProperty.RentPrice = (item.Element("RentPrice").Value == "") ? null : int.Parse(item.Element("RentPrice").Value);
                    propertyService.Create(newProperty);

                    propertyIds.Add(int.Parse(item.Element("Id").Value), newProperty.Id);
                }

                //Customers
                List<Customer> customers = new List<Customer>();
                foreach (var item in xdoc.Descendants("Customer"))
                {
                    Customer newCustomer = new Customer();
                    newCustomer.MinRooms = int.Parse(item.Element("MinRooms").Value);
                    newCustomer.MaxRooms = int.Parse(item.Element("MaxRooms").Value);
                    newCustomer.MinArea = double.Parse(item.Element("MinArea").Value, CultureInfo.InvariantCulture);
                    newCustomer.MaxArea = double.Parse(item.Element("MaxArea").Value, CultureInfo.InvariantCulture);
                    newCustomer.LookingForRent = bool.Parse(item.Element("LookingForRent").Value);
                    newCustomer.LookingForPurchase = bool.Parse(item.Element("LookingForPurchase").Value);

                    newCustomer.MinPrice = (item.Element("MinPrice").Value == "") ? null : int.Parse(item.Element("MinPrice").Value);
                    newCustomer.MaxPrice = (item.Element("MaxPrice").Value == "") ? null : UInt32.Parse(item.Element("MaxPrice").Value);

                    List<int> prefDist = new List<int>();
                    var xmlDP = item.Descendants("DistrictPreferences");
                    foreach (var tag in xmlDP.Elements("int"))
                    {
                        prefDist.Add(int.Parse(tag.Value));
                    }
                    newCustomer.DistrictPreferences = prefDist;

                    var xmlOwnedP = item.Descendants("OwnedProperties");
                    foreach (var propId in xmlOwnedP.Elements("int"))
                    {
                        newCustomer.OwnedProperties.Add(propertyService.Read(propertyIds[int.Parse(propId.Value)]));
                    }

                    var xmlRentedP = item.Descendants("RentedProperties");
                    foreach (var propId in xmlRentedP.Elements("int"))
                    {
                        newCustomer.RentedProperties.Add(propertyService.Read(propertyIds[int.Parse(propId.Value)]));
                    }

                    customerService.Create(newCustomer);
                    customerIds.Add(int.Parse(item.Element("Id").Value), newCustomer.Id);
                }
                //Contracts
                List<Contract> contracts = new List<Contract>();
                foreach (var item in xdoc.Descendants("Contract"))
                {
                    Contract newContract = new Contract();
                    ;
                    newContract.PropertyId = propertyIds[int.Parse(item.Element("PropertyId").Value)];
                    newContract.SellerId = customerIds[int.Parse(item.Element("SellerId").Value)];
                    newContract.BuyerId = customerIds[int.Parse(item.Element("BuyerId").Value)];
                    newContract.Price = UInt32.Parse(item.Element("Price").Value);
                    newContract.SignDate = DateTime.Parse(item.Element("SignDate").Value);
                    newContract.ContractExpiration = (item.Element("ContractExpiration").Value == "") ? null : DateTime.Parse(item.Element("ContractExpiration").Value);

                    contractService.Create(newContract);
                }

            }
            //Exporting Db to XML
            void ExportCustomersToXML()
            {
                XDocument xml = new XDocument();
                XElement root = new XElement("Customers");
                xml.Add(root);
                foreach(var item in customerService.ReadAll().ToList())
                {
                    XElement sub = new XElement("Customer");
                    sub.Add(new XElement("Id", item.Id));
                    sub.Add(new XElement("OwnedProperties", item.OwnedProperties));
                    sub.Add(new XElement("RentedProperties", item.RentedProperties));
                    XElement sub2 = new XElement("DistrictPreferences");
                    foreach (var dp in item.DistrictPreferences.ToList())
                    {
                        sub2.Add(new XElement("int", dp));
                    }
                    sub.Add(new XElement("MinRooms", item.MinRooms));
                    sub.Add(new XElement("MaxRooms", item.MaxRooms));
                    sub.Add(new XElement("MinArea", item.MinArea));
                    sub.Add(new XElement("MaxArea", item.MaxArea));
                    sub.Add(new XElement("MinPrice", item.MinPrice));
                    sub.Add(new XElement("MaxPrice", item.MaxPrice));
                    sub.Add(new XElement("LookingForRent", item.LookingForRent));
                    sub.Add(new XElement("LookingForPurchase", item.LookingForPurchase));
                    root.Add(sub);
                }
                xml.Save("Customers.xml");
                Console.Write("Customers saved to 'Customers.xml'");
                Console.ReadKey();
            }
            void ExportPropertiesToXML()
            {
                XDocument xml = new XDocument();
                XElement root = new XElement("Properties");
                xml.Add(root);
                foreach (var item in propertyService.ReadAll().ToList())
                {
                    XElement sub = new XElement("Property");
                    sub.Add(new XElement("Id", item.Id));
                    sub.Add(new XElement("Address", item.Address));
                    sub.Add(new XElement("District", item.District));
                    sub.Add(new XElement("Rooms", item.Rooms));
                    sub.Add(new XElement("Area", item.Area));
                    sub.Add(new XElement("SellingPrice", item.SellingPrice));
                    sub.Add(new XElement("RentPrice", item.RentPrice));
                    root.Add(sub);
                }
                xml.Save("Properties.xml");
                Console.Write("Properties saved to 'Properties.xml'");
                Console.ReadKey();
            }
            void ExportContractsToXML()
            {
                XDocument xml = new XDocument();
                XElement root = new XElement("Contracts");
                xml.Add(root);
                foreach (var item in contractService.ReadAll().ToList())
                {
                    XElement sub = new XElement("Contract");
                    sub.Add(new XElement("Id", item.Id));
                    sub.Add(new XElement("PropertyId", item.PropertyId));
                    sub.Add(new XElement("SellerId", item.SellerId));
                    sub.Add(new XElement("BuyerId", item.BuyerId));
                    sub.Add(new XElement("Price", item.Price));
                    sub.Add(new XElement("SignDate", item.SignDate));
                    sub.Add(new XElement("ContractExpiration", item.ContractExpiration));
                    root.Add(sub);
                }
                xml.Save("Contracts.xml");
                Console.Write("Contracts saved to 'Contracts.xml'");
                Console.ReadKey();
            }
            void RealEstateToXml()
            {
                XDocument xml = new XDocument();
                XElement root = new XElement("RealEstate");
                xml.Add(root);

                XElement root1 = new XElement("Properties");
                root.Add(root1);
                foreach (var item in propertyService.ReadAll().ToList())
                {
                    XElement sub = new XElement("Property");
                    sub.Add(new XElement("Id", item.Id));
                    sub.Add(new XElement("Address", item.Address));
                    sub.Add(new XElement("District", item.District));
                    sub.Add(new XElement("Rooms", item.Rooms));
                    sub.Add(new XElement("Area", item.Area));
                    sub.Add(new XElement("SellingPrice", item.SellingPrice));
                    sub.Add(new XElement("RentPrice", item.RentPrice));
                    root1.Add(sub);
                }

                XElement root2 = new XElement("Customers");
                root.Add(root2);
                foreach (var item in customerService.ReadAll().ToList())
                {
                    XElement sub = new XElement("Customer");
                    sub.Add(new XElement("Id", item.Id));
                    //sub.Add(new XElement("OwnedProperties", item.OwnedProperties));
                    XElement subOP = new XElement("OwnedProperties");
                    foreach (var op in item.OwnedProperties.ToList())
                    {
                        subOP.Add(new XElement("int", op.Id));
                    }
                    sub.Add(subOP);

                    XElement subRP = new XElement("RentedProperties");
                    foreach (var rp in item.RentedProperties.ToList())
                    {
                        subRP.Add(new XElement("int", rp.Id));
                    }
                    sub.Add(subRP);

                    XElement subDP = new XElement("DistrictPreferences");
                    foreach (var dp in item.DistrictPreferences.ToList())
                    {
                        subDP.Add(new XElement("int", dp));
                    }

                    sub.Add(subDP);
                    sub.Add(new XElement("MinRooms", item.MinRooms));
                    sub.Add(new XElement("MaxRooms", item.MaxRooms));
                    sub.Add(new XElement("MinArea", item.MinArea));
                    sub.Add(new XElement("MaxArea", item.MaxArea));
                    sub.Add(new XElement("MinPrice", item.MinPrice));
                    sub.Add(new XElement("MaxPrice", item.MaxPrice));
                    sub.Add(new XElement("LookingForRent", item.LookingForRent));
                    sub.Add(new XElement("LookingForPurchase", item.LookingForPurchase));
                    root2.Add(sub);
                }

                XElement root3 = new XElement("Contracts");
                root.Add(root3);
                foreach (var item in contractService.ReadAll().ToList())
                {
                    XElement sub = new XElement("Contract");
                    sub.Add(new XElement("Id", item.Id));
                    sub.Add(new XElement("PropertyId", item.PropertyId));
                    sub.Add(new XElement("SellerId", item.SellerId));
                    sub.Add(new XElement("BuyerId", item.BuyerId));
                    sub.Add(new XElement("Price", item.Price));
                    sub.Add(new XElement("SignDate", item.SignDate));
                    sub.Add(new XElement("ContractExpiration", item.ContractExpiration));
                    root3.Add(sub);
                }
                xml.Save("RealEstate.xml");
                Console.Write("RealEstate saved to 'RealEstate.xml'");
                Console.ReadKey();
            }

            //Exporting Db to TXT
            void CustomerExportToTxt()
            {
                Console.Write("Enter the Path: ");
                string path = Console.ReadLine();
                Console.Write("Enter the name of the txt: ");
                string txt = Console.ReadLine();

                using (StreamWriter writer = new StreamWriter(Path.Combine(path, txt)))
                { 
                    foreach (var item in customerService.ReadAll().ToList())
                    {
                        writer.WriteLine($"Id: {item.Id}");
                        writer.WriteLine($"OwnedProperties: ");
                        foreach (var i in item.OwnedProperties.ToList())
                        {
                            writer.WriteLine(i.ToString());
                        }
                        writer.WriteLine($"RentedProperties: ");
                        foreach (var i in item.RentedProperties.ToList())
                        {
                            writer.WriteLine(i.ToString());
                        }
                        writer.WriteLine("DistrictPreferences");
                        foreach (var i in item.DistrictPreferences.ToList())
                        {
                            writer.WriteLine(i);
                        }
                        writer.WriteLine($"MinRooms: {item.MinRooms}");
                        writer.WriteLine($"MaxRooms: {item.MaxRooms}");
                        writer.WriteLine($"MinArea: {item.MinArea}");
                        writer.WriteLine($"MaxArea: {item.MaxArea}");
                        writer.WriteLine($"MinPrice: {item.MinPrice}");
                        writer.WriteLine($"MaxPrice: {item.MaxPrice}");
                        writer.WriteLine($"LookingForRent: {item.LookingForRent}");
                        writer.WriteLine($"LookingForPurchase: {item.LookingForPurchase}");
                        writer.WriteLine();
                    }
                }
                Console.Write("Customers exported to Txt!");
                Console.ReadKey();
            }
            void PropertyExportToTxt()
            {
                Console.Write("Enter the Path: ");
                string path = Console.ReadLine();
                Console.Write("Enter the name of the txt: ");
                string txt = Console.ReadLine();
                using (StreamWriter writer = new StreamWriter(Path.Combine(path, txt)))
                {
                    foreach (var item in propertyService.ReadAll().ToList())
                    {
                        writer.WriteLine($"Id: {item.Id}");
                        writer.WriteLine($"Address: {item.Address}");
                        writer.WriteLine($"District: {item.District}");
                        writer.WriteLine($"Rooms: {item.Rooms}");
                        writer.WriteLine($"Area: {item.Area}");
                        writer.WriteLine($"SellingPrice: {item.SellingPrice}");
                        writer.WriteLine($"RentPrice: {item.RentPrice}");
                        writer.WriteLine();
                    }
                }
                Console.Write("Properties exported to Txt!");
                Console.ReadKey();
            }
            void ContractExportToTxt()
            {
                Console.Write("Enter the Path: ");
                string path = Console.ReadLine();
                Console.Write("Enter the name of the txt: ");
                string txt = Console.ReadLine();
                using (StreamWriter writer = new StreamWriter(Path.Combine(path, txt)))
                {
                    foreach (var item in contractService.ReadAll().ToList())
                    {
                        writer.WriteLine($"Id: {item.Id}");
                        writer.WriteLine($"PropertyId: {item.PropertyId}");
                        writer.WriteLine($"SellerId: {item.SellerId}");
                        writer.WriteLine($"BuyerId: {item.BuyerId}");
                        writer.WriteLine($"Price: {item.Price}");
                        writer.WriteLine($"SignDate: {item.SignDate}");
                        writer.WriteLine($"ContractExpiration: {item.ContractExpiration}");
                        writer.WriteLine();
                    }
                }
                Console.Write("Contracts exported to Txt!");
                Console.ReadKey();
            }

            //Menürendszer
            //Export to file
            var exportXmlMenu = new ConsoleMenu(args, level: 2)
                .Add("Properties To Xml", () => ExportPropertiesToXML())
                .Add("Customers To Xml", () => ExportCustomersToXML())
                .Add("Contracts To Xml", () => ExportContractsToXML())
                .Add("RealEstate To Xml", () => RealEstateToXml())
                .Add("Back To Main Menu", ConsoleMenu.Close);

            var exportTxtMenu = new ConsoleMenu(args, level: 2)
                .Add("Properties To Txt", () => PropertyExportToTxt())
                .Add("Customers To Txt", () => CustomerExportToTxt())
                .Add("Contracts To Txt", () => ContractExportToTxt())
                .Add("Back To Main Menu", ConsoleMenu.Close);

            var fileSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Init DB from XML", ()=>DbFromXml())
                .Add("Export DB To XML", () => exportXmlMenu.Show())
                .Add("Export data To TXTs", () =>exportTxtMenu.Show())
                .Add("Back To Main Menu", ConsoleMenu.Close);

            //Properties Menu
            var propertiesSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Add new Property", () => AddNewProperty())
                .Add("Modify existing property", () => UpdateProperty())
                //.Add("Delete existing property", () => DeleteProperty())
                .Add("List all properties", () => ListAllProperties())
                .Add("Back To Main Menu", ConsoleMenu.Close);

            //Customres Menu
            var customersSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Add new Customer", () => AddNewCustomer())
                .Add("Modify existing customer", () => UpdateCustomer())
                //.Add("Delete existing customer", () => DeleteCustomer())
                .Add("List all customers", () => ListAllCustomers())
                .Add("Back To Main Menu", ConsoleMenu.Close);

            //Contracts Menu
            var contractsSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Add new Contract", () => AddNewContract())
                .Add("Modify existing contract", () => UpdateContract())
                //.Add("Delete existing contract", () => DeleteContract())
                .Add("List all contracts", () => ListAllContract())
                .Add("Back To Main Menu", ConsoleMenu.Close);

            //Queries Menu
            var quriesSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Properties for Sale", () => PropertiesForSale())
                .Add("Properties for Rent", () => PropertiesForRent())
                .Add("TOP10 Properties", () => Top10())
                .Add("Average price of Props for Sale", () => AveragePricePerDistinct())
                .Add("Back To Main Menu", ConsoleMenu.Close);

            //Main Menu
            var menu = new ConsoleMenu(args, level: 0)
                .Add("File", () => fileSubMenu.Show())
                .Add("Properties", () => propertiesSubMenu.Show())
                .Add("Customers", () => customersSubMenu.Show())
                .Add("Contracts", () => contractsSubMenu.Show())
                .Add("Queries", () => quriesSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }
    }
}
