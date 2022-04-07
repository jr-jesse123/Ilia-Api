namespace ILIA.SimpleStore.API.Models
{
    
    public class OrderModel
    {
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CustomerModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public IEnumerable<OrderModel> Orders { get; set; }
    }
}
