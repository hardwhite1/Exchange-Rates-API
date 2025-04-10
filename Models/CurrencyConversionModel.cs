

namespace TestMvc1.Models
{
    public class CurrencyConversionModel
    {
        public decimal Amount { get; set; }

        public required string BaseCurrency { get; set; }

        public required string TargetCurrency { get; set; }

        public decimal ConvertedAmount { get; set; }
    }
}