using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ByteDev.Hibp.Request;
using ByteDev.Hibp.Response;

namespace ByteDev.Hibp
{
    public interface IHibpClient
    {
        /// <summary>
        /// Returns a list of all breaches a particular account has been involved in.
        /// </summary>
        /// <param name="emailAddress">Account email address to filter on.</param>
        /// <param name="options">Request options.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returned breaches.</returns>
        Task<IEnumerable<HibpBreachResponse>> GetAccountBreachesAsync(string emailAddress, HibpRequestOptions options = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Returns the details of each of breach in the system.
        /// (A "breach" is an instance of a system having been compromised by an attacker and the data disclosed).
        /// </summary>
        /// <param name="domain">Filters the result set to only breaches against the domain specified. It is possible that one site (and consequently domain), is compromised on multiple occasions.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returned breaches.</returns>
        Task<IEnumerable<HibpBreachResponse>> GetBreachedSitesAsync(string domain = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Retrieve a single breach by the breach "breachName".
        /// This is the stable value which may or may not be the same as the breach "title" (which can change).
        /// </summary>
        /// <param name="breachName">Breach breachName to filter on.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returned breach.</returns>
        Task<HibpBreachResponse> GetBreachSiteByNameAsync(string breachName, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Retrieve all data classes. A data class is an attribute of a record compromised in a breach.
        /// For example, many breaches expose data classes such as "Email addresses" and "Passwords".
        /// The values returned by this service are ordered alphabetically in a string array and will expand over time as
        /// new breaches expose previously unseen classes of data.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returned data classes.</returns>
        Task<IEnumerable<string>> GetDataClassesAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Retrieves all pastes for an account. Unlike searching for breaches, usernames that are not email addresses
        /// cannot be searched for.
        /// </summary>
        /// <param name="emailAddress">Account email address to filter on.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returned account's pastes.</returns>
        Task<IEnumerable<HibpPasteResponse>> GetAccountPastesAsync(string emailAddress, CancellationToken cancellationToken = default(CancellationToken));
    }
}