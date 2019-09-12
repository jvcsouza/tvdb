using Newtonsoft.Json;
using System.Collections.Generic;

namespace apiSeries
{
    internal static class Utilidades
    {
        internal static T toObj<T>(string json) => JsonConvert.DeserializeObject<T>(json);

        internal static bool IsNull(this string s) => string.IsNullOrWhiteSpace(s);
    }
}
