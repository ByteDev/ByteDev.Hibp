using System.Net.Http;
using System.Net.Http.Headers;

namespace ByteDev.Hibp.Http
{
    internal static class HttpRequestMessageExtensions
    {
        private const string UserAgent = "ByteDev-HibpClient";

        public static void AddApiKeyHeader(this HttpRequestMessage source, string apiKey)
        {
            if (!string.IsNullOrEmpty(apiKey))
                source.Headers.Add("hibp-api-key", apiKey);
        }

        public static void AddUserAgentHeader(this HttpRequestMessage source)
        {
            source.Headers.Add("User-Agent", UserAgent);
        }

        public static void AddAcceptHeader(this HttpRequestMessage source)
        {
            source.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.haveibeenpwned.v2+json"));
        }
    }
}