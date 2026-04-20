using Services.DTO.RequestDTO.Production;
using Services.DTO.ResponseDTO.Production;

namespace Services.IService.Production
{
    public interface IProductionService
    {
        #region Product

        List<ProductDTO> GetProducts(string[] entities);
        ProductDTO GetProduct(long id, string[] entities);
        Task<ProductDTO> CreateProduct(ProductModel productModel);
        Task<bool> UpdateProduct(long id, ProductModel productModel);
        Task<bool> DeactivateProduct(long id);

        #endregion

        #region Media

        List<MediaDTO> GetMedias(string[] entities);
        MediaDTO GetMedia(long id, string[] entities);
        Task<MediaDTO> CreateMedia(MediaModel mediaModel);
        Task<bool> UpdateMedia(long id, MediaModel mediaModel);
        Task<bool> DeactivateMedia(long id);

        #endregion

        #region Sku

        List<SkuDTO> GetSkus(string[] entities);
        SkuDTO GetSku(long id, string[] entities);
        Task<SkuDTO> CreateSku(SkuModel skuModel);
        Task<bool> UpdateSku(long id, SkuModel skuModel);
        Task<bool> DeactivateSku(long id);

        #endregion

    }
}