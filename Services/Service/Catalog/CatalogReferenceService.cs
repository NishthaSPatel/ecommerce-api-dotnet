using AutoMapper;
using DataAccess.Repositories.IRepository.Catalog;
using Services.IService.Catalog;
using Services.DTO.ResponseDTO.Catalog;
using Services.DTO.RequestDTO.Catalog;

using DataAccess.Repositories.IRepository.Production;
using Newtonsoft.Json;
using System.Data.SqlTypes;
using Stripe;
using Product = DataAccess.Models.Production.Product;
using Coupon = DataAccess.Models.Catalog.Coupon;
using TaxRate = DataAccess.Models.Catalog.TaxRate;
using ShippingRate = DataAccess.Models.Catalog.ShippingRate;

#nullable disable

namespace Services.Service.Catalog
{
    public class CatalogReferenceService(IMapper mapper, ICatalogReferenceRepository catalogReferenceRepository, IProductionRepository productionRepository) : ICatalogReferenceService
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICatalogReferenceRepository _catalogReferenceRepository = catalogReferenceRepository;
        private readonly IProductionRepository _productionRepository = productionRepository;

        #region Coupon

        public List<CouponDTO> GetCoupons(string[] entities)
        {
            var response = _catalogReferenceRepository.GetAll<Coupon>(x => !x.IsDeleted, entities);
            var couponList = _mapper.Map<List<CouponDTO>>(response);

            return couponList;
        }

        public CouponDTO GetCoupon(long id, string[] entities)
        {
            var response = _catalogReferenceRepository.GetAsync<Coupon>(id, entities).FirstOrDefault(x => !x.IsDeleted);
            if (response != null)
                return _mapper.Map<CouponDTO>(response);
            else
                return null;
        }

        public StripeList<Stripe.Coupon> GetStripeCoupons()
        {
            var options = new CouponListOptions { Limit = 3 };
            var service = new CouponService();
            StripeList<Stripe.Coupon> coupons = service.List(options);
            return coupons;
        }

        public Stripe.Coupon GetStripeCoupon(string id)
        {
            var service = new CouponService();
            var stripeCouponData = service.Get(id);
            return stripeCouponData;
        }

        public async Task<CouponDTO> CreateCoupon(CouponModel couponModel)
        {
            List<string> productList = [];
            var product = _productionRepository.GetAsync<Product>(couponModel.ProductId).FirstOrDefault();
            if (product != null)
                productList.Add(product.Name);

            var couponOptions = new CouponCreateOptions
            {
                Name = couponModel.Name,
                Currency = "INR",
                Duration = couponModel.Duration,
                DurationInMonths = 3,
                PercentOff = couponModel.PercentOff,
                Metadata = new Dictionary<string, string>
                {
                    { "Coupon", couponModel.Name.ToString() }
                },
                RedeemBy = DateTimeOffset.UtcNow.AddHours(24).DateTime
            };

            var couponService = new CouponService();
            var coupon = await couponService.CreateAsync(couponOptions);

            var promotionCodeoptions = new PromotionCodeCreateOptions { Coupon = coupon.Id };
            var promotionCodeService = new PromotionCodeService();
            promotionCodeService.Create(promotionCodeoptions);

            couponOptions.Id = coupon.Id;

            couponModel.CouponCode = GenerateRandomCouponCode();
            couponModel.CouponObject = JsonConvert.SerializeObject(couponOptions);
            couponModel.StripeCouponId = coupon.Id;

            var couponMapper = _mapper.Map<Coupon>(couponModel);
            var response = await _catalogReferenceRepository.CreateAsync(couponMapper) ?? throw new SqlTypeException();
            var couponDTO = _mapper.Map<CouponDTO>(response);

            return couponDTO;
        }

        public async Task<bool> UpdateCoupon(long id, CouponModel couponModel)
        {
            var couponId = GetCoupon((long)couponModel.Id, null).StripeCouponId;
            var options = new CouponUpdateOptions
            {
                Name = couponModel.Name,
                Metadata = new Dictionary<string, string>
                {
                    { "Coupon", couponModel.CouponCode.ToString() }
                },
            };
            var service = new CouponService();
            service.Update(couponId, options);

            var couponMapper = _mapper.Map<Coupon>(couponModel);
            return await _catalogReferenceRepository.UpdateAsync(id, couponMapper);
        }

        public async Task<bool> DeactivateCoupon(long id)
        {
            if (id <= 0)
                throw new InvalidDataException();

            return await _catalogReferenceRepository.DeactivateAsync<Coupon>(id);
        }

        static string GenerateRandomCouponCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            Random random = new();

            return new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #endregion

        #region ShippingRate

        public List<ShippingRateDTO> GetShippingRates(string[] entities)
        {
            var response = _catalogReferenceRepository.GetAll<ShippingRate>(x => !x.IsDeleted, entities);
            var shippingRateList = _mapper.Map<List<ShippingRateDTO>>(response);

            return shippingRateList;
        }

        public ShippingRateDTO GetShippingRate(long id, string[] entities)
        {
            var response = _catalogReferenceRepository.GetAsync<ShippingRate>(id, entities).FirstOrDefault(x => !x.IsDeleted);
            if (response != null)
                return _mapper.Map<ShippingRateDTO>(response);
            else
                return null;
        }

