using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataAccess.Models.Production
{
    [Table("Media", Schema = "production")]
    public partial class Media
    {
        public Media()
        {

        }

        [Key]
        public long Id { get; set; }
        public long MediaTypeId { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey(nameof(MediaTypeId))]
        [InverseProperty("Medias")]
        public virtual MediaType MediaType { get; set; }
    }
}