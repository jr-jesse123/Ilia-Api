using ILIA.SimpleStore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILIA.SimpleStore.Persistence.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(SimpleStoreContext context) : base(context)
        {
        }

        public async Task<Customer> GetCustomersAndRelatedOrdersById(Guid id)
        {
            return await Entities.Include(c => c.Orders).FirstOrDefaultAsync(c => c.Id == id);
        }

    }
}
