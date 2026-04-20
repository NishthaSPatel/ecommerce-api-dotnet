#nullable disable

namespace Services.DTO.ResponseDTO.Catalog
{
    public class TaxRateDTO
    {
        public long Id { get; set; }
        public string StripeTaxRateId { get; set; }
        public string DisplayName { get; set; }
        public decimal Percentage { get; set; }
        public bool Inclusive { get; set; }
        public bool IsDeleted { get; set; }
    }
}