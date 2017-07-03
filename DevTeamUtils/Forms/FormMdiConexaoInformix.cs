using DevTeamUtils.Models;
using DevTeamUtils.Repository;
using IBM.Data.Informix;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevTeamUtils.Forms
{
    public partial class FormMdiConexaoInformix : Form
    {
        private static FormMdiConexaoInformix _Instance = new FormMdiConexaoInformix();
        public static FormMdiConexaoInformix Instance
        { get
            {
                if (_Instance == null)
                    _Instance = new FormMdiConexaoInformix();
                return _Instance;
            }
        }
        private FormMdiConexaoInformix()
        {
            InitializeComponent();

            CarregaDados();
        }
        
        private void CarregaDados()
        {
            dataGridView1.DataSource = ConexaoInformixRepository.GetDataTable<SQLiteConnection, SQLiteDataAdapter>("Select * from ConexaoInformix order by NomeServidor");
        }



        private ConexaoInformix GetDadosDoGrid()
        {
            try
            {
                int linha;
                linha = dataGridView1.CurrentRow.Index;
                ConexaoInformix conexaoInformix = new ConexaoInformix();
                conexaoInformix.Id = Convert.ToInt32(dataGridView1[0, linha].Value);
                conexaoInformix.NomeServidor = dataGridView1[1, linha].Value.ToString();
                conexaoInformix.Ip = dataGridView1[2, linha].Value.ToString();
                conexaoInformix.Porta = Convert.ToInt32(dataGridView1[3, linha].Value);
                conexaoInformix.Usuario = dataGridView1[4, linha].Value.ToString();
                conexaoInformix.Senha = dataGridView1[5, linha].Value.ToString();
                return conexaoInformix;
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
                FormConexaoInformix frm2 = new FormConexaoInformix(GetDadosDoGrid());
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
                    ConexaoInformixRepository.Delete(GetDadosDoGrid());
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
                ConexaoInformix conexaoInformix = new ConexaoInformix();
                conexaoInformix.Id = 0;
                FormConexaoInformix frm2 = new FormConexaoInformix(conexaoInformix);
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
            string qry = "Select * from ConexaoInformix where NomeServidor like '%" + textBoxLocaliza.Text + "%'";
            dataGridView1.DataSource = ConexaoInformixRepository.GetDataTable<SQLiteConnection, SQLiteDataAdapter>(qry);
        }

        private void buttonAtualizar_Click(object sender, EventArgs e)
        {
            CarregaDados();
        }

        private void FormMdiConexaoInformix_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_Instance != null)
            {
                _Instance.Dispose();
                _Instance = null;
            }
        }

        private void buttonTestar_Click(object sender, EventArgs e)
        {
            int linha;
            linha = dataGridView1.CurrentRow.Index;
            ConexaoInformix conexaoInformix = new ConexaoInformix();
            conexaoInformix.Id = Convert.ToInt32(dataGridView1[0, linha].Value);
            conexaoInformix.NomeServidor = dataGridView1[1, linha].Value.ToString();
            conexaoInformix.Ip = dataGridView1[2, linha].Value.ToString();
            conexaoInformix.Porta = Convert.ToInt32(dataGridView1[3, linha].Value);
            conexaoInformix.Usuario = dataGridView1[4, linha].Value.ToString();
            conexaoInformix.Senha = dataGridView1[5, linha].Value.ToString();

            string inconsistencias = ValidaDadosConexao(conexaoInformix);
            if (inconsistencias == "")
            {
                string database = "wpdhosp";

                string ConnectionString = "Host=" + conexaoInformix.Ip + "; " +
                     "Service=" + conexaoInformix.Porta + "; " +
                     "Server=" + conexaoInformix.NomeServidor + "; " +
                     "Database=" + database + "; " +
                     "User Id=" + conexaoInformix.Usuario + "; " +
                     "Password=" + conexaoInformix.Senha + "; ";
                //Can add other DB parameters here like DELIMIDENT, DB_LOCALE etc
                //Full list in Client SDK's .Net Provider Reference Guide p 3:13
                IfxConnection conn = new IfxConnection();
                conn.ConnectionString = ConnectionString;
                try
                {
                    conn.Open();
                    MessageBox.Show("Conectado com sucesso! [" + conexaoInformix.NomeServidor + "]");
                }
                catch (IfxException ex)
                {
                    MessageBox.Show("Erro ao tentar conectar [" + conexaoInformix.NomeServidor + "]: \n" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show(inconsistencias);
            }
        }

        private string ValidaDadosConexao(ConexaoInformix conexaoInformix)
        {
            string retorno = "";

            if (conexaoInformix.NomeServidor == string.Empty)
                retorno += "Informe o Nome do Servidor\n";
            if (conexaoInformix.Ip == string.Empty)
                retorno += "Informe o IP da conexão\n";
            if (conexaoInformix.Porta == 0)
                retorno += "Informe da Porta\n";
            if (conexaoInformix.Usuario == string.Empty)
                retorno += "Informe o Usuário\n";
            if (conexaoInformix.Senha == string.Empty)
                retorno += "Informe a Senha\n";
            return retorno;
        }
    }
}
