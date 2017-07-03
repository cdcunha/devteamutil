using DevTeamUtils.Models;
using DevTeamUtils.Repository;
using System;
using System.Windows.Forms;

namespace DevTeamUtils.Forms
{
    public partial class FormAgendaTelefonica : Form
    {
        private bool _IsInsert;
        public FormAgendaTelefonica(AgendaTelefonica agendaTelefonica)
        {
            InitializeComponent();

            if (agendaTelefonica.Id == 0)
            {
                textBoxId.Text = agendaTelefonica.Id.ToString();
                textBoxNome.Focus();
                _IsInsert = true;
            }
            else
            {
                _IsInsert = false;
                textBoxId.Text = agendaTelefonica.Id.ToString();
                textBoxNome.Text = agendaTelefonica.Nome;
                textBoxTelefone.Text = agendaTelefonica.Telefone;
                textBoxCargo.Text = agendaTelefonica.Cargo;
                textBoxLocal.Text = agendaTelefonica.Local;
                textBoxObservacao.Text = agendaTelefonica.Observacao;
            }
        }

        private void buttonSair_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void buttonGravar_Click(object sender, System.EventArgs e)
        {
            if (ValidaDados())
            {
                AgendaTelefonica agendaTelefonica = new AgendaTelefonica();
                agendaTelefonica.Id = Convert.ToInt32(textBoxId.Text);
                agendaTelefonica.Nome = textBoxNome.Text;
                agendaTelefonica.Telefone = textBoxTelefone.Text;
                agendaTelefonica.Cargo = textBoxCargo.Text;
                agendaTelefonica.Local = textBoxLocal.Text;
                agendaTelefonica.Observacao = textBoxObservacao.Text;
                try
                {
                    if (_IsInsert)
                    {
                        if (AgendaTelefonicaRepository.Insert(agendaTelefonica) > 0)
                        {
                            MessageBox.Show("Dados incluídos com sucesso.");
                        }
                        else
                        {
                            MessageBox.Show("Os dados não foram incluídos.");
                        }
                    }
                    else
                    {
                        if (AgendaTelefonicaRepository.Update(agendaTelefonica) > 0)
                        {
                            MessageBox.Show("Dados atualizados com sucesso.");
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
                MessageBox.Show("Dados inválidos.");
            }
        }

        private Boolean ValidaDados()
        {
            bool retorno = true;

            if (textBoxNome.Text == string.Empty)
                retorno = false;
            if (textBoxTelefone.Text == string.Empty)
                retorno = false;
            if (textBoxCargo.Text == string.Empty)
                retorno = false;
            if (textBoxLocal.Text == string.Empty)
                retorno = false;
            return retorno;
        }
    }
}
