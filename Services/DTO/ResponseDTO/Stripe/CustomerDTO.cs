#nullable disable

namespace Services.DTO.ResponseDTO.Stripe
{
    public class CustomerDTO
    {
        public long Id { get; set; }
        public string StripeCustomerId { get; set; }
        public bool IsDeleted { get; set; }
    }
}