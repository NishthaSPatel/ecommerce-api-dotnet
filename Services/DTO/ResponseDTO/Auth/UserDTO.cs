#nullable disable

namespace Services.DTO.ResponseDTO.Auth
{
    public class UserDTO
    {
        public long? Id { get; set; }
        public long UserTypeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string SsoIdentifier { get; set; }
        public bool IsGoogleLogin { get; set; }
        public bool IsFacebookLogin { get; set; }
        public bool IsMicrosoftLogin { get; set; }
        public bool IsAppleLogin { get; set; }
        public bool IsDeleted { get; set; }
    }
}