using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILIA.SimpleStore.Domain
{
    public interface IRepository<T>
    {
        public T GetById(Guid id);
        public T Add(T entity);
        public T Update(T entity);
        public IEnumerable<T> GetAll();
        public void DeleteById(Guid id);
    }
}
