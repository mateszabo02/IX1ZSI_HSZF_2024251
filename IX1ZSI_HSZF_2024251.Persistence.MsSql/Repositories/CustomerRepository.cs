using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public delegate void AddEventHandler(Customer item);
        public event AddEventHandler CustomerInserted;
        public CustomerRepository(RealEstateDbContext context) : base(context) { }

        public override void Create(Customer item)
        {
            base.Create(item);
            CustomerInserted?.Invoke(item);
        }
    }
}
