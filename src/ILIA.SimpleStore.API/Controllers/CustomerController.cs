using AutoMapper;
using ILIA.SimpleStore.API.Extentions;
using ILIA.SimpleStore.API.Models;
using ILIA.SimpleStore.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;



namespace ILIA.SimpleStore.API.Controllers;
[ApiController]
[Route("Customers")] 
public class CustomerController : BaseCustomController
{
    
    public static string _BaseUrl = nameof(CustomerController).RemoveSentence("Controller") + "s" + "/";
    public static string _GetAll = _BaseUrl ;
    public static string _Create = _BaseUrl ;
    public static string _GetById(Guid id) => _BaseUrl + "/" + id;


    private readonly ILogger<CustomerController> logger;
    private readonly ICustomerRepository customerRepository;
    

    public CustomerController(ILogger<CustomerController> logger, ICustomerRepository customerRepository, IMapper mapper) : base(mapper)
    {
        this.logger = logger;
        this.customerRepository = customerRepository;
    }


    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CustomerModel>), 200)]
    public async Task<ActionResult<IEnumerable<CustomerModel>>> Get()
    {
        var domainsCurstomers = await customerRepository.GetAll() ;
        var models = domainsCurstomers.Select(dc => mapper.Map<CustomerModel>(dc));
        
        return Ok(models);
    }
    
        
    [HttpPost]
    [ProducesResponseType(typeof(CustomerModel), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<CustomerModel>> Create(CustomerCreateModel customerModel) 
    {
        var domainCustomer = mapper.Map<Customer>(customerModel);
        
        var storedCustomer =  await customerRepository.Add(domainCustomer);
        await customerRepository.Commit();

        var outputCustomerModel = mapper.Map<CustomerModel>(storedCustomer);

        var request = this.HttpContext.Request;

        var uri = $"{request.Scheme}://{request.Host}/Customers/{outputCustomerModel.Id}";
        
        return Created(uri, outputCustomerModel);
    }




    [HttpGet]
    [Route("{customerId:Guid}",Name = "GetCustomerById")]
    [ProducesResponseType(typeof(CustomerModel), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<CustomerModel>> GetById(Guid customerId)
    {

        var domainCustomer = await customerRepository.GetCustomersAndRelatedOrdersById(customerId);

        return MaptoModelAndStatusCode<CustomerModel>(domainCustomer);
    }





    //TODO: IMPLEMENT CREATE



    //TODO: IMPLEMENT POST

    //TODO: IMPLEMENTE PUT

    //TODO: IMPLEMENT DELTE



}
