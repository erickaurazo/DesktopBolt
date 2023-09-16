namespace Asistencia
{
    partial class GoSistemaCatalogoFormularios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoSistemaCatalogoFormularios));
            this.BarraPrincipal = new Telerik.WinControls.UI.RadCommandBar();
            this.BarraSuperior = new Telerik.WinControls.UI.CommandBarRowElement();
            this.BarraModulo = new Telerik.WinControls.UI.CommandBarStripElement();
            this.btnSistema = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarStripElement3 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.btnActualizarLista = new Telerik.WinControls.UI.CommandBarButton();
            this.btnNuevo = new Telerik.WinControls.UI.CommandBarButton();
            this.btnEditar = new Telerik.WinControls.UI.CommandBarButton();
            this.btnSave = new Telerik.WinControls.UI.CommandBarButton();
            this.btnAtras = new Telerik.WinControls.UI.CommandBarButton();
            this.btnAnular = new Telerik.WinControls.UI.CommandBarButton();
            this.btnEliminarRegistro = new Telerik.WinControls.UI.CommandBarButton();
            this.btnHistorial = new Telerik.WinControls.UI.CommandBarButton();
            this.btnExportToExcel = new Telerik.WinControls.UI.CommandBarButton();
            this.btnCerrar = new Telerik.WinControls.UI.CommandBarButton();
            this.gbList = new System.Windows.Forms.GroupBox();
            this.cboModulos = new System.Windows.Forms.ComboBox();
            this.lblModulo = new System.Windows.Forms.Label();
            this.lblOrden = new System.Windows.Forms.Label();
            this.lblFormulario = new System.Windows.Forms.Label();
            this.txtFormulario = new Telerik.WinControls.UI.RadTextBox();
            this.cbOrder = new Telerik.WinControls.UI.RadDropDownButton();
            this.tvFormulario = new Telerik.WinControls.UI.RadTreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.gbEdition = new System.Windows.Forms.GroupBox();
            this.cboParentForm = new System.Windows.Forms.ComboBox();
            this.cboForm = new System.Windows.Forms.ComboBox();
            this.cboHierarchy = new System.Windows.Forms.ComboBox();
            this.chkIdModul = new System.Windows.Forms.CheckBox();
            this.txtNameInTheSystem = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.cboModulo = new System.Windows.Forms.ComboBox();
            this.cboEstatus = new System.Windows.Forms.ComboBox();
            this.txtFormCode = new System.Windows.Forms.TextBox();
            this.lblParentForm = new System.Windows.Forms.Label();
            this.lblFormName = new System.Windows.Forms.Label();
            this.lblHierarchy = new System.Windows.Forms.Label();
            this.lblIsModule = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblModule = new System.Windows.Forms.Label();
            this.lblFormCode = new System.Windows.Forms.Label();
            this.subMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.bgwHilo = new System.ComponentModel.BackgroundWorker();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
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
            this.bgwHiloInicio = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.BarraPrincipal)).BeginInit();
            this.gbList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFormulario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tvFormulario)).BeginInit();
            this.gbEdition.SuspendLayout();
            this.stsBarraEstado.SuspendLayout();
            this.SuspendLayout();
            // 
            // BarraPrincipal
            // 
            this.BarraPrincipal.Dock = System.Windows.Forms.DockStyle.Top;
            this.BarraPrincipal.Location = new System.Drawing.Point(0, 0);
            this.BarraPrincipal.Name = "BarraPrincipal";
            this.BarraPrincipal.Rows.AddRange(new Telerik.WinControls.UI.CommandBarRowElement[] {
            this.BarraSuperior});
            this.BarraPrincipal.Size = new System.Drawing.Size(963, 62);
            this.BarraPrincipal.TabIndex = 163;
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
            this.btnSistema});
            this.BarraModulo.Name = "commandBarStripElement2";
            this.BarraModulo.Text = "";
            this.BarraModulo.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // btnSistema
            // 
            this.btnSistema.AccessibleDescription = "Sistema";
            this.btnSistema.AccessibleName = "Sistema";
            this.btnSistema.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnSistema.DisplayName = "Sistema";
            this.btnSistema.DrawText = true;
            this.btnSistema.Image = ((System.Drawing.Image)(resources.GetObject("btnSistema.Image")));
            this.btnSistema.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSistema.Name = "btnSistema";
            this.btnSistema.Text = " Configuración del sistema   .";
            this.btnSistema.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSistema.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSistema.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnSistema.ToolTipText = "    Configuración del sistema";
            // 
            // commandBarStripElement3
            // 
            this.commandBarStripElement3.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarStripElement3.DisplayName = "commandBarStripElement3";
            this.commandBarStripElement3.Items.AddRange(new Telerik.WinControls.UI.RadCommandBarBaseItem[] {
            this.btnActualizarLista,
            this.btnNuevo,
            this.btnEditar,
            this.btnSave,
            this.btnAtras,
            this.btnAnular,
            this.btnEliminarRegistro,
            this.btnHistorial,
            this.btnExportToExcel,
            this.btnCerrar});
            this.commandBarStripElement3.Name = "commandBarStripElement3";
            this.commandBarStripElement3.Text = "";
            this.commandBarStripElement3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.commandBarStripElement3.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // btnActualizarLista
            // 
            this.btnActualizarLista.AccessibleDescription = "Actualizar Lista";
            this.btnActualizarLista.AccessibleName = "Actualizar Lista";
            this.btnActualizarLista.AutoSize = false;
            this.btnActualizarLista.Bounds = new System.Drawing.Rectangle(0, 0, 65, 35);
            this.btnActualizarLista.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnActualizarLista.DisplayName = "commandBarButton1";
            this.btnActualizarLista.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizarLista.Image")));
            this.btnActualizarLista.Name = "btnActualizarLista";
            this.btnActualizarLista.Text = "";
            this.btnActualizarLista.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnActualizarLista.ToolTipText = "Actualizar Lista";
            this.btnActualizarLista.Click += new System.EventHandler(this.btnActualizarLista_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.AccessibleDescription = "Nuevo";
            this.btnNuevo.AccessibleName = "Nuevo";
            this.btnNuevo.AutoSize = false;
            this.btnNuevo.Bounds = new System.Drawing.Rectangle(0, 0, 65, 35);
            this.btnNuevo.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnNuevo.DisplayName = "Nuevo";
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Text = "";
            this.btnNuevo.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnNuevo.ToolTipText = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.AccessibleDescription = "Editar";
            this.btnEditar.AccessibleName = "Editar";
            this.btnEditar.AutoSize = false;
            this.btnEditar.Bounds = new System.Drawing.Rectangle(0, 0, 65, 35);
            this.btnEditar.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnEditar.DisplayName = "Editar";
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Text = "";
            this.btnEditar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnEditar.ToolTipText = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleDescription = "Save";
            this.btnSave.AccessibleName = "Save";
            this.btnSave.AutoSize = false;
            this.btnSave.Bounds = new System.Drawing.Rectangle(0, 0, 65, 35);
            this.btnSave.DisplayName = "Save";
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Name = "btnSave";
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAtras
            // 
            this.btnAtras.AccessibleDescription = "Atras";
            this.btnAtras.AccessibleName = "Atras";
            this.btnAtras.AutoSize = false;
            this.btnAtras.Bounds = new System.Drawing.Rectangle(0, 0, 65, 35);
            this.btnAtras.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnAtras.DisplayName = "Atras";
            this.btnAtras.Image = ((System.Drawing.Image)(resources.GetObject("btnAtras.Image")));
            this.btnAtras.Name = "btnAtras";
            this.btnAtras.Text = "";
            this.btnAtras.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnAtras.Click += new System.EventHandler(this.btnAtras_Click);
            // 
            // btnAnular
            // 
            this.btnAnular.AccessibleDescription = "Anular";
            this.btnAnular.AccessibleName = "Anular";
            this.btnAnular.AutoSize = false;
            this.btnAnular.Bounds = new System.Drawing.Rectangle(0, 0, 65, 35);
            this.btnAnular.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnAnular.DisplayName = "Anular";
            this.btnAnular.Image = ((System.Drawing.Image)(resources.GetObject("btnAnular.Image")));
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.Text = "";
            this.btnAnular.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnAnular.ToolTipText = "Anular";
            this.btnAnular.Click += new System.EventHandler(this.btnAnular_Click);
            // 
            // btnEliminarRegistro
            // 
            this.btnEliminarRegistro.AccessibleDescription = "Eliminar";
            this.btnEliminarRegistro.AccessibleName = "Eliminar";
            this.btnEliminarRegistro.AutoSize = false;
            this.btnEliminarRegistro.Bounds = new System.Drawing.Rectangle(0, 0, 65, 35);
            this.btnEliminarRegistro.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnEliminarRegistro.DisplayName = "Eliminar";
            this.btnEliminarRegistro.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminarRegistro.Image")));
            this.btnEliminarRegistro.Name = "btnEliminarRegistro";
            this.btnEliminarRegistro.Text = "";
            this.btnEliminarRegistro.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnEliminarRegistro.ToolTipText = "Eliminar Registro";
            this.btnEliminarRegistro.Click += new System.EventHandler(this.btnEliminarRegistro_Click);
            // 
            // btnHistorial
            // 
            this.btnHistorial.AutoSize = false;
            this.btnHistorial.Bounds = new System.Drawing.Rectangle(0, 0, 65, 35);
            this.btnHistorial.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnHistorial.DisplayName = "Historial";
            this.btnHistorial.Image = ((System.Drawing.Image)(resources.GetObject("btnHistorial.Image")));
            this.btnHistorial.Name = "btnHistorial";
            this.btnHistorial.Text = "";
            this.btnHistorial.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnHistorial.ToolTipText = "Historial";
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.AccessibleDescription = "Exportar";
            this.btnExportToExcel.AccessibleName = "Exportar";
            this.btnExportToExcel.AutoSize = false;
            this.btnExportToExcel.Bounds = new System.Drawing.Rectangle(0, 0, 65, 35);
            this.btnExportToExcel.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnExportToExcel.DisplayName = "Exportar";
            this.btnExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportToExcel.Image")));
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Text = "";
            this.btnExportToExcel.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnExportToExcel.ToolTipText = "Exportar";
            // 
            // btnCerrar
            // 
            this.btnCerrar.AccessibleDescription = "Salir";
            this.btnCerrar.AccessibleName = "Salir";
            this.btnCerrar.AutoSize = false;
            this.btnCerrar.Bounds = new System.Drawing.Rectangle(0, 0, 65, 35);
            this.btnCerrar.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnCerrar.DisplayName = "Salir";
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Text = "";
            this.btnCerrar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnCerrar.ToolTipText = "Salir";
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // gbList
            // 
            this.gbList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbList.Controls.Add(this.cboModulos);
            this.gbList.Controls.Add(this.lblModulo);
            this.gbList.Controls.Add(this.lblOrden);
            this.gbList.Controls.Add(this.lblFormulario);
            this.gbList.Controls.Add(this.txtFormulario);
            this.gbList.Controls.Add(this.cbOrder);
            this.gbList.Controls.Add(this.tvFormulario);
            this.gbList.Location = new System.Drawing.Point(12, 43);
            this.gbList.Name = "gbList";
            this.gbList.Size = new System.Drawing.Size(475, 369);
            this.gbList.TabIndex = 164;
            this.gbList.TabStop = false;
            // 
            // cboModulos
            // 
            this.cboModulos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboModulos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModulos.FormattingEnabled = true;
            this.cboModulos.Location = new System.Drawing.Point(65, 46);
            this.cboModulos.Name = "cboModulos";
            this.cboModulos.Size = new System.Drawing.Size(404, 21);
            this.cboModulos.TabIndex = 18;
            this.cboModulos.SelectedIndexChanged += new System.EventHandler(this.cboModulos_SelectedIndexChanged);
            // 
            // lblModulo
            // 
            this.lblModulo.AutoSize = true;
            this.lblModulo.Location = new System.Drawing.Point(15, 49);
            this.lblModulo.Name = "lblModulo";
            this.lblModulo.Size = new System.Drawing.Size(48, 13);
            this.lblModulo.TabIndex = 193;
            this.lblModulo.Text = "Módulo :";
            // 
            // lblOrden
            // 
            this.lblOrden.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOrden.AutoSize = true;
            this.lblOrden.Location = new System.Drawing.Point(294, 21);
            this.lblOrden.Name = "lblOrden";
            this.lblOrden.Size = new System.Drawing.Size(42, 13);
            this.lblOrden.TabIndex = 2;
            this.lblOrden.Text = "Orden :";
            // 
            // lblFormulario
            // 
            this.lblFormulario.AutoSize = true;
            this.lblFormulario.Location = new System.Drawing.Point(2, 21);
            this.lblFormulario.Name = "lblFormulario";
            this.lblFormulario.Size = new System.Drawing.Size(61, 13);
            this.lblFormulario.TabIndex = 0;
            this.lblFormulario.Text = "Formulario :";
            // 
            // txtFormulario
            // 
            this.txtFormulario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFormulario.Location = new System.Drawing.Point(65, 19);
            this.txtFormulario.Name = "txtFormulario";
            this.txtFormulario.Size = new System.Drawing.Size(223, 20);
            this.txtFormulario.TabIndex = 0;
            this.txtFormulario.ThemeName = "VisualStudio2012Light";
            this.txtFormulario.TextChanged += new System.EventHandler(this.radTextBox1_TextChanged);
            // 
            // cbOrder
            // 
            this.cbOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbOrder.Location = new System.Drawing.Point(339, 19);
            this.cbOrder.Name = "cbOrder";
            this.cbOrder.Size = new System.Drawing.Size(130, 24);
            this.cbOrder.TabIndex = 1;
            this.cbOrder.ThemeName = "VisualStudio2012Light";
            // 
            // tvFormulario
            // 
            this.tvFormulario.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvFormulario.ImageList = this.imageList1;
            this.tvFormulario.LineColor = System.Drawing.SystemColors.Control;
            this.tvFormulario.Location = new System.Drawing.Point(3, 75);
            this.tvFormulario.Name = "tvFormulario";
            this.tvFormulario.Size = new System.Drawing.Size(469, 291);
            this.tvFormulario.TabIndex = 0;
            this.tvFormulario.ThemeName = "VisualStudio2012Light";
            this.tvFormulario.SelectedNodeChanged += new Telerik.WinControls.UI.RadTreeView.RadTreeViewEventHandler(this.tvFormulario_SelectedNodeChanged);
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
            // gbEdition
            // 
            this.gbEdition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbEdition.Controls.Add(this.cboParentForm);
            this.gbEdition.Controls.Add(this.cboForm);
            this.gbEdition.Controls.Add(this.cboHierarchy);
            this.gbEdition.Controls.Add(this.chkIdModul);
            this.gbEdition.Controls.Add(this.txtNameInTheSystem);
            this.gbEdition.Controls.Add(this.txtDescription);
            this.gbEdition.Controls.Add(this.cboModulo);
            this.gbEdition.Controls.Add(this.cboEstatus);
            this.gbEdition.Controls.Add(this.txtFormCode);
            this.gbEdition.Controls.Add(this.lblParentForm);
            this.gbEdition.Controls.Add(this.lblFormName);
            this.gbEdition.Controls.Add(this.lblHierarchy);
            this.gbEdition.Controls.Add(this.lblIsModule);
            this.gbEdition.Controls.Add(this.label5);
            this.gbEdition.Controls.Add(this.label4);
            this.gbEdition.Controls.Add(this.label3);
            this.gbEdition.Controls.Add(this.lblModule);
            this.gbEdition.Controls.Add(this.lblFormCode);
            this.gbEdition.Location = new System.Drawing.Point(490, 43);
            this.gbEdition.Name = "gbEdition";
            this.gbEdition.Size = new System.Drawing.Size(461, 369);
            this.gbEdition.TabIndex = 165;
            this.gbEdition.TabStop = false;
            // 
            // cboParentForm
            // 
            this.cboParentForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboParentForm.Enabled = false;
            this.cboParentForm.FormattingEnabled = true;
            this.cboParentForm.Location = new System.Drawing.Point(121, 142);
            this.cboParentForm.Name = "cboParentForm";
            this.cboParentForm.Size = new System.Drawing.Size(328, 21);
            this.cboParentForm.TabIndex = 17;
            // 
            // cboForm
            // 
            this.cboForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboForm.Enabled = false;
            this.cboForm.FormattingEnabled = true;
            this.cboForm.Location = new System.Drawing.Point(122, 230);
            this.cboForm.Name = "cboForm";
            this.cboForm.Size = new System.Drawing.Size(327, 21);
            this.cboForm.TabIndex = 16;
            // 
            // cboHierarchy
            // 
            this.cboHierarchy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHierarchy.Enabled = false;
            this.cboHierarchy.FormattingEnabled = true;
            this.cboHierarchy.Location = new System.Drawing.Point(122, 197);
            this.cboHierarchy.Name = "cboHierarchy";
            this.cboHierarchy.Size = new System.Drawing.Size(327, 21);
            this.cboHierarchy.TabIndex = 15;
            // 
            // chkIdModul
            // 
            this.chkIdModul.AutoSize = true;
            this.chkIdModul.Location = new System.Drawing.Point(122, 174);
            this.chkIdModul.Name = "chkIdModul";
            this.chkIdModul.Size = new System.Drawing.Size(15, 14);
            this.chkIdModul.TabIndex = 14;
            this.chkIdModul.UseVisualStyleBackColor = true;
            // 
            // txtNameInTheSystem
            // 
            this.txtNameInTheSystem.Location = new System.Drawing.Point(121, 113);
            this.txtNameInTheSystem.Name = "txtNameInTheSystem";
            this.txtNameInTheSystem.Size = new System.Drawing.Size(328, 20);
            this.txtNameInTheSystem.TabIndex = 13;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(121, 87);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(329, 20);
            this.txtDescription.TabIndex = 12;
            // 
            // cboModulo
            // 
            this.cboModulo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModulo.Enabled = false;
            this.cboModulo.FormattingEnabled = true;
            this.cboModulo.Location = new System.Drawing.Point(121, 55);
            this.cboModulo.Name = "cboModulo";
            this.cboModulo.Size = new System.Drawing.Size(328, 21);
            this.cboModulo.TabIndex = 11;
            // 
            // cboEstatus
            // 
            this.cboEstatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstatus.Enabled = false;
            this.cboEstatus.FormattingEnabled = true;
            this.cboEstatus.Location = new System.Drawing.Point(329, 23);
            this.cboEstatus.Name = "cboEstatus";
            this.cboEstatus.Size = new System.Drawing.Size(121, 21);
            this.cboEstatus.TabIndex = 10;
            // 
            // txtFormCode
            // 
            this.txtFormCode.Location = new System.Drawing.Point(121, 24);
            this.txtFormCode.Name = "txtFormCode";
            this.txtFormCode.ReadOnly = true;
            this.txtFormCode.Size = new System.Drawing.Size(156, 20);
            this.txtFormCode.TabIndex = 9;
            // 
            // lblParentForm
            // 
            this.lblParentForm.AutoSize = true;
            this.lblParentForm.Location = new System.Drawing.Point(52, 145);
            this.lblParentForm.Name = "lblParentForm";
            this.lblParentForm.Size = new System.Drawing.Size(66, 13);
            this.lblParentForm.TabIndex = 8;
            this.lblParentForm.Text = "BarraPadre :";
            // 
            // lblFormName
            // 
            this.lblFormName.AutoSize = true;
            this.lblFormName.Location = new System.Drawing.Point(53, 230);
            this.lblFormName.Name = "lblFormName";
            this.lblFormName.Size = new System.Drawing.Size(61, 13);
            this.lblFormName.TabIndex = 7;
            this.lblFormName.Text = "Formulario :";
            // 
            // lblHierarchy
            // 
            this.lblHierarchy.AutoSize = true;
            this.lblHierarchy.Location = new System.Drawing.Point(60, 200);
            this.lblHierarchy.Name = "lblHierarchy";
            this.lblHierarchy.Size = new System.Drawing.Size(56, 13);
            this.lblHierarchy.TabIndex = 6;
            this.lblHierarchy.Text = "Jerarquia :";
            // 
            // lblIsModule
            // 
            this.lblIsModule.AutoSize = true;
            this.lblIsModule.Location = new System.Drawing.Point(53, 174);
            this.lblIsModule.Name = "lblIsModule";
            this.lblIsModule.Size = new System.Drawing.Size(62, 13);
            this.lblIsModule.TabIndex = 5;
            this.lblIsModule.Text = "Es módulo :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(280, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Estado :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Nombre en sistema :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Descripción :";
            // 
            // lblModule
            // 
            this.lblModule.AutoSize = true;
            this.lblModule.Location = new System.Drawing.Point(67, 58);
            this.lblModule.Name = "lblModule";
            this.lblModule.Size = new System.Drawing.Size(48, 13);
            this.lblModule.TabIndex = 1;
            this.lblModule.Text = "Módulo :";
            // 
            // lblFormCode
            // 
            this.lblFormCode.AutoSize = true;
            this.lblFormCode.Location = new System.Drawing.Point(22, 26);
            this.lblFormCode.Name = "lblFormCode";
            this.lblFormCode.Size = new System.Drawing.Size(93, 13);
            this.lblFormCode.TabIndex = 0;
            this.lblFormCode.Text = "Formulario código:";
            // 
            // subMenu
            // 
            this.subMenu.Name = "subMenu";
            this.subMenu.Size = new System.Drawing.Size(61, 4);
            this.subMenu.Opening += new System.ComponentModel.CancelEventHandler(this.subMenu_Opening);
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
            this.stsBarraEstado.Location = new System.Drawing.Point(0, 415);
            this.stsBarraEstado.Name = "stsBarraEstado";
            this.stsBarraEstado.Size = new System.Drawing.Size(963, 22);
            this.stsBarraEstado.TabIndex = 191;
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
            // bgwHiloInicio
            // 
            this.bgwHiloInicio.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwHiloInicio_DoWork);
            this.bgwHiloInicio.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwHiloInicio_RunWorkerCompleted);
            // 
            // GoSistemaCatalogoFormularios
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(963, 437);
            this.Controls.Add(this.stsBarraEstado);
            this.Controls.Add(this.gbEdition);
            this.Controls.Add(this.gbList);
            this.Controls.Add(this.BarraPrincipal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GoSistemaCatalogoFormularios";
            this.Text = "Catálogo | Privilegios | Administración y registro";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Formularios_FormClosing);
            this.Load += new System.EventHandler(this.Formulario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BarraPrincipal)).EndInit();
            this.gbList.ResumeLayout(false);
            this.gbList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFormulario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tvFormulario)).EndInit();
            this.gbEdition.ResumeLayout(false);
            this.gbEdition.PerformLayout();
            this.stsBarraEstado.ResumeLayout(false);
            this.stsBarraEstado.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadCommandBar BarraPrincipal;
        private Telerik.WinControls.UI.CommandBarRowElement BarraSuperior;
        private Telerik.WinControls.UI.CommandBarStripElement BarraModulo;
        private Telerik.WinControls.UI.CommandBarButton btnSistema;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement3;
        private Telerik.WinControls.UI.CommandBarButton btnNuevo;
        private Telerik.WinControls.UI.CommandBarButton btnActualizarLista;
        private Telerik.WinControls.UI.CommandBarButton btnAtras;
        private Telerik.WinControls.UI.CommandBarButton btnEditar;
        private Telerik.WinControls.UI.CommandBarButton btnAnular;
        private Telerik.WinControls.UI.CommandBarButton btnEliminarRegistro;
        private Telerik.WinControls.UI.CommandBarButton btnExportToExcel;
        private Telerik.WinControls.UI.CommandBarButton btnCerrar;
        private Telerik.WinControls.UI.CommandBarButton btnHistorial;
        private System.Windows.Forms.GroupBox gbList;
        private System.Windows.Forms.GroupBox gbEdition;
        private System.Windows.Forms.ContextMenuStrip subMenu;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private System.ComponentModel.BackgroundWorker bgwHilo;
        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
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
        private Telerik.WinControls.UI.RadTreeView tvFormulario;
        private Telerik.WinControls.UI.RadDropDownButton cbOrder;
        private Telerik.WinControls.UI.RadTextBox txtFormulario;
        private System.Windows.Forms.ImageList imageList1;
        private Telerik.WinControls.UI.CommandBarButton btnSave;
        private System.Windows.Forms.Label lblFormulario;
        private System.Windows.Forms.Label lblOrden;
        private System.Windows.Forms.Label lblParentForm;
        private System.Windows.Forms.Label lblFormName;
        private System.Windows.Forms.Label lblHierarchy;
        private System.Windows.Forms.Label lblIsModule;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblModule;
        private System.Windows.Forms.Label lblFormCode;
        private System.Windows.Forms.ComboBox cboEstatus;
        private System.Windows.Forms.TextBox txtFormCode;
        private System.Windows.Forms.ComboBox cboModulo;
        private System.Windows.Forms.CheckBox chkIdModul;
        private System.Windows.Forms.TextBox txtNameInTheSystem;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.ComboBox cboHierarchy;
        private System.Windows.Forms.ComboBox cboParentForm;
        private System.Windows.Forms.ComboBox cboForm;
        private System.ComponentModel.BackgroundWorker bgwHiloInicio;
        private System.Windows.Forms.Label lblModulo;
        private System.Windows.Forms.ComboBox cboModulos;
    }
}