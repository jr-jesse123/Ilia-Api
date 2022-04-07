using ILIA.SimpleStore.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ILIA.SimpleStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly ILogger logger;

        public OrderController(ILogger logger)
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
