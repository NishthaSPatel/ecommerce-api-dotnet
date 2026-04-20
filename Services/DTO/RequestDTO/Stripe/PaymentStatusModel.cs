#nullable disable

namespace Services.DTO.RequestDTO.Stripe
{
    public class PaymentStatusModel
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
