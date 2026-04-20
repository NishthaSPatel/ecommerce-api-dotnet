#nullable disable

namespace Services.DTO.ResponseDTO.Production
{
    public class SkuDTO
    {
        public long? Id { get; set; }
        public long ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string SkuObject { get; set; }
    }
}