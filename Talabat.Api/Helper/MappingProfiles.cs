using AutoMapper;
using Talabat.Api.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Api.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToRreturnDto>()
                .ForMember( P => P.ProductBrand, O => O.MapFrom(s=>s.ProductBrand.Name))
                .ForMember( P => P.ProductType, O => O.MapFrom(s=>s.ProductType.Name))
                .ForMember( P => P.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());

            CreateMap<Talabat.Core.Entities.Identity.Address, AddressDto>().ReverseMap();

            CreateMap<AddressDto, Talabat.Core.Entities.Order_Aggregate.Address>();
            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(d => d.DeliveryMethod, O => O.MapFrom(s => s.DeliveryMethod.Name))
                .ForMember(d => d.DeliveryMethodcost, O => O.MapFrom(s => s.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(d => d.ProductId, O => O.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, O => O.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, O => O.MapFrom(s => s.Product.PictureUrl))
                .ForMember(P => P.PictureUrl, O => O.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<CustomerBasketDTO, CustomerBasket>();
            CreateMap<BasketItemDTO, BasketItem>();
        }
    }
}
