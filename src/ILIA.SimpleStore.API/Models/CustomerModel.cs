namespace ILIA.SimpleStore.API.Models;


public class CustomerModel
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public IEnumerable<OrderModel>? Orders { get; set; }
}
