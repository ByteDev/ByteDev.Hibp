using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ByteDev.Hibp.Response;

namespace ByteDev.Hibp
{
    internal class HibpResponseFactory
    {
        public static async Task<IEnumerable<string>> CreateDataClassesAsync(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await response.DeserializeAsync<IEnumerable<string>>();
               
                case HttpStatusCode.NotFound:
                    return Enumerable.Empty<string>();

                default:
                    throw new HibpClientException(await CreateUnhandledStatusCodeMessageAsync(response));
            }
        }

        public static async Task<IEnumerable<HibpPasteResponse>> CreatePasteResponsesAsync(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await response.DeserializeAsync<IEnumerable<HibpPasteResponse>>();

                case HttpStatusCode.NotFound:
                    return Enumerable.Empty<HibpPasteResponse>();

                default:
                    throw new HibpClientException(await CreateUnhandledStatusCodeMessageAsync(response));
            }
        }

        public static async Task<HibpBreachResponse> CreateBreachResponseAsync(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await response.DeserializeAsync<HibpBreachResponse>();

                case HttpStatusCode.NotFound:
                    return null;

                default:
                    throw new HibpClientException(await CreateUnhandledStatusCodeMessageAsync(response));
            }
        }

        public static async Task<IEnumerable<HibpBreachResponse>> CreateBreachResponsesAsync(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await response.DeserializeAsync<IEnumerable<HibpBreachResponse>>();
                 
                case HttpStatusCode.NotFound:
                    return Enumerable.Empty<HibpBreachResponse>();

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