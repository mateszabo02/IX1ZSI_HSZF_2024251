using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
    public class ContractService : EntityService<Contract>, IContractService<Contract>
    {
        IContractRepository contractRepo;
        public ContractService(IContractRepository contractRepo) : base(contractRepo)
        {
            this.contractRepo = contractRepo;
        }
    }
}
