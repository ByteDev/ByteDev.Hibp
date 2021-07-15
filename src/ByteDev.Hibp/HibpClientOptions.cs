namespace ByteDev.Hibp
{
    /// <summary>
    /// Represents options for <see cref="T:ByteDev.Hibp.HibpClient" />.
    /// </summary>
    public class HibpClientOptions
    {
        /// <summary>
        /// Indicates if the client should automatically retry (after a delay specified in the API response)
        /// when the API responds with a rate limit exceeded (429) response. Default is false.
        /// </summary>
        public bool RetryOnRateLimitExceeded { get; set; } = false;
    }
}