using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
    public interface ICustomerService<T> : IEntityService<T> where T : Entity
    {

    }
}
