using System;
using System.Collections.Generic;

namespace ByteDev.Hibp.Response
{
    /// <summary>
    /// Describes a single breach incident.
    /// See https://haveibeenpwned.com/API/v2#BreachModel for more information.
    /// </summary>
    public class HibpBreachResponse
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public string Domain { get; set; }

        public DateTime BreachDate { get; set; }

        public DateTime AddedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public long PwnCount { get; set; }

        public string Description { get; set; }

        public string LogoType { get; set; }

        public bool IsVerified { get; set; }

        public bool IsFabricated { get; set; }

        public bool IsSensitive { get; set; }

        public bool IsRetired { get; set; }

        public bool IsSpamList { get; set; }

        public IEnumerable<string> DataClasses { get; set; }
    }
}