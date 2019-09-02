using apiSeries;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Teste2
{
    public partial class Form1 : Form
    {
        tvdb conexao;
        List<Serie> listSerie;

        public Form1()
        {
            InitializeComponent();
            conexao = new tvdb(lblStatus);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            if (tvdb.codeStatus == 401)
                conexao.refreshToken();

            if (txtSerie.Text.Trim().Length < 3)
                return;
            
            listSerie = await Serie.BuscarSerie(txtSerie.Text);
            lsbSeries.Items.Clear();

            foreach (var serie in listSerie)
                lsbSeries.Items.Add(serie);
        }

        private async void lsbSeries_SelectedValueChanged(object sender, EventArgs e)
        {
            limparForm();
            var serie = (Serie)((ListBox)sender).SelectedItem;
            //serie.Imagens.FanArt
            txtResumo.Text = serie.Resumo;
            lblNome.Text = serie.Nome;
            lblLan.Text = serie.Lancamento;
            lblSt.Text = serie.Status;
            lblCodigo.Text = serie.Codigo.ToString();
            pictureBox1.ImageLocation = await serie.CarregarBanner();
            pictureBox1.Show();
        }

        private void limparForm()
        {
            txtResumo.Text = "";
            pictureBox1.Hide();
        }

        private void lblCodigo_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lblCodigo.Text);
        }
    }
}
