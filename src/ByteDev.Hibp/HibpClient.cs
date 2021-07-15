using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ByteDev.Hibp.Contract.Request;
using ByteDev.Hibp.Contract.Response;
using ByteDev.Hibp.Http;

namespace ByteDev.Hibp
{
    /// <summary>
    /// Represents a client to the HIBP API.
    /// </summary>
    public class HibpClient : IHibpClient
    {
        private readonly HttpClient _httpClient;
        private readonly HttpRequestMessageFactory _requestFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Hibp.HibpClient" /> class.
        /// </summary>
        /// <param name="httpClient">HttpClient to use in all requests to the API.</param>
        /// <param name="apiKey">Authorization key for the API.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="httpClient" /> is null.</exception>
        public HibpClient(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            _requestFactory = new HttpRequestMessageFactory(apiKey);
        }

        /// <summary>
        /// Get all breaches an account has been involved in.
        /// </summary>
        /// <param name="emailAddress">Email address for the account.</param>
        /// <param name="options">Search options.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation with a result of the breaches.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="emailAddress" /> is null or empty.</exception>
        /// <exception cref="T:ByteDev.Hibp.HibpClientException">Unhandled API error occured.</exception>
        public async Task<IEnumerable<HibpBreachResponse>> GetAccountBreachesAsync(string emailAddress, HibpRequestOptions options = null, CancellationToken cancellationToken = default)
        {
            var uri = HibpUriFactory.CreateBreachedAccountUri(emailAddress, options);

            var request = _requestFactory.CreateGet(uri);

            var response = await _httpClient.SendAsync(request, cancellationToken);

            return await HibpResponseFactory.CreateBreachResponsesAsync(response);
        }

        /// <summary>
        /// Get all breached sites in the system.
        /// </summary>
        /// <param name="domain">Optional. Filters the result set to only breaches against the domain specified. It is possible that one site (and consequently domain), is compromised on multiple occasions.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation with a result of the breaches.</returns>
        /// <exception cref="T:ByteDev.Hibp.HibpClientException">Unhandled API error occured.</exception>
        public async Task<IEnumerable<HibpBreachResponse>> GetBreachedSitesAsync(string domain = null, CancellationToken cancellationToken = default)
        {
            var uri = HibpUriFactory.CreateBreachedSiteUri(domain);

            var request = _requestFactory.CreateGet(uri);

            var response = await _httpClient.SendAsync(request, cancellationToken);

            return await HibpResponseFactory.CreateBreachResponsesAsync(response);
        }

        /// <summary>
        /// Get a single breached site by the breach name.
        /// </summary>
        /// <param name="breachName">Breach name. This is the stable value which may or may not be the same as the breach "title".</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation with a result of the breach.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="breachName" /> is null or empty.</exception>
        /// <exception cref="T:ByteDev.Hibp.HibpClientException">Unhandled API error occured.</exception>
        public async Task<HibpBreachResponse> GetBreachSiteByNameAsync(string breachName, CancellationToken cancellationToken = default)
        {
            var uri = HibpUriFactory.CreateBreachSiteByNameUri(breachName);

            var request = _requestFactory.CreateGet(uri);

            var response = await _httpClient.SendAsync(request, cancellationToken);

            return await HibpResponseFactory.CreateBreachResponseAsync(response);
        }

        /// <summary>
        /// Get all data classes in the system.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation with a result of data classes.</returns>
        /// <exception cref="T:ByteDev.Hibp.HibpClientException">Unhandled API error occured.</exception>
        public async Task<IEnumerable<string>> GetDataClassesAsync(CancellationToken cancellationToken = default)
        {
            var uri = HibpUriFactory.CreateDataClassesUri();

            var request = _requestFactory.CreateGet(uri);

            var response = await _httpClient.SendAsync(request, cancellationToken);

            return await HibpResponseFactory.CreateDataClassesAsync(response);
        }

        /// <summary>
        /// Get all pastes for an account.
        /// </summary>
        /// <param name="emailAddress">Email address for the account.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation with a result of pastes.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="emailAddress" /> is null or empty.</exception>
        /// <exception cref="T:ByteDev.Hibp.HibpClientException">Unhandled API error occured.</exception>
        public async Task<IEnumerable<HibpPasteResponse>> GetAccountPastesAsync(string emailAddress, CancellationToken cancellationToken = default)
        {
            var uri = HibpUriFactory.CreateAccountPastesUri(emailAddress);

            var request = _requestFactory.CreateGet(uri);

            var response = await _httpClient.SendAsync(request, cancellationToken);

            return await HibpResponseFactory.CreatePasteResponsesAsync(response);
        }
    }
}