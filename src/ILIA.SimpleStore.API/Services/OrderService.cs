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
        private readonly IRepository<Order> orderRepository;

        public OrderService(ICustomerRepository customerRepository, IRepository<Order> orderRepository)
        {
            _customerRepository = customerRepository;
            this.orderRepository = orderRepository;
        }
        
        public async Task<(Order? orderCreated, IEnumerable<string> Errors)> CreateOrder(Order order, Guid customerId)
        {
            var customer = await _customerRepository.GetById(customerId);

            if (customer is null) return (null, new string[] { "Customer Not Found" });

            customer.AddOrder(order);
            var orderOutput = await orderRepository.Add(order);
            _customerRepository.Update(customer);
            
            
            await orderRepository.Commit();
            await _customerRepository.Commit();

            return (order, null);

        }
    }
}
