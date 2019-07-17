using System.Collections.Generic;
using System.Threading.Tasks;
using ByteDev.Hibp.Request;
using ByteDev.Hibp.Response;

namespace ByteDev.Hibp
{
    public interface IHibpClient
    {
        Task<IEnumerable<HibpBreachResponse>> GetAccountBreachesAsync(string emailAddress, HibpRequestOptions options = null);

        Task<IEnumerable<HibpBreachResponse>> GetBreachedSitesAsync(string domain = null);

        Task<HibpBreachResponse> GetBreachSiteByNameAsync(string breachName);

        Task<IEnumerable<string>> GetDataClassesAsync();

        Task<IEnumerable<HibpPasteResponse>> GetAccountPastesAsync(string emailAddress);
    }
}