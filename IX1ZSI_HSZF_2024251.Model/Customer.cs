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
}
}
