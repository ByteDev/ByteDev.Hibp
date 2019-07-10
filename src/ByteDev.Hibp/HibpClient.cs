using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ByteDev.Hibp.Response;
using Newtonsoft.Json;

namespace ByteDev.Hibp
{
    public class HibpClient : IHibpClient
    {
        private static readonly HttpClient HttpClient;

        private readonly HibpUriFactory _uriFactory;

        static HibpClient()
        {
            HttpClient = new HttpClient();

            HttpClient.DefaultRequestHeaders.Add("User-Agent", "ByteDev-HibpClient");
            HttpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.haveibeenpwned.v2+json");
        }

        public HibpClient()
        {
            _uriFactory = new HibpUriFactory();
        }

        public async Task<HibpResponse> GetHasBeenPwnedAsync(string emailAddress, HibpRequestOptions options = null)
        {
            if(string.IsNullOrEmpty(emailAddress))
                throw new ArgumentException("Email address was null or empty.", nameof(emailAddress));

            var uri = _uriFactory.CreateBreachedAccountUri(emailAddress, options);   

            var response = await HttpClient.GetAsync(uri);

            return await CreateHibpResponseAsync(response);
        }

        public async Task<HibpResponse> GetBreachedSitesAsync(string domain = null)
        {
            var uri = _uriFactory.CreateBreachedSiteUri(domain);

            var response = await HttpClient.GetAsync(uri);

            return await CreateHibpResponseAsync(response);
        }

        private static async Task<HibpResponse> CreateHibpResponseAsync(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                {
                    var json = await response.Content.ReadAsStringAsync();

                    var breaches = JsonConvert.DeserializeObject<IEnumerable<HibpBreachResponse>>(json);

                    return HibpResponse.CreateIsPwned(breaches);
                }

                case HttpStatusCode.NotFound:
                    return HibpResponse.CreateIsNotPwned();

                default:
                    throw new HibpClientException(await CreateUnhandledStatusCodeMessageAsync(response));
            }
        }
        
        private static async Task<string> CreateUnhandledStatusCodeMessageAsync(HttpResponseMessage response)
        {
            var body = await response.Content.ReadAsStringAsync();

            return $"Unhandled StatusCode: '{(int)response.StatusCode} {response.StatusCode}' returned.  Response body:\n{body}";
        }
    }
}