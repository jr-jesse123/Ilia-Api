using AutoMapper;
using Microsoft.AspNetCore.Mvc;



namespace ILIA.SimpleStore.API.Controllers;

[ApiController]
public abstract class BaseCustomController: ControllerBase
{
    protected readonly IMapper mapper;

    public BaseCustomController(IMapper mapper)
    {
        this.mapper = mapper;
    }
    protected ActionResult<T> MaptoModelAndStatusCode<T>(object domainObj)
    {


        if (domainObj is null)
        {
            return NotFound();
        }
        else
        {
            var customerModel = mapper.Map<T>(domainObj);
            return Ok(customerModel);
        }
    }

}
