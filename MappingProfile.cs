using Odev.Core.Entitys;
using Odev.Core.TransferObjects;
using AutoMapper;

namespace VeriNesneOdev2
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<Order, OrderDto>();
            CreateMap<CreateOrderDto, Order>();
            CreateMap<Customer, CustomerDto>();
        }
    }
}
