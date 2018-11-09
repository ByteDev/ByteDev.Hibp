using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ByteDev.Hibp
{
    public class HibpClient : IHibpClient
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        static HibpClient()
        {
            HttpClient.DefaultRequestHeaders.Add("User-Agent", "ByteDev-HibpClient");
            HttpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.haveibeenpwned.v2+json");
        }

        public async Task<HibpResponse> GetHasBeenPwnedAsync(string emailAddress)
        {
            return await GetHasBeenPwnedAsync(emailAddress, null);
        }

        public async Task<HibpResponse> GetHasBeenPwnedAsync(string emailAddress, HibpRequestOptions options)
        {
            if(string.IsNullOrEmpty(emailAddress))
                throw new ArgumentException(nameof(emailAddress));

            var uri = new HibpUriFactory().Create(emailAddress, options);   

            var response = await HttpClient.GetAsync(uri);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();

                var breaches = JsonConvert.DeserializeObject<IEnumerable<HibpBreachResponse>>(json);

                return HibpResponse.CreateIsPwned(breaches);
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return HibpResponse.CreateIsNotPwned();
            }

            throw new HibpClientException(CreateUnhandledStatusCodeMessage(response));
        }

        private static string CreateUnhandledStatusCodeMessage(HttpResponseMessage response)
        {
            return $"Unhandled StatusCode: '{(int)response.StatusCode} {response.StatusCode}' returned.";
        }
    }
}