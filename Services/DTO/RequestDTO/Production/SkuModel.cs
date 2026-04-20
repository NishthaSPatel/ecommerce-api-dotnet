using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

#nullable disable

namespace Services.DTO.RequestDTO.Production
{
    public class SkuModel
    {
        public long? Id { get; set; }
        public long ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        [JsonIgnore]
        public string SkuObject { get; set; }
        public IFormFile Photo { get; set; }
    }
}