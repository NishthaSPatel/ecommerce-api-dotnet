#nullable disable

namespace Services.DTO.RequestDTO.Stripe
{
    public class PaymentModel
    {
        public long? Id { get; set; }
        public string StripePaymentIntentId { get; set; }
        public string StripePaymentIntentClientSecret { get; set; }
        public string EphemeralKey { get; set; }
        public string Description { get; set; }
        public double AmountPaid { get; set; }
        public DateTime PaidAt { get; set; }
        public long PaymentStatusId { get; set; }
    }
}