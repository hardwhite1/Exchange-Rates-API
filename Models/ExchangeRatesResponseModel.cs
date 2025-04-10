using System.Collections.Generic;
using TestMvc1.Services;

namespace TestMvc1.Models
{
    public class ExchangeRatesResponse
    {
        public required string Base { get; set; }

        public required Dictionary<string, decimal> Rates { get; set; }

        public required Pagination pagination {get; set;}
    }
}    