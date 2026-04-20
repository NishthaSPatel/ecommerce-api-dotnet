using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataAccess.Models.Stripe
{
    [Table("Product", Schema = "stripe")]
    public class Product
    {
        [Key]
        public long Id { get; set; }
        [StringLength(255)]
        public string StripeProductId { get; set; }
        public decimal? Price { get; set; }
        [StringLength(255)]
        public string StripePriceId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(Id))]
        [InverseProperty("StripeProduct")]
        public virtual Production.Product ProductionProduct { get; set; }
    }
}