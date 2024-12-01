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
    [Table("contract")]
    public partial class Contract : Entity
    {
        public Contract() { }
        public int PropertyId {  get; set; }
        public int SellerId {  get; set; }
        public int BuyerId {  get; set; }
        [Required]
        public int Price {  get; set; }
        [Required]
        public DateTime SignDate {  get; set; }
        public DateTime? ContractExpiration { get; set; }
        public override string ToString()
        {
            return $"({Id}) PropertyId: {PropertyId}; SellerId: {SellerId}; BuyerId: {BuyerId}; Price: {Price}; SignDate: {SignDate}, ContractExpiration: {ContractExpiration}";
        }
        //public void Load()
        //{
        //    XDocument xdoc1 = XDocument.Load("Contracts.xml");
        //    List<Contract> contracts = new List<Contract>();
        //    foreach (var item in xdoc1.Descendants("Contract"))
        //    {
        //        //Console.WriteLine($"-{item.Element("ContractExpiration").Value}-");
        //        if (item.Element("ContractExpiration").Value != "")
        //        {
        //            contracts.Add(new Contract()
        //            {
        //                Id = int.Parse(item.Element("Id").Value),
        //                PropertyId = int.Parse(item.Element("PropertyId").Value),
        //                SellerId = int.Parse(item.Element("SellerId").Value),
        //                BuyerId = int.Parse(item.Element("BuyerId").Value),
        //                Price = int.Parse(item.Element("Price").Value),
        //                SignDate = DateTime.Parse(item.Element("SignDate").Value),
        //                ContractExpiration = DateTime.Parse(item.Element("ContractExpiration").Value)
        //            });
        //        }
        //        else
        //        {
        //            contracts.Add(new Contract()
        //            {
        //                Id = int.Parse(item.Element("Id").Value),
        //                PropertyId = int.Parse(item.Element("PropertyId").Value),
        //                SellerId = int.Parse(item.Element("SellerId").Value),
        //                BuyerId = int.Parse(item.Element("BuyerId").Value),
        //                Price = int.Parse(item.Element("Price").Value),
        //                SignDate = DateTime.Parse(item.Element("SignDate").Value)

        //            });
        //        }
        //    }
        //}
    }
}
