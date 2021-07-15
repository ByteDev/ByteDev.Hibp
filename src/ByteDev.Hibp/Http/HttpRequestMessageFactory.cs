using System;
using System.Net.Http;

namespace ByteDev.Hibp.Http
{
    internal class HttpRequestMessageFactory
    {
        private readonly string _apiKey;

        public HttpRequestMessageFactory(string apiKey)
        {
            _apiKey = apiKey;
        }

        public HttpRequestMessage CreateGet(Uri uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            request.AddAcceptHeader();
            request.AddUserAgentHeader();
            request.AddApiKeyHeader(_apiKey);

            return request;
        }
    }
}