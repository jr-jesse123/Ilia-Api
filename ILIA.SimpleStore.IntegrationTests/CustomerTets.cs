using FluentAssertions;
using ILIA.SimpleStore.API.Controllers;
using ILIA.SimpleStore.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ILIA.SimpleStore.IntegrationTests;

public class CustomerTets : IntegrationTestBase
{
    private readonly ITestOutputHelper outputHelper;

    private CustomerModel costumer = new ()
    {
        Email = "teste@teste.com",
        Name = "happy costumer"
    };

    public CustomerTets(ITestOutputHelper outputHelper)
    {
        this.outputHelper = outputHelper;
    }

    protected async Task<IEnumerable<CustomerModel>> GetCostumers()
    {
        var response = await testClient.GetAsync(CustomerController._GetAll);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsAsync<List<CustomerModel>>();

    }

    [Fact(DisplayName = "Get costumers should return empty List when there are no customers to return")]
    public async Task Test1()
    {
        //Act
        var underTest = await GetCostumers();
        
        //Assert
        underTest.ToList().Count.Should().Be(0);
    }

    [Fact(DisplayName ="Adding a valid new costumer Should return the same costumer with id populated")]
    public async Task InsertClient()
    {
        //Act
        var response = await testClient.PostAsJsonAsync(CustomerController._Create,costumer);

        //Assert
        response.EnsureSuccessStatusCode();
        var underTest = await response.Content.ReadAsAsync<CustomerModel>();
        underTest.Id.Should().NotBeNull();
        underTest.Id.Should().NotBe(System.Guid.Empty);
    }


    [Fact(DisplayName = "Get costumers should return populated List after valid Customers have been Created")]
    public async Task Test3Async()
    {
        //Arrange
        await InsertClient();

        //Act
        var underTest = await GetCostumers();

        //Assert
        underTest.Count().Should().BeGreaterThan(0);
    }







}