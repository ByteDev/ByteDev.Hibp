using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ByteDev.Hibp.UnitTests
{
    public class Return429RateLimitExceededResponseHandler : HttpMessageHandler
    {
        private const string RateLimitExceededResponseBody = "{ \"statusCode\": 429, \"message\": \"Rate limit is exceeded. Try again in 5 seconds.\" }";

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage((HttpStatusCode) 429)
            {
                Content = new JsonContent(RateLimitExceededResponseBody)
            };
            
            return Task.FromResult(response);
        }
    }
}