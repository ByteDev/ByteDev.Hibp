using System;
using System.Collections.Generic;

namespace ByteDev.Hibp
{
    /// <summary>
    /// Describes a single breach incident.
    /// See https://haveibeenpwned.com/API/v2#BreachModel for more information.
    /// </summary>
    public class HibpBreachResponse
    {
        public string Name { get; internal set; }

        public string Title { get; internal set; }

        public string Domain { get; internal set; }

        public DateTime BreachDate { get; internal set; }

        public DateTime AddedDate { get; internal set; }

        public DateTime ModifiedDate { get; internal set; }

        public long PwnCount { get; internal set; }

        public string Description { get; internal set; }

        public string LogoType { get; internal set; }

        public bool IsVerified { get; internal set; }

        public bool IsFabricated { get; internal set; }

        public bool IsSensitive { get; internal set; }

        public bool IsRetired { get; internal set; }

        public bool IsSpamList { get; internal set; }

        public IEnumerable<string> DataClasses { get; internal set; }
    }
}