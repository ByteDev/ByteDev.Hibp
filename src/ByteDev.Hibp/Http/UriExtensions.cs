using System;
using ByteDev.ResourceIdentifier;

namespace ByteDev.Hibp.Http
{
    internal static class UriExtensions
    {
        public static Uri AddTruncateResponse(this Uri source, bool truncateRespnse)
        {
            if(!truncateRespnse)
                return source.AddOrUpdateQueryParam("truncateResponse", "false");

            return source;
        }

        public static Uri AddIncludeUnverified(this Uri source, bool includeUnverified)
        {
            if (!includeUnverified)
                return source.AddOrUpdateQueryParam("includeUnverified", "false");

            return source;
        }

        public static Uri AddFilterByDomain(this Uri source, string domain)
        {
            if (!string.IsNullOrEmpty(domain))
                source = source.AddOrUpdateQueryParam("domain", domain);

            return source;
        }
    }
}