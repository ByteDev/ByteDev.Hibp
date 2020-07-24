using System;
using System.Net;
using ByteDev.Hibp.Http;
using ByteDev.Hibp.Request;

namespace ByteDev.Hibp
{
    internal class HibpUriFactory
    {
        private const string BaseUrl = "https://haveibeenpwned.com/api/v3/";

        public static Uri CreateBreachedAccountUri(string emailAddress, HibpRequestOptions options = null)
        {
            if (string.IsNullOrEmpty(emailAddress))
                throw new ArgumentException("Email address was null or empty.", nameof(emailAddress));

            var uri = new Uri(BaseUrl + UriServicePath.BreachedAccount + "/" + WebUtility.UrlEncode(emailAddress));

            if (options != null)
            {
                uri = uri.AddTruncateResponse(options.TruncateResponse);
                uri = uri.AddIncludeUnverified(options.IncludeUnverified);
                uri = uri.AddFilterByDomain(options.FilterByDomain);
            }

            return uri;
        }
        
        public static Uri CreateBreachedSiteUri(string domain)
        {
            var uri = new Uri(BaseUrl + UriServicePath.Breaches);

            if (!string.IsNullOrEmpty(domain))
            {
                uri = uri.AddFilterByDomain(domain);
            }

            return uri;
        }

        public static Uri CreateBreachSiteByNameUri(string breachName)
        {
            if (string.IsNullOrEmpty(breachName))
                throw new ArgumentException("Breach name was null or empty.", nameof(breachName));

            return new Uri(BaseUrl + UriServicePath.Breach + "/" + breachName);
        }

        public static Uri CreateDataClassesUri()
        {
            return new Uri(BaseUrl + UriServicePath.DataClasses);
        }

        public static Uri CreateAccountPastesUri(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
                throw new ArgumentException("Email address was null or empty.", nameof(emailAddress));

            return new Uri(BaseUrl + UriServicePath.PasteAccount + "/" + WebUtility.UrlEncode(emailAddress));
        }
    }
}