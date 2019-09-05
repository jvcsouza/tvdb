using apiSeries;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teste2
{
    public partial class Temporadas : Form
    {
        Serie contexto = default(Serie);
        public Temporadas(Serie serie)
        {
            InitializeComponent();
            contexto = serie;
            IniciarComponentes();
        }

        private async Task IniciarComponentes()
        {
            Text = contexto.Nome + " - Temporadas.";
            contexto = await Serie.BuscarSerie(contexto.Codigo.ToString());
            lblID.Text = contexto.Codigo.ToString();
            lblNome.Text = contexto.Nome;
            lblLanc.Text = contexto.Lancamento;
            lblNota.Text = contexto.SiteRating.ToString();
            lblTemp.Text = contexto.Duracao;
            lblStatus.Text = contexto.Status;
            var genero = "";
            foreach (var gen in contexto.Genre)
                genero += gen + ", ";
            if (genero.Trim().EndsWith(",")) genero = genero.Trim().Remove(genero.Length - 2) + ".";
            lblGen.Text = genero;
        }
    }
}
