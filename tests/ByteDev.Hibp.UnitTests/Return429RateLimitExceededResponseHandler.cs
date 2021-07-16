using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ByteDev.Hibp.UnitTests
{
    public class Return429RateLimitExceededResponseHandler : HttpMessageHandler
    {
        private const string OkResponseBody = "[{\"Name\": \"000webhost\"},{\"Name\": \"123RF\"},{\"Name\": \"2844Breaches\"}]";

        private const string RateLimitExceededResponseBody = "{ \"statusCode\": 429, \"message\": \"Rate limit is exceeded. Try again in 2 seconds.\" }";

        private int _requestCounter;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response;

            // Returns 429 on first request and 200 on the second request
            if (_requestCounter < 1)
            {
                response = new HttpResponseMessage((HttpStatusCode) 429)
                {
                    Content = new JsonContent(RateLimitExceededResponseBody)
                };
            }
            else
            {
                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new JsonContent(OkResponseBody)
                };
            }

            _requestCounter++;
            
            return Task.FromResult(response);
        }
    }
}