using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace DataAccess.Helper
{
    public class CloudinaryUpload
    {
        private readonly IConfiguration _configuration;

        public CloudinaryUpload()
        {

        }

        public CloudinaryUpload(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual async Task<RawUploadResult> ImageUpload(IFormFile file, string path, string publicId)
        {
            var myAccount = new Account { ApiKey = _configuration["Cloudinary:ApiKey"], ApiSecret = _configuration["Cloudinary:ApiSecret"], Cloud = _configuration["Cloudinary:CloudName"] };

            CloudinaryDotNet.Cloudinary cloudinary = new(myAccount);

            var uploadParams = new AutoUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Folder = path,
                PublicId = publicId,
                Overwrite = true
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            _ = uploadResult?.SecureUrl.ToString();
            return uploadResult;
        }

        public virtual string DeleteImage(string url, string extension)
        {
            var myAccount = new Account { ApiKey = _configuration["Cloudinary:ApiKey"], ApiSecret = _configuration["Cloudinary:ApiSecret"], Cloud = _configuration["Cloudinary:CloudName"] };

            CloudinaryDotNet.Cloudinary cloudinary = new(myAccount);

            var deleteParams = new DeletionParams(url)
            {
                PublicId = url,
                ResourceType = ResourceType.Image,
                Invalidate = true
            };

            _ = cloudinary.Destroy(deleteParams);
            return deleteParams.ToString();
        }

        public virtual string DeleteFile(string url, string extension)
        {
            var myAccount = new Account { ApiKey = _configuration["Cloudinary:ApiKey"], ApiSecret = _configuration["Cloudinary:ApiSecret"], Cloud = _configuration["Cloudinary:CloudName"] };

            CloudinaryDotNet.Cloudinary cloudinary = new(myAccount);

            var deleteParams = new DeletionParams(url)
            {
                PublicId = url,
                ResourceType = ResourceType.Raw
            };

            _ = cloudinary.Destroy(deleteParams);
            return deleteParams.ToString();
        }

        public virtual async Task<RawUploadResult> UploadBase64FileToCloudinary(string fileName, string path, string base64StringFile, string publicId)
        {
            MemoryStream openReadStream = new(Convert.FromBase64String(base64StringFile));
            var myAccount = new Account { ApiKey = _configuration["Cloudinary:ApiKey"], ApiSecret = _configuration["Cloudinary:ApiSecret"], Cloud = _configuration["Cloudinary:CloudName"] };

            CloudinaryDotNet.Cloudinary cloudinary = new(myAccount);

            var uploadParams = new AutoUploadParams()
            {
                File = new FileDescription(fileName, openReadStream),
                Folder = path,
                PublicId = publicId,
                Overwrite = true
            };
            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            return uploadResult;
        }
    }
}