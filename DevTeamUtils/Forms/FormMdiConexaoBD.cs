using DevTeamUtils.Models;
using DevTeamUtils.Repository;
using DevTeamUtils.Utils;
using IBM.Data.Informix;
using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace DevTeamUtils.Forms
{
    public partial class FormMdiConexaoBD : Form
    {
        private static FormMdiConexaoBD _Instance = new FormMdiConexaoBD();
        public static FormMdiConexaoBD Instance
        { get
            {
                if (_Instance == null)
                    _Instance = new FormMdiConexaoBD();
                return _Instance;
            }
        }
        private FormMdiConexaoBD()
        {
            InitializeComponent();

            CarregaDados();
        }
        
        private void CarregaDados()
        {
            dataGridView1.DataSource = ConexaoBDRepository.GetDataTable<SQLiteConnection, SQLiteDataAdapter>("Select * from conexaoBD order by NomeServidor");
        }



        private ConexaoBD GetDadosDoGrid()
        {
            try
            {
                int linha;
                linha = dataGridView1.CurrentRow.Index;
                ConexaoBD conexaoBD = new ConexaoBD();
                conexaoBD.Id = Convert.ToInt32(dataGridView1[0, linha].Value);
                conexaoBD.Tipo = dataGridView1[1, linha].Value.ToString();
                conexaoBD.NomeServidor = dataGridView1[2, linha].Value.ToString();
                conexaoBD.Ip = dataGridView1[3, linha].Value.ToString();
                conexaoBD.Porta = Convert.ToInt32(dataGridView1[4, linha].Value);
                conexaoBD.Usuario = dataGridView1[5, linha].Value.ToString();
                conexaoBD.Senha = dataGridView1[6, linha].Value.ToString();
                return conexaoBD;
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
                FormConexaoBD frm2 = new FormConexaoBD(GetDadosDoGrid());
                frm2.ShowDialog();
                CarregaDados();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void buttonExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                int linha;
                linha = dataGridView1.CurrentRow.Index;
                DialogResult response = MessageBox.Show($"Deseja deletar este registro?\n{dataGridView1[1, linha].Value.ToString()}", "Deletar Item",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (response == DialogResult.Yes)
                {
                    ConexaoBDRepository.Delete(GetDadosDoGrid());
                    CarregaDados();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void buttonIncluir_Click(object sender, EventArgs e)
        {
            try
            {
                ConexaoBD conexaoBD = new ConexaoBD();
                conexaoBD.Id = 0;
                FormConexaoBD frm2 = new FormConexaoBD(conexaoBD);
                frm2.ShowDialog();
                CarregaDados();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void textBoxLocaliza_TextChanged(object sender, EventArgs e)
        {
            string qry = $"Select * from conexaoBD where NomeServidor like '%{textBoxLocaliza.Text}%'";
            dataGridView1.DataSource = ConexaoBDRepository.GetDataTable<SQLiteConnection, SQLiteDataAdapter>(qry);
        }

        private void buttonAtualizar_Click(object sender, EventArgs e)
        {
            CarregaDados();
        }

        private void FormMdiconexaoBD_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_Instance != null)
            {
                _Instance.Dispose();
                _Instance = null;
            }
        }

        private void buttonTestar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            EnabledComponents(false);

            int linha;
            linha = dataGridView1.CurrentRow.Index;
            ConexaoBD conexaoBD = new ConexaoBD();
            conexaoBD.Id = Convert.ToInt32(dataGridView1[0, linha].Value);
            conexaoBD.Tipo = dataGridView1[1, linha].Value.ToString();
            conexaoBD.NomeServidor = dataGridView1[2, linha].Value.ToString();
            conexaoBD.Ip = dataGridView1[3, linha].Value.ToString();
            conexaoBD.Porta = Convert.ToInt32(dataGridView1[4, linha].Value);
            conexaoBD.Usuario = dataGridView1[5, linha].Value.ToString();
            conexaoBD.Senha = dataGridView1[6, linha].Value.ToString();
            
            string inconsistencias = ValidaDadosConexao(conexaoBD);
            if (inconsistencias == "")
            {
                string msg = Connection.TestConnection(ConexaoBDRepository.GetConnectionString(conexaoBD));
                MessageBox.Show(msg);
                ConexaoBDRepository.UpdateStatus(conexaoBD.Id, msg);
            }
            else
            {
                MessageBox.Show(inconsistencias);
            }

            CarregaDados();

            EnabledComponents(true);
            Cursor.Current = Cursors.Default;
        }

        private void EnabledComponents(bool enabled)
        {
            dataGridView1.Enabled = enabled;
            buttonAtualizar.Enabled = enabled;
            buttonEditar.Enabled = enabled;
            buttonExcluir.Enabled = enabled;
            buttonGerarIni.Enabled = enabled;
            buttonIncluir.Enabled = enabled;
            buttonTestar.Enabled = enabled;
            textBoxLocaliza.Enabled = enabled;
        }
        
        private string ValidaDadosConexao(ConexaoBD conexaoBD)
        {
            string retorno = "";

            if (conexaoBD.NomeServidor == string.Empty)
                retorno += "Informe o Nome do Servidor\n";
            if (conexaoBD.Ip == string.Empty)
                retorno += "Informe o IP da conexão\n";
            if (conexaoBD.Porta == 0)
                retorno += "Informe da Porta\n";
            if (conexaoBD.Usuario == string.Empty)
                retorno += "Informe o Usuário\n";
            if (conexaoBD.Senha == string.Empty)
                retorno += "Informe a Senha\n";
            return retorno;
        }

        private void buttonGerarIni_Click(object sender, EventArgs e)
        {
            EnabledComponents(false);
            int linha;
            linha = dataGridView1.CurrentRow.Index;
            ConexaoBD conexaoBD = new ConexaoBD();
            conexaoBD.Id = Convert.ToInt32(dataGridView1[0, linha].Value);
            conexaoBD.Tipo = dataGridView1[1, linha].Value.ToString();
            conexaoBD.NomeServidor = dataGridView1[2, linha].Value.ToString();
            conexaoBD.Ip = dataGridView1[3, linha].Value.ToString();
            conexaoBD.Porta = Convert.ToInt32(dataGridView1[4, linha].Value);
            conexaoBD.Usuario = dataGridView1[5, linha].Value.ToString();
            conexaoBD.Senha = dataGridView1[6, linha].Value.ToString();

            if (conexaoBD.Tipo == "Informix")
            {
                if (conexaoBD.Usuario.Trim() == "" || conexaoBD.Senha.Trim() == "")
                {
                    MessageBox.Show("É necessário que os campos Usuário e Senha estejam preenchidos");
                    return;
                }

                //MessageBox.Show($"Encrp={Utils.CryptographyHelper.EncryptPassword(conexaoBD.Senha)}}\nCerto=D6A38D9693CDC8CABFBF8A8287D9CEDE");
                //MessageBox.Show($"Decrp={Utils.CryptographyHelper.DecryptPassword("D6A38D9693CDC8CABFBF8A8287D9CEDE")}\nCerto=Amil2015@tes");


                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        //string[] files = System.IO.Directory.GetFiles(fbd.SelectedPath);
                        //MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
                        string fileNameAndPath = fbd.SelectedPath + $"\\sishosp_{conexaoBD.NomeServidor}.ini";
                        string section = "Connection";
                        if (Utils.IniFileHelper.WriteValue(section, "Username", conexaoBD.Usuario, fileNameAndPath))
                        {
                            string password = Utils.CryptographyHelper.EncryptPassword(conexaoBD.Senha);
                            if (Utils.IniFileHelper.WriteValue(section, "Password", password, fileNameAndPath))
                                MessageBox.Show($"{fileNameAndPath}\nArquivo gravado com sucesso!");
                            else
                                MessageBox.Show("Não cosegui gravar o arquivo");
                        }
                        else
                            MessageBox.Show("Não cosegui gravar o arquivo");
                    }
                }
            }
            else
            {
                throw new NotImplementedException("Geração de arquivo INI para Oracle ainda não implementado");
            }
            EnabledComponents(true);
        }
    }
}
