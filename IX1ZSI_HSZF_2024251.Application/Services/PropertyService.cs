using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
    public class PropertyService : EntityService<Property>, IPropertyService<Property>
    {
        IPropertyRepository propRepo;
        public PropertyService(IPropertyRepository propRepo) : base(propRepo)
        {
            this.propRepo = propRepo;
        }
    }
}
