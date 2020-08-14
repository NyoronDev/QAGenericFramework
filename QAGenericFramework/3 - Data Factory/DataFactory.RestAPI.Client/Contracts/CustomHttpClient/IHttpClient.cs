using System.Net.Http;
using System.Threading.Tasks;

namespace DataFactory.RestAPI.Client.Contracts.CustomHttpClient
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string url, string authorizationToken = null);

        Task<HttpResponseMessage> PostAsync<T>(string url, T item, string authorizationToken = null);

        Task<HttpResponseMessage> PutAsync<T>(string url, T item, string authorizationToken = null);

        Task<HttpResponseMessage> DeleteAsync(string url, string authorizationToken = null);
    }
}