using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Models.Auth;

#nullable disable

namespace DataAccess.Models.Stripe
{
    [Table("Customer", Schema = "stripe")]
    public class Customer
    {
        [Key]
        public long Id { get; set; }
        [StringLength(255)]
        public string StripeCustomerId { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey(nameof(Id))]
        [InverseProperty("Customer")]
        public virtual User User { get; set; }
    }
}