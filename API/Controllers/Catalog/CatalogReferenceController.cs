using Services.IService.Catalog;
using Microsoft.AspNetCore.Mvc;
using Services.DTO.ResponseDTO.Catalog;
using Services.DTO.RequestDTO.Catalog;
using Microsoft.AspNetCore.Authorization;
using Stripe;

#nullable disable

namespace API.Controllers.Catalog
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogReferenceController(ICatalogReferenceService catalogReferenceService) : ControllerBase
    {
        private readonly ICatalogReferenceService _catalogReferenceService = catalogReferenceService;

        #region Coupon

        [Tags("Coupon")]
        [HttpGet("Coupons")]
        public List<CouponDTO> GetCoupons([FromQuery] string[] entities)
        {
            return _catalogReferenceService.GetCoupons(entities);
        }

        // GET: api/CatalogReference/Coupon/5
        [Tags("Coupon")]
        [HttpGet("Coupon/{id}")]
        public CouponDTO GetCoupon(long id, [FromQuery] string[] entities)
        {
            var couponDTO = _catalogReferenceService.GetCoupon(id, entities);
            return couponDTO;
        }

        [Tags("Coupon")]
        [HttpGet("StripeCoupons")]
        public StripeList<Stripe.Coupon> GetStripeCoupons()
        {
            return _catalogReferenceService.GetStripeCoupons();
        }

        // GET: api/CatalogReference/Coupon/5
        [Tags("Coupon")]
        [HttpGet("StripeCoupon/{stripeCouponId}")]
        public Stripe.Coupon GetStripeCoupon(string stripeCouponId)
        {
            return _catalogReferenceService.GetStripeCoupon(stripeCouponId);
        }

        // POST: api/CatalogReference/Coupon
        [Tags("Coupon")]
        [HttpPost("Coupon")]
        [Authorize]
        public async Task<ActionResult<CouponDTO>> CreateCoupon(CouponModel couponModel)
        {
            return await _catalogReferenceService.CreateCoupon(couponModel);
        }

        // PUT: api/CatalogReference/Coupon/5
        [Tags("Coupon")]
        [HttpPut("Coupon/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCoupon(long id, CouponModel couponModel)
        {
            await _catalogReferenceService.UpdateCoupon(id, couponModel);
            return NoContent();
        }

        // DELETE: api/CatalogReference/Coupon/5
        [Tags("Coupon")]
        [HttpDelete("Coupon/{id}")]
        [Authorize]
        public async Task<IActionResult> DeactivateCoupon(long id)
        {
            await _catalogReferenceService.DeactivateCoupon(id);
            return NoContent();
        }

        #endregion

        #region TaxRate

        [Tags("TaxRate")]
        [HttpGet("TaxRates")]
        public List<TaxRateDTO> GetTaxRates([FromQuery] string[] entities)
        {
            return _catalogReferenceService.GetTaxRates(entities);
        }

        // GET: api/CatalogReference/TaxRate/5
        [Tags("TaxRate")]
        [HttpGet("TaxRate/{id}")]
        public TaxRateDTO GetTaxRate(long id, [FromQuery] string[] entities)
        {
            var taxRateDTO = _catalogReferenceService.GetTaxRate(id, entities);
            return taxRateDTO;
        }

        [Tags("TaxRate")]
        [HttpGet("StripeTaxRates")]
        public StripeList<Stripe.TaxRate> GetStripeTaxRates()
        {
            return _catalogReferenceService.GetStripeTaxRates();
        }

        // GET: api/CatalogReference/TaxRate/5
        [Tags("TaxRate")]
        [HttpGet("StripeTaxRate/{stripeTaxRateId}")]
        public Stripe.TaxRate GetStripeTaxRate(string stripeTaxRateId)
        {
            return _catalogReferenceService.GetStripeTaxRate(stripeTaxRateId);
        }

        // POST: api/CatalogReference/TaxRate
        [Tags("TaxRate")]
        [HttpPost("TaxRate")]
        [Authorize]
        public async Task<ActionResult<TaxRateDTO>> CreateTaxRate(TaxRateModel taxRateModel)
        {
            return await _catalogReferenceService.CreateTaxRate(taxRateModel);
        }

        // PUT: api/CatalogReference/TaxRate/5
        [Tags("TaxRate")]
        [HttpPut("TaxRate/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateTaxRate(long id, TaxRateModel taxRateModel)
        {
            await _catalogReferenceService.UpdateTaxRate(id, taxRateModel);
            return NoContent();
        }

        // DELETE: api/CatalogReference/TaxRate/5
        [Tags("TaxRate")]
        [HttpDelete("TaxRate/{id}")]
        [Authorize]
        public async Task<IActionResult> DeactivateTaxRate(long id)
        {
            await _catalogReferenceService.DeactivateTaxRate(id);
            return NoContent();
        }

        #endregion

        #region ShippingRate

        [Tags("ShippingRate")]
        [HttpGet("ShippingRates")]
        public List<ShippingRateDTO> GetShippingRates([FromQuery] string[] entities)
        {
            return _catalogReferenceService.GetShippingRates(entities);
        }

        // GET: api/CatalogReference/ShippingRate/5
        [Tags("ShippingRate")]
        [HttpGet("ShippingRate/{id}")]
        public ShippingRateDTO GetShippingRate(long id, [FromQuery] string[] entities)
        {
            var taxRateDTO = _catalogReferenceService.GetShippingRate(id, entities);
            return taxRateDTO;
        }

        [Tags("ShippingRate")]
        [HttpGet("StripeShippingRates")]
        public StripeList<Stripe.ShippingRate> GetStripeShippingRates()
        {
            return _catalogReferenceService.GetStripeShippingRates();
        }

        // GET: api/CatalogReference/ShippingRate/5
        [Tags("ShippingRate")]
        [HttpGet("StripeShippingRate/{stripeShippingRateId}")]
        public Stripe.ShippingRate GetStripeShippingRate(string stripeShippingRateId)
        {
            return _catalogReferenceService.GetStripeShippingRate(stripeShippingRateId);
        }

        // POST: api/CatalogReference/ShippingRate
        [Tags("ShippingRate")]
        [HttpPost("ShippingRate")]
        [Authorize]
        public async Task<ActionResult<ShippingRateDTO>> CreateShippingRate(ShippingRateModel taxRateModel)
        {
            return await _catalogReferenceService.CreateShippingRate(taxRateModel);
        }

        // PUT: api/CatalogReference/ShippingRate/5
        [Tags("ShippingRate")]
        [HttpPut("ShippingRate/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateShippingRate(long id, ShippingRateModel taxRateModel)
        {
            await _catalogReferenceService.UpdateShippingRate(id, taxRateModel);
            return NoContent();
        }

        // DELETE: api/CatalogReference/ShippingRate/5
        [Tags("ShippingRate")]
        [HttpDelete("ShippingRate/{id}")]
        [Authorize]
        public async Task<IActionResult> DeactivateShippingRate(long id)
        {
            await _catalogReferenceService.DeactivateShippingRate(id);
            return NoContent();
        }

        #endregion
    }
}