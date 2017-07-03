using DevTeamUtils.Models;
using DevTeamUtils.Repository;
using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace DevTeamUtils.Forms
{
    public partial class FormMdiAgendaTelefonica : Form
    {
        private static FormMdiAgendaTelefonica _Instance = new FormMdiAgendaTelefonica();
        public static FormMdiAgendaTelefonica Instance { get { return _Instance; } }

        private AgendaTelefonicaRepository _AgendaTelefonicaRepository = new AgendaTelefonicaRepository();
        private FormMdiAgendaTelefonica()
        {
            InitializeComponent();

            CarregaDados();
        }

        private void CarregaDados()
        {
            dataGridView1.DataSource = AgendaTelefonicaRepository.GetDataTable<SQLiteConnection, SQLiteDataAdapter>("Select * from AgendaTelefonica order by nome");
        }

        private AgendaTelefonica GetDadosDoGrid()
        {
            try
            {
                int linha;
                linha = dataGridView1.CurrentRow.Index;
                AgendaTelefonica agendaTelefonica = new AgendaTelefonica();
                agendaTelefonica.Id = Convert.ToInt32(dataGridView1[0, linha].Value);
                agendaTelefonica.Nome = dataGridView1[1, linha].Value.ToString();
                agendaTelefonica.Telefone = dataGridView1[2, linha].Value.ToString();
                agendaTelefonica.Cargo = dataGridView1[3, linha].Value.ToString();
                agendaTelefonica.Local = dataGridView1[4, linha].Value.ToString();
                agendaTelefonica.Observacao = dataGridView1[5, linha].Value.ToString();
                return agendaTelefonica;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void buttonEditar_Click(object sender, EventArgs e)
        {
            try
            {
                FormAgendaTelefonica frm2 = new FormAgendaTelefonica(GetDadosDoGrid());
                frm2.ShowDialog();
                CarregaDados();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }

        private void buttonExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                int linha;
                linha = dataGridView1.CurrentRow.Index;
                DialogResult response = MessageBox.Show("Deseja deletar este registro?\n" + dataGridView1[1, linha].Value.ToString(), "Deletar Item",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (response == DialogResult.Yes)
                {
                    AgendaTelefonicaRepository.Delete(GetDadosDoGrid());
                    CarregaDados();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }

        private void buttonIncluir_Click(object sender, EventArgs e)
        {
            try
            {
                AgendaTelefonica agendaTelefonica = new AgendaTelefonica();
                agendaTelefonica.Id = 0;
                FormAgendaTelefonica frm2 = new FormAgendaTelefonica(agendaTelefonica);
                frm2.ShowDialog();
                CarregaDados();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }

        private void textBoxLocaliza_TextChanged(object sender, EventArgs e)
        {
            string qry = "Select * from AgendaTelefonica where Nome like '%" + textBoxLocaliza.Text + "%'";
            dataGridView1.DataSource = AgendaTelefonicaRepository.GetDataTable<SQLiteConnection, SQLiteDataAdapter>(qry);
        }

        private void buttonAtualizar_Click(object sender, EventArgs e)
        {
            CarregaDados();
        }
    }
}
