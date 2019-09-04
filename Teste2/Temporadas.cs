using apiSeries;
using System;
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
        }

        private async void Temporadas_Load(object sender, EventArgs e)
        {
            Text = contexto.Nome + " - Temporadas.";
            contexto = await Serie.BuscarSerie(contexto.Codigo.ToString());
            lblID.Text = contexto.Codigo.ToString();
            lblNome.Text = contexto.Nome;
            lblLanc.Text = contexto.Lancamento;
            lblNota.Text = contexto.SiteRating.ToString();
            lblTemp.Text = contexto.Runtime;
            lblStatus.Text = contexto.Status;
            var genero = "";
            foreach (var gen in contexto.Genre)
                genero += gen + ", ";
            if (genero.Trim().EndsWith(",")) genero = genero.Trim().Remove(genero.Length - 2).Trim() + ".";
            lblGen.Text = genero;
        }
    }
}
