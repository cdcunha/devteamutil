using System;
using System.Windows.Forms;

namespace DevTeamUtils.Forms
{
    public partial class FormMdiMain : Form
    {
        public FormMdiMain()
        {
            InitializeComponent();
        }

        private void agendaTelefônicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMdiAgendaTelefonica formMdiAgendaTelefonica = FormMdiAgendaTelefonica.Instance;
            // Set the Parent Form of the Child window.
            formMdiAgendaTelefonica.MdiParent = this;
            // Display the new form.
            formMdiAgendaTelefonica.Show();
        }

        private void dadosDeConexõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMdiConexaoInformix formMdiConexaoInformix = FormMdiConexaoInformix.Instance;
            // Set the Parent Form of the Child window.
            formMdiConexaoInformix.MdiParent = this;
            // Display the new form.
            formMdiConexaoInformix.Show();
        }

        private void acionamentosJosiltonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMdiAcionamentoAgfa formMdiAcionamentoAgfa = FormMdiAcionamentoAgfa.Instance;
            // Set the Parent Form of the Child window.
            formMdiAcionamentoAgfa.MdiParent = this;
            // Display the new form.
            formMdiAcionamentoAgfa.Show();
            formMdiAcionamentoAgfa.Focus();
        }

        private void cascataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void organizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void fecharTodosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form mdiChildForm in MdiChildren)
            {
                mdiChildForm.Close();
            }
        }
    }
}
