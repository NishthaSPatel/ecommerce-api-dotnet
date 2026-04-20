#nullable disable

namespace Services.DTO.ResponseDTO.Stripe
{
    public class StripeProductDTO
    {
        public long Id { get; set; }
        public string StripeProductId { get; set; }
        public decimal? Price { get; set; }
        public string StripePriceId { get; set; }
        public bool IsDeleted { get; set; }
    }
}