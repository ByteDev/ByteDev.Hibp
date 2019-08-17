using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ByteDev.Hibp.Http;
using ByteDev.Hibp.Request;
using ByteDev.Hibp.Response;

namespace ByteDev.Hibp
{
    public class HibpClient : IHibpClient
    {
        private readonly HttpClient _httpClient;

        public HibpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
            _httpClient.AddRequestHeaderUserAgent();
            _httpClient.AddRequestHeaderContentNegotiation();
        }

        public async Task<IEnumerable<HibpBreachResponse>> GetAccountBreachesAsync(string emailAddress, HibpRequestOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var uri = HibpUriFactory.CreateBreachedAccountUri(emailAddress, options);   

            var response = await _httpClient.GetAsync(uri, cancellationToken);

            return await HibpResponseFactory.CreateBreachResponsesAsync(response);
        }

        public async Task<IEnumerable<HibpBreachResponse>> GetBreachedSitesAsync(string domain = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var uri = HibpUriFactory.CreateBreachedSiteUri(domain);

            var response = await _httpClient.GetAsync(uri, cancellationToken);

            return await HibpResponseFactory.CreateBreachResponsesAsync(response);
        }

        public async Task<HibpBreachResponse> GetBreachSiteByNameAsync(string breachName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var uri = HibpUriFactory.CreateBreachSiteByNameUri(breachName);

            var response = await _httpClient.GetAsync(uri, cancellationToken);

            return await HibpResponseFactory.CreateBreachResponseAsync(response);
        }

        public async Task<IEnumerable<string>> GetDataClassesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var uri = HibpUriFactory.CreateDataClassesUri();

            var response = await _httpClient.GetAsync(uri, cancellationToken);

            return await HibpResponseFactory.CreateDataClassesAsync(response);
        }

        public async Task<IEnumerable<HibpPasteResponse>> GetAccountPastesAsync(string emailAddress, CancellationToken cancellationToken = default(CancellationToken))
        {
            var uri = HibpUriFactory.CreateAccountPastesUri(emailAddress);

            var response = await _httpClient.GetAsync(uri, cancellationToken);

            return await HibpResponseFactory.CreatePasteResponsesAsync(response);
        }
    }
}