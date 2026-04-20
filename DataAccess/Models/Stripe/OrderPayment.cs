using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Models.Catalog;

#nullable disable

namespace DataAccess.Models.Stripe
{
    [Table("CheckoutPayment", Schema = "stripe")]
    public class CheckoutPayment
    {
        [Key]
        public long Id { get; set; }
        public long CheckoutId { get; set; }
        public long PaymentId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(CheckoutId))]
        [InverseProperty("CheckoutPayments")]
        public virtual Checkout Checkout { get; set; }

        [ForeignKey(nameof(PaymentId))]
        [InverseProperty("CheckoutPayments")]
        public virtual Payment Payment { get; set; }
    }
}