namespace DataAccess.Repositories.IRepository.Production
{
    public interface IProductionReferenceRepository : IRepositoryBase
    {
        #region Brand

        Task<bool> DeactivateBrand(long id);

        #endregion

        #region Category

        Task<bool> DeactivateCategory(long id);

        #endregion

        #region ProductType

        Task<bool> DeactivateProductType(long id);

        #endregion

        #region MediaType

        Task<bool> DeactivateMediaType(long id);

        #endregion
    }
}