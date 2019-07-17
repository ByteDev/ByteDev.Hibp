using System.Net.Http;

namespace ByteDev.Hibp
{
    internal static class HttpClientExtensions
    {
        public static void AddRequestHeaderUserAgent(this HttpClient source)
        {
            source.DefaultRequestHeaders.Add("User-Agent", "ByteDev-HibpClient");
        }

        public static void AddRequestHeaderContentNegotiation(this HttpClient source)
        {
            source.DefaultRequestHeaders.Add("Accept", "application/vnd.haveibeenpwned.v2+json");
        }
    }
}