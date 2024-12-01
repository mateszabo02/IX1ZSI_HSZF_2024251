using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RealEstate
{
    [Table("customer")]
    public partial class Customer  : Entity 
    {
        public Customer()
        {
            OwnedProperties = new HashSet<Property>();
            RentedProperties = new HashSet<Property> ();
        }
        public List<int> DistrictPreferences { get; set; }


        [Required]
        public int MinRooms {  get; set; }

        [Required]
        public int MaxRooms {  get; set; }

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public double MinArea {  get; set; }

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public double MaxArea {  get; set; }

        public int? MinPrice { get; set; }

        public UInt32? MaxPrice { get; set; }

        public bool LookingForRent {  get; set; }

        public bool LookingForPurchase {  get; set; }

        [ForeignKey("OwnerId")]
        public virtual ICollection<Property> OwnedProperties { get; set; }

        [ForeignKey("TenantId")]
        public virtual ICollection<Property> RentedProperties { get; set; }
        public override string ToString()
        {
            string output = "";
            output += $"({Id})";
            output += $"Rooms: {MinRooms}-{MaxRooms};";
            output += $"Area: {MinArea}-{MaxArea};";
            output += "Preferred districts: ";
            foreach(var item in DistrictPreferences)
            {
                output += item.ToString() + ", ";
            }
            output += $"Price: {MinPrice}-{MaxPrice};";
            output += $"Looking for rent: {LookingForRent};";
            output += $"Looking for purchase: {LookingForPurchase};";
            return output;
        }

        ////public void Load()
        ////{
        ////    XDocument xdoc2 = XDocument.Load("Customers.xml");
        ////    List<Customer> customers = new List<Customer>();
        ////    foreach (var item in xdoc2.Descendants("Customer"))
        ////    {
        ////        if (item.Element("MinPrice").Value == "" && item.Element("MaxPrice").Value == "")
        ////        {
        ////            customers.Add(new Customer()
        ////            {
        ////                Id = int.Parse(item.Element("Id").Value),
        ////                //OwnedProperties = int.Parse(item.Elements("OwnedProperties")),
        ////                //RentedProperties = int.Parse(item.Elements("RentedProperties")),
        ////                //DistrictPreferences = int.Parse(item.Elements("DistrictPreferences")),
        ////                MinRooms = int.Parse(item.Element("MinRooms").Value),
        ////                MaxRooms = int.Parse(item.Element("MaxRooms").Value),
        ////                MinArea = decimal.Parse(item.Element("MinArea").Value),
        ////                MaxArea = decimal.Parse(item.Element("MaxArea").Value),
        ////                LookingForRent = bool.Parse(item.Element("LookingForRent").Value),
        ////                LookingForPurchase = bool.Parse(item.Element("LookingForPurchase").Value)
        ////            });
        ////        }
        ////        else if (item.Element("MinPrice").Value != "" && item.Element("MaxPrice").Value == "")
        ////        {
        ////            customers.Add(new Customer()
        ////            {

        ////                Id = int.Parse(item.Element("Id").Value),
        ////                //OwnedProperties = int.Parse(item.Elements("OwnedProperties")),
        ////                //RentedProperties = int.Parse(item.Elements("RentedProperties")),
        ////                //DistrictPreferences = int.Parse(item.Elements("DistrictPreferences")),
        ////                MinRooms = int.Parse(item.Element("MinRooms").Value),
        ////                MaxRooms = int.Parse(item.Element("MaxRooms").Value),
        ////                MinArea = decimal.Parse(item.Element("MinArea").Value),
        ////                MaxArea = decimal.Parse(item.Element("MaxArea").Value),
        ////                MinPrice = int.Parse(item.Element("MinPrice").Value),
        ////                LookingForRent = bool.Parse(item.Element("LookingForRent").Value),
        ////                LookingForPurchase = bool.Parse(item.Element("LookingForPurchase").Value)
        ////            });
        ////        }
        ////        else if (item.Element("MinPrice").Value == "" && item.Element("MaxPrice").Value != "")
        ////        {
        ////            customers.Add(new Customer()
        ////            {

        ////                Id = int.Parse(item.Element("Id").Value),
        ////                //OwnedProperties = int.Parse(item.Elements("OwnedProperties")),
        ////                //RentedProperties = int.Parse(item.Elements("RentedProperties")),
        ////                //DistrictPreferences = int.Parse(item.Elements("DistrictPreferences")),
        ////                MinRooms = int.Parse(item.Element("MinRooms").Value),
        ////                MaxRooms = int.Parse(item.Element("MaxRooms").Value),
        ////                MinArea = decimal.Parse(item.Element("MinArea").Value),
        ////                MaxArea = decimal.Parse(item.Element("MaxArea").Value),
        ////                MaxPrice = int.Parse(item.Element("MaxPrice").Value),
        ////                LookingForRent = bool.Parse(item.Element("LookingForRent").Value),
        ////                LookingForPurchase = bool.Parse(item.Element("LookingForPurchase").Value)
        ////            });
        ////        }
        ////        else
        ////        {
        ////            customers.Add(new Customer()
        ////            {

        ////                Id = int.Parse(item.Element("Id").Value),
        ////                //OwnedProperties = int.Parse(item.Elements("OwnedProperties")),
        ////                //RentedProperties = int.Parse(item.Elements("RentedProperties")),
        ////                //DistrictPreferences = int.Parse(item.Elements("DistrictPreferences")),
        ////                MinRooms = int.Parse(item.Element("MinRooms").Value),
        ////                MaxRooms = int.Parse(item.Element("MaxRooms").Value),
        ////                MinArea = decimal.Parse(item.Element("MinArea").Value),
        ////                MaxArea = decimal.Parse(item.Element("MaxArea").Value),
        ////                MinPrice = int.Parse(item.Element("MinPrice").Value),
        ////                MaxPrice = int.Parse(item.Element("MaxPrice").Value),
        ////                LookingForRent = bool.Parse(item.Element("LookingForRent").Value),
        ////                LookingForPurchase = bool.Parse(item.Element("LookingForPurchase").Value)
        ////            });
        ////        }
        ////    }
        //}
    }
}
