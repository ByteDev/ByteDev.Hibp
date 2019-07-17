using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
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

        /// <summary>
        /// Returns a list of all breaches a particular account has been involved in.
        /// </summary>
        /// <param name="emailAddress">Account email address to filter on.</param>
        /// <param name="options">Request options.</param>
        /// <returns>Returned breaches.</returns>
        public async Task<IEnumerable<HibpBreachResponse>> GetAccountBreachesAsync(string emailAddress, HibpRequestOptions options = null)
        {
            var uri = HibpUriFactory.CreateBreachedAccountUri(emailAddress, options);   

            var response = await _httpClient.GetAsync(uri);

            return await HibpResponseFactory.CreateBreachResponsesAsync(response);
        }

        /// <summary>
        /// Returns the details of each of breach in the system.
        /// (A "breach" is an instance of a system having been compromised by an attacker and the data disclosed).
        /// </summary>
        /// <param name="domain">Filters the result set to only breaches against the domain specified. It is possible that one site (and consequently domain), is compromised on multiple occasions.</param>
        /// <returns>Returned breaches.</returns>
        public async Task<IEnumerable<HibpBreachResponse>> GetBreachedSitesAsync(string domain = null)
        {
            var uri = HibpUriFactory.CreateBreachedSiteUri(domain);

            var response = await _httpClient.GetAsync(uri);

            return await HibpResponseFactory.CreateBreachResponsesAsync(response);
        }

        /// <summary>
        /// Retrieve a single breach by the breach "breachName".
        /// This is the stable value which may or may not be the same as the breach "title" (which can change).
        /// </summary>
        /// <param name="breachName">Breach breachName to filter on.</param>
        /// <returns>Returned breach.</returns>
        public async Task<HibpBreachResponse> GetBreachSiteByNameAsync(string breachName)
        {
            var uri = HibpUriFactory.CreateBreachSiteByNameUri(breachName);

            var response = await _httpClient.GetAsync(uri);

            return await HibpResponseFactory.CreateBreachResponseAsync(response);
        }

        /// <summary>
        /// Retrieve all data classes. A data class is an attribute of a record compromised in a breach.
        /// For example, many breaches expose data classes such as "Email addresses" and "Passwords".
        /// The values returned by this service are ordered alphabetically in a string array and will expand over time as
        /// new breaches expose previously unseen classes of data.
        /// </summary>
        /// <returns>Returned data classes.</returns>
        public async Task<IEnumerable<string>> GetDataClassesAsync()
        {
            var uri = HibpUriFactory.CreateDataClassesUri();

            var response = await _httpClient.GetAsync(uri);

            return await HibpResponseFactory.CreateDataClassesAsync(response);
        }

        /// <summary>
        /// Retrieves all pastes for an account. Unlike searching for breaches, usernames that are not email addresses
        /// cannot be searched for.
        /// </summary>
        /// <param name="emailAddress">Account email address to filter on.</param>
        /// <returns></returns>
        public async Task<IEnumerable<HibpPasteResponse>> GetAccountPastesAsync(string emailAddress)
        {
            var uri = HibpUriFactory.CreateAccountPastesUri(emailAddress);

            var response = await _httpClient.GetAsync(uri);

            return await HibpResponseFactory.CreatePasteResponsesAsync(response);
        }
    }
}