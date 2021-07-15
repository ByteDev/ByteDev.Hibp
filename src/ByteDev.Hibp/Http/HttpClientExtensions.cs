using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ByteDev.Hibp.Contract.Response;

namespace ByteDev.Hibp.Http
{
    internal static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> ApiGetAsync(this HttpClient source, Uri uri, HibpClientOptions options, CancellationToken cancellationToken)
        {
            var requestFactory = new HttpRequestMessageFactory(options.ApiKey);

            var response = await source.SendAsync(requestFactory.CreateGet(uri), cancellationToken);

            if (options.RetryOnRateLimitExceeded && response.IsRateLimitedExceeded())
            {
                var rateLimitResponse = await response.DeserializeAsync<ApiRateLimitExceededResponse>();

                var seconds = rateLimitResponse.GetRetrySeconds();
                
                await Task.Delay(TimeSpan.FromSeconds(seconds), cancellationToken);

                response = await source.SendAsync(requestFactory.CreateGet(uri), cancellationToken);
            }

            return response;
        }
    }
}