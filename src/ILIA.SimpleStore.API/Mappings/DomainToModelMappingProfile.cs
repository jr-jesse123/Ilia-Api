using AutoMapper;
using ILIA.SimpleStore.API.Models;
using ILIA.SimpleStore.Domain;

namespace ILIA.SimpleStore.API.Mappings;

public class DomainToModelMappingProfile : Profile
{
    public DomainToModelMappingProfile()
    {
        CreateMap<Order, OrderModel>().ReverseMap();

        CreateMap<OrderCreateModel, Order>()
            .ForMember(dst => dst.CreatedAt, opt => opt.Ignore())
            .ForMember(dst => dst.Customer, opt => opt.Ignore());

        CreateMap<Customer, CustomerModel>().ReverseMap();
        CreateMap<CustomerCreateModel, Customer>()
            .ForMember(dst => dst.Orders, opt => opt.Ignore());
    }
}
