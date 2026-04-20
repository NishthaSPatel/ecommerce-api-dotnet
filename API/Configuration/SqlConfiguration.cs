using DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Configuration
{
    public static class SqlConfiguration
    {
        public static void AddSqlConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CoreFunctionalityContext>(optionsBuilder =>
            {
                var connectionString = configuration.GetConnectionString("Default");

                optionsBuilder.UseSqlServer(connectionString, a =>
                {
                    a.MigrationsAssembly("API");
                    a.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                });
                optionsBuilder.EnableDetailedErrors();
            });
        }
    }
}