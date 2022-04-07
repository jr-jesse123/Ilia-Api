namespace ILIA.SimpleStore.Domain
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        public Customer GetCustomersAndRelatedOrdersById();
    }
}
