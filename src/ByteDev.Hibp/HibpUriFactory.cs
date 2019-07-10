using System;
using System.Net;

namespace ByteDev.Hibp
{
    internal class HibpUriFactory
    {
        private const string BaseUrl = "https://haveibeenpwned.com/api/";

        public Uri CreateBreachedAccountUri(string emailAddress, HibpRequestOptions options = null)
        {
            var urlEncodedEmailAddress = WebUtility.UrlEncode(emailAddress);

            var uri = new Uri(BaseUrl + UriServicePath.BreachedAccount + "/" + urlEncodedEmailAddress);

            if (options != null)
            {
                uri = uri.AddTruncateResponse(options.TruncateResponse);
                uri = uri.AddIncludeUnverified(options.IncludeUnverified);
                uri = uri.AddFilterByDomain(options.FilterByDomain);
            }

            return uri;
        }
        
        public Uri CreateBreachedSiteUri(string domain)
        {
            var uri = new Uri(BaseUrl + UriServicePath.BreachedSite);

            if (!string.IsNullOrEmpty(domain))
            {
                uri = uri.AddFilterByDomain(domain);
            }

            return uri;
        }
    }
}