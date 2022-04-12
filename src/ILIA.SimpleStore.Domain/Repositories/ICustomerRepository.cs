namespace ILIA.SimpleStore.Domain
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        public Task<Customer> GetCustomersAndRelatedOrdersById(Guid id);
    }
}
