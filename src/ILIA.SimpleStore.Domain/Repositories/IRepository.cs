using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILIA.SimpleStore.Domain
{
    public interface IRepository<T> where T : EntityBase 
    {
        public Task<T> GetById(Guid id);
        public Task<T> Add(T entity);
        public void Update(T entity);
        public Task<IEnumerable<T>> GetAll();
        public Task<bool> DeleteById(Guid id);

        public Task<int> Commit();
    }
}
