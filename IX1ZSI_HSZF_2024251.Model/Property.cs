using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RealEstate
{
    public partial class Property
    {

        /// <remarks/>
        public int Id
        {
            get;
            set;
        }

        /// <remarks/>
        [Required]
        public string Address
        {
            get;
            set;
        }

        /// <remarks/>
        [Required]
        public int District
        {
            get;
            set;
        }

        /// <remarks/>
        [Required]
        public int Rooms
        {
            get;
            set;
        }

        /// <remarks/>
        [Required]
        public double Area
        {
            get;
            set;
        }

        /// <remarks/>
        public int SellingPrice
        {
            get;
            set;
        }

        /// <remarks/>
        public int RentPrice
        {
            get;
            set;
        }
        public void Load()
        {
            XDocument xdoc3 = XDocument.Load("Properties.xml");
            List<Property> properties = new List<Property>();
            foreach (var item in xdoc3.Descendants("Property"))
            {
                if (item.Element("SellingPrice").Value == "" && item.Element("RentPrice").Value == "")
                {
                    properties.Add(new Property()
                    {
                        Id = int.Parse(item.Element("Id").Value),
                        Address = item.Element("Address").Value,
                        District = int.Parse(item.Element("District").Value),
                        Rooms = int.Parse(item.Element("Rooms").Value),
                        Area = double.Parse(item.Element("Area").Value)
                    });
                }
                else if (item.Element("SellingPrice").Value == "" && item.Element("RentPrice").Value != "")
                {
                    properties.Add(new Property()
                    {
                        Id = int.Parse(item.Element("Id").Value),
                        Address = item.Element("Address").Value,
                        District = int.Parse(item.Element("District").Value),
                        Rooms = int.Parse(item.Element("Rooms").Value),
                        Area = double.Parse(item.Element("Area").Value),
                        RentPrice = int.Parse(item.Element("RentPrice").Value)
                    });
                }
                else if (item.Element("SellingPrice").Value != "" && item.Element("RentPrice").Value == "")
                {
                    properties.Add(new Property()
                    {
                        Id = int.Parse(item.Element("Id").Value),
                        Address = item.Element("Address").Value,
                        District = int.Parse(item.Element("District").Value),
                        Rooms = int.Parse(item.Element("Rooms").Value),
                        Area = double.Parse(item.Element("Area").Value),
                        SellingPrice = int.Parse(item.Element("SellingPrice").Value),
                    });
                }
                else
                {
                    properties.Add(new Property()
                    {
                        Id = int.Parse(item.Element("Id").Value),
                        Address = item.Element("Address").Value,
                        District = int.Parse(item.Element("District").Value),
                        Rooms = int.Parse(item.Element("Rooms").Value),
                        Area = double.Parse(item.Element("Area").Value),
                        SellingPrice = int.Parse(item.Element("SellingPrice").Value),
                        RentPrice = int.Parse(item.Element("RentPrice").Value)
                    });
                }
            }
        }
    }
}