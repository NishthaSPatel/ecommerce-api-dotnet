#nullable disable

namespace Services.DTO.RequestDTO.Stripe
{
    public class CheckoutPaymentModel
    {
        public long? Id { get; set; }
        public long OrderId { get; set; }
        public long PaymentId { get; set; }
    }
}