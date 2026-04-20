#nullable disable

namespace Services.DTO.RequestDTO.Auth
{
    public class RoleTypeModel
    {
        public RoleTypeModel()
        {
        }

        public long? Id { get; set; }
        public long? ParentId { get; set; }
        public string AuthRoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}