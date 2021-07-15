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
        /// Get all breaches an account has been involved in.
        /// </summary>
        /// <param name="emailAddress">Email address for the account.</param>
        /// <param name="options">Search options.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation with a result of the breaches.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="emailAddress" /> is null or empty.</exception>
        /// <exception cref="T:ByteDev.Hibp.HibpClientException">Unhandled API error occured.</exception>
        Task<IEnumerable<HibpBreachResponse>> GetAccountBreachesAsync(string emailAddress, HibpRequestOptions options = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all breached sites in the system.
        /// </summary>
        /// <param name="domain">Optional. Filters the result set to only breaches against the domain specified. It is possible that one site (and consequently domain), is compromised on multiple occasions.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation with a result of the breaches.</returns>
        /// <exception cref="T:ByteDev.Hibp.HibpClientException">Unhandled API error occured.</exception>
        Task<IEnumerable<HibpBreachResponse>> GetBreachedSitesAsync(string domain = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get a single breached site by the breach name.
        /// </summary>
        /// <param name="breachName">Breach name. This is the stable value which may or may not be the same as the breach "title".</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation with a result of the breach.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="breachName" /> is null or empty.</exception>
        /// <exception cref="T:ByteDev.Hibp.HibpClientException">Unhandled API error occured.</exception>
        Task<HibpBreachResponse> GetBreachSiteByNameAsync(string breachName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all data classes in the system.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation with a result of data classes.</returns>
        /// <exception cref="T:ByteDev.Hibp.HibpClientException">Unhandled API error occured.</exception>
        Task<IEnumerable<string>> GetDataClassesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all pastes for an account.
        /// </summary>
        /// <param name="emailAddress">Email address for the account.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The task object representing the asynchronous operation with a result of pastes.</returns>
        /// <exception cref="T:System.ArgumentException"><paramref name="emailAddress" /> is null or empty.</exception>
        /// <exception cref="T:ByteDev.Hibp.HibpClientException">Unhandled API error occured.</exception>
        Task<IEnumerable<HibpPasteResponse>> GetAccountPastesAsync(string emailAddress, CancellationToken cancellationToken = default);
    }
}