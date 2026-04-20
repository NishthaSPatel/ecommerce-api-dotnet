using DataAccess.Repositories.IRepository;
using DataAccess.Repositories.Repository;
using DataAccess.Repositories.IRepository.Auth;
using DataAccess.Repositories.Repository.Auth;
using Services.IService.Auth;
using Services.Service.Auth;
using DataAccess.Repositories.IRepository.Production;
using DataAccess.Repositories.Repository.Production;
using DataAccess.Repositories.IRepository.Stripe;
using DataAccess.Repositories.Repository.Stripe;
using Services.IService.Production;
using Services.Service.Production;
using Amazon.S3;
using DataAccess.Helper;
using Services.IService.Catalog;
using Services.Service.Catalog;
using DataAccess.Repositories.IRepository.Catalog;
using DataAccess.Repositories.Repository.Catalog;

namespace API.Configuration
{
    public static class DependencyConfiguration
    {
        public static IServiceCollection AddDependencyConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepositoryBase), typeof(RepositoryBase));

            #region Auth

            services.AddScoped<IAuthReferenceRepository, AuthReferenceRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthReferenceService, AuthReferenceService>();

            #endregion

            #region Production

            services.AddScoped<IProductionReferenceRepository, ProductionReferenceRepository>();
            services.AddScoped<IProductionRepository, ProductionRepository>();
            services.AddScoped<IProductionService, ProductionService>();
            services.AddScoped<IProductionReferenceService, ProductionReferenceService>();

            #endregion

            #region Stripe

            services.AddScoped<IStripeRepository, StripeRepository>();

            #endregion

            #region Catalog

            services.AddScoped<ICatalogReferenceRepository, CatalogReferenceRepository>();
            services.AddScoped<ICatalogRepository, CatalogRepository>();
            services.AddScoped<ICatalogService, CatalogService>();
            services.AddScoped<ICatalogReferenceService, CatalogReferenceService>();

            #endregion

            #region Third Party Configuration

            services.AddTransient<IAmazonS3>(options => new AmazonS3Client(
               configuration["Cloudflare:AccessKey"],
               configuration["Cloudflare:SecretKey"],
               new AmazonS3Config
               {
                   ServiceURL = $"https://{configuration["Cloudflare:AccountId"]}.r2.cloudflarestorage.com",
               })
            );

            services.AddScoped<CloudflareUpload>();

            services.Configure<Cloudinary>(configuration.GetSection("Cloudinary"));

            services.AddScoped<CloudinaryUpload>();

            #endregion

            return services;
        }
    }
}