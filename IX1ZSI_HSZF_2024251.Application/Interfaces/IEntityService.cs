using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
    public interface IEntityService<T> where T : Entity
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
        T Read(int id);
        IEnumerable<T> ReadAll();
    }
}
