using Microsoft.AspNet.OData.Builder;

#nullable disable

namespace Services.DTO.ResponseDTO.Auth
{
    public class RoleDTO
    {
        public RoleDTO()
        {
        }

        public long Id { get; set; }
        public long RoleTypeId { get; set; }
        public long UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}