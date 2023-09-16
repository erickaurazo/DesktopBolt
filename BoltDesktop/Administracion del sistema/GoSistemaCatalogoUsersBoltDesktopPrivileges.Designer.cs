namespace Asistencia
{
    partial class GoSistemaCatalogoUsersPrivileges
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoSistemaCatalogoUsersPrivileges));
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn1 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn2 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn3 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn4 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn5 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn6 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn7 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn8 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            this.gbEdition = new System.Windows.Forms.GroupBox();
            this.btnSincronizar = new System.Windows.Forms.Button();
            this.btnSavePrivileges = new System.Windows.Forms.Button();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.txtUserCode = new System.Windows.Forms.TextBox();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblUserCode = new System.Windows.Forms.Label();
            this.gbList = new System.Windows.Forms.GroupBox();
            this.dgvList = new Telerik.WinControls.UI.RadGridView();
            this.stsBarraEstado = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblPromedio = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblRecuento = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSumaSeleccionada = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel8 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTiempoTranscurrido = new System.Windows.Forms.ToolStripStatusLabel();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.lblNumeroResultados = new System.Windows.Forms.ToolStripStatusLabel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.subMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.bgwHilo = new System.ComponentModel.BackgroundWorker();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.gbEdition.SuspendLayout();
            this.gbList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList.MasterTemplate)).BeginInit();
            this.stsBarraEstado.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbEdition
            // 
            this.gbEdition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbEdition.Controls.Add(this.btnSincronizar);
            this.gbEdition.Controls.Add(this.btnSavePrivileges);
            this.gbEdition.Controls.Add(this.txtFullName);
            this.gbEdition.Controls.Add(this.txtUserCode);
            this.gbEdition.Controls.Add(this.lblFullName);
            this.gbEdition.Controls.Add(this.lblUserCode);
            this.gbEdition.Location = new System.Drawing.Point(4, 1);
            this.gbEdition.Name = "gbEdition";
            this.gbEdition.Size = new System.Drawing.Size(928, 45);
            this.gbEdition.TabIndex = 11;
            this.gbEdition.TabStop = false;
            // 
            // btnSincronizar
            // 
            this.btnSincronizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSincronizar.Image = ((System.Drawing.Image)(resources.GetObject("btnSincronizar.Image")));
            this.btnSincronizar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSincronizar.Location = new System.Drawing.Point(670, 11);
            this.btnSincronizar.Name = "btnSincronizar";
            this.btnSincronizar.Size = new System.Drawing.Size(150, 31);
            this.btnSincronizar.TabIndex = 195;
            this.btnSincronizar.Text = "   Sincronizar privilegios";
            this.btnSincronizar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSincronizar.UseVisualStyleBackColor = true;
            this.btnSincronizar.Visible = false;
            // 
            // btnSavePrivileges
            // 
            this.btnSavePrivileges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSavePrivileges.Image = ((System.Drawing.Image)(resources.GetObject("btnSavePrivileges.Image")));
            this.btnSavePrivileges.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSavePrivileges.Location = new System.Drawing.Point(837, 11);
            this.btnSavePrivileges.Name = "btnSavePrivileges";
            this.btnSavePrivileges.Size = new System.Drawing.Size(85, 31);
            this.btnSavePrivileges.TabIndex = 194;
            this.btnSavePrivileges.Text = "   Grabar";
            this.btnSavePrivileges.UseVisualStyleBackColor = true;
            this.btnSavePrivileges.Click += new System.EventHandler(this.btnResetPassword_Click);
            // 
            // txtFullName
            // 
            this.txtFullName.BackColor = System.Drawing.Color.White;
            this.txtFullName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFullName.Location = new System.Drawing.Point(283, 17);
            this.txtFullName.MaxLength = 150;
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.ReadOnly = true;
            this.txtFullName.Size = new System.Drawing.Size(381, 20);
            this.txtFullName.TabIndex = 18;
            // 
            // txtUserCode
            // 
            this.txtUserCode.BackColor = System.Drawing.Color.White;
            this.txtUserCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtUserCode.Location = new System.Drawing.Point(86, 18);
            this.txtUserCode.MaxLength = 20;
            this.txtUserCode.Name = "txtUserCode";
            this.txtUserCode.ReadOnly = true;
            this.txtUserCode.Size = new System.Drawing.Size(138, 20);
            this.txtUserCode.TabIndex = 13;
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Location = new System.Drawing.Point(225, 21);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(55, 13);
            this.lblFullName.TabIndex = 3;
            this.lblFullName.Text = "Nombres :";
            // 
            // lblUserCode
            // 
            this.lblUserCode.AutoSize = true;
            this.lblUserCode.Location = new System.Drawing.Point(6, 20);
            this.lblUserCode.Name = "lblUserCode";
            this.lblUserCode.Size = new System.Drawing.Size(83, 13);
            this.lblUserCode.TabIndex = 0;
            this.lblUserCode.Text = "Código usuario :";
            // 
            // gbList
            // 
            this.gbList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbList.Controls.Add(this.dgvList);
            this.gbList.Location = new System.Drawing.Point(4, 49);
            this.gbList.Name = "gbList";
            this.gbList.Size = new System.Drawing.Size(928, 426);
            this.gbList.TabIndex = 12;
            this.gbList.TabStop = false;
            // 
            // dgvList
            // 
            this.dgvList.BackColor = System.Drawing.SystemColors.Control;
            this.dgvList.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvList.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dgvList.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvList.Location = new System.Drawing.Point(3, 16);
            // 
            // dgvList
            // 
            this.dgvList.MasterTemplate.AllowAddNewRow = false;
            this.dgvList.MasterTemplate.AllowColumnReorder = false;
            this.dgvList.MasterTemplate.AutoGenerateColumns = false;
            this.dgvList.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewTextBoxColumn1.AllowSort = false;
            gridViewTextBoxColumn1.EnableExpressionEditor = false;
            gridViewTextBoxColumn1.FieldName = "UsuarioCodigo";
            gridViewTextBoxColumn1.HeaderText = "UsuarioCodigo";
            gridViewTextBoxColumn1.IsVisible = false;
            gridViewTextBoxColumn1.Name = "chUsuarioCodigo";
            gridViewTextBoxColumn2.AllowSort = false;
            gridViewTextBoxColumn2.EnableExpressionEditor = false;
            gridViewTextBoxColumn2.FieldName = "FormularioCodigo";
            gridViewTextBoxColumn2.HeaderText = "FormularioCodigo";
            gridViewTextBoxColumn2.IsVisible = false;
            gridViewTextBoxColumn2.Name = "chFormularioCodigo";
            gridViewTextBoxColumn2.Width = 45;
            gridViewTextBoxColumn3.AllowSort = false;
            gridViewTextBoxColumn3.EnableExpressionEditor = false;
            gridViewTextBoxColumn3.FieldName = "Modulo";
            gridViewTextBoxColumn3.HeaderText = "Módulo";
            gridViewTextBoxColumn3.Name = "chModulo";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn3.Width = 106;
            gridViewTextBoxColumn4.AllowGroup = false;
            gridViewTextBoxColumn4.AllowSort = false;
            gridViewTextBoxColumn4.EnableExpressionEditor = false;
            gridViewTextBoxColumn4.FieldName = "DescripcionFormulario";
            gridViewTextBoxColumn4.HeaderText = "Formulario";
            gridViewTextBoxColumn4.Name = "chDescripcionFormulario";
            gridViewTextBoxColumn4.ReadOnly = true;
            gridViewTextBoxColumn4.Width = 290;
            gridViewCheckBoxColumn1.AllowGroup = false;
            gridViewCheckBoxColumn1.AllowSort = false;
            gridViewCheckBoxColumn1.EnableExpressionEditor = false;
            gridViewCheckBoxColumn1.FieldName = "Consultar";
            gridViewCheckBoxColumn1.HeaderText = "Consultar";
            gridViewCheckBoxColumn1.MinWidth = 20;
            gridViewCheckBoxColumn1.Name = "chConsultar";
            gridViewCheckBoxColumn1.Width = 67;
            gridViewCheckBoxColumn2.AllowGroup = false;
            gridViewCheckBoxColumn2.AllowSort = false;
            gridViewCheckBoxColumn2.EnableExpressionEditor = false;
            gridViewCheckBoxColumn2.FieldName = "Nuevo";
            gridViewCheckBoxColumn2.HeaderText = "Nuevo";
            gridViewCheckBoxColumn2.MinWidth = 20;
            gridViewCheckBoxColumn2.Name = "chNuevo";
            gridViewCheckBoxColumn2.Width = 64;
            gridViewCheckBoxColumn3.AllowGroup = false;
            gridViewCheckBoxColumn3.AllowSort = false;
            gridViewCheckBoxColumn3.EnableExpressionEditor = false;
            gridViewCheckBoxColumn3.FieldName = "Editar";
            gridViewCheckBoxColumn3.HeaderText = "Editar";
            gridViewCheckBoxColumn3.MinWidth = 20;
            gridViewCheckBoxColumn3.Name = "chEditar";
            gridViewCheckBoxColumn3.Width = 64;
            gridViewCheckBoxColumn4.AllowGroup = false;
            gridViewCheckBoxColumn4.AllowSort = false;
            gridViewCheckBoxColumn4.EnableExpressionEditor = false;
            gridViewCheckBoxColumn4.FieldName = "Anular";
            gridViewCheckBoxColumn4.HeaderText = "Anular";
            gridViewCheckBoxColumn4.MinWidth = 20;
            gridViewCheckBoxColumn4.Name = "chAnular";
            gridViewCheckBoxColumn4.Width = 64;
            gridViewCheckBoxColumn5.AllowGroup = false;
            gridViewCheckBoxColumn5.AllowSort = false;
            gridViewCheckBoxColumn5.EnableExpressionEditor = false;
            gridViewCheckBoxColumn5.FieldName = "Eliminar";
            gridViewCheckBoxColumn5.HeaderText = "Eliminar";
            gridViewCheckBoxColumn5.MinWidth = 20;
            gridViewCheckBoxColumn5.Name = "chEliminar";
            gridViewCheckBoxColumn5.Width = 64;
            gridViewCheckBoxColumn6.AllowGroup = false;
            gridViewCheckBoxColumn6.AllowSort = false;
            gridViewCheckBoxColumn6.EnableExpressionEditor = false;
            gridViewCheckBoxColumn6.FieldName = "Imprimir";
            gridViewCheckBoxColumn6.HeaderText = "Imprimir";
            gridViewCheckBoxColumn6.MinWidth = 20;
            gridViewCheckBoxColumn6.Name = "chImprimir";
            gridViewCheckBoxColumn6.Width = 64;
            gridViewCheckBoxColumn7.AllowGroup = false;
            gridViewCheckBoxColumn7.AllowSort = false;
            gridViewCheckBoxColumn7.EnableExpressionEditor = false;
            gridViewCheckBoxColumn7.FieldName = "Exportar";
            gridViewCheckBoxColumn7.HeaderText = "Exportar";
            gridViewCheckBoxColumn7.MinWidth = 20;
            gridViewCheckBoxColumn7.Name = "chExportar";
            gridViewCheckBoxColumn7.Width = 64;
            gridViewCheckBoxColumn8.AllowGroup = false;
            gridViewCheckBoxColumn8.AllowSort = false;
            gridViewCheckBoxColumn8.EnableExpressionEditor = false;
            gridViewCheckBoxColumn8.FieldName = "Ninguno";
            gridViewCheckBoxColumn8.HeaderText = "Ninguno";
            gridViewCheckBoxColumn8.MinWidth = 20;
            gridViewCheckBoxColumn8.Name = "chNinguno";
            gridViewCheckBoxColumn8.Width = 66;
            gridViewTextBoxColumn5.AllowGroup = false;
            gridViewTextBoxColumn5.EnableExpressionEditor = false;
            gridViewTextBoxColumn5.FieldName = "Jerarquia";
            gridViewTextBoxColumn5.HeaderText = "Jerarquia";
            gridViewTextBoxColumn5.IsVisible = false;
            gridViewTextBoxColumn5.Name = "chJerarquia";
            gridViewTextBoxColumn5.Width = 43;
            gridViewTextBoxColumn6.AllowGroup = false;
            gridViewTextBoxColumn6.AllowSort = false;
            gridViewTextBoxColumn6.EnableExpressionEditor = false;
            gridViewTextBoxColumn6.FieldName = "ModuloCodigo";
            gridViewTextBoxColumn6.HeaderText = "ModuloCodigo";
            gridViewTextBoxColumn6.IsVisible = false;
            gridViewTextBoxColumn6.Name = "chModuloCodigo";
            gridViewTextBoxColumn6.Width = 42;
            gridViewTextBoxColumn7.AllowGroup = false;
            gridViewTextBoxColumn7.AllowSort = false;
            gridViewTextBoxColumn7.EnableExpressionEditor = false;
            gridViewTextBoxColumn7.FieldName = "TipoFormulario";
            gridViewTextBoxColumn7.HeaderText = "TipoFormulario";
            gridViewTextBoxColumn7.IsVisible = false;
            gridViewTextBoxColumn7.Name = "chTipoFormulario";
            gridViewTextBoxColumn7.Width = 35;
            this.dgvList.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewCheckBoxColumn1,
            gridViewCheckBoxColumn2,
            gridViewCheckBoxColumn3,
            gridViewCheckBoxColumn4,
            gridViewCheckBoxColumn5,
            gridViewCheckBoxColumn6,
            gridViewCheckBoxColumn7,
            gridViewCheckBoxColumn8,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6,
            gridViewTextBoxColumn7});
            this.dgvList.MasterTemplate.EnableFiltering = true;
            this.dgvList.Name = "dgvList";
            this.dgvList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvList.Size = new System.Drawing.Size(922, 407);
            this.dgvList.TabIndex = 0;
            this.dgvList.ThemeName = "VisualStudio2012Light";
            this.dgvList.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.dgvList_CellFormatting);
            this.dgvList.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.dgvList_CellBeginEdit);
            this.dgvList.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgvList_CellEndEdit);
            this.dgvList.ValueChanged += new System.EventHandler(this.dgvList_ValueChanged);
            this.dgvList.CurrentCellChanged += new Telerik.WinControls.UI.CurrentCellChangedEventHandler(this.dgvList_CurrentCellChanged);
            this.dgvList.CellValueChanged += new Telerik.WinControls.UI.GridViewCellEventHandler(this.dgvList_CellValueChanged);
            // 
            // stsBarraEstado
            // 
            this.stsBarraEstado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel6,
            this.lblPromedio,
            this.toolStripStatusLabel5,
            this.toolStripStatusLabel4,
            this.lblRecuento,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel2,
            this.lblSumaSeleccionada,
            this.toolStripStatusLabel7,
            this.toolStripStatusLabel8,
            this.lblTiempoTranscurrido,
            this.ProgressBar,
            this.lblNumeroResultados});
            this.stsBarraEstado.Location = new System.Drawing.Point(0, 478);
            this.stsBarraEstado.Name = "stsBarraEstado";
            this.stsBarraEstado.Size = new System.Drawing.Size(935, 22);
            this.stsBarraEstado.TabIndex = 193;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(59, 17);
            this.toolStripStatusLabel6.Text = "Promedio";
            // 
            // lblPromedio
            // 
            this.lblPromedio.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblPromedio.Name = "lblPromedio";
            this.lblPromedio.Size = new System.Drawing.Size(32, 17);
            this.lblPromedio.Text = "0.00";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(28, 17);
            this.toolStripStatusLabel5.Text = "       ";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(57, 17);
            this.toolStripStatusLabel4.Text = "Recuento";
            // 
            // lblRecuento
            // 
            this.lblRecuento.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblRecuento.Name = "lblRecuento";
            this.lblRecuento.Size = new System.Drawing.Size(14, 17);
            this.lblRecuento.Text = "0";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(28, 17);
            this.toolStripStatusLabel3.Text = "       ";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(37, 17);
            this.toolStripStatusLabel2.Text = "Suma";
            // 
            // lblSumaSeleccionada
            // 
            this.lblSumaSeleccionada.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblSumaSeleccionada.Name = "lblSumaSeleccionada";
            this.lblSumaSeleccionada.Size = new System.Drawing.Size(32, 17);
            this.lblSumaSeleccionada.Text = "0.00";
            // 
            // toolStripStatusLabel7
            // 
            this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            this.toolStripStatusLabel7.Size = new System.Drawing.Size(22, 17);
            this.toolStripStatusLabel7.Text = "     ";
            // 
            // toolStripStatusLabel8
            // 
            this.toolStripStatusLabel8.Name = "toolStripStatusLabel8";
            this.toolStripStatusLabel8.Size = new System.Drawing.Size(130, 17);
            this.toolStripStatusLabel8.Text = "  Tiempo Transcurrido : ";
            // 
            // lblTiempoTranscurrido
            // 
            this.lblTiempoTranscurrido.Name = "lblTiempoTranscurrido";
            this.lblTiempoTranscurrido.Size = new System.Drawing.Size(49, 17);
            this.lblTiempoTranscurrido.Text = "00:00:00";
            // 
            // ProgressBar
            // 
            this.ProgressBar.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ProgressBar.MarqueeAnimationSpeed = 25;
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(160, 16);
            this.ProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.ProgressBar.Visible = false;
            // 
            // lblNumeroResultados
            // 
            this.lblNumeroResultados.Name = "lblNumeroResultados";
            this.lblNumeroResultados.Size = new System.Drawing.Size(0, 17);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icon1.png");
            this.imageList1.Images.SetKeyName(1, "icon2.png");
            this.imageList1.Images.SetKeyName(2, "icon3.png");
            this.imageList1.Images.SetKeyName(3, "icon4.png");
            this.imageList1.Images.SetKeyName(4, "find.png");
            // 
            // subMenu
            // 
            this.subMenu.Name = "subMenu";
            this.subMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // bgwHilo
            // 
            this.bgwHilo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwHilo_DoWork);
            this.bgwHilo.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwHilo_RunWorkerCompleted);
            // 
            // GoSistemaCatalogoUsersPrivileges
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(935, 500);
            this.Controls.Add(this.stsBarraEstado);
            this.Controls.Add(this.gbList);
            this.Controls.Add(this.gbEdition);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GoSistemaCatalogoUsersPrivileges";
            this.Text = "Privilegios y accesos para usuario | Edición";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Privileges_Load);
            this.gbEdition.ResumeLayout(false);
            this.gbEdition.PerformLayout();
            this.gbList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.stsBarraEstado.ResumeLayout(false);
            this.stsBarraEstado.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbEdition;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtUserCode;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label lblUserCode;
        private System.Windows.Forms.GroupBox gbList;
        private Telerik.WinControls.UI.RadGridView dgvList;
        private System.Windows.Forms.StatusStrip stsBarraEstado;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel lblPromedio;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel lblRecuento;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel lblSumaSeleccionada;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel8;
        private System.Windows.Forms.ToolStripStatusLabel lblTiempoTranscurrido;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel lblNumeroResultados;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ContextMenuStrip subMenu;
        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private System.ComponentModel.BackgroundWorker bgwHilo;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private System.Windows.Forms.Button btnSavePrivileges;
        private System.Windows.Forms.Button btnSincronizar;
    }
}