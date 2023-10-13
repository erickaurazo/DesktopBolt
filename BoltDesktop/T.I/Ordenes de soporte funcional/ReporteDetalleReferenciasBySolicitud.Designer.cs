namespace ComparativoHorasVisualSATNISIRA.T.I
{
    partial class ReporteDetalleReferenciasBySolicitud
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteDetalleReferenciasBySolicitud));
            Telerik.WinControls.UI.GridViewDateTimeColumn gridViewDateTimeColumn1 = new Telerik.WinControls.UI.GridViewDateTimeColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            this.btnMenu = new Telerik.WinControls.UI.RadCommandBar();
            this.BarraSuperior = new Telerik.WinControls.UI.CommandBarRowElement();
            this.BarraModulo = new Telerik.WinControls.UI.CommandBarStripElement();
            this.btnTI = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarStripElement3 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.btnExportar = new Telerik.WinControls.UI.CommandBarButton();
            this.btnSalir = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarRowElement1 = new Telerik.WinControls.UI.CommandBarRowElement();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.bgwHilo = new System.ComponentModel.BackgroundWorker();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.stsBarraEstado = new System.Windows.Forms.StatusStrip();
            this.progressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.gbList = new Telerik.WinControls.UI.RadGroupBox();
            this.dgvListado = new Telerik.WinControls.UI.RadGridView();
            this.gbDispositivo = new Telerik.WinControls.UI.RadGroupBox();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.txtDispositivoDescripcion = new MyControlsDataBinding.Controles.MyTextBoxSearchSimple(this.components);
            this.txtDispositivoCodigo = new MyControlsDataBinding.Controles.MyTextBoxSearchSimple(this.components);
            this.btnDispositivoBajaBuscar = new MyControlsDataBinding.Controles.MyButtonSearchSimple(this.components);
            this.lblDispositivo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btnMenu)).BeginInit();
            this.stsBarraEstado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbList)).BeginInit();
            this.gbList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListado.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbDispositivo)).BeginInit();
            this.gbDispositivo.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnMenu
            // 
            this.btnMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMenu.Location = new System.Drawing.Point(0, 0);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Rows.AddRange(new Telerik.WinControls.UI.CommandBarRowElement[] {
            this.BarraSuperior,
            this.commandBarRowElement1});
            this.btnMenu.Size = new System.Drawing.Size(1008, 62);
            this.btnMenu.TabIndex = 217;
            this.btnMenu.ThemeName = "VisualStudio2012Light";
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
            this.btnTI});
            this.BarraModulo.Name = "commandBarStripElement2";
            this.BarraModulo.Text = "";
            this.BarraModulo.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // btnTI
            // 
            this.btnTI.AccessibleDescription = "T.I ";
            this.btnTI.AccessibleName = "T.I ";
            this.btnTI.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnTI.DisplayName = "T.I ";
            this.btnTI.DrawText = true;
            this.btnTI.Image = ((System.Drawing.Image)(resources.GetObject("btnTI.Image")));
            this.btnTI.ImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTI.Name = "btnTI";
            this.btnTI.Text = "Tecnologías de información ";
            this.btnTI.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTI.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTI.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnTI.ToolTipText = "T.I ";
            // 
            // commandBarStripElement3
            // 
            this.commandBarStripElement3.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarStripElement3.DisplayName = "commandBarStripElement3";
            this.commandBarStripElement3.Items.AddRange(new Telerik.WinControls.UI.RadCommandBarBaseItem[] {
            this.btnExportar,
            this.btnSalir});
            this.commandBarStripElement3.Name = "commandBarStripElement3";
            this.commandBarStripElement3.Text = "";
            this.commandBarStripElement3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.commandBarStripElement3.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // btnExportar
            // 
            this.btnExportar.AccessibleDescription = "Exportar";
            this.btnExportar.AccessibleName = "Exportar";
            this.btnExportar.AutoSize = false;
            this.btnExportar.Bounds = new System.Drawing.Rectangle(0, 0, 75, 35);
            this.btnExportar.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnExportar.DisplayName = "Exportar";
            this.btnExportar.Image = ((System.Drawing.Image)(resources.GetObject("btnExportar.Image")));
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Text = "";
            this.btnExportar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnExportar.ToolTipText = "Exportar";
            // 
            // btnSalir
            // 
            this.btnSalir.AccessibleDescription = "Salir";
            this.btnSalir.AccessibleName = "Salir";
            this.btnSalir.AutoSize = false;
            this.btnSalir.Bounds = new System.Drawing.Rectangle(0, 0, 75, 35);
            this.btnSalir.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnSalir.DisplayName = "Salir";
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Text = "";
            this.btnSalir.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnSalir.ToolTipText = "Salir";
            // 
            // commandBarRowElement1
            // 
            this.commandBarRowElement1.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarRowElement1.MinSize = new System.Drawing.Size(25, 25);
            this.commandBarRowElement1.Text = "";
            this.commandBarRowElement1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // bgwHilo
            // 
            this.bgwHilo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwHilo_DoWork);
            this.bgwHilo.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwHilo_RunWorkerCompleted);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "actualizar.png");
            this.imageList1.Images.SetKeyName(1, "Agrupar documentos.png");
            this.imageList1.Images.SetKeyName(2, "anular.png");
            this.imageList1.Images.SetKeyName(3, "Aprobed.png");
            this.imageList1.Images.SetKeyName(4, "Atras.png");
            this.imageList1.Images.SetKeyName(5, "Buscar documento.png");
            this.imageList1.Images.SetKeyName(6, "Buscar QR.png");
            this.imageList1.Images.SetKeyName(7, "Buscar.png");
            this.imageList1.Images.SetKeyName(8, "Cambiar estado detalle.png");
            this.imageList1.Images.SetKeyName(9, "CANCELAR.png");
            this.imageList1.Images.SetKeyName(10, "codigo de barra.png");
            this.imageList1.Images.SetKeyName(11, "ConsultarMini.png");
            this.imageList1.Images.SetKeyName(12, "copiar.png");
            this.imageList1.Images.SetKeyName(13, "duplicar.png");
            this.imageList1.Images.SetKeyName(14, "editar.png");
            this.imageList1.Images.SetKeyName(15, "elimina.png");
            this.imageList1.Images.SetKeyName(16, "Eliminar detalle.png");
            this.imageList1.Images.SetKeyName(17, "excel.png");
            this.imageList1.Images.SetKeyName(18, "FILTRAR.png");
            this.imageList1.Images.SetKeyName(19, "FiltrarMini.png");
            this.imageList1.Images.SetKeyName(20, "FIND3.png");
            this.imageList1.Images.SetKeyName(21, "Grabar(1).png");
            this.imageList1.Images.SetKeyName(22, "Grabar(2).png");
            this.imageList1.Images.SetKeyName(23, "grabar.png");
            this.imageList1.Images.SetKeyName(24, "GrabarMini (2).png");
            this.imageList1.Images.SetKeyName(25, "GrabarMini.png");
            this.imageList1.Images.SetKeyName(26, "GUARDAR Y SEGUIR EDITANDO.png");
            this.imageList1.Images.SetKeyName(27, "GUARDAR.png");
            this.imageList1.Images.SetKeyName(28, "historial.png");
            this.imageList1.Images.SetKeyName(29, "icono Modulo.png");
            this.imageList1.Images.SetKeyName(30, "importar.png");
            this.imageList1.Images.SetKeyName(31, "imprime.png");
            this.imageList1.Images.SetKeyName(32, "license-16.png");
            this.imageList1.Images.SetKeyName(33, "logo4.png");
            this.imageList1.Images.SetKeyName(34, "mensaje.png");
            this.imageList1.Images.SetKeyName(35, "Modificar Azul.png");
            this.imageList1.Images.SetKeyName(36, "nuevo.png");
            this.imageList1.Images.SetKeyName(37, "PDF.png");
            this.imageList1.Images.SetKeyName(38, "pegar.png");
            this.imageList1.Images.SetKeyName(39, "Point Location.png");
            this.imageList1.Images.SetKeyName(40, "QR Azul.png");
            this.imageList1.Images.SetKeyName(41, "i");
            this.imageList1.Images.SetKeyName(42, "quitar detalle.png");
            this.imageList1.Images.SetKeyName(43, "QUITAR.png");
            this.imageList1.Images.SetKeyName(44, "QuitarMini.png");
            this.imageList1.Images.SetKeyName(45, "refrescar.png");
            this.imageList1.Images.SetKeyName(46, "RESALTAR.png");
            this.imageList1.Images.SetKeyName(47, "salir.png");
            this.imageList1.Images.SetKeyName(48, "Ver documentos agrupados.png");
            // 
            // stsBarraEstado
            // 
            this.stsBarraEstado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar1});
            this.stsBarraEstado.Location = new System.Drawing.Point(0, 364);
            this.stsBarraEstado.Name = "stsBarraEstado";
            this.stsBarraEstado.Size = new System.Drawing.Size(1008, 22);
            this.stsBarraEstado.TabIndex = 219;
            // 
            // progressBar1
            // 
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // gbList
            // 
            this.gbList.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.gbList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbList.Controls.Add(this.dgvListado);
            this.gbList.HeaderText = "Listado";
            this.gbList.Location = new System.Drawing.Point(3, 111);
            this.gbList.Name = "gbList";
            this.gbList.Size = new System.Drawing.Size(993, 250);
            this.gbList.TabIndex = 220;
            this.gbList.Text = "Listado";
            this.gbList.ThemeName = "Windows8";
            // 
            // dgvListado
            // 
            this.dgvListado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.dgvListado.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvListado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvListado.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvListado.ForeColor = System.Drawing.Color.Black;
            this.dgvListado.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvListado.Location = new System.Drawing.Point(2, 18);
            // 
            // dgvListado
            // 
            this.dgvListado.MasterTemplate.AllowAddNewRow = false;
            this.dgvListado.MasterTemplate.AutoGenerateColumns = false;
            this.dgvListado.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewDateTimeColumn1.EnableExpressionEditor = false;
            gridViewDateTimeColumn1.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.ShortDate;
            gridViewDateTimeColumn1.FieldName = "fecha";
            gridViewDateTimeColumn1.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            gridViewDateTimeColumn1.FormatString = "{0:d}";
            gridViewDateTimeColumn1.HeaderText = "Fecha";
            gridViewDateTimeColumn1.Name = "chfecha";
            gridViewDateTimeColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewDateTimeColumn1.Width = 184;
            gridViewTextBoxColumn1.EnableExpressionEditor = false;
            gridViewTextBoxColumn1.FieldName = "modulo";
            gridViewTextBoxColumn1.HeaderText = "Módulo";
            gridViewTextBoxColumn1.Name = "chmodulo";
            gridViewTextBoxColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewTextBoxColumn1.Width = 291;
            gridViewTextBoxColumn2.EnableExpressionEditor = false;
            gridViewTextBoxColumn2.FieldName = "tabla";
            gridViewTextBoxColumn2.HeaderText = "Tabla";
            gridViewTextBoxColumn2.IsVisible = false;
            gridViewTextBoxColumn2.Name = "chtabla";
            gridViewTextBoxColumn2.Width = 108;
            gridViewTextBoxColumn3.EnableExpressionEditor = false;
            gridViewTextBoxColumn3.FieldName = "documento";
            gridViewTextBoxColumn3.HeaderText = "Documento";
            gridViewTextBoxColumn3.Name = "chdocumento";
            gridViewTextBoxColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewTextBoxColumn3.Width = 304;
            gridViewTextBoxColumn4.EnableExpressionEditor = false;
            gridViewTextBoxColumn4.FieldName = "iddispositivo";
            gridViewTextBoxColumn4.HeaderText = "Cod. Disp. ERP ";
            gridViewTextBoxColumn4.IsVisible = false;
            gridViewTextBoxColumn4.Name = "chiddispositivo";
            gridViewTextBoxColumn4.Width = 119;
            gridViewTextBoxColumn5.EnableExpressionEditor = false;
            gridViewTextBoxColumn5.FieldName = "idEstado";
            gridViewTextBoxColumn5.HeaderText = "Cod. Estado";
            gridViewTextBoxColumn5.Name = "chidEstado";
            gridViewTextBoxColumn5.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            gridViewTextBoxColumn5.Width = 195;
            this.dgvListado.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewDateTimeColumn1,
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5});
            this.dgvListado.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgvListado.MasterTemplate.EnableFiltering = true;
            this.dgvListado.MasterTemplate.MultiSelect = true;
            this.dgvListado.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect;
            this.dgvListado.MasterTemplate.ShowGroupedColumns = true;
            this.dgvListado.Name = "dgvListado";
            this.dgvListado.ReadOnly = true;
            this.dgvListado.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvListado.Size = new System.Drawing.Size(989, 230);
            this.dgvListado.TabIndex = 163;
            this.dgvListado.ThemeName = "VisualStudio2012Light";
            // 
            // gbDispositivo
            // 
            this.gbDispositivo.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.gbDispositivo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDispositivo.Controls.Add(this.btnConsultar);
            this.gbDispositivo.Controls.Add(this.txtDispositivoDescripcion);
            this.gbDispositivo.Controls.Add(this.txtDispositivoCodigo);
            this.gbDispositivo.Controls.Add(this.btnDispositivoBajaBuscar);
            this.gbDispositivo.Controls.Add(this.lblDispositivo);
            this.gbDispositivo.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.gbDispositivo.HeaderText = "Listado";
            this.gbDispositivo.Location = new System.Drawing.Point(3, 42);
            this.gbDispositivo.Name = "gbDispositivo";
            this.gbDispositivo.Size = new System.Drawing.Size(991, 63);
            this.gbDispositivo.TabIndex = 221;
            this.gbDispositivo.Text = "Listado";
            this.gbDispositivo.ThemeName = "Windows8";
            // 
            // btnConsultar
            // 
            this.btnConsultar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConsultar.Image = ((System.Drawing.Image)(resources.GetObject("btnConsultar.Image")));
            this.btnConsultar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsultar.Location = new System.Drawing.Point(894, 30);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(91, 28);
            this.btnConsultar.TabIndex = 277;
            this.btnConsultar.Text = "    Consultar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // txtDispositivoDescripcion
            // 
            this.txtDispositivoDescripcion.BackColor = System.Drawing.Color.White;
            this.txtDispositivoDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDispositivoDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtDispositivoDescripcion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDispositivoDescripcion.Location = new System.Drawing.Point(149, 33);
            this.txtDispositivoDescripcion.Name = "txtDispositivoDescripcion";
            this.txtDispositivoDescripcion.P_BotonEnlace = null;
            this.txtDispositivoDescripcion.P_BuscarSoloCodigoExacto = false;
            this.txtDispositivoDescripcion.P_EsEditable = false;
            this.txtDispositivoDescripcion.P_EsModificable = false;
            this.txtDispositivoDescripcion.P_EsPrimaryKey = false;
            this.txtDispositivoDescripcion.P_ExigeInformacion = false;
            this.txtDispositivoDescripcion.P_NombreColumna = null;
            this.txtDispositivoDescripcion.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtDispositivoDescripcion.ReadOnly = true;
            this.txtDispositivoDescripcion.Size = new System.Drawing.Size(739, 20);
            this.txtDispositivoDescripcion.TabIndex = 275;
            this.txtDispositivoDescripcion.Text = "CELULAR XIAOMI RED MI 8 PRO";
            // 
            // txtDispositivoCodigo
            // 
            this.txtDispositivoCodigo.BackColor = System.Drawing.Color.White;
            this.txtDispositivoCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDispositivoCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtDispositivoCodigo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDispositivoCodigo.Location = new System.Drawing.Point(92, 33);
            this.txtDispositivoCodigo.MaxLength = 9;
            this.txtDispositivoCodigo.Name = "txtDispositivoCodigo";
            this.txtDispositivoCodigo.P_BotonEnlace = this.btnDispositivoBajaBuscar;
            this.txtDispositivoCodigo.P_BuscarSoloCodigoExacto = false;
            this.txtDispositivoCodigo.P_EsEditable = true;
            this.txtDispositivoCodigo.P_EsModificable = true;
            this.txtDispositivoCodigo.P_EsPrimaryKey = false;
            this.txtDispositivoCodigo.P_ExigeInformacion = false;
            this.txtDispositivoCodigo.P_NombreColumna = null;
            this.txtDispositivoCodigo.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtDispositivoCodigo.Size = new System.Drawing.Size(51, 20);
            this.txtDispositivoCodigo.TabIndex = 274;
            this.txtDispositivoCodigo.Text = "390";
            this.txtDispositivoCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnDispositivoBajaBuscar
            // 
            this.btnDispositivoBajaBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnDispositivoBajaBuscar.Image")));
            this.btnDispositivoBajaBuscar.Location = new System.Drawing.Point(2, 29);
            this.btnDispositivoBajaBuscar.Name = "btnDispositivoBajaBuscar";
            this.btnDispositivoBajaBuscar.P_CampoCodigo = "id";
            this.btnDispositivoBajaBuscar.P_CampoDescripcion = "(tipodispositivo + \' | \'  +rtrim(nombres) + \' | \'+ rtrim(item))";
            this.btnDispositivoBajaBuscar.P_EsEditable = true;
            this.btnDispositivoBajaBuscar.P_EsModificable = true;
            this.btnDispositivoBajaBuscar.P_FilterByTextBox = null;
            this.btnDispositivoBajaBuscar.P_TablaConsulta = "SAS_ListadoDeDispositivos";
            this.btnDispositivoBajaBuscar.P_TextBoxCodigo = this.txtDispositivoCodigo;
            this.btnDispositivoBajaBuscar.P_TextBoxDescripcion = this.txtDispositivoDescripcion;
            this.btnDispositivoBajaBuscar.P_TituloFormulario = "... Buscar dispositivo";
            this.btnDispositivoBajaBuscar.Size = new System.Drawing.Size(24, 23);
            this.btnDispositivoBajaBuscar.TabIndex = 276;
            this.btnDispositivoBajaBuscar.UseVisualStyleBackColor = true;
            this.btnDispositivoBajaBuscar.Visible = false;
            // 
            // lblDispositivo
            // 
            this.lblDispositivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDispositivo.Location = new System.Drawing.Point(9, 26);
            this.lblDispositivo.Name = "lblDispositivo";
            this.lblDispositivo.Size = new System.Drawing.Size(77, 32);
            this.lblDispositivo.TabIndex = 273;
            this.lblDispositivo.Text = "Dispositivo :";
            this.lblDispositivo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ReporteDetalleReferenciasBySolicitud
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1008, 386);
            this.Controls.Add(this.gbDispositivo);
            this.Controls.Add(this.gbList);
            this.Controls.Add(this.stsBarraEstado);
            this.Controls.Add(this.btnMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReporteDetalleReferenciasBySolicitud";
            this.Text = "Dispositivo | Reporte de referencias por Dispositivo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ReporteDetalleReferenciasBySolicitud_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnMenu)).EndInit();
            this.stsBarraEstado.ResumeLayout(false);
            this.stsBarraEstado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbList)).EndInit();
            this.gbList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListado.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbDispositivo)).EndInit();
            this.gbDispositivo.ResumeLayout(false);
            this.gbDispositivo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadCommandBar btnMenu;
        private Telerik.WinControls.UI.CommandBarRowElement BarraSuperior;
        private Telerik.WinControls.UI.CommandBarStripElement BarraModulo;
        private Telerik.WinControls.UI.CommandBarButton btnTI;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement3;
        private Telerik.WinControls.UI.CommandBarButton btnExportar;
        private Telerik.WinControls.UI.CommandBarButton btnSalir;
        private Telerik.WinControls.UI.CommandBarRowElement commandBarRowElement1;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private System.ComponentModel.BackgroundWorker bgwHilo;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.StatusStrip stsBarraEstado;
        private System.Windows.Forms.ToolStripProgressBar progressBar1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private Telerik.WinControls.UI.RadGroupBox gbList;
        private Telerik.WinControls.UI.RadGridView dgvListado;
        private Telerik.WinControls.UI.RadGroupBox gbDispositivo;
        private MyControlsDataBinding.Controles.MyTextBoxSearchSimple txtDispositivoDescripcion;
        private MyControlsDataBinding.Controles.MyTextBoxSearchSimple txtDispositivoCodigo;
        private MyControlsDataBinding.Controles.MyButtonSearchSimple btnDispositivoBajaBuscar;
        private System.Windows.Forms.Label lblDispositivo;
        private System.Windows.Forms.Button btnConsultar;
    }
}