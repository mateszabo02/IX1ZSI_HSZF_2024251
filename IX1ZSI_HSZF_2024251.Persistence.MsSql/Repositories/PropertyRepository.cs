using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
    public class PropertyRepository : Repository<Property>, IPropertyRepository
    {
        public delegate void AddEventHandler(Property item);
        public event AddEventHandler PropertyInserted;
        public PropertyRepository(RealEstateDbContext context) : base(context) { }
        public override void Create(Property item)
        {
            base.Create(item);
            PropertyInserted?.Invoke(item);
        }
    }
}
