using FluentAssertions;
using ILIA.SimpleStore.API.Controllers;
using ILIA.SimpleStore.API.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ILIA.SimpleStore.IntegrationTests
{
    public class Customerstests
    {
        private readonly HttpClient client;

        public Customerstests()
        {
            var appFactory = new WebApplicationFactory<Program>();
            client = appFactory.CreateClient();

        }

        [Fact(DisplayName = "Get all costumers should return empty List when there are no customers to return")]
        public async Task Test1Async()
        {
            var response = await client.GetAsync(CustomerController._GetAll);

            response.EnsureSuccessStatusCode();


            var jsonClients = await response.Content.ReadAsStringAsync();    

            var clients = JsonConvert.DeserializeObject<List<CustomerModel>>(jsonClients);

            clients.Count.Should().Be(0);


        }
    }
}