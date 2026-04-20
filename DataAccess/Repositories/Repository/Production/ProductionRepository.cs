using DataAccess.Context;
using DataAccess.Repositories.IRepository.Production;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Amazon.S3.Model;
using DataAccess.Helper;
using DataAccess.Models.Production;
using Microsoft.AspNetCore.Http;
using System.Drawing;

#nullable disable

namespace DataAccess.Repositories.Repository.Production
{
    public class ProductionRepository(CoreFunctionalityContext context, IConfiguration configuration, CloudflareUpload cloudflareUpload, CloudinaryUpload cloudinaryUpload) : RepositoryBase(context), IProductionRepository
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly CloudflareUpload _cloudflareUpload = cloudflareUpload;
        private readonly CloudinaryUpload _cloudinaryUpload = cloudinaryUpload;

        #region Cloudflare Media

        public async Task<Media> CreateMedia(string pathFolderName, IFormFile file, bool isCloudflare = false)
        {
            Media media = new();
            if (isCloudflare)
            {
                var currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                var key = $"corefunctionality/{pathFolderName}/{currentDateTime}_{file.FileName.Replace(" ", string.Empty)}";

                await _cloudflareUpload.UploadFileToS3(file, key);
                media.Name = currentDateTime + "_" + file.FileName.Replace(" ", string.Empty);
                media.URL = _configuration["Cloudflare:BucketURL"] + key;
            }
            else
            {
                var path = $"corefunctionality/media/{pathFolderName.ToLower()}";
                var uploadResult = await _cloudinaryUpload.ImageUpload(file, path, string.Empty);
                media.MediaTypeId = _context.MediaTypes.Where(x => x.Name.ToLower().Equals("image")).FirstOrDefault().Id;
                media.Name = uploadResult?.OriginalFilename;
                media.URL = uploadResult?.SecureUrl.ToString();
            }
            _context.Medias.Add(media);
            await _context.SaveChangesAsync();

            return media;
        }

        public async Task<bool> UpdateMedia(long id, Media media, string pathFolderName, IFormFile file, bool isCloudflare = false)
        {
            var existingMedia = await _context.Medias.FindAsync(id) ?? throw new NullReferenceException();

            var currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var key = $"corefunctionality/{pathFolderName}/{currentDateTime}_{file.FileName.Replace(" ", string.Empty)}";

            if (existingMedia.URL != null && file != null)
            {
                var deleteUrlPrefix = $"corefunctionality/{pathFolderName}/";
                var deleteUrl1 = existingMedia.URL?.Substring(deleteUrlPrefix.Length);


                var deleteUrl = existingMedia.URL.Substring(existingMedia.URL.IndexOf($"corefunctionality/{pathFolderName}/"));
                var result = await _cloudflareUpload.DeleteFileFromS3(deleteUrl);

                if (result.HttpStatusCode != System.Net.HttpStatusCode.NoContent)
                    throw new Exception("Can not modify the media.");
            }

            var response = await _cloudflareUpload.UploadFileToS3(file, key);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                string domain = _configuration["Auth0:Domain"];
                var url = domain == "gamecodex.us.auth0.com" ? _configuration["Cloudflare:Prod_BucketURL"] : _configuration["Cloudflare:BucketURL"];
                url += key;

                existingMedia.URL = url;
                existingMedia.Name = currentDateTime + "_" + file.FileName.Replace(" ", string.Empty);
                existingMedia.Description = media.Description ?? null;
            }

            _context.Entry(existingMedia).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}