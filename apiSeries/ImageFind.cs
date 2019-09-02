using Newtonsoft.Json;

namespace apiSeries
{
    internal partial class FindImages
    {
        [JsonProperty("data")]
        public FindImages QtdImagens { get; set; }
    }

    internal partial class FindImages
    {
        [JsonProperty("fanart")]
        public long Fanart { get; set; }

        [JsonProperty("poster")]
        public long Poster { get; set; }

        [JsonProperty("season")]
        public long Season { get; set; }

        [JsonProperty("seasonwide")]
        public long Seasonwide { get; set; }

        [JsonProperty("series")]
        public long Series { get; set; }
    }
}
