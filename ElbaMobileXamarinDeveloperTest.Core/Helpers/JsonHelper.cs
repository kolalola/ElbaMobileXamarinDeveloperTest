using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

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
            catch (Exception ex)
            {
                return default;
            }
        }
    }
}
