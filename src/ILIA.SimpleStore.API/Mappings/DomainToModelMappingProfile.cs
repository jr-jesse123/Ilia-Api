using AutoMapper;
using ILIA.SimpleStore.API.Models;
using ILIA.SimpleStore.Domain;

namespace ILIA.SimpleStore.API.Mappings;

public class DomainToModelMappingProfile : Profile
{
    public DomainToModelMappingProfile()
    {
        CreateMap<Order, OrderModel>().ReverseMap();
        CreateMap<Customer, CustomerModel>().ReverseMap();
        CreateMap<Customer, CustomerCreateModel>().ReverseMap();
    }
}
