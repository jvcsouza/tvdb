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
        private static Dictionary<string, string> dic =
            new Dictionary<string, string>();
        internal string _status;
        internal string _lancamento;

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
        public string Network { get; set; }

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
