#nullable disable

namespace Services.DTO.RequestDTO.Auth
{
    public class UserTypeModel
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
    }
}