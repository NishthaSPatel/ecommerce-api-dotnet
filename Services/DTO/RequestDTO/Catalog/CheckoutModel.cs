#nullable disable

namespace Services.DTO.RequestDTO.Catalog
{
    public class CheckoutModel
    {
        public long? Id { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public long CouponId { get; set; }
        public long? TaxRateId { get; set; }
        public long? ShippingRateId { get; set; }
    }
}