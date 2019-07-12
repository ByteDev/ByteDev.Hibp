using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ByteDev.Hibp.Response;
using Newtonsoft.Json;

namespace ByteDev.Hibp
{
    internal class HibpResponseFactory
    {
        public static async Task<HibpResponse> CreateAsync(HttpResponseMessage response)
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

            return $"Unhandled StatusCode: '{(int)response.StatusCode} {response.StatusCode}' returned.  Response body:\n'{body}'.";
        }
    }
}