using FluentAssertions;
using ILIA.SimpleStore.API.Controllers;
using ILIA.SimpleStore.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ILIA.SimpleStore.IntegrationTests;

public class CustomerControllerTests : IntegrationTestBase
{
    private readonly ITestOutputHelper outputHelper;

    private CustomerModel validCustumer = new ()
    {
        Email = "teste@teste.com",
        Name = "happy costumer"
    };

    public CustomerControllerTests(ITestOutputHelper outputHelper)
    {
        this.outputHelper = outputHelper;
    }

    [Fact(DisplayName = "Get costumers should return empty List when there are no customers to return")]
    public async Task Test1()
    {
        //Act
        var underTest = await GetCustumers();
        
        //Assert
        underTest.ToList().Count.Should().Be(0);
    }

    [Fact(DisplayName ="Adding a valid new costumer Should return the same costumer with id populated")]
    public async Task CreateClientTest()
    {
        //Act
        var underTest = await CreateCustomer(validCustumer);

        //Assert
        underTest.Id.Should().NotBeNull();
        underTest.Id.Should().NotBe(System.Guid.Empty);

        
    }


    [Fact(DisplayName = "Get costumers should return populated List after valid Customers have been Created")]
    public async Task Test3()
    {
        //Arrange
        await CreateClientTest();

        //Act
        var underTest = await GetCustumers();

        //Assert
        underTest.Count().Should().BeGreaterThan(0);
    }

    [Fact(DisplayName = "Get costumers by id should return costumer for valid id")]
    public async Task Test4()
    {
        //Arrange
        var customer = await CreateCustomer(validCustumer);

        //Act
        var underTest = await GetNullabeClientByIdAsync(customer.Id.Value);

        //Assert
        underTest.Id.Should().Be(customer.Id);
        underTest.Email.Should().Be(customer.Email);
        underTest.Name.Should().Be(customer.Name);
    }


    [Fact(DisplayName = "Get costumers by id with valid id should return costumer and related Orders")]
    public async Task Test5()
    {
        //Arrange
        var customer = await CreateCustomer(validCustumer);


        var orderModel = new OrderModel()
        {
            Price = 100.00M
        };

        var response = await testClient.PostAsJsonAsync($"/Order/customers/{customer.Id}", orderModel);
        response.EnsureSuccessStatusCode();
        
        response = await testClient.PostAsJsonAsync($"/Order/customers/{customer.Id}", orderModel);
        response.EnsureSuccessStatusCode();        


        //Act
        var underTest = await GetNullabeClientByIdAsync(customer.Id.Value);

        //Assert
        underTest.Id.Should().Be(customer.Id);
        underTest.Email.Should().Be(customer.Email);
        underTest.Name.Should().Be(customer.Name);

        underTest.Orders.Count().Should().BeGreaterThan(0);
    }


    [Fact(DisplayName = "Get costumers by id should return NOT Found for unexistent id")]
    public async Task Test6()
    {
        //Arrange
        var id = Guid.NewGuid();

        //Act
        var underTest = await GetNullabeClientByIdAsync(id);

        //Assert
        underTest.Should().BeNull();
    }






    [Fact(DisplayName = "We can create more than one order fo a custumer")]
    public async Task Test8()
    {
        //Arrange
        var customer = await CreateCustomer(validCustumer);

        var orderModel = new OrderModel()
        {
            Price = 100.00M
        };

        var response = await testClient.PostAsJsonAsync($"/Order/customers/{customer.Id}", orderModel);
        response.EnsureSuccessStatusCode();
        
        var response2 = await testClient.PostAsJsonAsync($"/Order/customers/{customer.Id}", orderModel);
        response.EnsureSuccessStatusCode();


        ////throw new NotImplementedException("create orders for this costumer");

        ////Act
        //var underTest = await GetCustumers();

        ////Assert
        //underTest.Count().Should().BeGreaterThan(0);
        //underTest.All(c => c.Orders.Count() > 0).Should().BeTrue();

    }


}