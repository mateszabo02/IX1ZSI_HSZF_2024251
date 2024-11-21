using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
    /*public class PropertyDB
    {
        public PropertyDB(string Address, int District, int Rooms, double Area)
        {
            this.Address = Address;
            this.District = District;
            this.Rooms = Rooms;
            this.Area = Area;
        }
        public int ID { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int District { get; set; }
        [Required]
        public int Rooms { get; set; }
        [Required]
        public double Area { get; set; }
        public int SellingPrice { get; set; }
        public int RentPrice { get; set; }
    }*/
    public class PropertyDbContext : DbContext
    {
        public DbSet<Property> Properties { get; set; }
        public PropertyDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=propertydb;Integrated Security=True;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connStr);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
