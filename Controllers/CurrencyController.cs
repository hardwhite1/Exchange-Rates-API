using TestMvc1.Models;
using TestMvc1.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TestMvc1.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly ExchangeRateService _exchangeRateService;

        public CurrencyController(ExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        public async Task<IActionResult> Index()
        {
            var exchangeRate = await _exchangeRateService.GetExchangeRatesAsync();

            return View(exchangeRate);
        }

        [HttpGet]
        public IActionResult Convert()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Convert(CurrencyConversionModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    //perform the conversion
                    model.ConvertedAmount = await _exchangeRateService.ConvertCurrencyAsync(model.Amount, model.BaseCurrency, model.TargetCurrency);

                    //passs the result to the view
                    return View(model);
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(model);
        }
    }
}