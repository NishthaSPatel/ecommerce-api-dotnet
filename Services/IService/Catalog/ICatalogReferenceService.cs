using Services.DTO.RequestDTO.Catalog;
using Services.DTO.ResponseDTO.Catalog;
using Stripe;

namespace Services.IService.Catalog
{
    public interface ICatalogReferenceService
    {
        #region Coupon

        List<CouponDTO> GetCoupons(string[] entities);
        CouponDTO GetCoupon(long id, string[] entities);
        StripeList<Stripe.Coupon> GetStripeCoupons();
        Stripe.Coupon GetStripeCoupon(string id);
        Task<CouponDTO> CreateCoupon(CouponModel couponModel);
        Task<bool> UpdateCoupon(long id, CouponModel couponModel);
        Task<bool> DeactivateCoupon(long id);

        #endregion

        #region TaxRate

        List<TaxRateDTO> GetTaxRates(string[] entities);
        TaxRateDTO GetTaxRate(long id, string[] entities);
        StripeList<Stripe.TaxRate> GetStripeTaxRates();
        Stripe.TaxRate GetStripeTaxRate(string id);
        Task<TaxRateDTO> CreateTaxRate(TaxRateModel taxRateModel);
        Task<bool> UpdateTaxRate(long id, TaxRateModel taxRateModel);
        Task<bool> DeactivateTaxRate(long id);

        #endregion

        #region ShippingRate

        List<ShippingRateDTO> GetShippingRates(string[] entities);
        ShippingRateDTO GetShippingRate(long id, string[] entities);
        StripeList<Stripe.ShippingRate> GetStripeShippingRates();
        Stripe.ShippingRate GetStripeShippingRate(string id);
        Task<ShippingRateDTO> CreateShippingRate(ShippingRateModel shippingRateModel);
        Task<bool> UpdateShippingRate(long id, ShippingRateModel shippingRateModel);
        Task<bool> DeactivateShippingRate(long id);

        #endregion
    }
}