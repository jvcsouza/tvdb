using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace apiSeries
{
    public partial class Serie
    {
        internal static tvdb conn = new tvdb();

        public static async Task<List<Serie>> BuscarSeries(string nome)
        {
            var retorno = new List<Serie>();
            try
            {
                using (var wClient = await conn.preparaClient())
                {
                    tvdb.loaderStatus.Text = "Buscando...";
                    var json = await wClient.DownloadStringTaskAsync(tvdb.URL + "search/series?name=" + nome);
                    retorno = Utilidades.toObj<Series>(json).Data;
                    tvdb.loaderStatus.Text = "Finalizado.";
                }
            }
            catch (WebException e)
            {
                switch (e.Status)
                {
                    case WebExceptionStatus.NameResolutionFailure:
                    case WebExceptionStatus.ConnectFailure:
                    case WebExceptionStatus.Timeout:
                    case WebExceptionStatus.SendFailure:
                    case WebExceptionStatus.TrustFailure:
                        tvdb.loaderStatus.Text = "Sem conexão com a Internet.";
                        break;
                    case WebExceptionStatus.ProtocolError:
                        tvdb.loaderStatus.Text = "Série não encontrada.";
                        break;
                    case WebExceptionStatus.ReceiveFailure:
                    case WebExceptionStatus.PipelineFailure:
                    case WebExceptionStatus.RequestCanceled:
                    case WebExceptionStatus.ConnectionClosed:
                    case WebExceptionStatus.SecureChannelFailure:
                    case WebExceptionStatus.ServerProtocolViolation:
                    case WebExceptionStatus.KeepAliveFailure:
                    case WebExceptionStatus.Pending:
                    case WebExceptionStatus.ProxyNameResolutionFailure:
                    case WebExceptionStatus.UnknownError:
                    case WebExceptionStatus.MessageLengthLimitExceeded:
                    case WebExceptionStatus.CacheEntryNotFound:
                    case WebExceptionStatus.RequestProhibitedByCachePolicy:
                    case WebExceptionStatus.RequestProhibitedByProxy:
                        Console.WriteLine(e.Status);
                        break;
                    default:
                        break;
                }
            }
            return retorno;
        }

        public async Task<string> CarregarBanner()
        {

            tvdb.loaderStatus.Text = "Verificando Imagem...";
            if (string.IsNullOrWhiteSpace(UrlBanner))
            {
                tvdb.loaderStatus.Text = "Finalizado.";
                return tvdb.temp + "404.jpg";
            }

            tvdb.loaderStatus.Text = "Examinando Caminho...";
            var caminho = tvdb.temp + UrlBanner;
            if (File.Exists(caminho))
            {
                tvdb.loaderStatus.Text = "Finalizado.";
                return caminho;
            }
            await DownloadImage(caminho);
            return caminho;

        }

        internal async Task<List<Image>> CarregaImagens(string tipo, string subkey = null)
        {
            using (var wClient = await conn.preparaClient())
            {
                tvdb.loaderStatus.Text = $"Buscando {tipo}...";
                var url = $"series/{Codigo}/images/query?keyType={tipo}" + (subkey.IsNull() ? "" : $"&subKey={subkey}");
                var uri = new Uri(tvdb.URL + url);
                var json = await wClient.DownloadStringTaskAsync(uri);
                return Utilidades.toObj<Images>(json).Data;
            }
        }

        public async Task CarregarImgFanArt() =>
            _imgFanArt = await CarregaImagens("FanArt");

        public async Task CarregarImgTemporada() =>
            _imgTemporada = await CarregaImagens("Season");

        public async Task CarregarImgPoster() =>
            _imgPoster = await CarregaImagens("poster");

        public async Task CarregarImgSerie() =>
            _imgSeries = await CarregaImagens("series");

        public async Task<string> BaixarImagem(Image img)
        {
            var path = tvdb.temp + img.FileName;
            if (File.Exists(path))
                return path;

            await DownloadImage(path);
            return path;
        }

        public async Task<Image> CarregarImgTemporada(int temporada)
        {
            if (_imgTemporada == null) _imgTemporada = await CarregaImagens("Season");
            foreach (var i in _imgTemporada)
                if (i.SubKey == temporada.ToString())
                {
                    await DownloadImage(tvdb.temp + i.FileName);
                    return i;
                }
            return null;
        }

        public static async Task<Serie> BuscarSerie(string codigo)
        {
            tvdb.loaderStatus.Text = "Vasculhando Serie...";
            using (var wClient = await conn.preparaClient())
            {
                var url = tvdb.URL + $"series/{codigo}";
                var json = await wClient.DownloadStringTaskAsync(url);
                var obj = Utilidades.toObj<ASerieInfo>(json).Data;
                tvdb.loaderStatus.Text = "Finalizado.";
                return obj;
            }
        }

        internal async Task<string> DownloadImage(string urlDestino)
        {
            tvdb.loaderStatus.Text = "Examinando Diretorio...";
            string path = urlDestino.Substring(0, urlDestino.IndexOf('/'));
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            using (var wClient = await conn.preparaClient())
            {
                tvdb.loaderStatus.Text = "Baixando Imagem...";
                var urlImagens = "https://www.thetvdb.com/banners/";
                await wClient.DownloadFileTaskAsync(new Uri(urlImagens + UrlBanner), urlDestino);
                tvdb.loaderStatus.Text = "Finalizado.";
            }
            return urlDestino;
        }

        internal static void populaDicionario()
        {
            if (dic is null)
                dic = new Dictionary<string, string>();
            dic.Add("", "Não Informado.");
            dic.Add("Continuing", "Continua.");
            dic.Add("Ended", "Finalizada.");
            dic.Add("Upcoming", "Retornando.");
            dic.Add("Drama", "Drama");
            dic.Add("Thriller", "Suspense");
            dic.Add("Suspense", "Suspense");
            dic.Add("Comedy", "Comédia");
            dic.Add("Horror", "Terror");
            dic.Add("Action", "Ação");
            dic.Add("Crime", "Crime");
            dic.Add("Adventure", "Aventura");
            dic.Add("Fantasy", "Fantasia");
            dic.Add("Romance", "Romance");
            dic.Add("Talk Show", "Talk Show");
            dic.Add("Documentary", "Documentário");
            dic.Add("Reality", "Reality");
            dic.Add("Mystery", "Mistério");
            dic.Add("Animation", "Animação");
            dic.Add("Biography", "Biografia");
            dic.Add("Musical", "Musical");
            dic.Add("War", "Guerra");
            dic.Add("Science Fiction", "Ficção Científica");
            dic.Add("Science-Fiction", "Ficção Científica");
            dic.Add("Sci-fi", "Ficção Científica");
            dic.Add("Classic", "Clássico");
            dic.Add("Western", "Faroeste");
            dic.Add("News", "Notícia");
            dic.Add("Family", "Família");
            dic.Add("Travel", "Viagem");
            dic.Add("Soap", "Novela");
            dic.Add("Food", "Comida");
            dic.Add("Game Show", "Game Show");
            dic.Add("Children", "Infantil");
            dic.Add("Special Interest", "Special Interest");
            dic.Add("Mini-Series", "Mini-Series");
        }
    }
}
