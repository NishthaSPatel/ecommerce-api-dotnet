using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace DataAccess.Helper
{
    public class CloudflareUpload
    {
        public readonly IConfiguration _configuration;
        public readonly IAmazonS3 _s3Client;

        public CloudflareUpload(IAmazonS3 s3Client, IConfiguration configuration)
        {
            _s3Client = s3Client;
            _configuration = configuration;
        }

        public virtual async Task<PutObjectResponse> UploadFileToS3(IFormFile file, string key)
        {
            try
            {
                var bucketName = "";
                if (_configuration["Auth0:Domain"] == "gamecodex.us.auth0.com")
                    bucketName = _configuration["Cloudflare:Prod_BucketName"];
                else
                    bucketName = _configuration["Cloudflare:BucketName"];

                var putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    ContentType = file.ContentType,
                    InputStream = file.OpenReadStream(),
                    DisablePayloadSigning = true,
                    Key = key
                };

                PutObjectResponse response = await _s3Client.PutObjectAsync(putRequest);
                if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception("File is not uploaded successfully.");
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<DeleteObjectResponse> DeleteFileFromS3(string key)
        {
            try
            {
                var bucketName = "";
                if (_configuration["Auth0:Domain"] == "gamecodex.us.auth0.com")
                    bucketName = _configuration["Cloudflare:Prod_BucketName"];
                else
                    bucketName = _configuration["Cloudflare:BucketName"];

                var deleteObjectRequest = new DeleteObjectRequest()
                {
                    BucketName = bucketName,
                    Key = key
                };

                DeleteObjectResponse response = await _s3Client.DeleteObjectAsync(deleteObjectRequest);
                if (response.HttpStatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    throw new Exception("File is not deleted successfully.");
                }

                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public virtual async Task<PutObjectResponse> UploadByteFileToS3(string file, string contentType, string key)
        {
            try
            {
                var bucketName = "";
                if (_configuration["Auth0:Domain"] == "gamecodex.us.auth0.com")
                    bucketName = _configuration["Cloudflare:Prod_BucketName"];
                else
                    bucketName = _configuration["Cloudflare:BucketName"];

                MemoryStream inputStream = new MemoryStream(Convert.FromBase64String(file));

                var putRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    ContentType = contentType,
                    InputStream = inputStream,
                    DisablePayloadSigning = true,
                    Key = key
                };

                PutObjectResponse response = await _s3Client.PutObjectAsync(putRequest);
                if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
                    throw new Exception("File is not uploaded successfully.");

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}