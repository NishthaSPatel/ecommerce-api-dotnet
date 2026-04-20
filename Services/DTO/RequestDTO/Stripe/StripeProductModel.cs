#nullable disable

namespace Services.DTO.RequestDTO.Stripe
{
    public class StripeProductModel
    {
        public long Id { get; set; }
        public string StripeProductId { get; set; }
        public decimal? Price { get; set; }
        public string StripePriceId { get; set; }
    }
}