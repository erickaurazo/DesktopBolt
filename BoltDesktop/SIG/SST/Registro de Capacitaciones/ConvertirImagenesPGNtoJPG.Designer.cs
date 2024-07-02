namespace ComparativoHorasVisualSATNISIRA.SIG.SST.Registro_de_Capacitaciones
{
    partial class ConvertirImagenesPGNtoJPG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConvertirImagenesPGNtoJPG));
            this.gbEdicion = new System.Windows.Forms.GroupBox();
            this.btnConvertir = new System.Windows.Forms.Button();
            this.txtRutaDestino = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDestino = new System.Windows.Forms.Button();
            this.txtRutaOrigen = new System.Windows.Forms.TextBox();
            this.lblRuta = new System.Windows.Forms.Label();
            this.btnOrigen = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.stsBarraEstado = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblUsuarioId = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblUsuario = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.bgwHilo = new System.ComponentModel.BackgroundWorker();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.gbEdicion.SuspendLayout();
            this.stsBarraEstado.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbEdicion
            // 
            this.gbEdicion.Controls.Add(this.btnConvertir);
            this.gbEdicion.Controls.Add(this.txtRutaDestino);
            this.gbEdicion.Controls.Add(this.label1);
            this.gbEdicion.Controls.Add(this.btnDestino);
            this.gbEdicion.Controls.Add(this.txtRutaOrigen);
            this.gbEdicion.Controls.Add(this.lblRuta);
            this.gbEdicion.Controls.Add(this.btnOrigen);
            this.gbEdicion.Location = new System.Drawing.Point(12, 12);
            this.gbEdicion.Name = "gbEdicion";
            this.gbEdicion.Size = new System.Drawing.Size(767, 110);
            this.gbEdicion.TabIndex = 206;
            this.gbEdicion.TabStop = false;
            this.gbEdicion.Text = "Origen y Destino";
            // 
            // btnConvertir
            // 
            this.btnConvertir.Image = ((System.Drawing.Image)(resources.GetObject("btnConvertir.Image")));
            this.btnConvertir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConvertir.Location = new System.Drawing.Point(261, 66);
            this.btnConvertir.Name = "btnConvertir";
            this.btnConvertir.Size = new System.Drawing.Size(195, 38);
            this.btnConvertir.TabIndex = 10;
            this.btnConvertir.Text = "Convertir de PNG a JPG";
            this.btnConvertir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConvertir.UseVisualStyleBackColor = true;
            this.btnConvertir.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // txtRutaDestino
            // 
            this.txtRutaDestino.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRutaDestino.Location = new System.Drawing.Point(56, 40);
            this.txtRutaDestino.Name = "txtRutaDestino";
            this.txtRutaDestino.Size = new System.Drawing.Size(652, 20);
            this.txtRutaDestino.TabIndex = 7;
            this.txtRutaDestino.Text = "G:\\Mi unidad\\appsheet\\data\\CAPACITACIONE-252296585\\CapacitacionCapacitador_Images" +
    "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Destino :";
            // 
            // btnDestino
            // 
            this.btnDestino.Location = new System.Drawing.Point(714, 38);
            this.btnDestino.Name = "btnDestino";
            this.btnDestino.Size = new System.Drawing.Size(51, 23);
            this.btnDestino.TabIndex = 9;
            this.btnDestino.Text = "...";
            this.btnDestino.UseVisualStyleBackColor = true;
            this.btnDestino.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtRutaOrigen
            // 
            this.txtRutaOrigen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRutaOrigen.Location = new System.Drawing.Point(56, 17);
            this.txtRutaOrigen.Name = "txtRutaOrigen";
            this.txtRutaOrigen.Size = new System.Drawing.Size(652, 20);
            this.txtRutaOrigen.TabIndex = 0;
            this.txtRutaOrigen.Text = "G:\\Mi unidad\\appsheet\\data\\CAPACITACIONE-252296585\\CapacitacionCapacitador_Images" +
    "";
            // 
            // lblRuta
            // 
            this.lblRuta.AutoSize = true;
            this.lblRuta.Location = new System.Drawing.Point(9, 20);
            this.lblRuta.Name = "lblRuta";
            this.lblRuta.Size = new System.Drawing.Size(44, 13);
            this.lblRuta.TabIndex = 2;
            this.lblRuta.Text = "Origen :";
            // 
            // btnOrigen
            // 
            this.btnOrigen.Location = new System.Drawing.Point(714, 15);
            this.btnOrigen.Name = "btnOrigen";
            this.btnOrigen.Size = new System.Drawing.Size(51, 23);
            this.btnOrigen.TabIndex = 6;
            this.btnOrigen.Text = "...";
            this.btnOrigen.UseVisualStyleBackColor = true;
            this.btnOrigen.Click += new System.EventHandler(this.btnExaminar_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // stsBarraEstado
            // 
            this.stsBarraEstado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblUsuarioId,
            this.toolStripStatusLabel3,
            this.lblUsuario,
            this.progressBar1});
            this.stsBarraEstado.Location = new System.Drawing.Point(0, 142);
            this.stsBarraEstado.Name = "stsBarraEstado";
            this.stsBarraEstado.Size = new System.Drawing.Size(791, 22);
            this.stsBarraEstado.TabIndex = 207;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(53, 17);
            this.toolStripStatusLabel1.Text = "Usuario: ";
            // 
            // lblUsuarioId
            // 
            this.lblUsuarioId.Name = "lblUsuarioId";
            this.lblUsuarioId.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(59, 17);
            this.toolStripStatusLabel3.Text = "Nombres:";
            // 
            // lblUsuario
            // 
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(0, 17);
            // 
            // progressBar1
            // 
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(300, 16);
            // 
            // bgwHilo
            // 
            this.bgwHilo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwHilo_DoWork);
            this.bgwHilo.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwHilo_RunWorkerCompleted);
            // 
            // ConvertirImagenesPGNtoJPG
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(791, 164);
            this.Controls.Add(this.stsBarraEstado);
            this.Controls.Add(this.gbEdicion);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConvertirImagenesPGNtoJPG";
            this.Text = "Convertir Imagenesde PGN a JPG";
            this.Load += new System.EventHandler(this.ConvertirImagenesPGNtoJPG_Load);
            this.gbEdicion.ResumeLayout(false);
            this.gbEdicion.PerformLayout();
            this.stsBarraEstado.ResumeLayout(false);
            this.stsBarraEstado.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbEdicion;
        private System.Windows.Forms.TextBox txtRutaDestino;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDestino;
        private System.Windows.Forms.TextBox txtRutaOrigen;
        private System.Windows.Forms.Label lblRuta;
        private System.Windows.Forms.Button btnOrigen;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.StatusStrip stsBarraEstado;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblUsuarioId;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel lblUsuario;
        private System.Windows.Forms.ToolStripProgressBar progressBar1;
        private System.Windows.Forms.Button btnConvertir;
        private System.ComponentModel.BackgroundWorker bgwHilo;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}