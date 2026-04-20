#nullable disable

namespace Services.DTO.ResponseDTO.Catalog
{
    public class ShippingRateDTO
    {
        public long Id { get; set; }
        public string StripeShippingRateId { get; set; }
        public decimal Cost { get; set; }
        public bool IsDeleted { get; set; }
    }
}