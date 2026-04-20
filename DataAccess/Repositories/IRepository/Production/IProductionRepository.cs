using DataAccess.Models.Production;
using Microsoft.AspNetCore.Http;

namespace DataAccess.Repositories.IRepository.Production
{
    public interface IProductionRepository : IRepositoryBase
    {
        #region Media

        Task<Media> CreateMedia(string pathFolderName, IFormFile file, bool isCloudflare = false);
        Task<bool> UpdateMedia(long id, Media media, string pathFolderName, IFormFile file, bool isCloudflare = false);

        #endregion
    }
}