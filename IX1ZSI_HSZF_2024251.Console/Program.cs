using ConsoleTools;
using System.Xml.Linq;
namespace RealEstate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            var fileMenu = new ConsoleMenu(args, level:1);
            //fileMenu.Add("Load from XML file", );
            //fileMenu.Add("Manual load", );
        

            var customerMenu = new ConsoleMenu(args, level: 1);
        

            var propertyMenu = new ConsoleMenu(args, level: 1);
            //    propertyMenu.Add("Refresh properties", () => refreshProp.Show());
        

            var contractMenu = new ConsoleMenu(args, level: 1);
        

            var districtMenu = new ConsoleMenu(args, level: 1);
            //    districtMenu.Add("1. District", );
        

            var queryMenu = new ConsoleMenu(args, level: 1);
            //    queryMenu.Add("Properties for sale", );
            //    queryMenu.Add("Properties for rent", );
            //    queryMenu.Add("Top 10 properties", );
            //    queryMenu.Add("Avarage price", );
        

            var Menu = new ConsoleMenu(args, level: 0);
            Menu.Add("File", ()=>fileMenu.Show());
            Menu.Add("Customers", () => customerMenu.Show());
            Menu.Add("Properties", () => propertyMenu.Show());
            Menu.Add("Contrancts", () => contractMenu.Show());
            Menu.Add("Districts", () => districtMenu.Show());
            Menu.Add("Queries", () => queryMenu.Show());
            Menu.Add("Exit", ConsoleMenu.Close);
            Menu.Show();
            */
            XDocument xdoc1 = XDocument.Load("Contracts.xml");
            List<Contract> contracts = new List<Contract>();
            foreach(var item in xdoc1.Descendants("Contract"))
            {
                contracts.Add(new Contract()
                {
                    Id = int.Parse(item.Element("Id").Value),
                    PropertyId = int.Parse(item.Element("PropertyId").Value),
                    SellerId = int.Parse(item.Element("SellerId").Value),
                    BuyerId = int.Parse(item.Element("BuyerId").Value),
                    Price = int.Parse(item.Element("Price").Value),
                    SignDate = DateTime.Parse(item.Element("SignDate").Value),
                    ContractExpiration = DateTime.Parse(item.Element("ContractExpiration").Value),
                });
            }
            /*for (int i = 0; i < contracts.Count; i++)
            {
                Console.WriteLine($"{contracts[i].Id} - {contracts[i].PropertyId} - {contracts[i].SellerId} - {contracts[i].BuyerId} - {contracts[i].Price} - {contracts[i].SignDate} - {contracts[i].ContractExpiration}");
            }*/
            XDocument xdoc2 = XDocument.Load("Contracts.xml");
            List<Customer> customers = new List<Customer>();
            XDocument xdoc3 = XDocument.Load("Properties.xml");
            List<Property> pr = new List<Property>();

        }
    }
}
