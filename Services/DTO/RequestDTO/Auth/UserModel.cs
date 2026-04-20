#nullable disable

using Newtonsoft.Json;

namespace Services.DTO.RequestDTO.Auth
{
    public class UserModel
    {
        public long? Id { get; set; }
        public long UserTypeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string SsoIdentifier { get; set; }
        public string Role { get; set; }
        [JsonIgnore]
        public bool IsGoogleLogin { get; set; }
        [JsonIgnore]
        public bool IsFacebookLogin { get; set; }
        [JsonIgnore]
        public bool IsMicrosoftLogin { get; set; }
        [JsonIgnore]
        public bool IsAppleLogin { get; set; }
    }
}