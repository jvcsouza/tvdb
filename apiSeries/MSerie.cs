using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace apiSeries
{
    public partial class Serie
    {
        public static async Task<List<Serie>> BuscarSerie(string nome)
        {
            var retorno = new List<Serie>();
            try
            {
                tvdb.loaderStatus.Text = "Buscando...";
                var json = await tvdb.wClient.DownloadStringTaskAsync(tvdb.URL + "search/series?name=" + nome);
                retorno = Utilidades.toObj<Series>(json).Data;
                tvdb.loaderStatus.Text = "Finalizado.";
            }
            catch (WebException e)
            {
                tvdb.loaderStatus.Text = "Sem conexão com a Internet.";
                //tvdb.messageError = e.Message;
                //riginalError = e;
                //codeStatus = e.Status.ToString();
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
            tvdb.loaderStatus.Text = $"Buscando {tipo}...";
            var url = $"series/{Codigo}/images/query?keyType={tipo}" + (subkey.IsNull() ? "" : $"&subKey={subkey}");
            var uri = new Uri(tvdb.URL + url);
            var json = await tvdb.wClient.DownloadStringTaskAsync(uri);
            return Utilidades.toObj<Images>(json).Data;
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

        internal async Task<string> DownloadImage(string urlDestino)
        {
            tvdb.loaderStatus.Text = "Examinando Diretorio...";
            string path = urlDestino.Substring(0, urlDestino.IndexOf('/'));
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            tvdb.loaderStatus.Text = "Baixando Imagem...";
            var urlImagens = "https://www.thetvdb.com/banners/";
            await tvdb.wClient.DownloadFileTaskAsync(new Uri(urlImagens + UrlBanner), urlDestino);
            tvdb.loaderStatus.Text = "Finalizado.";
            return urlDestino;
        }

        internal static void populaDicionario()
        {
            dic.Add("", "Não Informado.");
            dic.Add("Continuing", "Continua.");
            dic.Add("Ended", "Finalizada.");
        }
    }
}
