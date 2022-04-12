using ILIA.SimpleStore.API.Controllers;
using ILIA.SimpleStore.API.Models;
using ILIA.SimpleStore.API.Services;
using ILIA.SimpleStore.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ILIA.SimpleStore.IntegrationTests;

public abstract class IntegrationTestBase : IDisposable
{
    protected readonly HttpClient testClient;
    //private static readonly Random rnd = new Random();

    private  SimpleStoreContext _context;

    protected CustomerModel validCustumer = new()
    {
        Email = "teste@teste.com",
        Name = "happy costumer"
    };


    public IntegrationTestBase() 
    {

        var options = new DbContextOptionsBuilder<SimpleStoreContext>();
        options.UseInMemoryDatabase("teste");
        var mockMailService = new Mock<IMailService>();

        var context = new SimpleStoreContext(options.Options, mockMailService.Object);

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



    protected async Task<IEnumerable<CustomerModel>> GetCustumers()
    {
        var response = await testClient.GetAsync(CustomerController._GetAll);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsAsync<List<CustomerModel>>();

    }

    protected async Task<CustomerModel> CreateCustomer(CustomerModel costumer)
    {
        var response = await testClient.PostAsJsonAsync(CustomerController._Create, costumer);

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsAsync<CustomerModel>();

    }


    protected async Task<CustomerModel> GetNullabeClientByIdAsync(Guid id, bool EnsureSuccess = false)
    {
        var endpoint = $"/Customers/{id}";

        var response = await testClient.GetAsync(endpoint);

        if (EnsureSuccess)
        {
            response.EnsureSuccessStatusCode();
        }

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<CustomerModel>();
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
        else
        {
            throw new NotImplementedException("unplaned behavour");
        }
    }


    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
} 
