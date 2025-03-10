

namespace TestMvc1.Models
{
    public class CurrencyConversionModel
    {
        public decimal Amount { get; set; }

        public string? BaseCurrency { get; set; }

        public string? TargetCurrency { get; set; }

        public decimal ConvertedAmount { get; set; }
    }
}