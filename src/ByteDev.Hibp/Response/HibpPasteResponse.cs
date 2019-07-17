using System;

namespace ByteDev.Hibp.Response
{
    public class HibpPasteResponse
    {
        /// <summary>
        /// The ID of the paste as it was given at the source service. Combined with the "Source" attribute, this can be used to resolve the URL of the paste. 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The paste service the record was retrieved from. Current values are: Pastebin, Pastie, Slexy, Ghostbin, QuickLeak, JustPaste, AdHocUrl, PermanentOptOut, OptOut.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// The title of the paste as observed on the source site. This may be null and if so will be omitted from the response.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The date and time (precision to the second) that the paste was posted.
        /// This is taken directly from the paste site when this information is available but may be null if no date is published. 
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// The number of emails that were found when processing the paste.
        /// </summary>
        public int EmailCount { get; set; }
    }
}
