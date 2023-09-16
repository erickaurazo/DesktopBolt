namespace ComparativoHorasVisualSATNISIRA.Almacen
{
    partial class Productos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Productos));
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn1 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            this.BarraPrincipal = new Telerik.WinControls.UI.RadCommandBar();
            this.BarraSuperior = new Telerik.WinControls.UI.CommandBarRowElement();
            this.BarraModulo = new Telerik.WinControls.UI.CommandBarStripElement();
            this.btnAlmacen = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarStripElement3 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.btnActualizar = new Telerik.WinControls.UI.CommandBarButton();
            this.btnExportToExcel = new Telerik.WinControls.UI.CommandBarButton();
            this.btnVistaPrevia = new Telerik.WinControls.UI.CommandBarButton();
            this.btnImprimir = new Telerik.WinControls.UI.CommandBarButton();
            this.btnCerrar = new Telerik.WinControls.UI.CommandBarButton();
            this.gbCabecera = new System.Windows.Forms.GroupBox();
            this.chkIncluirAnulados = new System.Windows.Forms.CheckBox();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.gbListado = new System.Windows.Forms.GroupBox();
            this.dgvListado = new Telerik.WinControls.UI.RadGridView();
            this.subMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnImprimirTicketQR = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.stsBarraEstado = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.bgwHilo = new System.ComponentModel.BackgroundWorker();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            ((System.ComponentModel.ISupportInitialize)(this.BarraPrincipal)).BeginInit();
            this.gbCabecera.SuspendLayout();
            this.gbListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListado.MasterTemplate)).BeginInit();
            this.subMenu.SuspendLayout();
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
            this.BarraPrincipal.Size = new System.Drawing.Size(973, 37);
            this.BarraPrincipal.TabIndex = 193;
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
            this.btnAlmacen.AccessibleDescription = "T.I";
            this.btnAlmacen.AccessibleName = "T.I";
            this.btnAlmacen.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnAlmacen.DisplayName = "T.I ";
            this.btnAlmacen.DrawText = true;
            this.btnAlmacen.FitToSizeMode = Telerik.WinControls.RadFitToSizeMode.FitToParentContent;
            this.btnAlmacen.Image = ((System.Drawing.Image)(resources.GetObject("btnAlmacen.Image")));
            this.btnAlmacen.Name = "btnAlmacen";
            this.btnAlmacen.Text = "  Almacén    ";
            this.btnAlmacen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAlmacen.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnAlmacen.ToolTipText = "  Almacén  ";
            this.btnAlmacen.UseCompatibleTextRendering = true;
            // 
            // commandBarStripElement3
            // 
            this.commandBarStripElement3.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarStripElement3.DisplayName = "commandBarStripElement3";
            this.commandBarStripElement3.Items.AddRange(new Telerik.WinControls.UI.RadCommandBarBaseItem[] {
            this.btnActualizar,
            this.btnExportToExcel,
            this.btnVistaPrevia,
            this.btnImprimir,
            this.btnCerrar});
            this.commandBarStripElement3.Name = "commandBarStripElement3";
            this.commandBarStripElement3.Text = "";
            this.commandBarStripElement3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.commandBarStripElement3.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // btnActualizar
            // 
            this.btnActualizar.AccessibleDescription = "Actualizar";
            this.btnActualizar.AccessibleName = "Actualizar";
            this.btnActualizar.AutoSize = false;
            this.btnActualizar.Bounds = new System.Drawing.Rectangle(0, 0, 75, 35);
            this.btnActualizar.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnActualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizar.Image")));
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Text = "";
            this.btnActualizar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnActualizar.ToolTipText = "Actualizar Lista";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.AccessibleDescription = "Exportar";
            this.btnExportToExcel.AccessibleName = "Exportar";
            this.btnExportToExcel.AutoSize = false;
            this.btnExportToExcel.Bounds = new System.Drawing.Rectangle(0, 0, 75, 35);
            this.btnExportToExcel.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnExportToExcel.DisplayName = "Exportar";
            this.btnExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportToExcel.Image")));
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Text = "";
            this.btnExportToExcel.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnExportToExcel.ToolTipText = "Exportar";
            // 
            // btnVistaPrevia
            // 
            this.btnVistaPrevia.AutoSize = false;
            this.btnVistaPrevia.Bounds = new System.Drawing.Rectangle(0, 0, 75, 35);
            this.btnVistaPrevia.DisplayName = "commandBarButton1";
            this.btnVistaPrevia.Image = ((System.Drawing.Image)(resources.GetObject("btnVistaPrevia.Image")));
            this.btnVistaPrevia.Name = "btnVistaPrevia";
            this.btnVistaPrevia.Text = "";
            this.btnVistaPrevia.Click += new System.EventHandler(this.btnVistaPrevia_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.AccessibleDescription = "btnImprimir";
            this.btnImprimir.AccessibleName = "btnImprimir";
            this.btnImprimir.AutoSize = false;
            this.btnImprimir.Bounds = new System.Drawing.Rectangle(0, 0, 75, 35);
            this.btnImprimir.DisplayName = "commandBarButton2";
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Text = "";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.AccessibleDescription = "Salir";
            this.btnCerrar.AccessibleName = "Salir";
            this.btnCerrar.AutoSize = false;
            this.btnCerrar.Bounds = new System.Drawing.Rectangle(0, 0, 75, 35);
            this.btnCerrar.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnCerrar.DisplayName = "Salir";
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Text = "";
            this.btnCerrar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnCerrar.ToolTipText = "Salir";
            // 
            // gbCabecera
            // 
            this.gbCabecera.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCabecera.Controls.Add(this.chkIncluirAnulados);
            this.gbCabecera.Controls.Add(this.btnConsultar);
            this.gbCabecera.Location = new System.Drawing.Point(12, 41);
            this.gbCabecera.Name = "gbCabecera";
            this.gbCabecera.Size = new System.Drawing.Size(954, 44);
            this.gbCabecera.TabIndex = 195;
            this.gbCabecera.TabStop = false;
            this.gbCabecera.Text = "Consulta";
            // 
            // chkIncluirAnulados
            // 
            this.chkIncluirAnulados.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkIncluirAnulados.AutoSize = true;
            this.chkIncluirAnulados.Location = new System.Drawing.Point(724, 18);
            this.chkIncluirAnulados.Name = "chkIncluirAnulados";
            this.chkIncluirAnulados.Size = new System.Drawing.Size(100, 17);
            this.chkIncluirAnulados.TabIndex = 1;
            this.chkIncluirAnulados.Text = "Incluir anulados";
            this.chkIncluirAnulados.UseVisualStyleBackColor = true;
            // 
            // btnConsultar
            // 
            this.btnConsultar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConsultar.Image = ((System.Drawing.Image)(resources.GetObject("btnConsultar.Image")));
            this.btnConsultar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsultar.Location = new System.Drawing.Point(843, 15);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(104, 28);
            this.btnConsultar.TabIndex = 0;
            this.btnConsultar.Text = "&Consultar      ";
            this.btnConsultar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // gbListado
            // 
            this.gbListado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbListado.Controls.Add(this.dgvListado);
            this.gbListado.Location = new System.Drawing.Point(12, 90);
            this.gbListado.Name = "gbListado";
            this.gbListado.Size = new System.Drawing.Size(954, 264);
            this.gbListado.TabIndex = 196;
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
            gridViewTextBoxColumn1.FieldName = "idproducto";
            gridViewTextBoxColumn1.HeaderText = "Código";
            gridViewTextBoxColumn1.Name = "chidproducto";
            gridViewTextBoxColumn1.Width = 144;
            gridViewTextBoxColumn2.EnableExpressionEditor = false;
            gridViewTextBoxColumn2.FieldName = "descripcion";
            gridViewTextBoxColumn2.HeaderText = "Producto";
            gridViewTextBoxColumn2.Name = "chdescripcion";
            gridViewTextBoxColumn2.Width = 592;
            gridViewTextBoxColumn3.EnableExpressionEditor = false;
            gridViewTextBoxColumn3.FieldName = "idmedida";
            gridViewTextBoxColumn3.HeaderText = "UM";
            gridViewTextBoxColumn3.Name = "chidmedida";
            gridViewTextBoxColumn3.Width = 95;
            gridViewCheckBoxColumn1.EnableExpressionEditor = false;
            gridViewCheckBoxColumn1.FieldName = "estado";
            gridViewCheckBoxColumn1.HeaderText = "Estado ";
            gridViewCheckBoxColumn1.MinWidth = 20;
            gridViewCheckBoxColumn1.Name = "chestado";
            gridViewCheckBoxColumn1.Width = 100;
            this.dgvListado.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewCheckBoxColumn1});
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
            this.dgvListado.Size = new System.Drawing.Size(948, 245);
            this.dgvListado.TabIndex = 193;
            this.dgvListado.ThemeName = "VisualStudio2012Light";
            this.dgvListado.SelectionChanged += new System.EventHandler(this.dgvListado_SelectionChanged);
            // 
            // subMenu
            // 
            this.subMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnImprimirTicketQR});
            this.subMenu.Name = "subMenu";
            this.subMenu.Size = new System.Drawing.Size(158, 26);
            // 
            // btnImprimirTicketQR
            // 
            this.btnImprimirTicketQR.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimirTicketQR.Image")));
            this.btnImprimirTicketQR.Name = "btnImprimirTicketQR";
            this.btnImprimirTicketQR.Size = new System.Drawing.Size(157, 22);
            this.btnImprimirTicketQR.Text = "Imprimir tickets";
            this.btnImprimirTicketQR.Click += new System.EventHandler(this.btnImprimirTicketQR_Click);
            // 
            // stsBarraEstado
            // 
            this.stsBarraEstado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar});
            this.stsBarraEstado.Location = new System.Drawing.Point(0, 357);
            this.stsBarraEstado.Name = "stsBarraEstado";
            this.stsBarraEstado.Size = new System.Drawing.Size(973, 22);
            this.stsBarraEstado.TabIndex = 198;
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(500, 16);
            this.progressBar.Visible = false;
            // 
            // bgwHilo
            // 
            this.bgwHilo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwHilo_DoWork);
            this.bgwHilo.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwHilo_RunWorkerCompleted);
            // 
            // printDialog
            // 
            this.printDialog.UseEXDialog = true;
            // 
            // Productos
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(973, 379);
            this.Controls.Add(this.stsBarraEstado);
            this.Controls.Add(this.gbListado);
            this.Controls.Add(this.gbCabecera);
            this.Controls.Add(this.BarraPrincipal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Productos";
            this.Text = "Productos | Listado de registros";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.BarraPrincipal)).EndInit();
            this.gbCabecera.ResumeLayout(false);
            this.gbCabecera.PerformLayout();
            this.gbListado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListado.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListado)).EndInit();
            this.subMenu.ResumeLayout(false);
            this.stsBarraEstado.ResumeLayout(false);
            this.stsBarraEstado.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadCommandBar BarraPrincipal;
        private Telerik.WinControls.UI.CommandBarRowElement BarraSuperior;
        private Telerik.WinControls.UI.CommandBarStripElement BarraModulo;
        private Telerik.WinControls.UI.CommandBarButton btnAlmacen;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement3;
        private Telerik.WinControls.UI.CommandBarButton btnActualizar;
        private Telerik.WinControls.UI.CommandBarButton btnExportToExcel;
        private Telerik.WinControls.UI.CommandBarButton btnCerrar;
        private System.Windows.Forms.GroupBox gbCabecera;
        private System.Windows.Forms.CheckBox chkIncluirAnulados;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.GroupBox gbListado;
        private Telerik.WinControls.UI.RadGridView dgvListado;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private System.Windows.Forms.ContextMenuStrip subMenu;
        private System.Windows.Forms.ToolStripMenuItem btnImprimirTicketQR;
        private System.Windows.Forms.StatusStrip stsBarraEstado;
        private System.ComponentModel.BackgroundWorker bgwHilo;
        private Telerik.WinControls.UI.CommandBarButton btnVistaPrevia;
        private Telerik.WinControls.UI.CommandBarButton btnImprimir;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.PrintDialog printDialog;
    }
}