using System.Threading.Tasks;

namespace ByteDev.Hibp
{
    public interface IHibpClient
    {
        Task<HibpResponse> GetHasBeenPwnedAsync(string emailAddress);
        Task<HibpResponse> GetHasBeenPwnedAsync(string emailAddress, HibpRequestOptions options);
    }
}