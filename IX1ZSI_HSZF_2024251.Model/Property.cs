using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RealEstate
{
    [Table("property")]
    public class Property : Entity
    {
        public Property() { }
        //public Property(int Id, string Address, int District, int Rooms, double Area, UInt32 SellingPrice, int RentPrice)
        //{
        //    this.Id = Id;
        //    this.Address = Address;
        //    this.District = District;
        //    this.Rooms = Rooms;
        //    this.Area = Area;
        //    this.SellingPrice = SellingPrice;
        //    this.RentPrice = RentPrice;
        //}

        [Required]
        public string Address
        {
            get;
            set;
        }

        [Required]
        public int District
        {
            get;
            set;
        }

        [Required]
        public int Rooms
        {
            get;
            set;
        }

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public double Area
        {
            get;
            set;
        }

        [AllowNull]
        public UInt32? SellingPrice
        {
            get;
            set;
        }

        [AllowNull]
        public int? RentPrice
        {
            get;
            set;
        }
        public override string ToString()
        {
            return $"{Id} District: {District}. Address: {Address}, Area: {Area}, Rooms: {Rooms}, SellingPrice: {SellingPrice}, RentPrice: {RentPrice}";
        }

    }
}