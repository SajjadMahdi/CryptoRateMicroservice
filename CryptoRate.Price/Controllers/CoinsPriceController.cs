using Microsoft.AspNetCore.Mvc;
using CryptoRate.Price.Services;
namespace CryptoRate.Price.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoinsPriceController : ControllerBase
    {
        IApiClient _apiClient;

        public CoinsPriceController(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [HttpGet]
        [Route("{currency}")]
        public IActionResult GetAction(string currency)
        {
            var result = _apiClient.ConnectToApi(currency);
            return Ok(result)
            ;
        }
    }
}