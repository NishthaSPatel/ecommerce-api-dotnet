#nullable disable

namespace Services.DTO.RequestDTO.Stripe
{
    public class CustomerModel
    {
        public long Id { get; set; }
        public string StripeCustomerId { get; set; }
    }
}