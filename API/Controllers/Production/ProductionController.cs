using Services.IService.Production;
using Microsoft.AspNetCore.Mvc;
using Services.DTO.ResponseDTO.Production;
using Services.DTO.RequestDTO.Production;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNet.OData;

#nullable disable

namespace API.Controllers.Production
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductionController(IProductionService productionService) : ControllerBase
    {
        private readonly IProductionService _productionService = productionService;

        #region Product

        [Tags("Product")]
        [HttpGet("Products")]
        [EnableQuery]
        public List<ProductDTO> GetProducts([FromQuery] string[] entities)
        {
            return _productionService.GetProducts(entities);
        }

        // GET: api/AuthReference/Product/5
        [Tags("Product")]
        [HttpGet("Product/{id}")]
        [EnableQuery]
        public ProductDTO GetProduct(long id, [FromQuery] string[] entities)
        {
            var productDTO = _productionService.GetProduct(id, entities);
            return productDTO;
        }

        // PUT: api/AuthReference/Product/5
        [Tags("Product")]
        [HttpPut("Product/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(long id, ProductModel productModel)
        {
            await _productionService.UpdateProduct(id, productModel);
            return NoContent();
        }

        // POST: api/AuthReference/Product
        [Tags("Product")]
        [HttpPost("Product")]
        [Authorize]
        public async Task<ActionResult<ProductDTO>> CreateProduct(ProductModel productModel)
        {
            return await _productionService.CreateProduct(productModel);
        }

        // DELETE: api/AuthReference/Product/5
        [Tags("Product")]
        [HttpDelete("Product/{id}")]
        [Authorize]
        public async Task<IActionResult> DeactivateProduct(long id)
        {
            await _productionService.DeactivateProduct(id);
            return NoContent();
        }

        #endregion

        #region Sku

        [Tags("Sku")]
        [HttpGet("Skus")]
        [EnableQuery]
        public List<SkuDTO> GetSkus([FromQuery] string[] entities)
        {
            return _productionService.GetSkus(entities);
        }

        // GET: api/CMS/Sku/5
        [Tags("Sku")]
        [HttpGet("Sku/{id}")]
        [EnableQuery]
        public SkuDTO GetSku(long id, string[] entities)
        {
            var skuDTO = _productionService.GetSku(id, entities);
            return skuDTO;
        }

        // PUT: api/CMS/Sku/5
        [Tags("Sku")]
        [HttpPut("Sku/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateSku(long id, SkuModel skuModel)
        {
            await _productionService.UpdateSku(id, skuModel);
            return NoContent();
        }

        // POST: api/CMS/Sku
        [Tags("Sku")]
        [HttpPost("Sku")]
        [Authorize]
        public async Task<ActionResult<SkuDTO>> CreateSku(SkuModel skuModel)
        {
            return await _productionService.CreateSku(skuModel);
        }

        // DELETE: api/CMS/Sku/5
        [Tags("Sku")]
        [HttpDelete("Sku/{id}")]
        [Authorize]
        public async Task<IActionResult> DeactivateSku(long id)
        {
            await _productionService.DeactivateSku(id);
            return NoContent();
        }

        #endregion
    }
}