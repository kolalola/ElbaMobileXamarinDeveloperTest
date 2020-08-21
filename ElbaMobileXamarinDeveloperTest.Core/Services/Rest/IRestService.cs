using System.Threading.Tasks;

namespace ElbaMobileXamarinDeveloperTest.Core.Services.Rest
{
    public interface IRestService
    {
        Task<T> GetOrDefaultAsync<T>(string uri);
    }
}
