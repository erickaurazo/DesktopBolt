namespace ComparativoHorasVisualSATNISIRA.T.I.Ordenes_de_soporte_tecnico
{
    partial class TipoSoporteTecnicoVistaAgrupada
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
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn19 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn20 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn21 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn22 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn23 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn24 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TipoSoporteTecnicoVistaAgrupada));
            this.dgvRegistro = new Telerik.WinControls.UI.RadGridView();
            this.bgwHilo = new System.ComponentModel.BackgroundWorker();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.stsBarraEstado = new System.Windows.Forms.StatusStrip();
            this.progressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistro.MasterTemplate)).BeginInit();
            this.stsBarraEstado.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvRegistro
            // 
            this.dgvRegistro.BackColor = System.Drawing.Color.White;
            this.dgvRegistro.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvRegistro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRegistro.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.dgvRegistro.ForeColor = System.Drawing.Color.Black;
            this.dgvRegistro.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgvRegistro.Location = new System.Drawing.Point(0, 0);
            // 
            // dgvRegistro
            // 
            this.dgvRegistro.MasterTemplate.AllowAddNewRow = false;
            this.dgvRegistro.MasterTemplate.AutoGenerateColumns = false;
            this.dgvRegistro.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewTextBoxColumn19.EnableExpressionEditor = false;
            gridViewTextBoxColumn19.FieldName = "codigo";
            gridViewTextBoxColumn19.HeaderText = "Código";
            gridViewTextBoxColumn19.IsVisible = false;
            gridViewTextBoxColumn19.Name = "chcodigo";
            gridViewTextBoxColumn19.ReadOnly = true;
            gridViewTextBoxColumn19.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewTextBoxColumn19.Width = 112;
            gridViewTextBoxColumn20.EnableExpressionEditor = false;
            gridViewTextBoxColumn20.FieldName = "tipoSoporteFuncional";
            gridViewTextBoxColumn20.HeaderText = "Tipo soporte funcional";
            gridViewTextBoxColumn20.Name = "chtipoSoporteFuncional";
            gridViewTextBoxColumn20.ReadOnly = true;
            gridViewTextBoxColumn20.Width = 433;
            gridViewTextBoxColumn21.EnableExpressionEditor = false;
            gridViewTextBoxColumn21.FieldName = "idtipoHardware";
            gridViewTextBoxColumn21.HeaderText = "idtipoHardware";
            gridViewTextBoxColumn21.IsVisible = false;
            gridViewTextBoxColumn21.Name = "chidtipoHardware";
            gridViewTextBoxColumn21.Width = 76;
            gridViewTextBoxColumn22.EnableExpressionEditor = false;
            gridViewTextBoxColumn22.FieldName = "tipoDispositivo";
            gridViewTextBoxColumn22.HeaderText = "Tipo de dispositivo";
            gridViewTextBoxColumn22.Name = "chtipoDispositivo";
            gridViewTextBoxColumn22.Width = 371;
            gridViewTextBoxColumn23.EnableExpressionEditor = false;
            gridViewTextBoxColumn23.FieldName = "numeroDeActividades";
            gridViewTextBoxColumn23.HeaderText = "# Actividades";
            gridViewTextBoxColumn23.Name = "chnumeroDeActividades";
            gridViewTextBoxColumn23.Width = 124;
            gridViewTextBoxColumn24.EnableExpressionEditor = false;
            gridViewTextBoxColumn24.FieldName = "minutos";
            gridViewTextBoxColumn24.HeaderText = "Minutos";
            gridViewTextBoxColumn24.Name = "chminutos";
            gridViewTextBoxColumn24.Width = 96;
            this.dgvRegistro.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn19,
            gridViewTextBoxColumn20,
            gridViewTextBoxColumn21,
            gridViewTextBoxColumn22,
            gridViewTextBoxColumn23,
            gridViewTextBoxColumn24});
            this.dgvRegistro.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgvRegistro.MasterTemplate.EnableFiltering = true;
            this.dgvRegistro.MasterTemplate.MultiSelect = true;
            this.dgvRegistro.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect;
            this.dgvRegistro.MasterTemplate.ShowGroupedColumns = true;
            this.dgvRegistro.Name = "dgvRegistro";
            this.dgvRegistro.ReadOnly = true;
            this.dgvRegistro.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvRegistro.Size = new System.Drawing.Size(1039, 539);
            this.dgvRegistro.TabIndex = 164;
            this.dgvRegistro.ThemeName = "VisualStudio2012Light";
            // 
            // bgwHilo
            // 
            this.bgwHilo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwHilo_DoWork);
            this.bgwHilo.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwHilo_RunWorkerCompleted);
            // 
            // stsBarraEstado
            // 
            this.stsBarraEstado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar1});
            this.stsBarraEstado.Location = new System.Drawing.Point(0, 517);
            this.stsBarraEstado.Name = "stsBarraEstado";
            this.stsBarraEstado.Size = new System.Drawing.Size(1039, 22);
            this.stsBarraEstado.TabIndex = 212;
            // 
            // progressBar1
            // 
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(600, 16);
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
            // TipoSoporteTecnicoVistaAgrupada
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1039, 539);
            this.Controls.Add(this.stsBarraEstado);
            this.Controls.Add(this.dgvRegistro);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TipoSoporteTecnicoVistaAgrupada";
            this.Text = "Tipo de Soporte Técnico | Listado agrupaso";
            this.Load += new System.EventHandler(this.TipoSoporteTecnicoVistaAgrupada_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistro.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistro)).EndInit();
            this.stsBarraEstado.ResumeLayout(false);
            this.stsBarraEstado.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView dgvRegistro;
        private System.ComponentModel.BackgroundWorker bgwHilo;
        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private System.Windows.Forms.StatusStrip stsBarraEstado;
        private System.Windows.Forms.ToolStripProgressBar progressBar1;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ImageList imageList1;
    }
}