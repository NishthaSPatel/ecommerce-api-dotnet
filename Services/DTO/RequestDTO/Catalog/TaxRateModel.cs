#nullable disable

namespace Services.DTO.RequestDTO.Catalog
{
    public class TaxRateModel
    {
        public long? Id { get; set; }
        public string StripeTaxRateId { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public decimal Percentage { get; set; }
        public bool Inclusive { get; set; }
        public string TaxRateObject { get; set; }
    }
}