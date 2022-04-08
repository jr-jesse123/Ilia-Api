using ILIA.SimpleStore.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net.Http;

namespace ILIA.SimpleStore.IntegrationTests;

public abstract class IntegrationTestBase
{
    protected readonly HttpClient testClient;

    public IntegrationTestBase()
    {
        var appFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(webHostBuilder =>
        {
            webHostBuilder.ConfigureServices(services =>
            {
                services.AddDbContext<SimpleStoreContext>(opt => opt.UseInMemoryDatabase("testDb"));
            });
        });
        testClient = appFactory.CreateClient();

    }   
} 
