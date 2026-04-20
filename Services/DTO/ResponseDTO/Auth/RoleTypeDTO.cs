#nullable disable

namespace Services.DTO.ResponseDTO.Auth
{
    public class RoleTypeDTO
    {
        public RoleTypeDTO()
        {
        }

        public long Id { get; set; }
        public long ParentId { get; set; }
        public string AuthRoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}