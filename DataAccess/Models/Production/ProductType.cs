using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataAccess.Models.Production
{
    [Table("ProductType", Schema = "production")]
    public class ProductType
    {
        public ProductType()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(Product.ProductType))]
        public virtual ICollection<Product> Products { get; set; }
    }
}