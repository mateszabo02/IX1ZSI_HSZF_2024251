using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
    public class PropertyRepository : Repository<Property>, IPropertyRepository
    {
        public PropertyRepository(RealEstateDbContext ctx) : base(ctx)
        {
        }
    }
}
