using System;
using System.Net.Http;
using System.Threading.Tasks;
using ByteDev.Hibp.Response;

namespace ByteDev.Hibp
{
    public class HibpClient : IHibpClient
    {
        private readonly HttpClient _httpClient;
        private readonly HibpUriFactory _uriFactory;

        public HibpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;

            AddDefaultHeaders();

            _uriFactory = new HibpUriFactory();
        }

        public async Task<HibpResponse> GetHasBeenPwnedAsync(string emailAddress, HibpRequestOptions options = null)
        {
            if(string.IsNullOrEmpty(emailAddress))
                throw new ArgumentException("Email address was null or empty.", nameof(emailAddress));

            var uri = _uriFactory.CreateBreachedAccountUri(emailAddress, options);   

            var response = await _httpClient.GetAsync(uri);

            return await HibpResponseFactory.CreateAsync(response);
        }

        public async Task<HibpResponse> GetBreachedSitesAsync(string domain = null)
        {
            var uri = _uriFactory.CreateBreachedSiteUri(domain);

            var response = await _httpClient.GetAsync(uri);

            return await HibpResponseFactory.CreateAsync(response);
        }

        private void AddDefaultHeaders()
        {
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "ByteDev-HibpClient");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.haveibeenpwned.v2+json");
        }
    }
}