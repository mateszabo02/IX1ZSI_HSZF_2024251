using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RealEstate
{
    public partial class Customer
    {

        private int idField;

        private int[] ownedPropertiesField;

        private int[] rentedPropertiesField;

        private int[] districtPreferencesField;

        private int minRoomsField;

        private int maxRoomsField;

        private decimal minAreaField;

        private decimal maxAreaField;

        private int minPriceField;

        private int maxPriceField;

        private bool lookingForRentField;

        private bool lookingForPurchaseField;

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
        [System.Xml.Serialization.XmlArrayAttribute()]
        [System.Xml.Serialization.XmlArrayItemAttribute("int", IsNullable = false)]
        public int[] OwnedProperties
        {
            get
            {
                return this.ownedPropertiesField;
            }
            set
            {
                this.ownedPropertiesField = value;
            }
        }

        /// <remarks/>
        public int[] RentedProperties
        {
            get
            {
                return this.rentedPropertiesField;
            }
            set
            {
                this.rentedPropertiesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute()]
        [System.Xml.Serialization.XmlArrayItemAttribute("int", IsNullable = false)]
        public int[] DistrictPreferences
        {
            get
            {
                return this.districtPreferencesField;
            }
            set
            {
                this.districtPreferencesField = value;
            }
        }

        /// <remarks/>
        public int MinRooms
        {
            get
            {
                return this.minRoomsField;
            }
            set
            {
                this.minRoomsField = value;
            }
        }

        /// <remarks/>
        public int MaxRooms
        {
            get
            {
                return this.maxRoomsField;
            }
            set
            {
                this.maxRoomsField = value;
            }
        }

        /// <remarks/>
        public decimal MinArea
        {
            get
            {
                return this.minAreaField;
            }
            set
            {
                this.minAreaField = value;
            }
        }

        /// <remarks/>
        public decimal MaxArea
        {
            get
            {
                return this.maxAreaField;
            }
            set
            {
                this.maxAreaField = value;
            }
        }

        /// <remarks/>
        public int MinPrice
        {
            get
            {
                return this.minPriceField;
            }
            set
            {
                this.minPriceField = value;
            }
        }

        /// <remarks/>
        public int MaxPrice
        {
            get
            {
                return this.maxPriceField;
            }
            set
            {
                this.maxPriceField = value;
            }
        }

        /// <remarks/>
        public bool LookingForRent
        {
            get
            {
                return this.lookingForRentField;
            }
            set
            {
                this.lookingForRentField = value;
            }
        }

        /// <remarks/>
        public bool LookingForPurchase
        {
            get
            {
                return this.lookingForPurchaseField;
            }
            set
            {
                this.lookingForPurchaseField = value;
            }
        }
        public void Load()
        {
            XDocument xdoc2 = XDocument.Load("Customers.xml");
            List<Customer> customers = new List<Customer>();
            foreach (var item in xdoc2.Descendants("Customer"))
            {
                if (item.Element("MinPrice").Value == "" && item.Element("MaxPrice").Value == "")
                {
                    customers.Add(new Customer()
                    {
                        Id = int.Parse(item.Element("Id").Value),
                        //OwnedProperties = int.Parse(item.Elements("OwnedProperties")),
                        //RentedProperties = int.Parse(item.Elements("RentedProperties")),
                        //DistrictPreferences = int.Parse(item.Elements("DistrictPreferences")),
                        MinRooms = int.Parse(item.Element("MinRooms").Value),
                        MaxRooms = int.Parse(item.Element("MaxRooms").Value),
                        MinArea = decimal.Parse(item.Element("MinArea").Value),
                        MaxArea = decimal.Parse(item.Element("MaxArea").Value),
                        LookingForRent = bool.Parse(item.Element("LookingForRent").Value),
                        LookingForPurchase = bool.Parse(item.Element("LookingForPurchase").Value)
                    });
                }
                else if (item.Element("MinPrice").Value != "" && item.Element("MaxPrice").Value == "")
                {
                    customers.Add(new Customer()
                    {

                        Id = int.Parse(item.Element("Id").Value),
                        //OwnedProperties = int.Parse(item.Elements("OwnedProperties")),
                        //RentedProperties = int.Parse(item.Elements("RentedProperties")),
                        //DistrictPreferences = int.Parse(item.Elements("DistrictPreferences")),
                        MinRooms = int.Parse(item.Element("MinRooms").Value),
                        MaxRooms = int.Parse(item.Element("MaxRooms").Value),
                        MinArea = decimal.Parse(item.Element("MinArea").Value),
                        MaxArea = decimal.Parse(item.Element("MaxArea").Value),
                        MinPrice = int.Parse(item.Element("MinPrice").Value),
                        LookingForRent = bool.Parse(item.Element("LookingForRent").Value),
                        LookingForPurchase = bool.Parse(item.Element("LookingForPurchase").Value)
                    });
                }
                else if (item.Element("MinPrice").Value == "" && item.Element("MaxPrice").Value != "")
                {
                    customers.Add(new Customer()
                    {

                        Id = int.Parse(item.Element("Id").Value),
                        //OwnedProperties = int.Parse(item.Elements("OwnedProperties")),
                        //RentedProperties = int.Parse(item.Elements("RentedProperties")),
                        //DistrictPreferences = int.Parse(item.Elements("DistrictPreferences")),
                        MinRooms = int.Parse(item.Element("MinRooms").Value),
                        MaxRooms = int.Parse(item.Element("MaxRooms").Value),
                        MinArea = decimal.Parse(item.Element("MinArea").Value),
                        MaxArea = decimal.Parse(item.Element("MaxArea").Value),
                        MaxPrice = int.Parse(item.Element("MaxPrice").Value),
                        LookingForRent = bool.Parse(item.Element("LookingForRent").Value),
                        LookingForPurchase = bool.Parse(item.Element("LookingForPurchase").Value)
                    });
                }
                else
                {
                    customers.Add(new Customer()
                    {

                        Id = int.Parse(item.Element("Id").Value),
                        //OwnedProperties = int.Parse(item.Elements("OwnedProperties")),
                        //RentedProperties = int.Parse(item.Elements("RentedProperties")),
                        //DistrictPreferences = int.Parse(item.Elements("DistrictPreferences")),
                        MinRooms = int.Parse(item.Element("MinRooms").Value),
                        MaxRooms = int.Parse(item.Element("MaxRooms").Value),
                        MinArea = decimal.Parse(item.Element("MinArea").Value),
                        MaxArea = decimal.Parse(item.Element("MaxArea").Value),
                        MinPrice = int.Parse(item.Element("MinPrice").Value),
                        MaxPrice = int.Parse(item.Element("MaxPrice").Value),
                        LookingForRent = bool.Parse(item.Element("LookingForRent").Value),
                        LookingForPurchase = bool.Parse(item.Element("LookingForPurchase").Value)
                    });
                }
            }
        }
    }
}
