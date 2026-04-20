#nullable disable

namespace Services.DTO.RequestDTO.Catalog
{
    public class ShippingRateModel
    {
        public long? Id { get; set; }
        public string StripeShippingRateId { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string ShippingRateObject { get; set; }
    }
}