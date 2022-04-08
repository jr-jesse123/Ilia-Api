using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILIA.SimpleStore.Domain;

public class Customer : EntityBase
{

    protected Customer()
    {

    }
    public Customer(string name, string email, IEnumerable<Order> orders)
    {
        Name = name;
        Email = email;
        Orders = orders;
    }

    public string Name { get; set; }
    public string Email { get; set; }
    public IEnumerable<Order> Orders { get; set; }
    
    ////EF
    //protected Customer()
    //{

    //}
    //public Customer(string name, string email)
    //{
    //    Name = name;
    //    Email = email;

    //}

    //public string Name { get; set; }
    //public string Email { get; set; }
    //private List<Order> orders { get; set; } = new List<Order>();
    //public IEnumerable<Order> Orders { get => Orders.AsEnumerable(); }


    //public void AddOrder(Order order)
    //{
    //    orders.Add(order);
    //}
}

