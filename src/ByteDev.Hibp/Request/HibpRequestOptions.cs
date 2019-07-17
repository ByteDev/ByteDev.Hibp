namespace ByteDev.Hibp.Request
{
    public class HibpRequestOptions
    {
        /// <summary>
        /// Returns only the name of the breach.
        /// </summary>
        public bool TruncateResponse { get; set; }

        /// <summary>
        /// Returns breaches that have been flagged as "unverified". By default, only verified breaches are returned.
        /// </summary>
        public bool IncludeUnverified { get; set; }

        /// <summary>
        /// Filters the result set to only breaches against the domain specified.
        /// It is possible that one site (and consequently domain), is compromised on multiple occasions.
        /// </summary>
        public string FilterByDomain { get; set; }
    }
}