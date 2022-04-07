using ILIA.SimpleStore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILIA.SimpleStore.Persistence;

public class SimpleStoreContext : DbContext
{
    public SimpleStoreContext(DbContextOptions<SimpleStoreContext> options) : base(options)
    {
    }

    public DbSet<Customer> Products { get; set; }
    public DbSet<Order> Orders { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SimpleStoreContext).Assembly);
    }
    
 
}
