using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataAccess.Models.Production
{
    [Table("MediaType", Schema = "production")]
    public partial class MediaType
    {
        public MediaType()
        {
            Medias = new HashSet<Media>();
        }

        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Media> Medias { get; set; }
    }
}