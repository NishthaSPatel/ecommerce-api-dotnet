using Microsoft.AspNetCore.Http;

#nullable disable

namespace Services.DTO.ResponseDTO.Production
{
    public class MediaDTO
    {
        public long? Id { get; set; }
        public long? MediaTypeId { get; set; }
        public string PathFolderName { get; set; }
        public IFormFile File { get; set; }
    }
}