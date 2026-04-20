using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Services.DTO.RequestDTO.Stripe;

#nullable disable

namespace Services.DTO.RequestDTO.Production
{
    public class ProductModel
    {
        public long? Id { get; set; }
        public long BrandId { get; set; }
        public long CategoryId { get; set; }
        public long ProductTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public string ProductObject { get; set; }
        public IFormFile Photo { get; set; }
        public virtual StripeProductModel Product { get; set; }
    }
}