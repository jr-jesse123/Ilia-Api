using ILIA.SimpleStore.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ILIA.SimpleStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")] //TODO: CHECK IF THIS IS THE DEFAULT
    public class CustomerController : ControllerBase
    {
        private readonly ILogger logger;

        public CustomerController(ILogger logger)
        {
            this.logger = logger;
        }


        //TODO: DECORATE ACTIONS FOR SWAGGER
        [HttpGet]
        public ActionResult<CustomerModel> Get(bool includSubOrders = false)
        {
            var result = new CustomerModel();
            return Ok(result);
        }


        [HttpPost]
        public ActionResult<CustomerModel> Create(CustomerModel customerModel) //TOOD: should we have a sepated model for creatioon?
        {
            var result = new CustomerModel();

            var uri = "create output uri";

            return Created(uri, result);
        }







        //TODO: IMPLEMENT CREATE

        //TODO: IMPLEMENT GETBY ID

        //TODO: IMPLEMENT POST

        //TODO: IMPLEMENTE PUT

        //TODO: IMPLEMENT DELTE



    }
}
