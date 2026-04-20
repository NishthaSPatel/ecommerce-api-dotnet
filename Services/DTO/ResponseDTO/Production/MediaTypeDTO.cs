#nullable disable

namespace Services.DTO.ResponseDTO.Production
{
    public class MediaTypeDTO
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
    }
}