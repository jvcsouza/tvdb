using apiSeries;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Teste2
{
    public partial class Form1 : Form
    {
        List<Serie> listSerie;
        Serie contexto = default(Serie);

        public Form1()
        {
            InitializeComponent();
            // conexao = new tvdb(lblStatus);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tvdb.Loader = lblStatus;
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtSerie.Text.Trim().Length < 3)
                return;

           // listSerie.Add(await Serie.BuscarSerie(txtSerie.Text));
            listSerie = await Serie.BuscarSeries(txtSerie.Text);
            lsbSeries.Items.Clear();

            foreach (var serie in listSerie)
                lsbSeries.Items.Add(serie);
        }

        private async void lsbSeries_SelectedValueChanged(object sender, EventArgs e)
        {
            limparForm();
            var serie = (Serie)((ListBox)sender).SelectedItem;
            contexto = serie;
            txtResumo.Text = serie.Resumo;
            lblNome.Text = serie.Nome;
            lblLan.Text = serie.Lancamento;
            lblSt.Text = serie.Status;
            lblCodigo.Text = serie.Codigo.ToString();
            btnBuscaTemp.Visible = true;
            pictureBox1.ImageLocation = await serie.CarregarBanner();
            pictureBox1.Show();
        }

        private void limparForm()
        {
            btnBuscaTemp.Visible = false;
            txtResumo.Text = "";
            pictureBox1.Hide();
        }

        private void lblCodigo_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lblCodigo.Text);
        }

        private void btnBuscaTemp_Click(object sender, EventArgs e)
        {
            Temporadas temp = new Temporadas(contexto);
            temp.ShowDialog();
        }
    }
}
