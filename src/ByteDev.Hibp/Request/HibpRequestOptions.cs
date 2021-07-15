namespace ByteDev.Hibp.Request
{
    /// <summary>
    /// Represents request options to the HIBP API.
    /// </summary>
    public class HibpRequestOptions
    {
        /// <summary>
        /// Returns only the name of the breach. True by default.
        /// </summary>
        public bool TruncateResponse { get; set; } = true;

        /// <summary>
        /// Returns breaches that have been flagged as "unverified". True by default
        /// (both verified and unverified breaches are returned when performing a search).
        /// </summary>
        public bool IncludeUnverified { get; set; } = true;

        /// <summary>
        /// Filters the result set to only breaches against the domain specified.
        /// It is possible that one site (and consequently domain), is compromised on multiple occasions.
        /// </summary>
        public string FilterByDomain { get; set; }
    }
}