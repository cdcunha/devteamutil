using DevTeamUtils.Models;
using DevTeamUtils.Repository;
using System;
using System.Windows.Forms;

namespace DevTeamUtils.Forms
{
    public partial class FormConexaoInformix : Form
    {
        private bool _IsInsert;
        public FormConexaoInformix(ConexaoInformix conexaoInformix)
        {
            InitializeComponent();

            if (conexaoInformix.Id == 0)
            {
                textBoxId.Text = conexaoInformix.Id.ToString();
                textBoxNomeServidor.Focus();
                _IsInsert = true;
            }
            else
            {
                _IsInsert = false;
                textBoxId.Text = conexaoInformix.Id.ToString();
                textBoxNomeServidor.Text = conexaoInformix.NomeServidor;
                textBoxIp.Text = conexaoInformix.Ip;
                textBoxPorta.Text = conexaoInformix.Porta.ToString();
                textBoxUsuario.Text = conexaoInformix.Usuario;
                textBoxSenha.Text = conexaoInformix.Senha;
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
                ConexaoInformix conexaoInformix = new ConexaoInformix();
                conexaoInformix.Id = Convert.ToInt32(textBoxId.Text);
                conexaoInformix.NomeServidor = textBoxNomeServidor.Text;
                conexaoInformix.Ip = textBoxIp.Text;
                conexaoInformix.Porta = Convert.ToInt32(textBoxPorta.Text);
                conexaoInformix.Usuario = textBoxUsuario.Text;
                conexaoInformix.Senha = textBoxSenha.Text;
                try
                {
                    if (_IsInsert)
                    {
                        if (ConexaoInformixRepository.Insert(conexaoInformix) > 0)
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
                        if (ConexaoInformixRepository.Update(conexaoInformix) > 0)
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
    }
}
