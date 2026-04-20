using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataAccess.Models.Stripe
{
    [Table("Payment", Schema = "stripe")]
    public class Payment
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(255)]
        public string StripePaymentIntentId { get; set; }
        [Required]
        [StringLength(255)]
        public string StripePaymentIntentClientSecret { get; set; }
        [Required]
        [StringLength(255)]
        public string EphemeralKey { get; set; }
        public string Description { get; set; }
        public double AmountPaid { get; set; }
        public DateTime PaidAt { get; set; }
        public long PaymentStatusId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(PaymentStatusId))]
        [InverseProperty("Payments")]
        public virtual PaymentStatus PaymentStatus { get; set; }

        [InverseProperty(nameof(CheckoutPayment.Payment))]
        public virtual ICollection<CheckoutPayment> CheckoutPayments { get; set; }
    }
}