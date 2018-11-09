using System;
using System.Net;
using ByteDev.Common;

namespace ByteDev.Hibp
{
    internal class HibpUriFactory
    {
        private const string BaseUrl = "https://haveibeenpwned.com/api/breachedaccount/";

        public Uri Create(string emailAddress, HibpRequestOptions options = null)
        {
            var urlEncodedEmailAddress = WebUtility.UrlEncode(emailAddress);

            var uri = new Uri(BaseUrl + urlEncodedEmailAddress);

            if (options != null)
            {
                if (options.TruncateResponse)
                {
                    uri = uri.AddOrModifyQueryStringParam("truncateResponse", "true");
                }

                if (options.IncludeUnverified)
                {
                    uri = uri.AddOrModifyQueryStringParam("includeUnverified", "true");
                }

                if (!string.IsNullOrEmpty(options.FilterByDomain))
                {
                    uri = uri.AddOrModifyQueryStringParam("domain", options.FilterByDomain);
                }
            }
            
            return uri;
        }
    }
}