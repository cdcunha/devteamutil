namespace DevTeamUtils.Forms
{
    partial class FormMdiMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.agendaTelefônicaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dadosDeConexõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acionamentosAgfaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.janelasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cascataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.organizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.fecharTodosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.agendaTelefônicaToolStripMenuItem,
            this.dadosDeConexõesToolStripMenuItem,
            this.acionamentosAgfaToolStripMenuItem,
            this.janelasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(634, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // agendaTelefônicaToolStripMenuItem
            // 
            this.agendaTelefônicaToolStripMenuItem.Name = "agendaTelefônicaToolStripMenuItem";
            this.agendaTelefônicaToolStripMenuItem.Size = new System.Drawing.Size(118, 20);
            this.agendaTelefônicaToolStripMenuItem.Text = "Agenda Telefônica";
            this.agendaTelefônicaToolStripMenuItem.Click += new System.EventHandler(this.agendaTelefônicaToolStripMenuItem_Click);
            // 
            // dadosDeConexõesToolStripMenuItem
            // 
            this.dadosDeConexõesToolStripMenuItem.Name = "dadosDeConexõesToolStripMenuItem";
            this.dadosDeConexõesToolStripMenuItem.Size = new System.Drawing.Size(117, 20);
            this.dadosDeConexõesToolStripMenuItem.Text = "Conexões Informix";
            this.dadosDeConexõesToolStripMenuItem.Click += new System.EventHandler(this.dadosDeConexõesToolStripMenuItem_Click);
            // 
            // acionamentosAgfaToolStripMenuItem
            // 
            this.acionamentosAgfaToolStripMenuItem.Enabled = false;
            this.acionamentosAgfaToolStripMenuItem.Name = "acionamentosAgfaToolStripMenuItem";
            this.acionamentosAgfaToolStripMenuItem.Size = new System.Drawing.Size(124, 20);
            this.acionamentosAgfaToolStripMenuItem.Text = "Acionamentos Agfa";
            this.acionamentosAgfaToolStripMenuItem.Click += new System.EventHandler(this.acionamentosJosiltonToolStripMenuItem_Click);
            // 
            // janelasToolStripMenuItem
            // 
            this.janelasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cascataToolStripMenuItem,
            this.horizontalToolStripMenuItem,
            this.verticalToolStripMenuItem,
            this.organizarToolStripMenuItem,
            this.toolStripMenuItem2,
            this.fecharTodosToolStripMenuItem});
            this.janelasToolStripMenuItem.Name = "janelasToolStripMenuItem";
            this.janelasToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.janelasToolStripMenuItem.Text = "Janelas";
            // 
            // cascataToolStripMenuItem
            // 
            this.cascataToolStripMenuItem.Name = "cascataToolStripMenuItem";
            this.cascataToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.cascataToolStripMenuItem.Text = "Cascata";
            this.cascataToolStripMenuItem.Click += new System.EventHandler(this.cascataToolStripMenuItem_Click);
            // 
            // horizontalToolStripMenuItem
            // 
            this.horizontalToolStripMenuItem.Name = "horizontalToolStripMenuItem";
            this.horizontalToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.horizontalToolStripMenuItem.Text = "Horizontal";
            this.horizontalToolStripMenuItem.Click += new System.EventHandler(this.horizontalToolStripMenuItem_Click);
            // 
            // verticalToolStripMenuItem
            // 
            this.verticalToolStripMenuItem.Name = "verticalToolStripMenuItem";
            this.verticalToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.verticalToolStripMenuItem.Text = "Vertical";
            this.verticalToolStripMenuItem.Click += new System.EventHandler(this.verticalToolStripMenuItem_Click);
            // 
            // organizarToolStripMenuItem
            // 
            this.organizarToolStripMenuItem.Name = "organizarToolStripMenuItem";
            this.organizarToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.organizarToolStripMenuItem.Text = "Organizar";
            this.organizarToolStripMenuItem.Click += new System.EventHandler(this.organizarToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(142, 22);
            this.toolStripMenuItem2.Text = "--";
            // 
            // fecharTodosToolStripMenuItem
            // 
            this.fecharTodosToolStripMenuItem.Name = "fecharTodosToolStripMenuItem";
            this.fecharTodosToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.fecharTodosToolStripMenuItem.Text = "Fechar todos";
            this.fecharTodosToolStripMenuItem.Click += new System.EventHandler(this.fecharTodosToolStripMenuItem_Click);
            // 
            // FormMdiMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 265);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMdiMain";
            this.Text = "Dev Team Utils";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem agendaTelefônicaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dadosDeConexõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acionamentosAgfaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem janelasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cascataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem horizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem organizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem fecharTodosToolStripMenuItem;
    }
}

