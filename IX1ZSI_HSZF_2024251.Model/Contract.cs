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
        public UInt32 Price {  get; set; }
        [Required]
        public DateTime SignDate {  get; set; }
        public DateTime? ContractExpiration { get; set; }
        public override string ToString()
        {
            return $"({Id}) PropertyId: {PropertyId}; SellerId: {SellerId}; BuyerId: {BuyerId}; Price: {Price}; SignDate: {SignDate}; ContractExpiration: {ContractExpiration}";
        }
    }
}
