using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RealEstate
{
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
        public void Load()
        {
            XDocument xdoc1 = XDocument.Load("Contracts.xml");
            List<Contract> contracts = new List<Contract>();
            foreach (var item in xdoc1.Descendants("Contract"))
            {
                //Console.WriteLine($"-{item.Element("ContractExpiration").Value}-");
                if (item.Element("ContractExpiration").Value != "")
                {
                    contracts.Add(new Contract()
                    {
                        Id = int.Parse(item.Element("Id").Value),
                        PropertyId = int.Parse(item.Element("PropertyId").Value),
                        SellerId = int.Parse(item.Element("SellerId").Value),
                        BuyerId = int.Parse(item.Element("BuyerId").Value),
                        Price = int.Parse(item.Element("Price").Value),
                        SignDate = DateTime.Parse(item.Element("SignDate").Value),
                        ContractExpiration = DateTime.Parse(item.Element("ContractExpiration").Value)
                    });
                }
                else
                {
                    contracts.Add(new Contract()
                    {
                        Id = int.Parse(item.Element("Id").Value),
                        PropertyId = int.Parse(item.Element("PropertyId").Value),
                        SellerId = int.Parse(item.Element("SellerId").Value),
                        BuyerId = int.Parse(item.Element("BuyerId").Value),
                        Price = int.Parse(item.Element("Price").Value),
                        SignDate = DateTime.Parse(item.Element("SignDate").Value)

                    });
                }
            }
        }
    }
}
