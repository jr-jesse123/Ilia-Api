namespace ILIA.SimpleStore.Domain;

public class Order : EntityBase
{
    public Order(decimal price, DateTime createdAt, Customer customer)
    {
        Price = price;
        CreatedAt = createdAt;
        Customer = customer;
    }
    
    //EF
    protected Order()
    {

    }

    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }

    public Customer Customer {get;}

    //TODO: IMPLEMENT STATUS
}

