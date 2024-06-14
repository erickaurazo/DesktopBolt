namespace ComparativoHorasVisualSATNISIRA.Almacen
{
    partial class ReporteStockEPP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteStockEPP));
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn10 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            this.bgwHilo = new System.ComponentModel.BackgroundWorker();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.stsBarraEstado = new System.Windows.Forms.StatusStrip();
            this.lblUserNames = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCodeUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblFullName = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.subMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.BarraPrincipal = new Telerik.WinControls.UI.RadCommandBar();
            this.BarraSuperior = new Telerik.WinControls.UI.CommandBarRowElement();
            this.BarraModulo = new Telerik.WinControls.UI.CommandBarStripElement();
            this.btnAlmacen = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarStripElement3 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.btnActualizar = new Telerik.WinControls.UI.CommandBarButton();
            this.btnActivarFiltro = new Telerik.WinControls.UI.CommandBarButton();
            this.btnNuevo = new Telerik.WinControls.UI.CommandBarButton();
            this.btnAtras = new Telerik.WinControls.UI.CommandBarButton();
            this.btnEditar = new Telerik.WinControls.UI.CommandBarButton();
            this.btnGrabar = new Telerik.WinControls.UI.CommandBarButton();
            this.btnResaltarResultados = new Telerik.WinControls.UI.CommandBarButton();
            this.btnAnular = new Telerik.WinControls.UI.CommandBarButton();
            this.btnEliminar = new Telerik.WinControls.UI.CommandBarButton();
            this.btnHistorial = new Telerik.WinControls.UI.CommandBarButton();
            this.btnExportToExcel = new Telerik.WinControls.UI.CommandBarButton();
            this.btnAdjuntar = new Telerik.WinControls.UI.CommandBarButton();
            this.btnCambiarEstadoDispositivo = new Telerik.WinControls.UI.CommandBarButton();
            this.btnGenerarFormatosPDF = new Telerik.WinControls.UI.CommandBarButton();
            this.btnElegirColumna = new Telerik.WinControls.UI.CommandBarButton();
            this.btnCerrar = new Telerik.WinControls.UI.CommandBarButton();
            this.gbListado = new System.Windows.Forms.GroupBox();
            this.dgvListado = new Telerik.WinControls.UI.RadGridView();
            this.btnNotificar = new Telerik.WinControls.UI.CommandBarButton();
            this.stsBarraEstado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BarraPrincipal)).BeginInit();
            this.gbListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListado.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // bgwHilo
            // 
            this.bgwHilo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwHilo_DoWork);
            this.bgwHilo.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwHilo_RunWorkerCompleted);
            // 
            // stsBarraEstado
            // 
            this.stsBarraEstado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblUserNames,
            this.lblCodeUser,
            this.lblUser,
            this.lblFullName,
            this.progressBar1});
            this.stsBarraEstado.Location = new System.Drawing.Point(0, 376);
            this.stsBarraEstado.Name = "stsBarraEstado";
            this.stsBarraEstado.Size = new System.Drawing.Size(1131, 22);
            this.stsBarraEstado.TabIndex = 213;
            // 
            // lblUserNames
            // 
            this.lblUserNames.Name = "lblUserNames";
            this.lblUserNames.Size = new System.Drawing.Size(33, 17);
            this.lblUserNames.Text = "User:";
            // 
            // lblCodeUser
            // 
            this.lblCodeUser.Name = "lblCodeUser";
            this.lblCodeUser.Size = new System.Drawing.Size(59, 17);
            this.lblCodeUser.Text = "username";
            // 
            // lblUser
            // 
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(65, 17);
            this.lblUser.Text = "Nombres : ";
            // 
            // lblFullName
            // 
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(61, 17);
            this.lblFullName.Text = "userName";
            // 
            // progressBar1
            // 
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(700, 16);
            // 
            // subMenu
            // 
            this.subMenu.Name = "subMenu";
            this.subMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // BarraPrincipal
            // 
            this.BarraPrincipal.Dock = System.Windows.Forms.DockStyle.Top;
            this.BarraPrincipal.Location = new System.Drawing.Point(0, 0);
            this.BarraPrincipal.Name = "BarraPrincipal";
            this.BarraPrincipal.Rows.AddRange(new Telerik.WinControls.UI.CommandBarRowElement[] {
            this.BarraSuperior});
            this.BarraPrincipal.Size = new System.Drawing.Size(1131, 62);
            this.BarraPrincipal.TabIndex = 234;
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
            this.btnAlmacen});
            this.BarraModulo.Name = "commandBarStripElement2";
            this.BarraModulo.Text = "";
            this.BarraModulo.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // btnAlmacen
            // 
            this.btnAlmacen.AccessibleDescription = "Almacén";
            this.btnAlmacen.AccessibleName = "Almacén";
            this.btnAlmacen.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.FitToAvailableSize;
            this.btnAlmacen.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnAlmacen.DisplayName = "Almacén";
            this.btnAlmacen.DrawText = true;
            this.btnAlmacen.FitToSizeMode = Telerik.WinControls.RadFitToSizeMode.FitToParentContent;
            this.btnAlmacen.Image = ((System.Drawing.Image)(resources.GetObject("btnAlmacen.Image")));
            this.btnAlmacen.ImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAlmacen.Name = "btnAlmacen";
            this.btnAlmacen.Tag = "Almacén";
            this.btnAlmacen.Text = "       Almacén     ";
            this.btnAlmacen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAlmacen.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnAlmacen.ToolTipText = "Almacén";
            // 
            // commandBarStripElement3
            // 
            this.commandBarStripElement3.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarStripElement3.DisplayName = "commandBarStripElement3";
            this.commandBarStripElement3.Items.AddRange(new Telerik.WinControls.UI.RadCommandBarBaseItem[] {
            this.btnActualizar,
            this.btnNotificar,
            this.btnActivarFiltro,
            this.btnNuevo,
            this.btnAtras,
            this.btnEditar,
            this.btnGrabar,
            this.btnResaltarResultados,
            this.btnAnular,
            this.btnEliminar,
            this.btnHistorial,
            this.btnExportToExcel,
            this.btnAdjuntar,
            this.btnCambiarEstadoDispositivo,
            this.btnGenerarFormatosPDF,
            this.btnElegirColumna,
            this.btnCerrar});
            this.commandBarStripElement3.Name = "commandBarStripElement3";
            this.commandBarStripElement3.Text = "";
            this.commandBarStripElement3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.commandBarStripElement3.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // btnActualizar
            // 
            this.btnActualizar.AutoSize = false;
            this.btnActualizar.Bounds = new System.Drawing.Rectangle(0, 0, 55, 35);
            this.btnActualizar.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnActualizar.DisplayName = "Actualizar";
            this.btnActualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizar.Image")));
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Text = "";
            this.btnActualizar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnActivarFiltro
            // 
            this.btnActivarFiltro.AccessibleDescription = "ActivarFiltro";
            this.btnActivarFiltro.AccessibleName = "ActivarFiltro";
            this.btnActivarFiltro.AutoSize = false;
            this.btnActivarFiltro.Bounds = new System.Drawing.Rectangle(0, 0, 55, 35);
            this.btnActivarFiltro.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnActivarFiltro.DisplayName = "ActivarFiltro";
            this.btnActivarFiltro.Image = ((System.Drawing.Image)(resources.GetObject("btnActivarFiltro.Image")));
            this.btnActivarFiltro.Name = "btnActivarFiltro";
            this.btnActivarFiltro.Text = "";
            this.btnActivarFiltro.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // btnNuevo
            // 
            this.btnNuevo.AccessibleDescription = "Nuevo";
            this.btnNuevo.AccessibleName = "Nuevo";
            this.btnNuevo.AutoSize = false;
            this.btnNuevo.Bounds = new System.Drawing.Rectangle(0, 0, 55, 35);
            this.btnNuevo.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnNuevo.DisplayName = "Nuevo";
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Text = "";
            this.btnNuevo.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnNuevo.ToolTipText = "Nuevo";
            // 
            // btnAtras
            // 
            this.btnAtras.AccessibleDescription = "Atras";
            this.btnAtras.AccessibleName = "Atras";
            this.btnAtras.AutoSize = false;
            this.btnAtras.Bounds = new System.Drawing.Rectangle(0, 0, 55, 35);
            this.btnAtras.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnAtras.DisplayName = "Atras";
            this.btnAtras.Image = ((System.Drawing.Image)(resources.GetObject("btnAtras.Image")));
            this.btnAtras.Name = "btnAtras";
            this.btnAtras.Text = "";
            this.btnAtras.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // btnEditar
            // 
            this.btnEditar.AccessibleDescription = "Editar";
            this.btnEditar.AccessibleName = "Editar";
            this.btnEditar.AutoSize = false;
            this.btnEditar.Bounds = new System.Drawing.Rectangle(0, 0, 55, 35);
            this.btnEditar.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnEditar.DisplayName = "Editar";
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Text = "";
            this.btnEditar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnEditar.ToolTipText = "Editar";
            // 
            // btnGrabar
            // 
            this.btnGrabar.AccessibleDescription = "Registrar";
            this.btnGrabar.AccessibleName = "Registrar";
            this.btnGrabar.AutoSize = false;
            this.btnGrabar.Bounds = new System.Drawing.Rectangle(0, 0, 55, 35);
            this.btnGrabar.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnGrabar.DisplayName = "Registrar";
            this.btnGrabar.Image = ((System.Drawing.Image)(resources.GetObject("btnGrabar.Image")));
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Text = "";
            this.btnGrabar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnGrabar.ToolTipText = "Actualizar Lista";
            // 
            // btnResaltarResultados
            // 
            this.btnResaltarResultados.AccessibleDescription = "ResaltarResultados";
            this.btnResaltarResultados.AccessibleName = "ResaltarResultados";
            this.btnResaltarResultados.AutoSize = false;
            this.btnResaltarResultados.Bounds = new System.Drawing.Rectangle(0, 0, 55, 35);
            this.btnResaltarResultados.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnResaltarResultados.DisplayName = "ResaltarResultados";
            this.btnResaltarResultados.Image = ((System.Drawing.Image)(resources.GetObject("btnResaltarResultados.Image")));
            this.btnResaltarResultados.Name = "btnResaltarResultados";
            this.btnResaltarResultados.Tag = "Resaltar Resultados";
            this.btnResaltarResultados.Text = "";
            this.btnResaltarResultados.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // btnAnular
            // 
            this.btnAnular.AccessibleDescription = "Anular";
            this.btnAnular.AccessibleName = "Anular";
            this.btnAnular.AutoSize = false;
            this.btnAnular.Bounds = new System.Drawing.Rectangle(0, 0, 55, 35);
            this.btnAnular.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnAnular.DisplayName = "Anular";
            this.btnAnular.Image = ((System.Drawing.Image)(resources.GetObject("btnAnular.Image")));
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.Text = "";
            this.btnAnular.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnAnular.ToolTipText = "Anular";
            // 
            // btnEliminar
            // 
            this.btnEliminar.AccessibleDescription = "Eliminar";
            this.btnEliminar.AccessibleName = "Eliminar";
            this.btnEliminar.AutoSize = false;
            this.btnEliminar.Bounds = new System.Drawing.Rectangle(0, 0, 55, 35);
            this.btnEliminar.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnEliminar.DisplayName = "Eliminar";
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Text = "";
            this.btnEliminar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnEliminar.ToolTipText = "Eliminar Registro";
            // 
            // btnHistorial
            // 
            this.btnHistorial.AutoSize = false;
            this.btnHistorial.Bounds = new System.Drawing.Rectangle(0, 0, 55, 35);
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
            this.btnExportToExcel.Bounds = new System.Drawing.Rectangle(0, 0, 55, 35);
            this.btnExportToExcel.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnExportToExcel.DisplayName = "Exportar";
            this.btnExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportToExcel.Image")));
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Text = "";
            this.btnExportToExcel.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnExportToExcel.ToolTipText = "Exportar";
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // btnAdjuntar
            // 
            this.btnAdjuntar.AccessibleDescription = "Adjuntar";
            this.btnAdjuntar.AccessibleName = "Adjuntar";
            this.btnAdjuntar.AutoSize = false;
            this.btnAdjuntar.Bounds = new System.Drawing.Rectangle(0, 0, 55, 35);
            this.btnAdjuntar.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnAdjuntar.DisplayName = "Adjuntar";
            this.btnAdjuntar.Image = ((System.Drawing.Image)(resources.GetObject("btnAdjuntar.Image")));
            this.btnAdjuntar.Name = "btnAdjuntar";
            this.btnAdjuntar.Tag = "Adjuntar";
            this.btnAdjuntar.Text = "";
            this.btnAdjuntar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnAdjuntar.ToolTipText = "Adjuntar";
            // 
            // btnCambiarEstadoDispositivo
            // 
            this.btnCambiarEstadoDispositivo.AccessibleDescription = "CambiarEstadoDispositivo";
            this.btnCambiarEstadoDispositivo.AccessibleName = "CambiarEstadoDispositivo";
            this.btnCambiarEstadoDispositivo.AutoSize = false;
            this.btnCambiarEstadoDispositivo.Bounds = new System.Drawing.Rectangle(0, 0, 55, 35);
            this.btnCambiarEstadoDispositivo.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnCambiarEstadoDispositivo.DisplayName = "CambiarEstadoDispositivo";
            this.btnCambiarEstadoDispositivo.Image = ((System.Drawing.Image)(resources.GetObject("btnCambiarEstadoDispositivo.Image")));
            this.btnCambiarEstadoDispositivo.Name = "btnCambiarEstadoDispositivo";
            this.btnCambiarEstadoDispositivo.Tag = "CambiarEstadoDispositivo";
            this.btnCambiarEstadoDispositivo.Text = "";
            this.btnCambiarEstadoDispositivo.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnCambiarEstadoDispositivo.ToolTipText = "Cambiar el estado del dispositivo";
            // 
            // btnGenerarFormatosPDF
            // 
            this.btnGenerarFormatosPDF.AccessibleDescription = "GenerarFormatosPDF";
            this.btnGenerarFormatosPDF.AccessibleName = "GenerarFormatosPDF";
            this.btnGenerarFormatosPDF.AutoSize = false;
            this.btnGenerarFormatosPDF.Bounds = new System.Drawing.Rectangle(0, 0, 55, 35);
            this.btnGenerarFormatosPDF.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnGenerarFormatosPDF.DisplayName = "GenerarFormatosPDF";
            this.btnGenerarFormatosPDF.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarFormatosPDF.Image")));
            this.btnGenerarFormatosPDF.Name = "btnGenerarFormatosPDF";
            this.btnGenerarFormatosPDF.Tag = "GenerarFormatosPDF";
            this.btnGenerarFormatosPDF.Text = "";
            this.btnGenerarFormatosPDF.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnGenerarFormatosPDF.ToolTipText = "GenerarFormatosPDF";
            // 
            // btnElegirColumna
            // 
            this.btnElegirColumna.AccessibleDescription = "ElegirColumna";
            this.btnElegirColumna.AccessibleName = "ElegirColumna";
            this.btnElegirColumna.AutoSize = false;
            this.btnElegirColumna.Bounds = new System.Drawing.Rectangle(0, 0, 55, 35);
            this.btnElegirColumna.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnElegirColumna.DisplayName = "ElegirColumna";
            this.btnElegirColumna.Image = ((System.Drawing.Image)(resources.GetObject("btnElegirColumna.Image")));
            this.btnElegirColumna.Name = "btnElegirColumna";
            this.btnElegirColumna.Text = "";
            this.btnElegirColumna.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnElegirColumna.Click += new System.EventHandler(this.btnElegirColumna_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.AccessibleDescription = "Salir";
            this.btnCerrar.AccessibleName = "Salir";
            this.btnCerrar.AutoSize = false;
            this.btnCerrar.Bounds = new System.Drawing.Rectangle(0, 0, 55, 35);
            this.btnCerrar.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnCerrar.DisplayName = "Salir";
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Text = "";
            this.btnCerrar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnCerrar.ToolTipText = "Salir";
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // gbListado
            // 
            this.gbListado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbListado.Controls.Add(this.dgvListado);
            this.gbListado.Location = new System.Drawing.Point(12, 41);
            this.gbListado.Name = "gbListado";
            this.gbListado.Size = new System.Drawing.Size(1107, 332);
            this.gbListado.TabIndex = 235;
            this.gbListado.TabStop = false;
            this.gbListado.Text = "Listado";
            // 
            // dgvListado
            // 
            this.dgvListado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.dgvListado.ContextMenuStrip = this.subMenu;
            this.dgvListado.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvListado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvListado.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvListado.ForeColor = System.Drawing.Color.Black;
            this.dgvListado.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvListado.Location = new System.Drawing.Point(3, 16);
            // 
            // dgvListado
            // 
            this.dgvListado.MasterTemplate.AllowAddNewRow = false;
            this.dgvListado.MasterTemplate.AutoExpandGroups = true;
            this.dgvListado.MasterTemplate.AutoGenerateColumns = false;
            this.dgvListado.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewTextBoxColumn1.EnableExpressionEditor = false;
            gridViewTextBoxColumn1.FieldName = "EmpresaID";
            gridViewTextBoxColumn1.HeaderText = "EmpresaID";
            gridViewTextBoxColumn1.IsVisible = false;
            gridViewTextBoxColumn1.Name = "chEmpresaID";
            gridViewTextBoxColumn1.Width = 58;
            gridViewTextBoxColumn2.EnableExpressionEditor = false;
            gridViewTextBoxColumn2.FieldName = "Empresa";
            gridViewTextBoxColumn2.HeaderText = "Empresa";
            gridViewTextBoxColumn2.IsVisible = false;
            gridViewTextBoxColumn2.Name = "chEmpresa";
            gridViewTextBoxColumn2.Width = 129;
            gridViewTextBoxColumn3.EnableExpressionEditor = false;
            gridViewTextBoxColumn3.FieldName = "SucursalID";
            gridViewTextBoxColumn3.HeaderText = "SucursalID";
            gridViewTextBoxColumn3.IsVisible = false;
            gridViewTextBoxColumn3.Name = "chSucursalID";
            gridViewTextBoxColumn3.Width = 70;
            gridViewTextBoxColumn4.EnableExpressionEditor = false;
            gridViewTextBoxColumn4.FieldName = "Sucursal";
            gridViewTextBoxColumn4.HeaderText = "Sucursal";
            gridViewTextBoxColumn4.Name = "chSucursal";
            gridViewTextBoxColumn4.Width = 149;
            gridViewTextBoxColumn5.EnableExpressionEditor = false;
            gridViewTextBoxColumn5.FieldName = "AlmacenID";
            gridViewTextBoxColumn5.HeaderText = "AlmacenID";
            gridViewTextBoxColumn5.IsVisible = false;
            gridViewTextBoxColumn5.Name = "chAlmacenID";
            gridViewTextBoxColumn5.Width = 118;
            gridViewTextBoxColumn6.EnableExpressionEditor = false;
            gridViewTextBoxColumn6.FieldName = "Almacen";
            gridViewTextBoxColumn6.HeaderText = "Almacen";
            gridViewTextBoxColumn6.Name = "chAlmacen";
            gridViewTextBoxColumn6.Width = 208;
            gridViewTextBoxColumn7.EnableExpressionEditor = false;
            gridViewTextBoxColumn7.FieldName = "ProductoId";
            gridViewTextBoxColumn7.HeaderText = "Código";
            gridViewTextBoxColumn7.Name = "chProductoId";
            gridViewTextBoxColumn7.Width = 117;
            gridViewTextBoxColumn8.EnableExpressionEditor = false;
            gridViewTextBoxColumn8.FieldName = "producto";
            gridViewTextBoxColumn8.HeaderText = "Producto";
            gridViewTextBoxColumn8.Name = "chproducto";
            gridViewTextBoxColumn8.Width = 429;
            gridViewTextBoxColumn9.EnableExpressionEditor = false;
            gridViewTextBoxColumn9.FieldName = "UM";
            gridViewTextBoxColumn9.HeaderText = "UM";
            gridViewTextBoxColumn9.Name = "chUM";
            gridViewTextBoxColumn9.Width = 88;
            gridViewTextBoxColumn10.EnableExpressionEditor = false;
            gridViewTextBoxColumn10.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Standard;
            gridViewTextBoxColumn10.FieldName = "cantidadFactor";
            gridViewTextBoxColumn10.HeaderText = "Stock";
            gridViewTextBoxColumn10.Name = "chcantidadFactor";
            gridViewTextBoxColumn10.Width = 95;
            this.dgvListado.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6,
            gridViewTextBoxColumn7,
            gridViewTextBoxColumn8,
            gridViewTextBoxColumn9,
            gridViewTextBoxColumn10});
            this.dgvListado.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgvListado.MasterTemplate.EnableFiltering = true;
            this.dgvListado.MasterTemplate.MultiSelect = true;
            this.dgvListado.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect;
            this.dgvListado.MasterTemplate.ShowGroupedColumns = true;
            this.dgvListado.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgvListado.Name = "dgvListado";
            this.dgvListado.Padding = new System.Windows.Forms.Padding(1);
            this.dgvListado.ReadOnly = true;
            this.dgvListado.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvListado.ShowHeaderCellButtons = true;
            this.dgvListado.Size = new System.Drawing.Size(1101, 313);
            this.dgvListado.TabIndex = 196;
            this.dgvListado.ThemeName = "VisualStudio2012Light";
            // 
            // btnNotificar
            // 
            this.btnNotificar.AccessibleDescription = "Notificar";
            this.btnNotificar.AccessibleName = "Notificar";
            this.btnNotificar.AutoSize = false;
            this.btnNotificar.Bounds = new System.Drawing.Rectangle(0, 0, 55, 35);
            this.btnNotificar.DisplayName = "commandBarButton1";
            this.btnNotificar.Image = ((System.Drawing.Image)(resources.GetObject("btnNotificar.Image")));
            this.btnNotificar.Name = "btnNotificar";
            this.btnNotificar.Tag = "Notificar";
            this.btnNotificar.Text = "";
            this.btnNotificar.Click += new System.EventHandler(this.btnNotificar_Click);
            // 
            // ReporteStockEPP
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1131, 398);
            this.Controls.Add(this.gbListado);
            this.Controls.Add(this.BarraPrincipal);
            this.Controls.Add(this.stsBarraEstado);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReporteStockEPP";
            this.Text = "Reporte de Stock EPP";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReporteStockEPP_FormClosing);
            this.Load += new System.EventHandler(this.ReporteStockEPP_Load);
            this.stsBarraEstado.ResumeLayout(false);
            this.stsBarraEstado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BarraPrincipal)).EndInit();
            this.gbListado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListado.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListado)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bgwHilo;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private System.Windows.Forms.StatusStrip stsBarraEstado;
        private System.Windows.Forms.ToolStripStatusLabel lblUserNames;
        private System.Windows.Forms.ToolStripStatusLabel lblCodeUser;
        private System.Windows.Forms.ToolStripStatusLabel lblUser;
        private System.Windows.Forms.ToolStripStatusLabel lblFullName;
        private System.Windows.Forms.ToolStripProgressBar progressBar1;
        private System.Windows.Forms.ContextMenuStrip subMenu;
        private Telerik.WinControls.UI.RadCommandBar BarraPrincipal;
        private Telerik.WinControls.UI.CommandBarRowElement BarraSuperior;
        private Telerik.WinControls.UI.CommandBarStripElement BarraModulo;
        private Telerik.WinControls.UI.CommandBarButton btnAlmacen;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement3;
        private Telerik.WinControls.UI.CommandBarButton btnActualizar;
        private Telerik.WinControls.UI.CommandBarButton btnActivarFiltro;
        private Telerik.WinControls.UI.CommandBarButton btnNuevo;
        private Telerik.WinControls.UI.CommandBarButton btnAtras;
        private Telerik.WinControls.UI.CommandBarButton btnEditar;
        private Telerik.WinControls.UI.CommandBarButton btnGrabar;
        private Telerik.WinControls.UI.CommandBarButton btnResaltarResultados;
        private Telerik.WinControls.UI.CommandBarButton btnAnular;
        private Telerik.WinControls.UI.CommandBarButton btnEliminar;
        private Telerik.WinControls.UI.CommandBarButton btnHistorial;
        private Telerik.WinControls.UI.CommandBarButton btnExportToExcel;
        private Telerik.WinControls.UI.CommandBarButton btnAdjuntar;
        private Telerik.WinControls.UI.CommandBarButton btnCambiarEstadoDispositivo;
        private Telerik.WinControls.UI.CommandBarButton btnGenerarFormatosPDF;
        private Telerik.WinControls.UI.CommandBarButton btnElegirColumna;
        private Telerik.WinControls.UI.CommandBarButton btnCerrar;
        private System.Windows.Forms.GroupBox gbListado;
        private Telerik.WinControls.UI.RadGridView dgvListado;
        private Telerik.WinControls.UI.CommandBarButton btnNotificar;
    }
}