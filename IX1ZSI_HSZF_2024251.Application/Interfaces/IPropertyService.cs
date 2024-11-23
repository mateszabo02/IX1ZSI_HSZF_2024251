using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
    public interface IPropertyService<T> : IEntityService<T> where T : Entity
    {
    }
}
