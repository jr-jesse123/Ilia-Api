namespace ILIA.SimpleStore.Domain;

public class Order : EntityBase
{
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }

    public Customer Customer {get;}

    //TODO: IMPLEMENT STATUS
}

