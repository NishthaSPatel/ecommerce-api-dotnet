#nullable disable

namespace Services.DTO.ResponseDTO.Production
{
    public class CategoryDTO
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
        public bool IsDeleted { get; set; }
    }
}