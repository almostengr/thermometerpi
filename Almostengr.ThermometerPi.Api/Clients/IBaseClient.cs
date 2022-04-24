using System.Net.Http;
using System.Threading.Tasks;

namespace Almostengr.ThermometerPi.Api.Clients
{
    public interface IBaseClient
    {
        Task<T> HttpGetAsync<T>(HttpClient httpClient, string route);
    }
}