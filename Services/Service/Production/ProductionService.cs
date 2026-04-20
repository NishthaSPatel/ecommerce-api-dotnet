using System.Data.SqlTypes;
using AutoMapper;
using DataAccess.Models.Production;
using DataAccess.Repositories.IRepository.Production;
using Services.IService.Production;
using Services.DTO.ResponseDTO.Production;
using Services.DTO.RequestDTO.Production;
using Stripe;
using Product = DataAccess.Models.Production.Product;
using Services.DTO.ResponseDTO.Stripe;
using Newtonsoft.Json;

#nullable disable

namespace Services.Service.Production
{
    public class ProductionService(IMapper mapper, IProductionRepository productionRepository) : IProductionService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IProductionRepository _productionRepository = productionRepository;

        #region Product

        public List<ProductDTO> GetProducts(string[] entities)
        {
            var response = _productionRepository.GetAll<Product>(x => !x.IsDeleted, entities);
            var ProductList = _mapper.Map<List<ProductDTO>>(response);

            return ProductList;
        }

        public ProductDTO GetProduct(long id, string[] entities)
        {
            var response = _productionRepository.GetAsync<Product>(id, entities).FirstOrDefault(x => !x.IsDeleted);
            var mappedProduct = _mapper.Map<ProductDTO>(response);

            if (response != null)
                return mappedProduct;
            else
                return null;
        }

        public async Task<ProductDTO> CreateProduct(ProductModel productModel)
        {
            dynamic skuMedia = null;
            if (productModel.Photo != null)
                skuMedia = await _productionRepository.CreateMedia("Production", productModel.Photo);

            var productOption = new ProductCreateOptions
            {
                Name = productModel.Name,
                Type = "good",
                Metadata = new Dictionary<string, string>
                {
                    { "ProductTypeId", productModel.ProductTypeId.ToString() }
                },
                Images = [skuMedia?.URL]
            };

            var productService = new ProductService();
            Stripe.Product stripeProduct = productService.Create(productOption);
            productOption.Id = stripeProduct.Id;

            var productOptionJSON = JsonConvert.SerializeObject(productOption);
            productModel.ProductObject = productOptionJSON;

            var productMapper = _mapper.Map<Product>(productModel);
            var response = await _productionRepository.CreateAsync(productMapper) ?? throw new SqlTypeException();
            var productDTO = _mapper.Map<ProductDTO>(response);

            productModel.Product = new DTO.RequestDTO.Stripe.StripeProductModel()
            {
                StripeProductId = stripeProduct.Id,
                Id = productDTO.Id.Value,
            };

            var stripeProductMapper = _mapper.Map<DataAccess.Models.Stripe.Product>(productModel.Product);
            var stripeResponse = await _productionRepository.CreateAsync(stripeProductMapper) ?? throw new SqlTypeException();
            var stripeProductDTO = _mapper.Map<StripeProductDTO>(stripeResponse);

            productDTO.StripeProductDTO = stripeProductDTO;

            return productDTO;
        }

        public async Task<bool> UpdateProduct(long id, ProductModel ProductModel)
        {
            if (id != ProductModel.Id)
                throw new InvalidDataException();

            var product = _mapper.Map<Product>(ProductModel);
            return await _productionRepository.UpdateAsync(id, product);
        }

        public async Task<bool> DeactivateProduct(long id)
        {
            if (id <= 0)
                throw new InvalidDataException();

            return await _productionRepository.DeactivateAsync<Product>(id);
        }

        #endregion

        #region Media

        public List<MediaDTO> GetMedias(string[] entities)
        {
            var response = _productionRepository.GetAll<Media>(x => !x.IsDeleted, entities);
            var mediaDTOList = _mapper.Map<List<MediaDTO>>(response);

            return mediaDTOList;
        }

        public MediaDTO GetMedia(long id, string[] entities)
        {
            var response = _productionRepository.GetAsync<Media>(id, entities).FirstOrDefault(x => !x.IsDeleted);
            var mappedMedia = _mapper.Map<MediaDTO>(response);

            if (response != null)
                return mappedMedia;
            else
                return null;
        }

