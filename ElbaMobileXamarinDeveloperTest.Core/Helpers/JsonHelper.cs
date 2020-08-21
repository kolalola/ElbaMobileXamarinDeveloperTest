using Newtonsoft.Json;

namespace ElbaMobileXamarinDeveloperTest.Core.Helpers
{
    public static class JsonHelper
    {
        public static T DeserializeOrDefault<T>(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return default;

            try 
            {
                return JsonConvert.DeserializeObject<T>(str);
            }
            catch
            {
                return default;
            }
        }
    }
}
