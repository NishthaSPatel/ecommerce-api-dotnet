namespace Services.DTO.ResponseDTO.Stripe
{
    public class CheckoutPaymentDTO
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long PaymentId { get; set; }
        public bool IsDeleted { get; set; }
    }
}