        public async Task<MediaDTO> CreateMedia(MediaModel mediaModel)
        {
            var response = await _productionRepository.CreateMedia(mediaModel.PathFolderName, mediaModel.File) ?? throw new SqlTypeException();
            var mediaDTO = _mapper.Map<MediaDTO>(response);

            return mediaDTO;
        }

        public async Task<bool> UpdateMedia(long id, MediaModel mediaModel)
        {
            if (id != mediaModel.Id)
                throw new InvalidDataException();

            var media = _mapper.Map<Media>(mediaModel);
            return await _productionRepository.UpdateMedia(id, media, mediaModel.PathFolderName, mediaModel.File);
        }

        public async Task<bool> DeactivateMedia(long id)
        {
            if (id <= 0)
                throw new InvalidDataException();

            return await _productionRepository.DeactivateAsync<Media>(id);
        }

        #endregion

        #region Sku

        public List<SkuDTO> GetSkus(string[] entities)
        {
            var response = _productionRepository.GetAll<DataAccess.Models.Production.Sku>(x => !x.IsDeleted, entities);
            var skuDTOList = _mapper.Map<List<SkuDTO>>(response);

            return skuDTOList;
        }

        public SkuDTO GetSku(long id, string[] entities)
        {
            var response = _productionRepository.GetAsync<DataAccess.Models.Production.Sku>(id, entities).FirstOrDefault(x => !x.IsDeleted);
            var mappedSku = _mapper.Map<SkuDTO>(response);

            if (response != null)
                return mappedSku;
            else
                return null;
        }

        public async Task<SkuDTO> CreateSku(SkuModel skuModel)
        {
            dynamic skuMedia = null;
            if (skuModel.Photo != null)
                skuMedia = await _productionRepository.CreateMedia("Production/Sku", skuModel.Photo);

            string[] entities = ["StripeProduct"];
            var product = _productionRepository.GetAsync<Product>(skuModel.ProductId, entities).FirstOrDefault(x => !x.IsDeleted);

            var skuOptions = new SkuCreateOptions
            {
                Active = true,
                Price = (long)(skuModel.Price * 100),
                Currency = "INR",
                Product = product.StripeProduct.StripeProductId,
                Inventory = new SkuInventoryOptions
                {
                    Type = "finite",
                    Quantity = skuModel.Quantity
                },
                Metadata = new Dictionary<string, string>
                {
                    { "ProductId", skuModel.ProductId.ToString() }
                },
                Image = skuMedia.URL,
            };

            var skuService = new SkuService();
            Stripe.Sku stripeSKU = skuService.Create(skuOptions);
            skuOptions.Id = stripeSKU.Id;

            var skuOptionJSON = JsonConvert.SerializeObject(skuOptions);
            skuModel.SkuObject = skuOptionJSON;

            var skuMapper = _mapper.Map<DataAccess.Models.Production.Sku>(skuModel);
            var response = await _productionRepository.CreateAsync(skuMapper) ?? throw new SqlTypeException();
            var skuDTO = _mapper.Map<SkuDTO>(response);

            // Product Actual Price
            var priceOptions = new PriceCreateOptions
            {
                UnitAmount = (long)(skuModel.Price * 100),
                Currency = "INR",
                Product = product.StripeProduct.StripeProductId,
                Metadata = new Dictionary<string, string>
                    {
                        { "ProductId", skuModel.ProductId.ToString() }
                    },
            };
            var priceService = new PriceService();
            var price = priceService.Create(priceOptions);

            product.StripeProduct.StripePriceId = price.Id;
            product.StripeProduct.Price = price.UnitAmountDecimal / 100;

            await _productionRepository.UpdateAsync(product.Id, product);

            return skuDTO;
        }

        public async Task<bool> UpdateSku(long id, SkuModel skuModel)
        {
            if (id != skuModel.Id)
                throw new InvalidDataException();

            var sku = _mapper.Map<DataAccess.Models.Production.Sku>(skuModel);
            return await _productionRepository.UpdateAsync(id, sku);
        }

        public async Task<bool> DeactivateSku(long id)
        {
            if (id <= 0)
                throw new InvalidDataException();

            return await _productionRepository.DeactivateAsync<DataAccess.Models.Production.Sku>(id);
        }

        #endregion
    }
}