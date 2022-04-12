using ILIA.SimpleStore.API.Services;
using ILIA.SimpleStore.Domain;
using ILIA.SimpleStore.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace ILIA.SimpleStore.Tests;

public class EmailServiceTests
{
    [Fact(DisplayName = "Mail is correctly sent to papercut server"
        , Skip = "Remove this if you need to test the final mail"
        )]

    public void Test1()
    {
        var options = new Mock<IOptions<MailOptions>>();
        options.Setup(o => o.Value).Returns(new MailOptions
        {
            Host = "localhost",
            Port = 25
        });

        var mailService = new MailService(options.Object);
        mailService.SendMail("to@mail.com", "subject", "body");
    }



    [Fact(DisplayName = "Email to client is raised when client is added")]

    public void Test2()
    {

        var options = new DbContextOptionsBuilder<SimpleStoreContext>();
        options.UseInMemoryDatabase("teste");
        var mockMailService = new Mock<IMailService>();

        var context = new SimpleStoreContext(options.Options, mockMailService.Object);

        var customer = new Customer("customer1", "customersEmail@mail.com");


        context.Customers.Add(customer);
        context.SaveChanges();

        mockMailService
            .Verify(m => m.SendMail(customer.Email, 
            "Welcome to ILIA.SimpleStore", "Welcome to ILIA.SimpleStore")
            , Times.Once);


    }


    [Fact(DisplayName = "Email isn´t raised when client is updated with orders")]

    public void Test3()
    {

        var options = new DbContextOptionsBuilder<SimpleStoreContext>();
        options.UseInMemoryDatabase("teste");
        var mockMailService = new Mock<IMailService>();

        var context = new SimpleStoreContext(options.Options, mockMailService.Object);

        var customer = new Customer("customer1", "customersEmail@mail.com");


        context.Customers.Add(customer);
        context.SaveChanges();

        var order = new Order(10M);
        
        
        customer.AddOrder(order);
        context.Orders.Add(order);

        context.SaveChanges();

            

        mockMailService
            .Verify(m => m.SendMail(customer.Email,
            "Welcome to ILIA.SimpleStore", "Welcome to ILIA.SimpleStore")
            , Times.Once);

        


    }    
}



