using ILIA.SimpleStore.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;

namespace ILIA.SimpleStore.IntegrationTests;

public abstract class IntegrationTestBase : IDisposable
{
    protected readonly HttpClient testClient;
    //private static readonly Random rnd = new Random();

    private  SimpleStoreContext _context;

    public IntegrationTestBase() 
    {

        var options = new DbContextOptionsBuilder<SimpleStoreContext>();
        options.UseInMemoryDatabase("teste");
        var context = new SimpleStoreContext(options.Options);

        var appFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(webHostBuilder =>
        {
            webHostBuilder.ConfigureServices(services =>
            {
                var serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(SimpleStoreContext));
                services.Remove(serviceDescriptor);

                

                services.AddDbContext<SimpleStoreContext>(opt =>
                    opt.UseInMemoryDatabase(Guid.NewGuid().ToString()));

                _context = services.BuildServiceProvider().GetRequiredService<SimpleStoreContext>();
                //services.AddTransient<SimpleStoreContext>()
            });
        });
        testClient = appFactory.CreateClient();

    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
} 
