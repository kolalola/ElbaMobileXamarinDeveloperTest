using ElbaMobileXamarinDeveloperTest.Core.Helpers;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ElbaMobileXamarinDeveloperTest.Core.Services.Rest
{
    public class RestService : IRestService
    {
        public async Task<T> GetOrDefaultAsync<T>(string uri)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(new Uri(uri));
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return content.DeserializeOrDefault<T>();
                    }

                    return default;
                }
            }
            catch
            {
                return default;
            }
        }
    }
}
