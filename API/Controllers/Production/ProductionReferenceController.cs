using Services.IService.Production;
using Microsoft.AspNetCore.Mvc;
using Services.DTO.ResponseDTO.Production;
using Services.DTO.RequestDTO.Production;
using Microsoft.AspNetCore.Authorization;

#nullable disable

namespace API.Controllers.Production
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductionReferenceController : ControllerBase
    {
        private readonly IProductionReferenceService _productionReferenceService;

        public ProductionReferenceController(IProductionReferenceService productionReferenceService)
        {
            _productionReferenceService = productionReferenceService;
        }

        #region Brand

        [Tags("Brand")]
        [HttpGet("Brands")]
        public List<BrandDTO> GetBrands([FromQuery] string[] entities)
        {
            return _productionReferenceService.GetBrands(entities);
        }

        // GET: api/ProductionReference/Brand/5
        // [Tags("Brand")]
        // [HttpGet("Brand/{id}")]
        // public async Task<ActionResult<BrandDTO>> GetBrand(long id)
        // {
        //     var brandDTO = await _productionReferenceService.GetBrand(id);
        //     if (brandDTO == null)
        //         return NotFound();
        //     else
        //         return brandDTO;
        // }

        // PUT: api/ProductionReference/Brand/5
        [Tags("Brand")]
        [HttpPut("Brand/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateBrand(long id, BrandModel brandModel)
        {
            await _productionReferenceService.UpdateBrand(id, brandModel);
            return NoContent();
        }

        // POST: api/ProductionReference/Brand
        [Tags("Brand")]
        [HttpPost("Brand")]
        [Authorize]
        public async Task<ActionResult<BrandDTO>> CreateBrand(BrandModel brandModel)
        {
            return await _productionReferenceService.CreateBrand(brandModel);
        }

        // DELETE: api/ProductionReference/Brand/5
        [Tags("Brand")]
        [HttpDelete("Brand/{id}")]
        [Authorize]
        public async Task<IActionResult> DeactivateBrand(long id)
        {
            await _productionReferenceService.DeactivateBrand(id);
            return NoContent();
        }

        #endregion

        #region Category

        [Tags("Category")]
        [HttpGet("Categories")]
        public List<CategoryDTO> GetCategories([FromQuery] string[] entities)
        {
            return _productionReferenceService.GetCategories(entities);
        }

        // GET: api/AuthReference/Category/5
        // [Tags("Category")]
        // [HttpGet("Category/{id}")]
        // public async Task<ActionResult<CategoryDTO>> GetCategory(long id)
        // {
        //     var categoryDTO = await _productionReferenceService.GetCategory(id);
        //     if (categoryDTO == null)
        //         return NotFound();
        //     else
        //         return categoryDTO;
        // }

        // PUT: api/AuthReference/Category/5
        [Tags("Category")]
        [HttpPut("Category/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCategory(long id, CategoryModel categoryModel)
        {
            await _productionReferenceService.UpdateCategory(id, categoryModel);
            return NoContent();
        }

        // POST: api/AuthReference/Category
        [Tags("Category")]
        [HttpPost("Category")]
        [Authorize]
        public async Task<ActionResult<CategoryDTO>> CreateCategory(CategoryModel categoryModel)
        {
            return await _productionReferenceService.CreateCategory(categoryModel);
        }

        // DELETE: api/AuthReference/Category/5
        [Tags("Category")]
        [HttpDelete("Category/{id}")]
        [Authorize]
        public async Task<IActionResult> DeactivateCategory(long id)
        {
            await _productionReferenceService.DeactivateCategory(id);
            return NoContent();
        }

        #endregion

        #region ProductType

        [Tags("ProductType")]
        [HttpGet("ProductTypes")]
        public List<ProductTypeDTO> GetProductTypes([FromQuery] string[] entities)
        {
            return _productionReferenceService.GetProductTypes(entities);
        }

        // GET: api/AuthReference/ProductType/5
        // [Tags("ProductType")]
        // [HttpGet("ProductType/{id}")]
        // public async Task<ActionResult<ProductTypeDTO>> GetProductType(long id)
        // {
        //     var productTypeDTO = await _productionReferenceService.GetProductType(id);
        //     if (productTypeDTO == null)
        //         return NotFound();
        //     else
        //         return productTypeDTO;
        // }

        // PUT: api/AuthReference/ProductType/5
        [Tags("ProductType")]
        [HttpPut("ProductType/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProductType(long id, ProductTypeModel productTypeModel)
        {
            await _productionReferenceService.UpdateProductType(id, productTypeModel);
            return NoContent();
        }

        // POST: api/AuthReference/ProductType
        [Tags("ProductType")]
        [HttpPost("ProductType")]
        [Authorize]
        public async Task<ActionResult<ProductTypeDTO>> CreateProductType(ProductTypeModel productTypeModel)
        {
            return await _productionReferenceService.CreateProductType(productTypeModel);
        }

        // DELETE: api/AuthReference/ProductType/5
        [Tags("ProductType")]
        [HttpDelete("ProductType/{id}")]
        [Authorize]
        public async Task<IActionResult> DeactivateProductType(long id)
        {
            await _productionReferenceService.DeactivateProductType(id);
            return NoContent();
        }

        #endregion
    }
}