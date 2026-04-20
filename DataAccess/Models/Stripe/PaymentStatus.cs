using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataAccess.Models.Stripe
{
    [Table("PaymentStatus", Schema = "stripe")]
    public class PaymentStatus
    {
        public PaymentStatus()
        {
            Payments = new HashSet<Payment>();
        }

        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(Payment.PaymentStatus))]
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
