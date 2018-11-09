using System.Collections.Generic;
using System.Linq;

namespace ByteDev.Hibp
{
    public class HibpResponse
    {
        private HibpResponse()
        {
        }

        public static HibpResponse CreateIsNotPwned()
        {
            return new HibpResponse
            {
                IsPwned = false,
                Breaches = Enumerable.Empty<HibpBreachResponse>()
            };
        }

        public static HibpResponse CreateIsPwned(IEnumerable<HibpBreachResponse> hibpBreachResponses)
        {
            return new HibpResponse
            {
                IsPwned = true,
                Breaches = hibpBreachResponses
            };
        }

        public bool IsPwned { get; private set; }

        public IEnumerable<HibpBreachResponse> Breaches { get; private set; }
    }
}