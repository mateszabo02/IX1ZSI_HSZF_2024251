using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
    public class CustomerService : EntityService<Customer>, ICustomerService<Customer>
    {
        ICustomerRepository customerRepo;
        public CustomerService(ICustomerRepository customerRepo) : base(customerRepo) 
        {
            this.customerRepo = customerRepo;
        }
    }
}
