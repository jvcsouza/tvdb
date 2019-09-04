using Newtonsoft.Json;

namespace apiSeries
{
    internal class ASerieInfo
    {
        [JsonProperty("data")]
        public Serie Data { get; set; }
    }
}