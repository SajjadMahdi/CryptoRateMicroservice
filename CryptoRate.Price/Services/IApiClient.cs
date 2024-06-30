using static CryptoRate.Price.Services.ApiClient;

namespace CryptoRate.Price.Services
{
    public  interface IApiClient
    {
        CoinsInfo ConnectToApi(string currency);
    }
}
