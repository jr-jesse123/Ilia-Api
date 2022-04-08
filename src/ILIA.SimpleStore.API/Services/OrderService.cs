using ILIA.SimpleStore.API.Models;
using ILIA.SimpleStore.Domain;
using ILIA.SimpleStore.Persistence;

namespace ILIA.SimpleStore.API.Services
{
    public interface IOrderService
    {
        public Task<(Order orderCreated, IEnumerable<string> Errors)> CreateOrder(Order order, Guid customerId);
    }

    public class OrderService : IOrderService
    {
        private ICustomerRepository _customerRepository;

        public OrderService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        
        public async Task<(Order? orderCreated, IEnumerable<string> Errors)> CreateOrder(Order order, Guid customerId)
        {
            var customer = await _customerRepository.GetById(customerId);

            if (customer is null) return (null, new string[] { "Customer Not Found" });

            customer.AddOrder(order);

            _customerRepository.Update(customer);

            await _customerRepository.Commit();

            return (order, null);

        }
    }
}
