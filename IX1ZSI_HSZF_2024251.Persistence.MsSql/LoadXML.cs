

namespace RealEstate
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Contract
    {

        private int idField;

        private int propertyIdField;

        private int sellerIdField;

        private int buyerIdField;

        private int priceField;

        private DateTime signDateField;

        private DateTime contractExpirationField;

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
        public int PropertyId
        {
            get
            {
                return this.propertyIdField;
            }
            set
            {
                this.propertyIdField = value;
            }
        }

        /// <remarks/>
        public int SellerId
        {
            get
            {
                return this.sellerIdField;
            }
            set
            {
                this.sellerIdField = value;
            }
        }

        /// <remarks/>
        public int BuyerId
        {
            get
            {
                return this.buyerIdField;
            }
            set
            {
                this.buyerIdField = value;
            }
        }

        /// <remarks/>
        public int Price
        {
            get
            {
                return this.priceField;
            }
            set
            {
                this.priceField = value;
            }
        }

        /// <remarks/>
        public DateTime SignDate
        {
            get
            {
                return this.signDateField;
            }
            set
            {
                this.signDateField = value;
            }
        }

        /// <remarks/>
        public DateTime ContractExpiration
        {
            get
            {
                return this.contractExpirationField;
            }
            set
            {
                this.contractExpirationField = value;
            }
        }
    }


    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Customer
    {

        private int idField;

        private int[] ownedPropertiesField;

        private int rentedPropertiesField;

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
        public int RentedProperties
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
    }


    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
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
    }


}
