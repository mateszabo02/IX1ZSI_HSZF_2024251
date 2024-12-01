using RealEstate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RealEstate.CustomerRepository;

namespace RealEstate
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        event AddEventHandler CustomerInserted;
    }
}
