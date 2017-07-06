namespace DevTeamUtils.Forms
{
    partial class FormMdiConexaoBD
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMdiConexaoBD));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonGerarIni = new System.Windows.Forms.Button();
            this.textBoxLocaliza = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonTestar = new System.Windows.Forms.Button();
            this.buttonAtualizar = new System.Windows.Forms.Button();
            this.buttonIncluir = new System.Windows.Forms.Button();
            this.buttonExcluir = new System.Windows.Forms.Button();
            this.buttonEditar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(850, 250);
            this.dataGridView1.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonGerarIni);
            this.panel1.Controls.Add(this.buttonTestar);
            this.panel1.Controls.Add(this.buttonAtualizar);
            this.panel1.Controls.Add(this.textBoxLocaliza);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonIncluir);
            this.panel1.Controls.Add(this.buttonExcluir);
            this.panel1.Controls.Add(this.buttonEditar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(908, 44);
            this.panel1.TabIndex = 15;
            // 
            // buttonGerarIni
            // 
            this.buttonGerarIni.Image = global::DevTeamUtils.Properties.Resources.btn_writeini;
            this.buttonGerarIni.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGerarIni.Location = new System.Drawing.Point(426, 11);
            this.buttonGerarIni.Name = "buttonGerarIni";
            this.buttonGerarIni.Size = new System.Drawing.Size(75, 23);
            this.buttonGerarIni.TabIndex = 15;
            this.buttonGerarIni.Text = "     Salvar Ini";
            this.toolTip1.SetToolTip(this.buttonGerarIni, "Clique aqui para gerar arquivo INI");
            this.buttonGerarIni.UseVisualStyleBackColor = true;
            this.buttonGerarIni.Click += new System.EventHandler(this.buttonGerarIni_Click);
            // 
            // textBoxLocaliza
            // 
            this.textBoxLocaliza.Location = new System.Drawing.Point(559, 14);
            this.textBoxLocaliza.Name = "textBoxLocaliza";
            this.textBoxLocaliza.Size = new System.Drawing.Size(231, 20);
            this.textBoxLocaliza.TabIndex = 16;
            this.textBoxLocaliza.TextChanged += new System.EventHandler(this.textBoxLocaliza_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(507, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Localizar";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 44);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(908, 488);
            this.panel2.TabIndex = 16;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 336);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(908, 196);
            this.panel3.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkRed;
            this.label3.Location = new System.Drawing.Point(5, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(691, 153);
            this.label3.TabIndex = 1;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "A T E N Ç Ã O";
            // 
            // buttonTestar
            // 
            this.buttonTestar.Image = global::DevTeamUtils.Properties.Resources.btn_testdb;
            this.buttonTestar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTestar.Location = new System.Drawing.Point(345, 11);
            this.buttonTestar.Name = "buttonTestar";
            this.buttonTestar.Size = new System.Drawing.Size(75, 23);
            this.buttonTestar.TabIndex = 14;
            this.buttonTestar.Text = "Testar";
            this.toolTip1.SetToolTip(this.buttonTestar, "Testa conexão com o banco de dados");
            this.buttonTestar.UseVisualStyleBackColor = true;
            this.buttonTestar.Click += new System.EventHandler(this.buttonTestar_Click);
            // 
            // buttonAtualizar
            // 
            this.buttonAtualizar.Image = global::DevTeamUtils.Properties.Resources.btn_update;
            this.buttonAtualizar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAtualizar.Location = new System.Drawing.Point(264, 11);
            this.buttonAtualizar.Name = "buttonAtualizar";
            this.buttonAtualizar.Size = new System.Drawing.Size(75, 23);
            this.buttonAtualizar.TabIndex = 13;
            this.buttonAtualizar.Text = "  Atualizar";
            this.buttonAtualizar.UseVisualStyleBackColor = true;
            this.buttonAtualizar.Click += new System.EventHandler(this.buttonAtualizar_Click);
            // 
            // buttonIncluir
            // 
            this.buttonIncluir.Image = global::DevTeamUtils.Properties.Resources.btn_new;
            this.buttonIncluir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonIncluir.Location = new System.Drawing.Point(21, 12);
            this.buttonIncluir.Name = "buttonIncluir";
            this.buttonIncluir.Size = new System.Drawing.Size(75, 23);
            this.buttonIncluir.TabIndex = 10;
            this.buttonIncluir.Text = "Novo";
            this.buttonIncluir.UseVisualStyleBackColor = true;
            this.buttonIncluir.Click += new System.EventHandler(this.buttonIncluir_Click);
            // 
            // buttonExcluir
            // 
            this.buttonExcluir.Image = global::DevTeamUtils.Properties.Resources.btn_delete;
            this.buttonExcluir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExcluir.Location = new System.Drawing.Point(183, 12);
            this.buttonExcluir.Name = "buttonExcluir";
            this.buttonExcluir.Size = new System.Drawing.Size(75, 23);
            this.buttonExcluir.TabIndex = 12;
            this.buttonExcluir.Text = "Excluir";
            this.buttonExcluir.UseVisualStyleBackColor = true;
            this.buttonExcluir.Click += new System.EventHandler(this.buttonExcluir_Click);
            // 
            // buttonEditar
            // 
            this.buttonEditar.Image = ((System.Drawing.Image)(resources.GetObject("buttonEditar.Image")));
            this.buttonEditar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEditar.Location = new System.Drawing.Point(102, 12);
            this.buttonEditar.Name = "buttonEditar";
            this.buttonEditar.Size = new System.Drawing.Size(75, 23);
            this.buttonEditar.TabIndex = 11;
            this.buttonEditar.Text = "Editar";
            this.buttonEditar.UseVisualStyleBackColor = true;
            this.buttonEditar.Click += new System.EventHandler(this.buttonEditar_Click);
            // 
            // FormMdiConexaoBD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 532);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FormMdiConexaoBD";
            this.Text = "Conexão Informix";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMdiconexaoBD_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonExcluir;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonIncluir;
        private System.Windows.Forms.Button buttonEditar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxLocaliza;
        private System.Windows.Forms.Button buttonAtualizar;
        private System.Windows.Forms.Button buttonTestar;
        private System.Windows.Forms.Button buttonGerarIni;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}