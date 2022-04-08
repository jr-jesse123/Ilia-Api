using AutoMapper;
using ILIA.SimpleStore.API.Extentions;
using ILIA.SimpleStore.API.Models;
using ILIA.SimpleStore.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;



namespace ILIA.SimpleStore.API.Controllers;

[Route("Customers")] //TODO: CHECK IF THIS IS THE DEFAULT
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


    //TODO: DECORATE ACTIONS FOR SWAGGER
    [HttpGet]
    public async Task<ActionResult<CustomerModel>> Get()
    {
        var domainsCurstomers = await customerRepository.GetAll() ;
        var models = domainsCurstomers.Select(dc => mapper.Map<CustomerModel>(dc));
        
        return Ok(models);
    }


    [HttpPost]
    public async Task<ActionResult<CustomerModel>> Create(CustomerModel customerModel) 
    {
        var domainCustomer = mapper.Map<Customer>(customerModel);

        var storedCustomer =  await customerRepository.Add(domainCustomer);
        await customerRepository.Commit();


        var outputCustomerModel = mapper.Map<CustomerModel>(storedCustomer);    

        var uri = "";//TODO: CREATE URI FOR GET BY ID

        return Created(uri, outputCustomerModel);
    }




    /// <summary>
    /// Save a person
    /// </summary>
    /// <response code="200">OkokOKOKOK</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">NOT FOUND</response>
    /// <response code="500">Internal Server error</response>
    [HttpGet]
    [Route("{CustumerId:Guid}")]
    [ProducesResponseType(typeof(CustomerModel), 201)]
    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(Order), 400)]

    public async Task<ActionResult<CustomerModel>> GetById(Guid CustumerId)
    {
        
        var domainCustomer = await customerRepository.GetById(CustumerId);

        return MaptoModelAndStatusCode<CustomerModel>(domainCustomer);
    }





    //TODO: IMPLEMENT CREATE



    //TODO: IMPLEMENT POST

    //TODO: IMPLEMENTE PUT

    //TODO: IMPLEMENT DELTE



}
