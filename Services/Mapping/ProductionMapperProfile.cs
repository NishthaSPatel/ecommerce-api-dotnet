using AutoMapper;
using DataAccess.Models.Production;
using Services.DTO.RequestDTO.Production;
using Services.DTO.ResponseDTO.Production;
using Services.DTO.ResponseDTO.Stripe;

namespace Services.Mapping
{
    public class ProductionMapperProfile : Profile
    {
        public ProductionMapperProfile()
        {
            #region Production Reference

            CreateMap<Brand, BrandModel>().ReverseMap();
            CreateMap<Brand, BrandDTO>().ReverseMap();

            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();

            CreateMap<ProductType, ProductTypeModel>().ReverseMap();
            CreateMap<ProductType, ProductTypeDTO>().ReverseMap();

            #endregion

            #region Production

            CreateMap<Product, ProductModel>().ReverseMap();

            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.StripeProductDTO, opt => opt.MapFrom(src => src.StripeProduct))
                .ReverseMap();

            CreateMap<Stripe.Product, StripeProductDTO>().ReverseMap();

            CreateMap<Sku, SkuModel>().ReverseMap();
            CreateMap<Sku, SkuDTO>().ReverseMap();

            #endregion
        }
    }
}