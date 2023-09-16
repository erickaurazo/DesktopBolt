namespace ComparativoHorasVisualSATNISIRA.T.I.Partes_Diarios
{
    partial class PartesDiariosDeEquipamientoByIdDevice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PartesDiariosDeEquipamientoByIdDevice));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbCabecera02 = new System.Windows.Forms.GroupBox();
            this.cboSemana = new Telerik.WinControls.UI.RadDropDownList();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFechaHasta = new MyDataGridViewColumns.MyDataGridViewMaskedTextEditingControl();
            this.txtFechaDesde = new MyDataGridViewColumns.MyDataGridViewMaskedTextEditingControl();
            this.txtPeriodo = new Telerik.WinControls.UI.RadSpinEditor();
            this.label3 = new System.Windows.Forms.Label();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.lblFechaDesde = new System.Windows.Forms.Label();
            this.lblFechaHasta = new System.Windows.Forms.Label();
            this.txtDipositivoDescripcion = new MyControlsDataBinding.Controles.MyTextBoxSearchSimple(this.components);
            this.btnDispositivoBuscar = new MyControlsDataBinding.Controles.MyButtonSearchSimple(this.components);
            this.txtDipositivoCodigo = new MyControlsDataBinding.Controles.MyTextBoxSearchSimple(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.stsBarraEstado = new System.Windows.Forms.StatusStrip();
            this.pbar = new System.Windows.Forms.ToolStripProgressBar();
            this.subMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEditarRegistro = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnVistaPrevia = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnVerDetalleDeDispositivos = new System.Windows.Forms.ToolStripMenuItem();
            this.bgwHilo = new System.ComponentModel.BackgroundWorker();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.BarraPrincipal = new Telerik.WinControls.UI.RadCommandBar();
            this.BarraSuperior = new Telerik.WinControls.UI.CommandBarRowElement();
            this.BarraModulo = new Telerik.WinControls.UI.CommandBarStripElement();
            this.btnITD = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarStripElement3 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.btnNuevo = new Telerik.WinControls.UI.CommandBarButton();
            this.btnEditar = new Telerik.WinControls.UI.CommandBarButton();
            this.btnRegistrar = new Telerik.WinControls.UI.CommandBarButton();
            this.btnAtras = new Telerik.WinControls.UI.CommandBarButton();
            this.btnAnular = new Telerik.WinControls.UI.CommandBarButton();
            this.btnEliminarRegistro = new Telerik.WinControls.UI.CommandBarButton();
            this.btnExportToExcel = new Telerik.WinControls.UI.CommandBarButton();
            this.btnAdjuntar = new Telerik.WinControls.UI.CommandBarButton();
            this.btnNotificar = new Telerik.WinControls.UI.CommandBarButton();
            this.btnCerrar = new Telerik.WinControls.UI.CommandBarButton();
            this.gbDetalle = new System.Windows.Forms.GroupBox();
            this.dgvDetalle = new MyControlsDataBinding.Controles.MyDataGridViewDetails(this.components);
            this.chId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chRazonSocial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chSede = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chTipoHardware = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chfecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chEstadoDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chItemDetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chPersonal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chArea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chHorasActivas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chHorasInactivas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chMotivoInactivoCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chMotivoInactivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chObservacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chEstadoId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bgwRegistrar = new System.ComponentModel.BackgroundWorker();
            this.gbCabecera02.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboSemana)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPeriodo)).BeginInit();
            this.stsBarraEstado.SuspendLayout();
            this.subMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BarraPrincipal)).BeginInit();
            this.gbDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.SuspendLayout();
            // 
            // gbCabecera02
            // 
            this.gbCabecera02.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCabecera02.Controls.Add(this.cboSemana);
            this.gbCabecera02.Controls.Add(this.label2);
            this.gbCabecera02.Controls.Add(this.txtFechaHasta);
            this.gbCabecera02.Controls.Add(this.txtFechaDesde);
            this.gbCabecera02.Controls.Add(this.txtPeriodo);
            this.gbCabecera02.Controls.Add(this.label3);
            this.gbCabecera02.Controls.Add(this.btnConsultar);
            this.gbCabecera02.Controls.Add(this.lblFechaDesde);
            this.gbCabecera02.Controls.Add(this.lblFechaHasta);
            this.gbCabecera02.Controls.Add(this.txtDipositivoDescripcion);
            this.gbCabecera02.Controls.Add(this.btnDispositivoBuscar);
            this.gbCabecera02.Controls.Add(this.txtDipositivoCodigo);
            this.gbCabecera02.Controls.Add(this.label1);
            this.gbCabecera02.Location = new System.Drawing.Point(8, 41);
            this.gbCabecera02.Name = "gbCabecera02";
            this.gbCabecera02.Size = new System.Drawing.Size(1431, 102);
            this.gbCabecera02.TabIndex = 209;
            this.gbCabecera02.TabStop = false;
            // 
            // cboSemana
            // 
            this.cboSemana.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.cboSemana.Location = new System.Drawing.Point(192, 19);
            this.cboSemana.Name = "cboSemana";
            this.cboSemana.Size = new System.Drawing.Size(198, 20);
            this.cboSemana.TabIndex = 279;
            this.cboSemana.ThemeName = "VisualStudio2012Light";
            this.cboSemana.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.cboSemana_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(127, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 278;
            this.label2.Text = "Semana :";
            // 
            // txtFechaHasta
            // 
            this.txtFechaHasta.EditingControlDataGridView = null;
            this.txtFechaHasta.EditingControlFormattedValue = "  /  /";
            this.txtFechaHasta.EditingControlRowIndex = 0;
            this.txtFechaHasta.EditingControlValueChanged = true;
            this.txtFechaHasta.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txtFechaHasta.Location = new System.Drawing.Point(322, 46);
            this.txtFechaHasta.Mask = "00/00/0000";
            this.txtFechaHasta.Name = "txtFechaHasta";
            this.txtFechaHasta.P_EsEditable = false;
            this.txtFechaHasta.P_EsModificable = false;
            this.txtFechaHasta.P_ExigeInformacion = false;
            this.txtFechaHasta.P_Hora = null;
            this.txtFechaHasta.P_NombreColumna = null;
            this.txtFechaHasta.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtFechaHasta.ReadOnly = true;
            this.txtFechaHasta.Size = new System.Drawing.Size(68, 20);
            this.txtFechaHasta.TabIndex = 277;
            this.txtFechaHasta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFechaHasta.ValidatingType = typeof(System.DateTime);
            // 
            // txtFechaDesde
            // 
            this.txtFechaDesde.EditingControlDataGridView = null;
            this.txtFechaDesde.EditingControlFormattedValue = "  /  /";
            this.txtFechaDesde.EditingControlRowIndex = 0;
            this.txtFechaDesde.EditingControlValueChanged = true;
            this.txtFechaDesde.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txtFechaDesde.Location = new System.Drawing.Point(192, 45);
            this.txtFechaDesde.Mask = "00/00/0000";
            this.txtFechaDesde.Name = "txtFechaDesde";
            this.txtFechaDesde.P_EsEditable = false;
            this.txtFechaDesde.P_EsModificable = false;
            this.txtFechaDesde.P_ExigeInformacion = false;
            this.txtFechaDesde.P_Hora = null;
            this.txtFechaDesde.P_NombreColumna = null;
            this.txtFechaDesde.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtFechaDesde.ReadOnly = true;
            this.txtFechaDesde.Size = new System.Drawing.Size(66, 20);
            this.txtFechaDesde.TabIndex = 275;
            this.txtFechaDesde.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFechaDesde.ValidatingType = typeof(System.DateTime);
            // 
            // txtPeriodo
            // 
            this.txtPeriodo.Location = new System.Drawing.Point(57, 23);
            this.txtPeriodo.Maximum = new decimal(new int[] {
            2025,
            0,
            0,
            0});
            this.txtPeriodo.Minimum = new decimal(new int[] {
            2020,
            0,
            0,
            0});
            this.txtPeriodo.Name = "txtPeriodo";
            // 
            // 
            // 
            this.txtPeriodo.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.txtPeriodo.Size = new System.Drawing.Size(50, 20);
            this.txtPeriodo.TabIndex = 273;
            this.txtPeriodo.TabStop = false;
            this.txtPeriodo.TextAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPeriodo.ThemeName = "Windows8";
            this.txtPeriodo.Value = new decimal(new int[] {
            2023,
            0,
            0,
            0});
            this.txtPeriodo.ValueChanged += new System.EventHandler(this.txtPeriodo_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 272;
            this.label3.Text = "Año :";
            // 
            // btnConsultar
            // 
            this.btnConsultar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConsultar.Image = ((System.Drawing.Image)(resources.GetObject("btnConsultar.Image")));
            this.btnConsultar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsultar.Location = new System.Drawing.Point(1310, 62);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(102, 28);
            this.btnConsultar.TabIndex = 271;
            this.btnConsultar.Text = "&Consultar     ";
            this.btnConsultar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // lblFechaDesde
            // 
            this.lblFechaDesde.AutoSize = true;
            this.lblFechaDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaDesde.Location = new System.Drawing.Point(131, 49);
            this.lblFechaDesde.Name = "lblFechaDesde";
            this.lblFechaDesde.Size = new System.Drawing.Size(55, 13);
            this.lblFechaDesde.TabIndex = 274;
            this.lblFechaDesde.Text = " Desde :";
            // 
            // lblFechaHasta
            // 
            this.lblFechaHasta.AutoSize = true;
            this.lblFechaHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaHasta.Location = new System.Drawing.Point(264, 49);
            this.lblFechaHasta.Name = "lblFechaHasta";
            this.lblFechaHasta.Size = new System.Drawing.Size(52, 13);
            this.lblFechaHasta.TabIndex = 276;
            this.lblFechaHasta.Text = " Hasta :";
            // 
            // txtDipositivoDescripcion
            // 
            this.txtDipositivoDescripcion.BackColor = System.Drawing.Color.White;
            this.txtDipositivoDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDipositivoDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtDipositivoDescripcion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDipositivoDescripcion.Location = new System.Drawing.Point(151, 73);
            this.txtDipositivoDescripcion.Name = "txtDipositivoDescripcion";
            this.txtDipositivoDescripcion.P_BotonEnlace = null;
            this.txtDipositivoDescripcion.P_BuscarSoloCodigoExacto = false;
            this.txtDipositivoDescripcion.P_EsEditable = false;
            this.txtDipositivoDescripcion.P_EsModificable = false;
            this.txtDipositivoDescripcion.P_EsPrimaryKey = false;
            this.txtDipositivoDescripcion.P_ExigeInformacion = false;
            this.txtDipositivoDescripcion.P_NombreColumna = null;
            this.txtDipositivoDescripcion.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtDipositivoDescripcion.ReadOnly = true;
            this.txtDipositivoDescripcion.Size = new System.Drawing.Size(329, 20);
            this.txtDipositivoDescripcion.TabIndex = 269;
            // 
            // btnDispositivoBuscar
            // 
            this.btnDispositivoBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnDispositivoBuscar.Image")));
            this.btnDispositivoBuscar.Location = new System.Drawing.Point(68, 72);
            this.btnDispositivoBuscar.Name = "btnDispositivoBuscar";
            this.btnDispositivoBuscar.P_CampoCodigo = "id";
            this.btnDispositivoBuscar.P_CampoDescripcion = "ltrim(upper(dispositivo))";
            this.btnDispositivoBuscar.P_EsEditable = true;
            this.btnDispositivoBuscar.P_EsModificable = true;
            this.btnDispositivoBuscar.P_FilterByTextBox = null;
            this.btnDispositivoBuscar.P_TablaConsulta = "SAS_ListadoDeDispositivosDisponiblesParaPartesDeEquipamientoAllProveedor order by" +
    " 2";
            this.btnDispositivoBuscar.P_TextBoxCodigo = this.txtDipositivoCodigo;
            this.btnDispositivoBuscar.P_TextBoxDescripcion = this.txtDipositivoDescripcion;
            this.btnDispositivoBuscar.P_TituloFormulario = "... Buscar personal interno y extetno";
            this.btnDispositivoBuscar.Size = new System.Drawing.Size(24, 23);
            this.btnDispositivoBuscar.TabIndex = 270;
            this.btnDispositivoBuscar.UseVisualStyleBackColor = true;
            // 
            // txtDipositivoCodigo
            // 
            this.txtDipositivoCodigo.BackColor = System.Drawing.Color.White;
            this.txtDipositivoCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDipositivoCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtDipositivoCodigo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDipositivoCodigo.Location = new System.Drawing.Point(96, 73);
            this.txtDipositivoCodigo.MaxLength = 7;
            this.txtDipositivoCodigo.Name = "txtDipositivoCodigo";
            this.txtDipositivoCodigo.P_BotonEnlace = this.btnDispositivoBuscar;
            this.txtDipositivoCodigo.P_BuscarSoloCodigoExacto = false;
            this.txtDipositivoCodigo.P_EsEditable = true;
            this.txtDipositivoCodigo.P_EsModificable = true;
            this.txtDipositivoCodigo.P_EsPrimaryKey = false;
            this.txtDipositivoCodigo.P_ExigeInformacion = false;
            this.txtDipositivoCodigo.P_NombreColumna = null;
            this.txtDipositivoCodigo.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtDipositivoCodigo.Size = new System.Drawing.Size(49, 20);
            this.txtDipositivoCodigo.TabIndex = 268;
            this.txtDipositivoCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 267;
            this.label1.Text = "Dispositivo :";
            // 
            // stsBarraEstado
            // 
            this.stsBarraEstado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pbar});
            this.stsBarraEstado.Location = new System.Drawing.Point(0, 645);
            this.stsBarraEstado.Name = "stsBarraEstado";
            this.stsBarraEstado.Size = new System.Drawing.Size(1443, 22);
            this.stsBarraEstado.TabIndex = 213;
            // 
            // pbar
            // 
            this.pbar.ForeColor = System.Drawing.Color.PaleGreen;
            this.pbar.Maximum = 200;
            this.pbar.Minimum = 10;
            this.pbar.Name = "pbar";
            this.pbar.Size = new System.Drawing.Size(700, 16);
            this.pbar.Step = 5;
            this.pbar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbar.Value = 10;
            this.pbar.Visible = false;
            // 
            // subMenu
            // 
            this.subMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator4,
            this.btnEditarRegistro,
            this.toolStripMenuItem2,
            this.toolStripSeparator5,
            this.btnVistaPrevia,
            this.toolStripSeparator6,
            this.btnVerDetalleDeDispositivos});
            this.subMenu.Name = "subMenu";
            this.subMenu.Size = new System.Drawing.Size(210, 110);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(206, 6);
            // 
            // btnEditarRegistro
            // 
            this.btnEditarRegistro.Image = ((System.Drawing.Image)(resources.GetObject("btnEditarRegistro.Image")));
            this.btnEditarRegistro.Name = "btnEditarRegistro";
            this.btnEditarRegistro.Size = new System.Drawing.Size(209, 22);
            this.btnEditarRegistro.Text = "Editar";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Enabled = false;
            this.toolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem2.Image")));
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(209, 22);
            this.toolStripMenuItem2.Text = "Enviar por notificación";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(206, 6);
            // 
            // btnVistaPrevia
            // 
            this.btnVistaPrevia.Enabled = false;
            this.btnVistaPrevia.Image = ((System.Drawing.Image)(resources.GetObject("btnVistaPrevia.Image")));
            this.btnVistaPrevia.Name = "btnVistaPrevia";
            this.btnVistaPrevia.Size = new System.Drawing.Size(209, 22);
            this.btnVistaPrevia.Text = "Vista previa";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(206, 6);
            // 
            // btnVerDetalleDeDispositivos
            // 
            this.btnVerDetalleDeDispositivos.Image = ((System.Drawing.Image)(resources.GetObject("btnVerDetalleDeDispositivos.Image")));
            this.btnVerDetalleDeDispositivos.Name = "btnVerDetalleDeDispositivos";
            this.btnVerDetalleDeDispositivos.Size = new System.Drawing.Size(209, 22);
            this.btnVerDetalleDeDispositivos.Text = "Ver detalle de dispositivos";
            // 
            // bgwHilo
            // 
            this.bgwHilo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwHilo_DoWork);
            this.bgwHilo.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwHilo_RunWorkerCompleted);
            // 
            // BarraPrincipal
            // 
            this.BarraPrincipal.Dock = System.Windows.Forms.DockStyle.Top;
            this.BarraPrincipal.Location = new System.Drawing.Point(0, 0);
            this.BarraPrincipal.Name = "BarraPrincipal";
            this.BarraPrincipal.Rows.AddRange(new Telerik.WinControls.UI.CommandBarRowElement[] {
            this.BarraSuperior});
            this.BarraPrincipal.Size = new System.Drawing.Size(1443, 37);
            this.BarraPrincipal.TabIndex = 214;
            this.BarraPrincipal.ThemeName = "VisualStudio2012Light";
            // 
            // BarraSuperior
            // 
            this.BarraSuperior.BackColor = System.Drawing.SystemColors.Control;
            this.BarraSuperior.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.BarraSuperior.MinSize = new System.Drawing.Size(25, 25);
            this.BarraSuperior.Strips.AddRange(new Telerik.WinControls.UI.CommandBarStripElement[] {
            this.BarraModulo,
            this.commandBarStripElement3});
            this.BarraSuperior.Text = "";
            this.BarraSuperior.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // BarraModulo
            // 
            this.BarraModulo.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.BarraModulo.DisplayName = "commandBarStripElement2";
            this.BarraModulo.Items.AddRange(new Telerik.WinControls.UI.RadCommandBarBaseItem[] {
            this.btnITD});
            this.BarraModulo.Name = "commandBarStripElement2";
            this.BarraModulo.Text = "";
            this.BarraModulo.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // btnITD
            // 
            this.btnITD.AccessibleDescription = "T.I";
            this.btnITD.AccessibleName = "T.I";
            this.btnITD.AutoSize = false;
            this.btnITD.Bounds = new System.Drawing.Rectangle(0, 0, 80, 35);
            this.btnITD.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnITD.DisplayName = "Sistemas";
            this.btnITD.DrawText = true;
            this.btnITD.FitToSizeMode = Telerik.WinControls.RadFitToSizeMode.FitToParentContent;
            this.btnITD.Image = ((System.Drawing.Image)(resources.GetObject("btnITD.Image")));
            this.btnITD.Name = "btnITD";
            this.btnITD.Text = "ITD";
            this.btnITD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnITD.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnITD.ToolTipText = "Transportes";
            // 
            // commandBarStripElement3
            // 
            this.commandBarStripElement3.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarStripElement3.DisplayName = "commandBarStripElement3";
            this.commandBarStripElement3.Items.AddRange(new Telerik.WinControls.UI.RadCommandBarBaseItem[] {
            this.btnNuevo,
            this.btnEditar,
            this.btnRegistrar,
            this.btnAtras,
            this.btnAnular,
            this.btnEliminarRegistro,
            this.btnExportToExcel,
            this.btnAdjuntar,
            this.btnNotificar,
            this.btnCerrar});
            this.commandBarStripElement3.Name = "commandBarStripElement3";
            this.commandBarStripElement3.Text = "";
            this.commandBarStripElement3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.commandBarStripElement3.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // btnNuevo
            // 
            this.btnNuevo.AccessibleDescription = "Nuevo";
            this.btnNuevo.AccessibleName = "Nuevo";
            this.btnNuevo.AutoSize = false;
            this.btnNuevo.Bounds = new System.Drawing.Rectangle(0, 0, 70, 35);
            this.btnNuevo.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnNuevo.DisplayName = "Nuevo";
            this.btnNuevo.Enabled = false;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Text = "";
            this.btnNuevo.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnNuevo.ToolTipText = "Nuevo";
            // 
            // btnEditar
            // 
            this.btnEditar.AccessibleDescription = "Editar";
            this.btnEditar.AccessibleName = "Editar";
            this.btnEditar.AutoSize = false;
            this.btnEditar.Bounds = new System.Drawing.Rectangle(0, 0, 70, 35);
            this.btnEditar.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnEditar.DisplayName = "Editar";
            this.btnEditar.Enabled = false;
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Text = "";
            this.btnEditar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnEditar.ToolTipText = "Editar";
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.AccessibleDescription = "Registrar";
            this.btnRegistrar.AccessibleName = "Registrar";
            this.btnRegistrar.AutoSize = false;
            this.btnRegistrar.Bounds = new System.Drawing.Rectangle(0, 0, 70, 35);
            this.btnRegistrar.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnRegistrar.DisplayName = "Registrar";
            this.btnRegistrar.Image = ((System.Drawing.Image)(resources.GetObject("btnRegistrar.Image")));
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Text = "";
            this.btnRegistrar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnRegistrar.ToolTipText = "Registrar";
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // btnAtras
            // 
            this.btnAtras.AccessibleDescription = "Atras";
            this.btnAtras.AccessibleName = "Atras";
            this.btnAtras.AutoSize = false;
            this.btnAtras.Bounds = new System.Drawing.Rectangle(0, 0, 70, 35);
            this.btnAtras.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnAtras.DisplayName = "commandBarButton1";
            this.btnAtras.Enabled = false;
            this.btnAtras.Image = ((System.Drawing.Image)(resources.GetObject("btnAtras.Image")));
            this.btnAtras.Name = "btnAtras";
            this.btnAtras.Tag = "Atras";
            this.btnAtras.Text = "";
            this.btnAtras.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnAtras.ToolTipText = "Atras";
            // 
            // btnAnular
            // 
            this.btnAnular.AccessibleDescription = "Anular";
            this.btnAnular.AccessibleName = "Anular";
            this.btnAnular.AutoSize = false;
            this.btnAnular.Bounds = new System.Drawing.Rectangle(0, 0, 70, 35);
            this.btnAnular.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnAnular.DisplayName = "Anular";
            this.btnAnular.Enabled = false;
            this.btnAnular.Image = ((System.Drawing.Image)(resources.GetObject("btnAnular.Image")));
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.Text = "";
            this.btnAnular.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnAnular.ToolTipText = "Anular";
            // 
            // btnEliminarRegistro
            // 
            this.btnEliminarRegistro.AccessibleDescription = "Eliminar";
            this.btnEliminarRegistro.AccessibleName = "Eliminar";
            this.btnEliminarRegistro.AutoSize = false;
            this.btnEliminarRegistro.Bounds = new System.Drawing.Rectangle(0, 0, 70, 35);
            this.btnEliminarRegistro.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnEliminarRegistro.DisplayName = "Eliminar";
            this.btnEliminarRegistro.Enabled = false;
            this.btnEliminarRegistro.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminarRegistro.Image")));
            this.btnEliminarRegistro.Name = "btnEliminarRegistro";
            this.btnEliminarRegistro.Text = "";
            this.btnEliminarRegistro.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnEliminarRegistro.ToolTipText = "Eliminar Registro";
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.AccessibleDescription = "Exportar";
            this.btnExportToExcel.AccessibleName = "Exportar";
            this.btnExportToExcel.AutoSize = false;
            this.btnExportToExcel.Bounds = new System.Drawing.Rectangle(0, 0, 70, 35);
            this.btnExportToExcel.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnExportToExcel.DisplayName = "Exportar";
            this.btnExportToExcel.Enabled = false;
            this.btnExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportToExcel.Image")));
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Text = "";
            this.btnExportToExcel.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnExportToExcel.ToolTipText = "Exportar";
            // 
            // btnAdjuntar
            // 
            this.btnAdjuntar.AutoSize = false;
            this.btnAdjuntar.Bounds = new System.Drawing.Rectangle(0, 0, 70, 35);
            this.btnAdjuntar.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnAdjuntar.DisplayName = "Adjuntar";
            this.btnAdjuntar.Enabled = false;
            this.btnAdjuntar.Image = ((System.Drawing.Image)(resources.GetObject("btnAdjuntar.Image")));
            this.btnAdjuntar.Name = "btnAdjuntar";
            this.btnAdjuntar.Text = "";
            this.btnAdjuntar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // btnNotificar
            // 
            this.btnNotificar.AutoSize = false;
            this.btnNotificar.Bounds = new System.Drawing.Rectangle(0, 0, 70, 35);
            this.btnNotificar.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnNotificar.DisplayName = "Notificar";
            this.btnNotificar.Enabled = false;
            this.btnNotificar.Image = ((System.Drawing.Image)(resources.GetObject("btnNotificar.Image")));
            this.btnNotificar.Name = "btnNotificar";
            this.btnNotificar.Text = "";
            this.btnNotificar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // btnCerrar
            // 
            this.btnCerrar.AccessibleDescription = "Salir";
            this.btnCerrar.AccessibleName = "Salir";
            this.btnCerrar.AutoSize = false;
            this.btnCerrar.Bounds = new System.Drawing.Rectangle(0, 0, 70, 35);
            this.btnCerrar.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnCerrar.DisplayName = "Salir";
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Text = "";
            this.btnCerrar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnCerrar.ToolTipText = "Salir";
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // gbDetalle
            // 
            this.gbDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDetalle.Controls.Add(this.dgvDetalle);
            this.gbDetalle.Location = new System.Drawing.Point(8, 144);
            this.gbDetalle.Name = "gbDetalle";
            this.gbDetalle.Size = new System.Drawing.Size(1431, 498);
            this.gbDetalle.TabIndex = 215;
            this.gbDetalle.TabStop = false;
            this.gbDetalle.Text = "Listado";
            // 
            // dgvDetalle
            // 
            this.dgvDetalle.AllowUserToAddRows = false;
            this.dgvDetalle.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvDetalle.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDetalle.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDetalle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetalle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDetalle.ColumnHeadersHeight = 40;
            this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chId,
            this.chRazonSocial,
            this.chSede,
            this.chTipoHardware,
            this.chfecha,
            this.chDocumento,
            this.chEstadoDocumento,
            this.chItemDetalle,
            this.chPersonal,
            this.chArea,
            this.chHorasActivas,
            this.chHorasInactivas,
            this.chMotivoInactivoCodigo,
            this.chMotivoInactivo,
            this.chObservacion,
            this.chEstadoId});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDetalle.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetalle.GridColor = System.Drawing.SystemColors.Control;
            this.dgvDetalle.Location = new System.Drawing.Point(3, 16);
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.P_EsEditable = false;
            this.dgvDetalle.P_FormatoDecimal = null;
            this.dgvDetalle.P_FormatoFecha = null;
            this.dgvDetalle.P_NombreColCorrelativa = null;
            this.dgvDetalle.P_NombreTabla = null;
            this.dgvDetalle.P_NumeroDigitos = 0;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetalle.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDetalle.RowHeadersWidth = 10;
            this.dgvDetalle.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvDetalle.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvDetalle.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvDetalle.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvDetalle.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvDetalle.Size = new System.Drawing.Size(1425, 479);
            this.dgvDetalle.TabIndex = 179;
            this.dgvDetalle.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvDetalle_KeyUp);
            // 
            // chId
            // 
            this.chId.DataPropertyName = "Codigo";
            this.chId.HeaderText = "id";
            this.chId.Name = "chId";
            this.chId.ReadOnly = true;
            this.chId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.chId.Visible = false;
            this.chId.Width = 50;
            // 
            // chRazonSocial
            // 
            this.chRazonSocial.DataPropertyName = "RazonSocial";
            this.chRazonSocial.HeaderText = "Razón Social";
            this.chRazonSocial.Name = "chRazonSocial";
            // 
            // chSede
            // 
            this.chSede.DataPropertyName = "Sede";
            this.chSede.HeaderText = "Sede";
            this.chSede.Name = "chSede";
            // 
            // chTipoHardware
            // 
            this.chTipoHardware.DataPropertyName = "TipoHardware";
            this.chTipoHardware.HeaderText = "Tipo de Hardware";
            this.chTipoHardware.Name = "chTipoHardware";
            this.chTipoHardware.ReadOnly = true;
            // 
            // chfecha
            // 
            this.chfecha.DataPropertyName = "fecha";
            this.chfecha.HeaderText = "Fecha";
            this.chfecha.Name = "chfecha";
            this.chfecha.ReadOnly = true;
            this.chfecha.Width = 65;
            // 
            // chDocumento
            // 
            this.chDocumento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.chDocumento.DataPropertyName = "Documento";
            this.chDocumento.HeaderText = "Documento";
            this.chDocumento.Name = "chDocumento";
            this.chDocumento.ReadOnly = true;
            // 
            // chEstadoDocumento
            // 
            this.chEstadoDocumento.DataPropertyName = "EstadoDocumento";
            this.chEstadoDocumento.HeaderText = "Doc. Estado";
            this.chEstadoDocumento.Name = "chEstadoDocumento";
            this.chEstadoDocumento.ReadOnly = true;
            this.chEstadoDocumento.Width = 75;
            // 
            // chItemDetalle
            // 
            this.chItemDetalle.DataPropertyName = "Item";
            this.chItemDetalle.HeaderText = "Item";
            this.chItemDetalle.Name = "chItemDetalle";
            this.chItemDetalle.ReadOnly = true;
            this.chItemDetalle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.chItemDetalle.Visible = false;
            this.chItemDetalle.Width = 50;
            // 
            // chPersonal
            // 
            this.chPersonal.DataPropertyName = "personalAsignado";
            this.chPersonal.HeaderText = "Asginado a:";
            this.chPersonal.Name = "chPersonal";
            this.chPersonal.ReadOnly = true;
            this.chPersonal.Width = 250;
            // 
            // chArea
            // 
            this.chArea.DataPropertyName = "areaAsignada";
            this.chArea.HeaderText = "Área";
            this.chArea.Name = "chArea";
            this.chArea.ReadOnly = true;
            this.chArea.Width = 200;
            // 
            // chHorasActivas
            // 
            this.chHorasActivas.DataPropertyName = "HorasActivas";
            this.chHorasActivas.HeaderText = "Hrs. Activas";
            this.chHorasActivas.Name = "chHorasActivas";
            this.chHorasActivas.ReadOnly = true;
            this.chHorasActivas.Width = 65;
            // 
            // chHorasInactivas
            // 
            this.chHorasInactivas.DataPropertyName = "HorasInactivas";
            this.chHorasInactivas.HeaderText = "Hrs. Inactivas";
            this.chHorasInactivas.Name = "chHorasInactivas";
            this.chHorasInactivas.ReadOnly = true;
            this.chHorasInactivas.Width = 65;
            // 
            // chMotivoInactivoCodigo
            // 
            this.chMotivoInactivoCodigo.DataPropertyName = "MotivoInactivoCodigo";
            this.chMotivoInactivoCodigo.HeaderText = "Id. Motivo Inactivo";
            this.chMotivoInactivoCodigo.Name = "chMotivoInactivoCodigo";
            this.chMotivoInactivoCodigo.ReadOnly = true;
            this.chMotivoInactivoCodigo.Visible = false;
            // 
            // chMotivoInactivo
            // 
            this.chMotivoInactivo.DataPropertyName = "MotivoInactividad";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.chMotivoInactivo.DefaultCellStyle = dataGridViewCellStyle3;
            this.chMotivoInactivo.HeaderText = "Motivo de Inactividad";
            this.chMotivoInactivo.Name = "chMotivoInactivo";
            this.chMotivoInactivo.ReadOnly = true;
            this.chMotivoInactivo.Width = 200;
            // 
            // chObservacion
            // 
            this.chObservacion.DataPropertyName = "Observacion";
            this.chObservacion.HeaderText = "Observación";
            this.chObservacion.Name = "chObservacion";
            this.chObservacion.Width = 108;
            // 
            // chEstadoId
            // 
            this.chEstadoId.DataPropertyName = "Estado";
            this.chEstadoId.HeaderText = "Estado";
            this.chEstadoId.Name = "chEstadoId";
            this.chEstadoId.ReadOnly = true;
            this.chEstadoId.Width = 65;
            // 
            // bgwRegistrar
            // 
            this.bgwRegistrar.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwRegistrar_DoWork);
            this.bgwRegistrar.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwRegistrar_RunWorkerCompleted);
            // 
            // PartesDiariosDeEquipamientoByIdDevice
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1443, 667);
            this.Controls.Add(this.gbDetalle);
            this.Controls.Add(this.BarraPrincipal);
            this.Controls.Add(this.stsBarraEstado);
            this.Controls.Add(this.gbCabecera02);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PartesDiariosDeEquipamientoByIdDevice";
            this.Text = "Partes diarios por Dispositivo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PartesDiariosDeEquipamientoByIdDevice_FormClosing);
            this.Load += new System.EventHandler(this.PartesDiariosDeEquipamientoByIdDevice_Load);
            this.gbCabecera02.ResumeLayout(false);
            this.gbCabecera02.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboSemana)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPeriodo)).EndInit();
            this.stsBarraEstado.ResumeLayout(false);
            this.stsBarraEstado.PerformLayout();
            this.subMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BarraPrincipal)).EndInit();
            this.gbDetalle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbCabecera02;
        private System.Windows.Forms.StatusStrip stsBarraEstado;
        private System.Windows.Forms.ToolStripProgressBar pbar;
        private System.Windows.Forms.ContextMenuStrip subMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem btnEditarRegistro;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem btnVistaPrevia;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem btnVerDetalleDeDispositivos;
        private System.ComponentModel.BackgroundWorker bgwHilo;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private Telerik.WinControls.UI.RadCommandBar BarraPrincipal;
        private Telerik.WinControls.UI.CommandBarRowElement BarraSuperior;
        private Telerik.WinControls.UI.CommandBarStripElement BarraModulo;
        private Telerik.WinControls.UI.CommandBarButton btnITD;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement3;
        private Telerik.WinControls.UI.CommandBarButton btnNuevo;
        private Telerik.WinControls.UI.CommandBarButton btnEditar;
        private Telerik.WinControls.UI.CommandBarButton btnRegistrar;
        private Telerik.WinControls.UI.CommandBarButton btnAtras;
        private Telerik.WinControls.UI.CommandBarButton btnAnular;
        private Telerik.WinControls.UI.CommandBarButton btnEliminarRegistro;
        private Telerik.WinControls.UI.CommandBarButton btnExportToExcel;
        private Telerik.WinControls.UI.CommandBarButton btnAdjuntar;
        private Telerik.WinControls.UI.CommandBarButton btnNotificar;
        private Telerik.WinControls.UI.CommandBarButton btnCerrar;
        private MyControlsDataBinding.Controles.MyTextBoxSearchSimple txtDipositivoDescripcion;
        private System.Windows.Forms.Label label1;
        private MyControlsDataBinding.Controles.MyButtonSearchSimple btnDispositivoBuscar;
        private MyControlsDataBinding.Controles.MyTextBoxSearchSimple txtDipositivoCodigo;
        private Telerik.WinControls.UI.RadDropDownList cboSemana;
        private System.Windows.Forms.Label label2;
        private MyDataGridViewColumns.MyDataGridViewMaskedTextEditingControl txtFechaHasta;
        private MyDataGridViewColumns.MyDataGridViewMaskedTextEditingControl txtFechaDesde;
        private Telerik.WinControls.UI.RadSpinEditor txtPeriodo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.Label lblFechaDesde;
        private System.Windows.Forms.Label lblFechaHasta;
        private System.Windows.Forms.GroupBox gbDetalle;
        private MyControlsDataBinding.Controles.MyDataGridViewDetails dgvDetalle;
        private System.Windows.Forms.DataGridViewTextBoxColumn chId;
        private System.Windows.Forms.DataGridViewTextBoxColumn chRazonSocial;
        private System.Windows.Forms.DataGridViewTextBoxColumn chSede;
        private System.Windows.Forms.DataGridViewTextBoxColumn chTipoHardware;
        private System.Windows.Forms.DataGridViewTextBoxColumn chfecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn chDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn chEstadoDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn chItemDetalle;
        private System.Windows.Forms.DataGridViewTextBoxColumn chPersonal;
        private System.Windows.Forms.DataGridViewTextBoxColumn chArea;
        private System.Windows.Forms.DataGridViewTextBoxColumn chHorasActivas;
        private System.Windows.Forms.DataGridViewTextBoxColumn chHorasInactivas;
        private System.Windows.Forms.DataGridViewTextBoxColumn chMotivoInactivoCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn chMotivoInactivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn chObservacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn chEstadoId;
        private System.ComponentModel.BackgroundWorker bgwRegistrar;
    }
}