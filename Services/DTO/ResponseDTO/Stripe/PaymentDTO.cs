#nullable disable

namespace Services.DTO.ResponseDTO.Stripe
{
    public class PaymentDTO
    {
        public long Id { get; set; }
        public string StripePaymentIntentId { get; set; }
        public string StripePaymentIntentClientSecret { get; set; }
        public string EphemeralKey { get; set; }
        public string Description { get; set; }
        public double AmountPaid { get; set; }
        public DateTime PaidAt { get; set; }
        public long PaymentStatusId { get; set; }
        public bool IsDeleted { get; set; }
    }
}