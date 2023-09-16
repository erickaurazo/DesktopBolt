namespace ComparativoHorasVisualSATNISIRA.Administracion_del_sistema
{
    partial class AdjuntarArchivos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdjuntarArchivos));
            Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn1 = new Telerik.WinControls.UI.GridViewDecimalColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.lblRuta = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnExaminar = new System.Windows.Forms.Button();
            this.dgvListado = new Telerik.WinControls.UI.RadGridView();
            this.bgwHilo = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.stsBarraEstado = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblUsuarioId = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblUsuario = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.btnDetalleCambiarEstado = new Telerik.WinControls.UI.RadButton();
            this.btnDetalleQuitar = new Telerik.WinControls.UI.RadButton();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.gbListado = new System.Windows.Forms.GroupBox();
            this.gbEdicion = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListado.MasterTemplate)).BeginInit();
            this.stsBarraEstado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnDetalleCambiarEstado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDetalleQuitar)).BeginInit();
            this.gbListado.SuspendLayout();
            this.gbEdicion.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtRuta
            // 
            this.txtRuta.Location = new System.Drawing.Point(49, 17);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.ReadOnly = true;
            this.txtRuta.Size = new System.Drawing.Size(587, 20);
            this.txtRuta.TabIndex = 0;
            // 
            // txtTitulo
            // 
            this.txtTitulo.Location = new System.Drawing.Point(49, 40);
            this.txtTitulo.MaxLength = 50;
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new System.Drawing.Size(642, 20);
            this.txtTitulo.TabIndex = 1;
            // 
            // lblRuta
            // 
            this.lblRuta.AutoSize = true;
            this.lblRuta.Location = new System.Drawing.Point(6, 20);
            this.lblRuta.Name = "lblRuta";
            this.lblRuta.Size = new System.Drawing.Size(33, 13);
            this.lblRuta.TabIndex = 2;
            this.lblRuta.Text = "Ruta:";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(6, 43);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(41, 13);
            this.lblTitulo.TabIndex = 3;
            this.lblTitulo.Text = "Título :";
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Image = ((System.Drawing.Image)(resources.GetObject("btnRegistrar.Image")));
            this.btnRegistrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegistrar.Location = new System.Drawing.Point(529, 66);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(82, 24);
            this.btnRegistrar.TabIndex = 4;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(618, 66);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 24);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnExaminar
            // 
            this.btnExaminar.Location = new System.Drawing.Point(642, 15);
            this.btnExaminar.Name = "btnExaminar";
            this.btnExaminar.Size = new System.Drawing.Size(51, 23);
            this.btnExaminar.TabIndex = 6;
            this.btnExaminar.Text = "...";
            this.btnExaminar.UseVisualStyleBackColor = true;
            this.btnExaminar.Click += new System.EventHandler(this.btnExaminar_Click);
            // 
            // dgvListado
            // 
            this.dgvListado.BackColor = System.Drawing.SystemColors.Control;
            this.dgvListado.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvListado.EnableGestures = false;
            this.dgvListado.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvListado.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvListado.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvListado.Location = new System.Drawing.Point(6, 45);
            // 
            // dgvListado
            // 
            this.dgvListado.MasterTemplate.AllowAddNewRow = false;
            this.dgvListado.MasterTemplate.AllowCellContextMenu = false;
            this.dgvListado.MasterTemplate.AllowColumnChooser = false;
            this.dgvListado.MasterTemplate.AllowColumnHeaderContextMenu = false;
            this.dgvListado.MasterTemplate.AllowDeleteRow = false;
            this.dgvListado.MasterTemplate.AllowEditRow = false;
            this.dgvListado.MasterTemplate.AutoGenerateColumns = false;
            this.dgvListado.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewDecimalColumn1.EnableExpressionEditor = false;
            gridViewDecimalColumn1.FieldName = "id";
            gridViewDecimalColumn1.HeaderText = "id";
            gridViewDecimalColumn1.IsVisible = false;
            gridViewDecimalColumn1.Name = "chid";
            gridViewDecimalColumn1.Width = 68;
            gridViewTextBoxColumn1.EnableExpressionEditor = false;
            gridViewTextBoxColumn1.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Standard;
            gridViewTextBoxColumn1.FieldName = "fecha";
            gridViewTextBoxColumn1.FormatString = "{0:d}";
            gridViewTextBoxColumn1.HeaderText = "Fecha";
            gridViewTextBoxColumn1.Name = "chfecha";
            gridViewTextBoxColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewTextBoxColumn1.Width = 105;
            gridViewTextBoxColumn2.EnableExpressionEditor = false;
            gridViewTextBoxColumn2.FieldName = "nombre";
            gridViewTextBoxColumn2.HeaderText = "Documento";
            gridViewTextBoxColumn2.Name = "chnombre";
            gridViewTextBoxColumn2.Width = 455;
            gridViewTextBoxColumn3.EnableExpressionEditor = false;
            gridViewTextBoxColumn3.FieldName = "extension";
            gridViewTextBoxColumn3.HeaderText = "extension";
            gridViewTextBoxColumn3.IsVisible = false;
            gridViewTextBoxColumn3.Name = "chextension";
            gridViewTextBoxColumn3.Width = 76;
            gridViewTextBoxColumn4.EnableExpressionEditor = false;
            gridViewTextBoxColumn4.FieldName = "formulario";
            gridViewTextBoxColumn4.HeaderText = "formulario";
            gridViewTextBoxColumn4.IsVisible = false;
            gridViewTextBoxColumn4.Name = "chformulario";
            gridViewTextBoxColumn4.Width = 87;
            gridViewTextBoxColumn5.EnableExpressionEditor = false;
            gridViewTextBoxColumn5.FieldName = "idReferencia";
            gridViewTextBoxColumn5.HeaderText = "idReferencia";
            gridViewTextBoxColumn5.IsVisible = false;
            gridViewTextBoxColumn5.Name = "chidReferencia";
            gridViewTextBoxColumn5.Width = 101;
            gridViewTextBoxColumn6.EnableExpressionEditor = false;
            gridViewTextBoxColumn6.FieldName = "estado";
            gridViewTextBoxColumn6.HeaderText = "Estado";
            gridViewTextBoxColumn6.Name = "chestado";
            gridViewTextBoxColumn6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewTextBoxColumn6.Width = 53;
            gridViewTextBoxColumn7.EnableExpressionEditor = false;
            gridViewTextBoxColumn7.FieldName = "visibleEnReporte";
            gridViewTextBoxColumn7.HeaderText = "Vis. en Rpt";
            gridViewTextBoxColumn7.Name = "chvisibleEnReporte";
            gridViewTextBoxColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewTextBoxColumn7.Width = 51;
            gridViewTextBoxColumn8.EnableExpressionEditor = false;
            gridViewTextBoxColumn8.FieldName = "idusuario";
            gridViewTextBoxColumn8.HeaderText = "idusuario";
            gridViewTextBoxColumn8.IsVisible = false;
            gridViewTextBoxColumn8.Name = "chidusuario";
            gridViewTextBoxColumn8.Width = 126;
            this.dgvListado.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewDecimalColumn1,
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6,
            gridViewTextBoxColumn7,
            gridViewTextBoxColumn8});
            this.dgvListado.MasterTemplate.EnableGrouping = false;
            this.dgvListado.MasterTemplate.ShowFilteringRow = false;
            this.dgvListado.Name = "dgvListado";
            this.dgvListado.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvListado.ShowGroupPanel = false;
            this.dgvListado.Size = new System.Drawing.Size(679, 192);
            this.dgvListado.TabIndex = 7;
            this.dgvListado.Text = "radGridView1";
            this.dgvListado.ThemeName = "VisualStudio2012Light";
            this.dgvListado.SelectionChanged += new System.EventHandler(this.dgvListado_SelectionChanged);
            this.dgvListado.DoubleClick += new System.EventHandler(this.dgvListado_DoubleClick);
            // 
            // bgwHilo
            // 
            this.bgwHilo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwHilo_DoWork);
            this.bgwHilo.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwHilo_RunWorkerCompleted);
            // 
            // stsBarraEstado
            // 
            this.stsBarraEstado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblUsuarioId,
            this.toolStripStatusLabel3,
            this.lblUsuario,
            this.progressBar1});
            this.stsBarraEstado.Location = new System.Drawing.Point(0, 377);
            this.stsBarraEstado.Name = "stsBarraEstado";
            this.stsBarraEstado.Size = new System.Drawing.Size(723, 22);
            this.stsBarraEstado.TabIndex = 198;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(53, 17);
            this.toolStripStatusLabel1.Text = "Usuario: ";
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
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
            // btnDetalleCambiarEstado
            // 
            this.btnDetalleCambiarEstado.Image = ((System.Drawing.Image)(resources.GetObject("btnDetalleCambiarEstado.Image")));
            this.btnDetalleCambiarEstado.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDetalleCambiarEstado.Location = new System.Drawing.Point(632, 13);
            this.btnDetalleCambiarEstado.Name = "btnDetalleCambiarEstado";
            this.btnDetalleCambiarEstado.Size = new System.Drawing.Size(25, 26);
            this.btnDetalleCambiarEstado.TabIndex = 201;
            this.btnDetalleCambiarEstado.ThemeName = "VisualStudio2012Light";
            this.btnDetalleCambiarEstado.Click += new System.EventHandler(this.btnDetalleCambiarEstado_Click);
            // 
            // btnDetalleQuitar
            // 
            this.btnDetalleQuitar.Image = ((System.Drawing.Image)(resources.GetObject("btnDetalleQuitar.Image")));
            this.btnDetalleQuitar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDetalleQuitar.Location = new System.Drawing.Point(660, 13);
            this.btnDetalleQuitar.Name = "btnDetalleQuitar";
            this.btnDetalleQuitar.Size = new System.Drawing.Size(25, 26);
            this.btnDetalleQuitar.TabIndex = 200;
            this.btnDetalleQuitar.ThemeName = "VisualStudio2012Light";
            this.btnDetalleQuitar.Click += new System.EventHandler(this.btnDetalleQuitar_Click);
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(49, 66);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.ReadOnly = true;
            this.txtCodigo.Size = new System.Drawing.Size(47, 20);
            this.txtCodigo.TabIndex = 202;
            this.txtCodigo.Visible = false;
            // 
            // gbListado
            // 
            this.gbListado.Controls.Add(this.dgvListado);
            this.gbListado.Controls.Add(this.btnDetalleQuitar);
            this.gbListado.Controls.Add(this.btnDetalleCambiarEstado);
            this.gbListado.Enabled = false;
            this.gbListado.Location = new System.Drawing.Point(12, 116);
            this.gbListado.Name = "gbListado";
            this.gbListado.Size = new System.Drawing.Size(699, 254);
            this.gbListado.TabIndex = 204;
            this.gbListado.TabStop = false;
            this.gbListado.Text = "Listado";
            // 
            // gbEdicion
            // 
            this.gbEdicion.Controls.Add(this.btnCancelar);
            this.gbEdicion.Controls.Add(this.txtRuta);
            this.gbEdicion.Controls.Add(this.txtCodigo);
            this.gbEdicion.Controls.Add(this.txtTitulo);
            this.gbEdicion.Controls.Add(this.lblRuta);
            this.gbEdicion.Controls.Add(this.btnExaminar);
            this.gbEdicion.Controls.Add(this.lblTitulo);
            this.gbEdicion.Controls.Add(this.btnRegistrar);
            this.gbEdicion.Enabled = false;
            this.gbEdicion.Location = new System.Drawing.Point(12, 12);
            this.gbEdicion.Name = "gbEdicion";
            this.gbEdicion.Size = new System.Drawing.Size(699, 98);
            this.gbEdicion.TabIndex = 205;
            this.gbEdicion.TabStop = false;
            this.gbEdicion.Text = "Edicion";
            // 
            // AdjuntarArchivos
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(723, 399);
            this.Controls.Add(this.gbEdicion);
            this.Controls.Add(this.gbListado);
            this.Controls.Add(this.stsBarraEstado);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdjuntarArchivos";
            this.Text = "Adjuntar documentos al registro";
            this.Load += new System.EventHandler(this.AdjuntarArchivos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListado.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListado)).EndInit();
            this.stsBarraEstado.ResumeLayout(false);
            this.stsBarraEstado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnDetalleCambiarEstado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDetalleQuitar)).EndInit();
            this.gbListado.ResumeLayout(false);
            this.gbEdicion.ResumeLayout(false);
            this.gbEdicion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.Label lblRuta;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnExaminar;
        private Telerik.WinControls.UI.RadGridView dgvListado;
        private System.ComponentModel.BackgroundWorker bgwHilo;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.StatusStrip stsBarraEstado;
        private System.Windows.Forms.ToolStripProgressBar progressBar1;
        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Telerik.WinControls.UI.RadButton btnDetalleCambiarEstado;
        private Telerik.WinControls.UI.RadButton btnDetalleQuitar;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.GroupBox gbListado;
        private System.Windows.Forms.GroupBox gbEdicion;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblUsuarioId;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel lblUsuario;
    }
}