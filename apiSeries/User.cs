using Newtonsoft.Json;

namespace apiSeries
{
    internal class User
    {
        internal static string apikey = "GXR3Q3NTMOYBOLWZ";
        internal static string userkey = "U1R5EGZ3K877ZJMI";
        internal static string username = "victor_jaunnol";

        [JsonProperty("token")]
        private string token = "";

        public string Token { get => "Bearer " + token; }

        public override string ToString()
        {
            return @"{" +
                $"\"{nameof(apikey)}\": \"{apikey}\"," +
                $"\"{nameof(userkey)}\" : \"{userkey}\"," +
                $"\"{nameof(username)}\": \"{username}\"" +
                    "}";
        }
    }

}
