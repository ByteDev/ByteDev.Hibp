using System.Linq;
using System.Net.Http;

namespace ByteDev.Hibp.Http
{
    internal static class HttpClientExtensions
    {
        public static void AddApiKeyHeader(this HttpClient source, string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                return;

            source.DefaultRequestHeaders.Add("hibp-api-key", apiKey);
        }

        public static void AddRequestHeaderUserAgent(this HttpClient source)
        {
            if(source.DefaultRequestHeaders.UserAgent.Any())
                source.DefaultRequestHeaders.UserAgent.Clear();

            source.DefaultRequestHeaders.Add("User-Agent", "ByteDev-HibpClient");
        }

        public static void AddRequestHeaderContentNegotiation(this HttpClient source)
        {
            if(source.DefaultRequestHeaders.Accept.Any())
                source.DefaultRequestHeaders.Accept.Clear();

            source.DefaultRequestHeaders.Add("Accept", "application/vnd.haveibeenpwned.v2+json");
        }
    }
}