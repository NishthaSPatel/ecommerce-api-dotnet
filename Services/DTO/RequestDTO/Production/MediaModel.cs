using Microsoft.AspNetCore.Http;

#nullable disable

namespace Services.DTO.RequestDTO.Production
{
    public class MediaModel
    {
        public long? Id { get; set; }
        public long? MediaTypeId { get; set; }
        public string PathFolderName { get; set; }
        public IFormFile File { get; set; }
    }
}