using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataAccess.Models.Auth
{
    [Table("RoleType", Schema = "auth")]
    public class RoleType
    {
        public RoleType()
        {
            Roles = new HashSet<Role>();
        }

        [Key]
        public long Id { get; set; }
        public long? RoleTypeParentId { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string AuthRoleId { get; set; }
        public int SortOrder { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(RoleTypeParentId))]
        [InverseProperty(nameof(RoleType.InverseRoleTypeParent))]
        public virtual RoleType RoleTypeParent { get; set; }

        [InverseProperty(nameof(RoleType.RoleTypeParent))]
        public virtual ICollection<RoleType> InverseRoleTypeParent { get; set; }

        [InverseProperty(nameof(Role.RoleType))]
        public virtual ICollection<Role> Roles { get; set; }
    }
}