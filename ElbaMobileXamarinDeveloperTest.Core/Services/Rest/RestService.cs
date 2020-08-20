using ElbaMobileXamarinDeveloperTest.Core.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ElbaMobileXamarinDeveloperTest.Core.Services.Rest
{
    public class RestService : IRestService
    {
        public async Task<T> GetOrDefaultAsync<T>(string uri)
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
    }
}
