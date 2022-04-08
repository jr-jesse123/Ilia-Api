﻿namespace ILIA.SimpleStore.Domain;

public class Order : EntityBase
{
    public Order(decimal price)
    {
        Price = price;
        
        
    }
    
    //EF
    protected Order()
    {
        
        CreatedAt  = DateTime.Now;
    }

    internal void SetCustomer(Customer customer)
    {
        Customer = customer;
    }

    public decimal Price { get;  }
    public DateTime CreatedAt { get; } 

    public Customer Customer { get; private set; }

    public Guid CustomerId { get; }

    //TODO: IMPLEMENT STATUS
}

