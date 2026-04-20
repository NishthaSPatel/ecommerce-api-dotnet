#nullable disable

namespace Services.DTO.RequestDTO.Auth
{
    public class RoleModel
    {
        public RoleModel()
        {
        }

        public long? Id { get; set; }
        public long RoleTypeId { get; set; }
        public long UserId { get; set; }
    }
}