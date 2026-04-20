using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataAccess.Models.Auth
{
    [Table("Role", Schema = "auth")]
    public class Role
    {
        public Role()
        {

        }

        [Key]
        public long Id { get; set; }
        public long RoleTypeId { get; set; }
        public long UserId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(RoleTypeId))]
        [InverseProperty("Roles")]
        public virtual RoleType RoleType { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("Roles")]
        public virtual User User { get; set; }
    }
}