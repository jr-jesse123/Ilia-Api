using AutoMapper;
using ILIA.SimpleStore.API.Extentions;
using ILIA.SimpleStore.API.Models;
using ILIA.SimpleStore.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;



namespace ILIA.SimpleStore.API.Controllers
{
    [ApiController]
    [Route("Customers")] //TODO: CHECK IF THIS IS THE DEFAULT
    public class CustomerController : ControllerBase
    {
        
        public static string _BaseUrl = nameof(CustomerController).RemoveSentence("Controller");
        public static string _GetAll = _BaseUrl + "s" + "/" ;
        public static string _GetById(Guid id) => _BaseUrl + "/" + id;


        private readonly ILogger<CustomerController> logger;
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;

        public CustomerController(ILogger<CustomerController> logger, ICustomerRepository customerRepository, IMapper mapper)
        {
            this.logger = logger;
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }


        //TODO: DECORATE ACTIONS FOR SWAGGER
        [HttpGet]
        public async Task<ActionResult<CustomerModel>> Get()
        {
            var domainsCurstomers = await customerRepository.GetAll() ;
            var models = domainsCurstomers.Select(dc => mapper.Map<CustomerModel>(dc));
            
            return Ok(models);
        }


        [HttpPost]
        public async Task<ActionResult<CustomerModel>> Create(CustomerModel customerModel) //TOOD: should we have a diferente model for creatioon?
        {
            var domainCustomer = mapper.Map<Customer>(customerModel);

            await customerRepository.Add(domainCustomer);
            await customerRepository.Commit();

            var uri = "";//TODO: CREATE URI FOR GET BY ID

            return Created(uri, domainCustomer);
        }




        [HttpPost]
        [Route("/{CostumerId:Guid}")]
        public async Task<ActionResult<CustomerModel>> GetById(Guid id) //TOOD: should we have a diferente model for creatioon?
        {

            //TODO: ABSTRACT NOTFOUND VS OK RESULT

            var domainCustomer = await customerRepository.GetById(id);
            if (domainCustomer is null)
            {
                return NotFound();
            }
            else
            {
                var customerModel = mapper.Map<CustomerModel>(domainCustomer);
                return Ok(customerModel);
            }

        }




        //TODO: IMPLEMENT CREATE



        //TODO: IMPLEMENT POST

        //TODO: IMPLEMENTE PUT

        //TODO: IMPLEMENT DELTE



    }
}
