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
            string inconsistencias = ValidaDados();
            if (inconsistencias == "")
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
                            this.Close();
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

            if (textBoxNome.Text == string.Empty)
                retorno += "Informe o Nome\n";
            if (textBoxTelefone.Text == string.Empty)
                retorno += "Informe o Telefone\n";
            if (textBoxCargo.Text == string.Empty)
                retorno += "Informe o Cargo\n";
            if (textBoxLocal.Text == string.Empty)
                retorno += "Informe o Local\n";
            return retorno;
        }
    }
}
