#nullable disable

namespace Services.DTO.RequestDTO.Catalog
{
    public class CouponModel
    {
        public long? Id { get; set; }
        public long ProductId { get; set; }
        public string StripeCouponId { get; set; }
        public string Name { get; set; }
        public string CouponCode { get; set; }
        public int PercentOff { get; set; }
        public string Duration { get; set; }
        public string CouponObject { get; set; }
    }
}