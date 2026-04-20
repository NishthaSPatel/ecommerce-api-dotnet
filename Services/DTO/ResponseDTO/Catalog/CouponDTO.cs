#nullable disable

namespace Services.DTO.ResponseDTO.Catalog
{
    public class CouponDTO
    {
        public long Id { get; set; }
        public string StripeCouponId { get; set; }
        public string Name { get; set; }
        public string CouponCode { get; set; }
        public int PercentOff { get; set; }
        public string Duration { get; set; }
        public string CouponObject { get; set; }
        public bool IsDeleted { get; set; }
    }
}
