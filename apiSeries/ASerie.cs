using System.Collections.Generic;
using Newtonsoft.Json;

namespace apiSeries
{

    internal partial class Series
    {
        [JsonProperty("data")]
        public List<Serie> Data { get; set; }
    }

    public partial class Serie
    {
        internal static Dictionary<string, string> dic;// = new Dictionary<string, string>();
        internal string _status;
        internal string _lancamento;

        [JsonProperty("genre")]
        internal List<string> _genero;

        internal List<Image> _imgFanArt;
        internal List<Image> _imgTemporada;
        internal List<Image> _imgPoster;
        internal List<Image> _imgSeasonMaior;
        internal List<Image> _imgSeries;

        [JsonProperty("aliases")]
        public List<string> Aliases { get; set; }

        [JsonProperty("banner")]
        internal string UrlBanner { get; set; }

        [JsonProperty("firstAired")]
        public string Lancamento {
            get => string.IsNullOrWhiteSpace(_lancamento) ? "" :
            System.DateTime.Parse(_lancamento).ToShortDateString();
            set => _lancamento = value;
        }

        [JsonProperty("id")]
        public int Codigo { get; set; }

        [JsonProperty("network")]
        public string Canal { get; set; }

        [JsonProperty("overview")]
        public string Resumo { get; set; }

        [JsonProperty("seriesName")]
        public string Nome { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("status")]
        public string Status {
            get => dic[_status];
            set => _status = value;
        }

        [JsonProperty("added")]
        public string Added { get; set; }

        [JsonProperty("airsDayOfWeek")]
        public string AirsDayOfWeek { get; set; }

        [JsonProperty("airsTime")]
        public string AirsTime { get; set; }
        
        public List<string> Genre { get
            {
                var g = new List<string>();
                foreach (var i in _genero)
                    g.Add(dic[i]);
                return g;
            }
            internal set => _genero = value;
        }

        [JsonProperty("imdbId")]
        public string ImdbId { get; set; }

        [JsonProperty("lastUpdated")]
        public long LastUpdated { get; set; }

        [JsonProperty("networkId")]
        public string NetworkId { get; set; }
        
        [JsonProperty("rating")]
        public string Rating { get; set; }

        [JsonProperty("runtime")]
        public string Duracao { get; set; }

        [JsonProperty("seriesId")]
        public string SeriesId { get; set; }

        [JsonProperty("siteRating")]
        public long SiteRating { get; set; }

        [JsonProperty("siteRatingCount")]
        public long SiteRatingCount { get; set; }

        [JsonProperty("zap2itId")]
        public string Zap2ItId { get; set; }

        public List<Image> ImgFanArt { get => _imgFanArt; set => _imgFanArt = value; }
        public List<Image> ImgTemporada { get => _imgTemporada; set => _imgTemporada = value; }
        public List<Image> ImgPoster { get => _imgPoster; set => _imgPoster = value; }
        public List<Image> ImgSeasonMaior { get => _imgSeasonMaior; set => _imgSeasonMaior = value; }
        public List<Image> ImgSeries { get => _imgSeries; set => _imgSeries = value; }

        public override string ToString()
        {
            return Nome;
        }
    }
}
