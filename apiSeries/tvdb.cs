using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace apiSeries
{
    public class tvdb
    {
        internal const string URL = "https://api.thetvdb.com/";
        public static string temp = (Environment.GetLogicalDrives()[0]) + "temp\\";
        internal static WebClient wClient;
        public string result;
        internal static WebException originalError = null;
        public string messageError;
        public static int codeStatus { internal set; get; }
        private User user;
        internal static dynamic loaderStatus = new Label();

        public object Loader
        {
            set
            {
                if (value is Label)
                    loaderStatus = (Label)value;

                if (value is ToolStripStatusLabel)
                    loaderStatus = (ToolStripStatusLabel)value;

                if (value is null)
                    throw new FormatException("Apenas label's podem ser usados para exibir status da solicitação!");
            }
        }

        public tvdb() => instanciarClasse();

        public tvdb(Label loadStatus)
        {
            instanciarClasse();
            Loader = loadStatus;
            loaderStatus.Text = "Pronto.";
        }

        public tvdb(ToolStripStatusLabel loadStatus)
        {
            instanciarClasse();
            Loader = loadStatus;
            loaderStatus.Text = "Pronto.";
        }

        private void instanciarClasse()
        {
            getToken();
            createDirectory();
            Serie.populaDicionario();
        }

        private WebClient preparaClient(WebClient client = null)
        {
            if (client == null)
                client = new WebClient();
            client.Encoding = Encoding.UTF8;
            client.Headers.Add("accept-language", "pt-BR,pt;q=0.9,en-US;q=0.8,en;q=0.7");
            client.Headers.Add("content-type", "application/json");
            client.Headers.Add("Accept", "application/json");
            client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Safari/537.36");
            return client;
        }

        private void createDirectory()
        {
            if (!Directory.Exists(temp))
                Directory.CreateDirectory(temp);
            if (!File.Exists(temp + "404.jpg"))
                Properties.Resources._404.Save(temp + "404.jpg");
        }

        public async void refreshToken()
        {
            try
            {
                loaderStatus.Text = "Atualizando token de acesso...";
                var json = await wClient.DownloadStringTaskAsync(URL + "refresh_token");
                user = Utilidades.toObj<User>(json);
                wClient.Headers.Add("Authorization", user.Token);
                loaderStatus.Text = "Finalizado.";
            }
            catch (WebException e)
            {
                loaderStatus.Text = "Impossivel atualizar token de acesso.";
                messageError = e.Message;
                codeStatus = 401;
            }
        }

        internal async void getToken()
        {
            try
            {
                wClient = preparaClient(wClient);
                var json = await wClient.UploadStringTaskAsync(URL + "login", "POST", new User().ToString());
                user = Utilidades.toObj<User>(json);
                wClient.Headers.Add("Authorization", user.Token);
            }
            catch (WebException e)
            {
                loaderStatus.Text = "Impossivel gerar token de acesso.";
                messageError = e.Message;
                codeStatus = 401;
            }
        }
    }



}
