using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Models.Catalog;

#nullable disable

namespace DataAccess.Models.Production
{
    [Table("Product", Schema = "production")]
    public class Product
    {
        [Key]
        public long Id { get; set; }
        public long BrandId { get; set; }
        public long CategoryId { get; set; }
        public long ProductTypeId { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductObject { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(BrandId))]
        [InverseProperty("Products")]
        public virtual Brand Brand { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Products")]
        public virtual Category Category { get; set; }

        [ForeignKey(nameof(ProductTypeId))]
        [InverseProperty("Products")]
        public virtual ProductType ProductType { get; set; }

        [InverseProperty(nameof(Stripe.Product.ProductionProduct))]
        public virtual Stripe.Product StripeProduct { get; set; }

        [InverseProperty(nameof(Sku.Product))]
        public virtual ICollection<Sku> ProductSkus { get; set; }

        [InverseProperty(nameof(Checkout.Product))]
        public virtual ICollection<Checkout> Checkouts { get; set; }
    }
}