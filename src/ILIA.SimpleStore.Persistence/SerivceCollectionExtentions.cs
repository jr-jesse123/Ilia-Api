using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ILIA.SimpleStore.Domain;
using ILIA.SimpleStore.Persistence.Repositories;

namespace ILIA.SimpleStore.Persistence
{
    public static class SerivceCollectionExtentions 
    {
        public static IServiceCollection AddDefaultPersisntece(this IServiceCollection services)
        {
            //TOOD: ADD LOAD TEST
            //TODO: CHANGE TO REAL DB
            services.AddDbContext<SimpleStoreContext>(opt => opt.UseInMemoryDatabase("temporaryDb"));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            return services;

        }
    }
}
