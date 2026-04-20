using API.Configuration;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.OData.Edm;
using Services.DTO.ResponseDTO.Production;
using Stripe;

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();

    builder.EntitySet<ProductDTO>("Products");
    builder.EntitySet<ProductDTO>("Product");

    return builder.GetEdmModel();
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();

builder.Services.AddDependencyConfiguration(builder.Configuration);

builder.Services.AddSqlConfiguration(builder.Configuration);

builder.Services.AddAutoMapperConfiguration();

builder.Services.AddSwaggerConfiguration(builder.Configuration);

builder.Services.AddCorsConfiguration(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddOData();

StripeConfiguration.ApiKey = builder.Configuration["Stripe:Test_SecretKey"];

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint(builder.Configuration["App:SwaggerEndPoint"], "API");
    c.OAuthClientId(builder.Configuration["Auth0:ClientId"]);
    c.OAuthUsePkce();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.EnableDependencyInjection();

app.MapODataRoute("odata", "api/{controller}", GetEdmModel());

app.MaxTop(null).Select().Count().Filter().Expand().OrderBy().SkipToken();

app.Run();
