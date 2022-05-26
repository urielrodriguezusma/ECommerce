using AutoMapper;
using Core.Entities;
using ECommerce.Dto;
using ECommerce.Mappers.Resolvers;

namespace ECommerce.Mappers
{
    public class ProductDtoMapper : Profile
    {
        public ProductDtoMapper()
        {
            this.CreateMap<Product, ProductToReturnDto>()
                .ForMember(dest => dest.ProductType, src => src.MapFrom(d => d.ProductType.Name))
                .ForMember(dest => dest.ProductBrand, src => src.MapFrom(d => d.ProductBrand.Name))
                .ForMember(dest => dest.PictureUrl, src => src.MapFrom<ProductUrlResolver>());
        }
    }
}
