using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataAccess.Models.Auth
{
    [Table("UserType", Schema = "auth")]
    public class UserType
    {
        public UserType()
        {
            Users = new HashSet<User>();
        }

        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
        public bool IsDeleted { get; set; }
        [InverseProperty(nameof(User.UserType))]
        public virtual ICollection<User> Users { get; set; }
    }
}