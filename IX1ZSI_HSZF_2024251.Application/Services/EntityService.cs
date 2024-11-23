using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
    public class EntityService<T> : IEntityService<T> where T : Entity
    {
        IRepository<T> repo;
        public EntityService(IRepository<T> repo)
        {
            this.repo = repo;
        }
        public void Create(T entity)
        {
            repo.Create(entity);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public T Read(int id)
        {
            return repo.Read(id) ?? throw new ArgumentNullException("Entity with the specified ID does not exist");
        }

        public IEnumerable<T> ReadAll()
        {
            return repo.ReadAll();
        }

        public void Update(T entity)
        {
            repo.Update(entity);
        }
    }
}
