using AutoMapper;
using ILIA.SimpleStore.API.Models;
using ILIA.SimpleStore.API.Services;
using ILIA.SimpleStore.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ILIA.SimpleStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : BaseCustomController
    {

        private readonly ILogger<OrderController> logger;
        private readonly IOrderService orderService;
        private readonly IRepository<Order> repository;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService, IMapper mapper) : base(mapper)
        {
            this.logger = logger;
            this.orderService = orderService;
            this.repository = repository;
        }

        [Route("customers/{customerId:Guid}")]
        [HttpPost]
        public async Task<ActionResult<OrderModel>> CreateAsync(OrderModel orderModel, Guid customerId)
        {
            var domainOrder = mapper.Map<Order>(orderModel);

            var (orderCreated, errors) = await orderService.CreateOrder(domainOrder, customerId);
            
            if (errors.Count() > 0)
            {
                return BadRequest(errors);
            }
            else
            {
                var uri = ""; //TODO: POPLATE URI
                return Created(uri, new OrderModel());
            }

            
        }


        [Route("{customerId:Guid}")]
        [HttpGet]
        public ActionResult<OrderModel> Get(Guid customerId)
        {

            return Ok();
        }
    }
}
