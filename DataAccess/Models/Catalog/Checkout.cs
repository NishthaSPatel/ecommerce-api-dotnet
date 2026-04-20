using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Models.Stripe;
using Product = DataAccess.Models.Production.Product;

#nullable disable

namespace DataAccess.Models.Catalog
{
    [Table("Checkout", Schema = "catalog")]
    public class Checkout
    {
        [Key]
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public long CouponId { get; set; }
        public long? TaxRateId { get; set; }
        public long? ShippingRateId { get; set; }
        public string CheckoutObject { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("Checkouts")]
        public Product Product { get; set; }

        [ForeignKey(nameof(CouponId))]
        [InverseProperty("Checkouts")]
        public Coupon Coupon { get; set; }

        [ForeignKey(nameof(TaxRateId))]
        [InverseProperty("Checkouts")]
        public TaxRate TaxRate { get; set; }

        [ForeignKey(nameof(ShippingRateId))]
        [InverseProperty("Checkouts")]
        public ShippingRate ShippingRate { get; set; }

        [InverseProperty(nameof(CheckoutPayment.Checkout))]
        public virtual ICollection<CheckoutPayment> CheckoutPayments { get; set; }
    }
}