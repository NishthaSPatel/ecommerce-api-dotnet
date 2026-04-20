#nullable disable

namespace Services.DTO.RequestDTO.Production
{
    public class BrandModel
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
    }
}