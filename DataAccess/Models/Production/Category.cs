using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataAccess.Models.Production
{
    [Table("Category", Schema = "production")]
    public class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(Product.Category))]
        public virtual ICollection<Product> Products { get; set; }
    }
}