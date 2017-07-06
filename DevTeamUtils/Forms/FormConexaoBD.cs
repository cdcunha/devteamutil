using DevTeamUtils.Models;
using DevTeamUtils.Models.Enums;
using DevTeamUtils.Repository;
using System;
using System.Windows.Forms;

namespace DevTeamUtils.Forms
{
    public partial class FormConexaoBD : Form
    {
        private bool _IsInsert;
        public FormConexaoBD(ConexaoBD conexaoBD)
        {
            InitializeComponent();

            if (conexaoBD.Id == 0)
            {
                textBoxId.Text = conexaoBD.Id.ToString();
                comboBoxTipo.Focus();
                _IsInsert = true;
            }
            else
            {
                _IsInsert = false;
                textBoxId.Text = conexaoBD.Id.ToString();

                if (Enum.IsDefined(typeof(TipoConexaoDBEnum), conexaoBD.Tipo))
                    comboBoxTipo.SelectedIndex = (int)Enum.Parse(typeof(TipoConexaoDBEnum), conexaoBD.Tipo);
                else
                    comboBoxTipo.SelectedIndex = Array.IndexOf(Enum.GetValues(typeof(TipoConexaoDBEnum)), TipoConexaoDBEnum.Informix);

                textBoxNomeServidor.Text = conexaoBD.NomeServidor;
                textBoxIp.Text = conexaoBD.Ip;
                textBoxPorta.Text = conexaoBD.Porta.ToString();
                textBoxUsuario.Text = conexaoBD.Usuario;
                textBoxSenha.Text = conexaoBD.Senha;
            }
        }

        private void buttonSair_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void buttonGravar_Click(object sender, System.EventArgs e)
        {
            string inconsistencias = ValidaDados();
            if (inconsistencias == "")
            {
                ConexaoBD conexaoBD = new ConexaoBD();
                conexaoBD.Id = Convert.ToInt32(textBoxId.Text);
                conexaoBD.Tipo = ((TipoConexaoDBEnum)comboBoxTipo.SelectedIndex).ToString();
                conexaoBD.NomeServidor = textBoxNomeServidor.Text;
                conexaoBD.Ip = textBoxIp.Text;
                conexaoBD.Porta = Convert.ToInt32(textBoxPorta.Text);
                conexaoBD.Usuario = textBoxUsuario.Text;
                conexaoBD.Senha = textBoxSenha.Text;
                try
                {
                    if (_IsInsert)
                    {
                        if (ConexaoBDRepository.Insert(conexaoBD) > 0)
                        {
                            MessageBox.Show("Dados incluídos com sucesso.");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Os dados não foram incluídos.");
                        }
                    }
                    else
                    {
                        if (ConexaoBDRepository.Update(conexaoBD) > 0)
                        {
                            MessageBox.Show("Dados atualizados com sucesso.");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Os dados não foram atualizados.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show(inconsistencias);
            }
        }

        private string ValidaDados()
        {
            string retorno = "";

            if (!Enum.IsDefined(typeof(TipoConexaoDBEnum), comboBoxTipo.SelectedIndex))
                retorno += "Informe o tipo da Conexão do Banco de dados\n";
            if (textBoxNomeServidor.Text == string.Empty)
                retorno += "Informe o Nome do Servidor\n";
            if (textBoxIp.Text == string.Empty)
                retorno += "Informe o IP da conexão\n";
            if (textBoxPorta.Text == string.Empty)
                retorno += "Informe da Porta\n";
            /*if (textBoxUsuario.Text == string.Empty)
                retorno += "Informe o Usuário\n";*/
            return retorno;
        }

        private void comboBoxTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTipo.SelectedIndex == 0)
            {
                labelNome.Text = "Nome Servidor";
                labelIp.Text = "IP";
            }
            else
            {
                labelNome.Text = "Service Name";
                labelIp.Text = "Host";
            }
        }
    }
}
