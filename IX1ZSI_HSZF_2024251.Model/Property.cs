using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RealEstate
{
    public partial class Property
    {

        private int idField;

        private string addressField;

        private int districtField;

        private int roomsField;

        private decimal areaField;

        private int sellingPriceField;

        private int rentPriceField;

        /// <remarks/>
        public int Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public string Address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }

        /// <remarks/>
        public int District
        {
            get
            {
                return this.districtField;
            }
            set
            {
                this.districtField = value;
            }
        }

        /// <remarks/>
        public int Rooms
        {
            get
            {
                return this.roomsField;
            }
            set
            {
                this.roomsField = value;
            }
        }

        /// <remarks/>
        public decimal Area
        {
            get
            {
                return this.areaField;
            }
            set
            {
                this.areaField = value;
            }
        }

        /// <remarks/>
        public int SellingPrice
        {
            get
            {
                return this.sellingPriceField;
            }
            set
            {
                this.sellingPriceField = value;
            }
        }

        /// <remarks/>
        public int RentPrice
        {
            get
            {
                return this.rentPriceField;
            }
            set
            {
                this.rentPriceField = value;
            }
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
                        Area = decimal.Parse(item.Element("Area").Value)
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
                        Area = decimal.Parse(item.Element("Area").Value),
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
                        Area = decimal.Parse(item.Element("Area").Value),
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
                        Area = decimal.Parse(item.Element("Area").Value),
                        SellingPrice = int.Parse(item.Element("SellingPrice").Value),
                        RentPrice = int.Parse(item.Element("RentPrice").Value)
                    });
                }
            }
        }
    }
}