using Services.DTO.RequestDTO.Production;
using Services.DTO.ResponseDTO.Production;

namespace Services.IService.Production
{
    public interface IProductionReferenceService
    {
        #region Brand

        List<BrandDTO> GetBrands(string[] entities);
        //IQueryable<BrandDTO> GetBrand(long id);
        Task<BrandDTO> CreateBrand(BrandModel brandModel);
        Task<bool> UpdateBrand(long id, BrandModel brandModel);
        Task<bool> DeactivateBrand(long id);

        #endregion

        #region Category

        List<CategoryDTO> GetCategories(string[] entities);
        //IQueryable<CategoryDTO> GetCategory(long id);
        Task<CategoryDTO> CreateCategory(CategoryModel categoryModel);
        Task<bool> UpdateCategory(long id, CategoryModel categoryModel);
        Task<bool> DeactivateCategory(long id);

        #endregion

        #region ProductType

        List<ProductTypeDTO> GetProductTypes(string[] entities);
        //IQueryable<ProductTypeDTO> GetProductType(long id);
        Task<ProductTypeDTO> CreateProductType(ProductTypeModel productTypeModel);
        Task<bool> UpdateProductType(long id, ProductTypeModel productTypeModel);
        Task<bool> DeactivateProductType(long id);

        #endregion

        #region MediaType

        List<MediaTypeDTO> GetMediaTypes(string[] entities);
        //IQueryable<MediaTypeDTO> GetMediaType(long id);
        Task<MediaTypeDTO> CreateMediaType(MediaTypeModel mediaTypeModel);
        Task<bool> UpdateMediaType(long id, MediaTypeModel mediaTypeModel);
        Task<bool> DeactivateMediaType(long id);

        #endregion
    }
}