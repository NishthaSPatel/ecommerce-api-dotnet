using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataAccess.Models.Catalog
{
    [Table("TaxRate", Schema = "catalog")]
    public class TaxRate
    {
        public TaxRate()
        {
            Checkouts = new HashSet<Checkout>();
        }

        [Key]
        public long Id { get; set; }
        public string StripeTaxRateId { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public decimal Percentage { get; set; }
        public bool Inclusive { get; set; }
        public string TaxRateObject { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(Checkout.TaxRate))]
        public virtual ICollection<Checkout> Checkouts { get; set; }
    }
}