using AutoMapper;
using DataAccess.Models.Catalog;
using Services.DTO.RequestDTO.Catalog;
using Services.DTO.ResponseDTO.Catalog;

namespace Services.Mapping
{
    public class CatalogMapperProfile : Profile
    {
        public CatalogMapperProfile()
        {
            #region Catalog Reference

            CreateMap<Coupon, CouponModel>().ReverseMap();
            CreateMap<Coupon, CouponDTO>().ReverseMap();

            CreateMap<ShippingRate, ShippingRateModel>().ReverseMap();
            CreateMap<ShippingRate, ShippingRateDTO>().ReverseMap();

            CreateMap<TaxRate, TaxRateModel>().ReverseMap();
            CreateMap<TaxRate, TaxRateDTO>().ReverseMap();

            #endregion

            #region Catalog

            CreateMap<Checkout, CheckoutModel>().ReverseMap();
            CreateMap<Checkout, CheckoutDTO>().ReverseMap();

            #endregion
        }
    }
}