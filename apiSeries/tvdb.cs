using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apiSeries
{
    public class tvdb
    {
        internal const string URL = "https://api.thetvdb.com/";
        public static string temp = Environment.GetLogicalDrives()[0] + "temp\\";
        internal static WebException originalError = null;
        public string messageError;
        public static int codeStatus { internal set; get; }
        private User user;
        internal static dynamic loaderStatus = new Label();

        public static object Loader
        {
            set
            {
                if (value is Label)
                    loaderStatus = (Label)value;

                if (value is ToolStripStatusLabel)
                    loaderStatus = (ToolStripStatusLabel)value;

                if (value is null)
                    throw new FormatException("Apenas label's podem ser usados para exibir status da solicitação!");

                loaderStatus.Text = "Pronto.";
            }
        }

        internal tvdb() => instanciarClasse();

        internal tvdb(Label loadStatus) : base()
        {
            Loader = loadStatus;
            loaderStatus.Text = "Pronto.";
        }

        internal tvdb(ToolStripStatusLabel loadStatus) : base()
        {
            Loader = loadStatus;
            loaderStatus.Text = "Pronto.";
        }

        private void instanciarClasse()
        {
            createDirectory();
            Serie.populaDicionario();
        }

        internal async Task<WebClient> preparaClient(WebClient client = null)
        {
            try
            {
                loaderStatus.Text = "Preparando Cliente...";
                if (client is null)
                    client = new WebClient();
                client.Encoding = Encoding.UTF8;
                client.Headers.Add("accept-language", "pt-BR,pt;q=0.9,en-US;q=0.8,en;q=0.7");
                client.Headers.Add("content-type", "application/json");
                client.Headers.Add("Accept", "application/json");
                client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Safari/537.36");
                if (user != null)
                {
                    if (user.Token.Length > 10)
                        client.Headers.Add("Authorization", user.Token);
                }
                else await getToken(client);
                loaderStatus.Text = "Finalizado.";

            }
            catch (WebException we)
            {
                await refreshToken(client);
            }
            return client;
        }

        private void createDirectory()
        {
            if (!Directory.Exists(temp))
                Directory.CreateDirectory(temp);
            if (!File.Exists(temp + "404.jpg"))
                Properties.Resources._404.Save(temp + "404.jpg");
        }

        internal async Task refreshToken(WebClient wc)
        {
            try
            {
                loaderStatus.Text = "Atualizando token de acesso...";
                var json = await wc.DownloadStringTaskAsync(URL + "refresh_token");
                user = Utilidades.toObj<User>(json);
                wc.Headers.Add("Authorization", user.Token);
                loaderStatus.Text = "Finalizado.";
            }
            catch (WebException e)
            {
                loaderStatus.Text = "Impossivel atualizar token de acesso.";
                messageError = e.Message;
                codeStatus = 401;
                originalError = e;
            }
        }

        internal async Task getToken(WebClient wc = null)
        {
            loaderStatus.Text = "Gerando token de acesso...";
            try
            {
                if (wc is null) wc = await preparaClient();
                var json = await wc.UploadStringTaskAsync(URL + "login", "POST", new User().ToString());
                user = Utilidades.toObj<User>(json);
                wc.Headers.Add("Authorization", user.Token);
            }
            catch (WebException e)
            {
                loaderStatus.Text = "Impossivel gerar token de acesso.";
                messageError = e.Message;
                codeStatus = 401;
                originalError = e;
            }
        }
    }



}
