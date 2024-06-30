using Microsoft.Extensions.Options;
using Polly;
using RestSharp;
using System.Net;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CryptoRate.Price.Services
{
    public class ApiClient : IApiClient
    {
        private readonly ILogger<ApiClient> _logger;
        private static readonly List<HttpStatusCode> invalidStatusCode = new List<HttpStatusCode>{
            HttpStatusCode.BadRequest,
            HttpStatusCode.BadGateway,
            HttpStatusCode.InternalServerError,
            HttpStatusCode.RequestTimeout,
            HttpStatusCode.Forbidden,
            HttpStatusCode.GatewayTimeout
        };
        public ApiClient(ILogger<ApiClient> logger)
        {
            _logger = logger;
        }

        public CoinsInfo ConnectToApi(string currency)
        {

            //polly
            var retrypolicy = Policy.
            HandleResult<IRestResponse>(resp => invalidStatusCode.Contains(resp.StatusCode))
            .WaitAndRetry(10, i => TimeSpan.FromSeconds(Math.Pow(2, i)), (result, TimeSpan, currentRetryCount, context) =>
            {
                _logger.LogError($"Request has failed with a {result.Result.StatusCode}. Waiting {TimeSpan} before next retry . This is the {currentRetryCount} retry");
            });


            var client = new RestClient($"https://www.worldcoinindex.com/apiservice/ticker");

            var request = new RestRequest(Method.GET);
            request.RequestFormat = DataFormat.Json;

            request.AddParameter("key", "t2LiwnOgqnZXp0U5e2muSCYaPLGz72iWM3U", ParameterType.GetOrPost);
            request.AddParameter("label", "ethbtc-ltcbtc-btcbtc", ParameterType.GetOrPost);
            request.AddParameter("fiat", "usd", ParameterType.GetOrPost);

            var response = client.Get(request);

            var markets = JsonSerializer.Deserialize<CoinsInfo>(response.Content);

            return markets;

        }

        public record Market(string Label, string Name, double Price);
        public record CoinsInfo(Market[] Markets);

    }

}
