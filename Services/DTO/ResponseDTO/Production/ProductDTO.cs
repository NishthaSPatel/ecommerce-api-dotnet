using Microsoft.AspNet.OData.Builder;
using Services.DTO.ResponseDTO.Stripe;

#nullable disable

namespace Services.DTO.ResponseDTO.Production
{
    public class ProductDTO
    {
        public long? Id { get; set; }
        public long BrandId { get; set; }
        public long CategoryId { get; set; }
        public long ProductTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        [AutoExpand]
        public virtual StripeProductDTO StripeProductDTO { get; set; }
    }
}