using DataAccess.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace API.Configuration
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 var domain = $"https://{configuration["Auth0:Domain"]}/";
                 options.Authority = domain;
                 options.Audience = configuration["Auth0:Audience"];
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     NameClaimType = "Roles",
                     RoleClaimType = configuration["ClaimType"]
                 };
             });

            services.AddSwaggerGen(x =>
            {
                x.CustomSchemaIds(x => x.FullName);

                x.OperationFilter<SwaggerAuthorizeOperationFilter>();
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "CoreFunctionality API", Version = "v1" });
                x.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"https://{configuration["Auth0:Domain"]}/authorize?audience={configuration["Auth0:Audience"]}"),
                            TokenUrl = new Uri($"https://{configuration["Auth0:Domain"]}/oauth/token")
                        }
                    }
                });
            });
        }
    }
}