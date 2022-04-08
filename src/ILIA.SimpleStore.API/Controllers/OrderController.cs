using AutoMapper;
using ILIA.SimpleStore.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ILIA.SimpleStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : BaseCustomController
    {

        private readonly ILogger<OrderController> logger;

        public OrderController(ILogger<OrderController> logger, IMapper mapper) : base(mapper)
        {
            this.logger = logger;
        }

        [Route("customers/{customerId:Int}")]
        [HttpPost]
        public ActionResult<OrderModel> Create(int customerId)
        {
            var uri = "";
            return Created(uri, new OrderModel());
        }
    }
}
