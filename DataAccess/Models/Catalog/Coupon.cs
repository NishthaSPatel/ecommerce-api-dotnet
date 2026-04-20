using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataAccess.Models.Catalog
{
    [Table("Coupon", Schema = "catalog")]
    public class Coupon
    {
        public Coupon()
        {
            Checkouts = new HashSet<Checkout>();
        }

        [Key]
        public long Id { get; set; }
        public string StripeCouponId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string CouponCode { get; set; }
        public int PercentOff { get; set; }
        public string Duration { get; set; }
        public string CouponObject { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(Checkout.Coupon))]
        public virtual ICollection<Checkout> Checkouts { get; set; }
    }
}