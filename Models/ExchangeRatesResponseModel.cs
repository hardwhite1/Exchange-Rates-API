using System.Collections.Generic;

namespace TestMvc1.Models
{
    public class ExchangeRatesResponse
    {
        public string? Base { get; set; }

        public Dictionary<string, decimal>? Rates { get; set; }
    }
}    