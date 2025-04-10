using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TestMvc1.Models;

namespace TestMvc1.Services
{
    public class ExchangeRateService
    {
        private readonly HttpClient _httpClient;

        private const string ApiKey="263ce7ff21223c2b68e96d67";

        public ExchangeRateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ExchangeRatesResponse?> GetExchangeRatesAsync(string baseCurrency="USD", int currentPage = 1, int pageSize = 10)
        {
            string url = $"https://api.exchangerate-api.com/v4/latest/{baseCurrency}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if(response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();

                var exchangeRates =  JsonConvert.DeserializeObject<ExchangeRatesResponse>(json);

                int totalItems = exchangeRates.Rates.Count;

                //cretae pagination subsets
                var paginatedRates = exchangeRates.Rates
                .Skip((currentPage-1) * pageSize)
                .Take(pageSize)
                .ToDictionary(kvp=> kvp.Key, kvp=> kvp.Value);

                exchangeRates.Rates = paginatedRates;

                //perform pagination

                exchangeRates.pagination = new Pagination(
                    totalItems : totalItems,
                    currentPage : currentPage,
                    pageSize : pageSize
                );

                return exchangeRates;
            }


            return null;
        }

        public async Task<decimal> ConvertCurrencyAsync(decimal amount, string baseCurrency, string targetCurrency)
        {
            // Fetch the latest exchange rates
            var exchangeRates = await GetExchangeRatesAsync(baseCurrency);

            if(exchangeRates == null || !exchangeRates.Rates.ContainsKey(targetCurrency))
            {
                throw new Exception("Invalid target currency or unable to fetch exchange rates");
            } 
            //Get the exchange rate for the target currency

            decimal rate = exchangeRates.Rates[targetCurrency];

            //calculate the converted amount

            return amount * rate;

        }
    }
}