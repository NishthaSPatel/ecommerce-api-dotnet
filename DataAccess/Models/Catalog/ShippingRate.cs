using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataAccess.Models.Catalog
{
    [Table("ShippingRate", Schema = "catalog")]
    public class ShippingRate
    {
        public ShippingRate()
        {
            Checkouts = new HashSet<Checkout>();
        }

        [Key]
        public long Id { get; set; }
        public string StripeShippingRateId { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string ShippingRateObject { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(Checkout.ShippingRate))]
        public virtual ICollection<Checkout> Checkouts { get; set; }
    }
}