using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Models.Catalog;

#nullable disable

namespace DataAccess.Models.Production
{
    [Table("Sku", Schema = "production")]
    public partial class Sku
    {
        [Key]
        public long Id { get; set; }
        public long ProductId { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string SkuObject { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductSkus")]
        public virtual Product Product { get; set; }
    }
}
