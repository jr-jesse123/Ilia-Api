using FluentAssertions;
using ILIA.SimpleStore.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ILIA.SimpleStore.IntegrationTests
{
    public class OrderControllerTests : IntegrationTestBase
    {


        [Fact(DisplayName = "We can create more than one order for a custumer")]
        public async Task Test1()
        {
            //Arrange
            var customer = await CreateCustomer(validCustumer);

            var orderModel = new OrderModel()
            {
                Price = 100.00M
            };

            var response = await testClient.PostAsJsonAsync($"/Orders/customers/{customer.Id}", orderModel);
            response.EnsureSuccessStatusCode();

            var response2 = await testClient.PostAsJsonAsync($"/Orders/customers/{customer.Id}", orderModel);
            response2.EnsureSuccessStatusCode();

        }

        [Fact(DisplayName = "Created orders can be retrieved by the location header")]
        public async Task Test2()
        {
            //Arrange
            var customer = await CreateCustomer(validCustumer);

            var orderModel = new OrderModel()
            {
                Price = 100.00M
            };

            var response = await testClient.PostAsJsonAsync($"/Orders/customers/{customer.Id}", orderModel);
            response.EnsureSuccessStatusCode();

            var loc = response.Headers.Location;

            var orderResponse = await testClient.GetAsync(loc.ToString());
            orderResponse.EnsureSuccessStatusCode();

            var retrivedOrder = await orderResponse.Content.ReadAsAsync<OrderModel>();

            retrivedOrder.Price.Should().Be(orderModel.Price);

            retrivedOrder.CreatedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMinutes(1));

            

        }
    }
}
