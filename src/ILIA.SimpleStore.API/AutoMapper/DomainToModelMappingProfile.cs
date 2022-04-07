using AutoMapper;
using ILIA.SimpleStore.API.Models;
using ILIA.SimpleStore.Domain;

namespace ILIA.SimpleStore.API.AutoMapper
{
    public class DomainToModelMappingProfile : Profile
    {
        public DomainToModelMappingProfile()
        {
            CreateMap<Order,OrderModel>().ReverseMap();
            CreateMap<Customer,CustomerModel>().ReverseMap();
        }
    }
}
