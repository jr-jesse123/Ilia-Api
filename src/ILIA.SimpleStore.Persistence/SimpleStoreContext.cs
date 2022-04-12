using ILIA.SimpleStore.API.Services;
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
    private readonly IMailService mailService;

    public SimpleStoreContext(
        DbContextOptions<SimpleStoreContext> options, 
        IMailService mailService) : base(options)
    {
        this.mailService = mailService;
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SimpleStoreContext).Assembly);
    }

    

    public override int SaveChanges()
    {
        
        var addedCustomers = GetAddedCustomers();

        var output = base.SaveChanges();

        foreach (var customer in addedCustomers)
        {
            mailService.SendMail(customer.Email, "Welcome to ILIA.SimpleStore", "Welcome to ILIA.SimpleStore");
        }


        return output;
    }


    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        var addedCustomers = GetAddedCustomers();

        var output = await base.SaveChangesAsync(cancellationToken);

        foreach (var customer in addedCustomers)
        {
            mailService.SendMail(customer.Email, "Welcome to ILIA.SimpleStore", "Welcome to ILIA.SimpleStore");
        }

        return output;
    }

    private Customer[] GetAddedCustomers()
    {
        this.ChangeTracker.DetectChanges();
        var addedCustomers = this.ChangeTracker.Entries()
                    .Where(t => t.State == EntityState.Added)
                    .Select(t => t.Entity)
                    .Where(entity => entity.GetType() == typeof(Customer))
                    .Cast<Customer>()
                    .ToArray();

        return addedCustomers;


    }
}
