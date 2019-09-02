using System.Collections.Generic;
using Newtonsoft.Json;

namespace apiSeries
{
    internal class Images
    {
        [JsonProperty("data")]
        internal List<Image> Data { get; set; }

        [JsonProperty("errors")]
        internal Errors Errors { get; set; }
    }

    public class Image
    {
        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("keyType")]
        public string KeyType { get; set; }

        [JsonProperty("languageId")]
        public long LanguageId { get; set; }

        [JsonProperty("ratingsInfo")]
        public RateInfo RateInfo { get; set; }

        [JsonProperty("resolution")]
        public string Resolucao { get; set; }

        [JsonProperty("subKey")]
        public string SubKey { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }
    }

    public partial class RateInfo
    {
        [JsonProperty("average")]
        public long Media { get; set; }

        [JsonProperty("count")]
        public long Votos { get; set; }
    }

    internal partial class Errors
    {
        [JsonProperty("invalidFilters")]
        public List<string> InvalidFilters { get; set; }

        [JsonProperty("invalidLanguage")]
        public string InvalidLanguage { get; set; }

        [JsonProperty("invalidQueryParams")]
        public List<string> InvalidQueryParams { get; set; }
    }
}

