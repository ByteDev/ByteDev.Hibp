using System.Threading.Tasks;
using ByteDev.Hibp.Response;

namespace ByteDev.Hibp
{
    public interface IHibpClient
    {
        Task<HibpResponse> GetHasBeenPwnedAsync(string emailAddress, HibpRequestOptions options = null);

        Task<HibpResponse> GetBreachedSitesAsync(string domain = null);
    }
}