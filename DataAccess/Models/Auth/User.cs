using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Models.Stripe;

#nullable disable

namespace DataAccess.Models.Auth
{
    [Table("User", Schema = "auth")]
    public class User
    {
        public User()
        {

        }

        [Key]
        public long Id { get; set; }
        public long UserTypeId { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        [StringLength(2000)]
        public string Email { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
        [StringLength(200)]
        public string SsoIdentifier { get; set; }
        public bool IsGoogleLogin { get; set; }
        public bool IsFacebookLogin { get; set; }
        public bool IsMicrosoftLogin { get; set; }
        public bool IsAppleLogin { get; set; }
        public int SortOrder { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastLogin { get; set; }

        [ForeignKey(nameof(UserTypeId))]
        [InverseProperty("Users")]
        public virtual UserType UserType { get; set; }

        [InverseProperty(nameof(Stripe.Customer.User))]
        public virtual Customer Customer { get; set; }

        [InverseProperty(nameof(Role.User))]
        public virtual ICollection<Role> Roles { get; set; }
    }
}