        public StripeList<Stripe.ShippingRate> GetStripeShippingRates()
        {
            var options = new ShippingRateListOptions { Limit = 3 };
            var service = new ShippingRateService();
            StripeList<Stripe.ShippingRate> shippingRates = service.List(options);
            return shippingRates;
        }

        public Stripe.ShippingRate GetStripeShippingRate(string id)
        {
            var service = new ShippingRateService();
            var stripeShippingRateData = service.Get(id);
            return stripeShippingRateData;
        }

        public async Task<ShippingRateDTO> CreateShippingRate(ShippingRateModel shippingRateModel)
        {
            var shippingRateOptions = new ShippingRateCreateOptions
            {
                DisplayName = shippingRateModel.Name,
                Type = "fixed_amount",
                FixedAmount = new ShippingRateFixedAmountOptions
                {
                    Amount = (long?)shippingRateModel.Cost,
                    Currency = "INR",
                },
                Metadata = new Dictionary<string, string>
                {
                    { "Shipping Rate", shippingRateModel.Cost.ToString() },
                    { "Display Name ", shippingRateModel.Name.ToString() }
                },
            };

            var shippingRateService = new ShippingRateService();
            var shippingRate = await shippingRateService.CreateAsync(shippingRateOptions);

            shippingRateModel.ShippingRateObject = JsonConvert.SerializeObject(shippingRateOptions);
            shippingRateModel.StripeShippingRateId = shippingRate.Id;

            var shippingRateMapper = _mapper.Map<ShippingRate>(shippingRateModel);
            var response = await _catalogReferenceRepository.CreateAsync(shippingRateMapper) ?? throw new SqlTypeException();
            var shippingRateDTO = _mapper.Map<ShippingRateDTO>(response);

            return shippingRateDTO;
        }

        public async Task<bool> UpdateShippingRate(long id, ShippingRateModel shippingRateModel)
        {
            var shippingRateId = GetShippingRate((long)shippingRateModel.Id, null).StripeShippingRateId;
            var options = new ShippingRateUpdateOptions { Active = false };
            var service = new ShippingRateService();
            service.Update(shippingRateId, options);

            var shippingRateMapper = _mapper.Map<ShippingRate>(shippingRateModel);
            return await _catalogReferenceRepository.UpdateAsync(id, shippingRateMapper);
        }

        public async Task<bool> DeactivateShippingRate(long id)
        {
            if (id <= 0)
                throw new InvalidDataException();

            return await _catalogReferenceRepository.DeactivateAsync<ShippingRate>(id);
        }

        #endregion

        #region TaxRate

        public List<TaxRateDTO> GetTaxRates(string[] entities)
        {
            var response = _catalogReferenceRepository.GetAll<TaxRate>(x => !x.IsDeleted, entities);
            var taxRateList = _mapper.Map<List<TaxRateDTO>>(response);

            return taxRateList;
        }

        public TaxRateDTO GetTaxRate(long id, string[] entities)
        {
            var response = _catalogReferenceRepository.GetAsync<TaxRate>(id, entities).FirstOrDefault(x => !x.IsDeleted);
            if (response != null)
                return _mapper.Map<TaxRateDTO>(response);
            else
                return null;
        }

        public StripeList<Stripe.TaxRate> GetStripeTaxRates()
        {
            var options = new TaxRateListOptions { Limit = 3 };
            var service = new TaxRateService();
            StripeList<Stripe.TaxRate> taxRates = service.List(options);
            return taxRates;
        }

        public Stripe.TaxRate GetStripeTaxRate(string id)
        {
            var service = new TaxRateService();
            var stripeTaxRateData = service.Get(id);
            return stripeTaxRateData;
        }

        public async Task<TaxRateDTO> CreateTaxRate(TaxRateModel taxRateModel)
        {
            var taxRateOptions = new TaxRateCreateOptions
            {
                DisplayName = taxRateModel.DisplayName,
                Description = taxRateModel.Description,
                Percentage = taxRateModel.Percentage,
                Jurisdiction = "DE",
                Inclusive = false,
            };

            var taxRateService = new TaxRateService();
            var taxRate = await taxRateService.CreateAsync(taxRateOptions);

            taxRateModel.TaxRateObject = JsonConvert.SerializeObject(taxRateOptions);
            taxRateModel.StripeTaxRateId = taxRate.Id;

            var taxRateMapper = _mapper.Map<TaxRate>(taxRateModel);
            var response = await _catalogReferenceRepository.CreateAsync(taxRateMapper) ?? throw new SqlTypeException();
            var taxRateDTO = _mapper.Map<TaxRateDTO>(response);

            return taxRateDTO;
        }

        public async Task<bool> UpdateTaxRate(long id, TaxRateModel taxRateModel)
        {
            var taxRateId = GetTaxRate((long)taxRateModel.Id, null).StripeTaxRateId;
            var options = new TaxRateUpdateOptions { Active = false };
            var service = new TaxRateService();
            service.Update(taxRateId, options);

            var taxRateMapper = _mapper.Map<TaxRate>(taxRateModel);
            return await _catalogReferenceRepository.UpdateAsync(id, taxRateMapper);
        }

        public async Task<bool> DeactivateTaxRate(long id)
        {
            if (id <= 0)
                throw new InvalidDataException();

            return await _catalogReferenceRepository.DeactivateAsync<TaxRate>(id);
        }

        #endregion
    }
}
