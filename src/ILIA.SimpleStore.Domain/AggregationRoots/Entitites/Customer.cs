using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILIA.SimpleStore.Domain;

public class Customer : EntityBase
{
    //EF
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
}

