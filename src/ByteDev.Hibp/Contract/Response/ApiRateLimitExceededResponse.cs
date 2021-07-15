using Newtonsoft.Json;

namespace ByteDev.Hibp.Contract.Response
{
    internal class ApiRateLimitExceededResponse
    {
        private const int SecondsPosition = 37; // 37 = "Rate limit is exceeded. Try again in ".Length

        private const int DefaultSeconds = 5;
        
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Attempts to determine the number of seconds from the returned message the client
        /// should wait before making another request.
        /// </summary>
        /// <returns>Number of seconds to wait before making another request.</returns>
        public int GetRetrySeconds()
        {
            if (string.IsNullOrEmpty(Message))
                return DefaultSeconds;
            
            var secondsStr = Message.SafeSubstring(SecondsPosition, 1);

            return int.TryParse(secondsStr, out var secondsInt) ? secondsInt : DefaultSeconds;
        }
    }
}