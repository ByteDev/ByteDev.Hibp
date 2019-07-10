using System;
using ByteDev.Common;

namespace ByteDev.Hibp
{
    internal static class UriExtensions
    {
        public static Uri AddTruncateResponse(this Uri source, bool truncateRespnse)
        {
            if(truncateRespnse)
                return source.AddOrModifyQueryStringParam("truncateResponse", "true");

            return source;
        }

        public static Uri AddIncludeUnverified(this Uri source, bool includeUnverified)
        {
            if (includeUnverified)
                return source.AddOrModifyQueryStringParam("includeUnverified", "true");

            return source;
        }

        public static Uri AddFilterByDomain(this Uri source, string domain)
        {
            if (!string.IsNullOrEmpty(domain))
                source = source.AddOrModifyQueryStringParam("domain", domain);

            return source;
        }
    }
}