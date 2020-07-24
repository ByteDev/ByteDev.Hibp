using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ByteDev.Hibp.Http;
using ByteDev.Hibp.Request;
using ByteDev.Hibp.Response;

namespace ByteDev.Hibp
{
    /// <summary>
    /// Represents a client to the HIBP API.
    /// </summary>
    public class HibpClient : IHibpClient
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Hibp.HibpClient" /> class.
        /// </summary>
        /// <param name="httpClient">HttpClient to use in all requests to the API.</param>
        /// <param name="apiKey">Authorization key for the API.</param>
        public HibpClient(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;

            _httpClient.AddApiKeyHeader(apiKey);
            _httpClient.AddRequestHeaderUserAgent();
            _httpClient.AddRequestHeaderContentNegotiation();
        }

        /// <summary>
        /// Get all breaches for an account.
        /// </summary>
        /// <param name="emailAddress">Email address for the account.</param>
        /// <param name="options">Search options.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<IEnumerable<HibpBreachResponse>> GetAccountBreachesAsync(string emailAddress, HibpRequestOptions options = null, CancellationToken cancellationToken = default)
        {
            var uri = HibpUriFactory.CreateBreachedAccountUri(emailAddress, options);   

            var response = await _httpClient.GetAsync(uri, cancellationToken);

            return await HibpResponseFactory.CreateBreachResponsesAsync(response);
        }

        /// <summary>
        /// Get all breached sites in the system.
        /// </summary>
        /// <param name="domain">Optional domain filter.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<IEnumerable<HibpBreachResponse>> GetBreachedSitesAsync(string domain = null, CancellationToken cancellationToken = default)
        {
            var uri = HibpUriFactory.CreateBreachedSiteUri(domain);

            var response = await _httpClient.GetAsync(uri, cancellationToken);

            return await HibpResponseFactory.CreateBreachResponsesAsync(response);
        }

        /// <summary>
        /// Get a single breached site by the breach name.
        /// </summary>
        /// <param name="breachName">Breach name. This is the stable value which may or may not be the same as the breach "title".</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<HibpBreachResponse> GetBreachSiteByNameAsync(string breachName, CancellationToken cancellationToken = default)
        {
            var uri = HibpUriFactory.CreateBreachSiteByNameUri(breachName);

            var response = await _httpClient.GetAsync(uri, cancellationToken);

            return await HibpResponseFactory.CreateBreachResponseAsync(response);
        }

        /// <summary>
        /// Get all data classes in the system.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<IEnumerable<string>> GetDataClassesAsync(CancellationToken cancellationToken = default)
        {
            var uri = HibpUriFactory.CreateDataClassesUri();

            var response = await _httpClient.GetAsync(uri, cancellationToken);

            return await HibpResponseFactory.CreateDataClassesAsync(response);
        }

        /// <summary>
        /// Get all pastes for an account.
        /// </summary>
        /// <param name="emailAddress">Email address for the account.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<IEnumerable<HibpPasteResponse>> GetAccountPastesAsync(string emailAddress, CancellationToken cancellationToken = default)
        {
            var uri = HibpUriFactory.CreateAccountPastesUri(emailAddress);

            var response = await _httpClient.GetAsync(uri, cancellationToken);

            return await HibpResponseFactory.CreatePasteResponsesAsync(response);
        }
    }
}