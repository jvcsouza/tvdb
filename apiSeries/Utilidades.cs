using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiSeries
{
    internal static class Utilidades
    {
        internal static string traduz(string key, IDictionary<string, string> dc)
        {
            dc.TryGetValue(key, out string value);
            return value;
        }
        internal static T toObj<T>(string json) => JsonConvert.DeserializeObject<T>(json);

        internal static bool IsNull(this string s) => string.IsNullOrWhiteSpace(s);
    }
}
