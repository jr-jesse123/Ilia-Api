using AutoMapper;
using ILIA.SimpleStore.API.Models;
using ILIA.SimpleStore.API.Services;
using ILIA.SimpleStore.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ILIA.SimpleStore.API.Controllers
{
    [ApiController]
    [Route("Orders")]
    public class OrderController : BaseCustomController
    {

        private readonly ILogger<OrderController> logger;
        private readonly IOrderService orderService;
        private readonly IRepository<Order> repository;

        public OrderController(ILogger<OrderController> logger, 
                IOrderService orderService,
                IRepository<Order> repository,
                IMapper mapper) : base(mapper)
        {
            this.logger = logger;
            this.orderService = orderService;
            this.repository = repository;
        }

        [Route("customers/{customerId:Guid}")]
        [HttpPost]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomerModel>), 200)]
        public async Task<ActionResult<OrderModel>> CreateAsync(OrderCreateModel orderModel, Guid customerId)
        {
            var domainOrder = mapper.Map<Order>(orderModel);
            //domainOrder.CreatedAt = DateTime.Now;
            
            
            var (orderCreated, errors) = await orderService.CreateOrder(domainOrder, customerId);

            
            if (errors is not null && errors.Count() > 0)
            {
                return BadRequest(errors);
            }
            else
            {
                var outputOrder = mapper.Map<OrderModel>(orderCreated);
                var request = this.HttpContext.Request;
                var uri = $"{request.Scheme}://{request.Host}/Orders/{orderCreated.Id}";
                return Created(uri, outputOrder);
            }

            
        }


        [Route("{orderId:Guid}")]
        [HttpGet]
        public async Task<ActionResult<OrderModel>> GetAsync(Guid orderId)
        {
            var order = await repository.GetById(orderId);
            return Ok(order);
        }
    }
}
