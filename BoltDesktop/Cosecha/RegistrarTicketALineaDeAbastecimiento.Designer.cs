namespace ComparativoHorasVisualSATNISIRA.Cosecha
{
    partial class RegistrarTicketALineaDeAbastecimiento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistrarTicketALineaDeAbastecimiento));
            this.stsBarraEstado = new System.Windows.Forms.StatusStrip();
            this.gbAcciones = new System.Windows.Forms.GroupBox();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.gbDatosDelTicket = new System.Windows.Forms.GroupBox();
            this.txtLote = new System.Windows.Forms.TextBox();
            this.txtVariedad = new System.Windows.Forms.TextBox();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.txtItemDetalle = new System.Windows.Forms.TextBox();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.txtTicket = new System.Windows.Forms.TextBox();
            this.txtTipoDeCultivo = new System.Windows.Forms.TextBox();
            this.txtlDocumentoAcopio = new System.Windows.Forms.TextBox();
            this.txtGuiaDeRemision = new System.Windows.Forms.TextBox();
            this.lblTicket = new System.Windows.Forms.Label();
            this.lblLote = new System.Windows.Forms.Label();
            this.lblVariedad = new System.Windows.Forms.Label();
            this.lblTipoDeCultivo = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.lblItemDetalle = new System.Windows.Forms.Label();
            this.lblDocumentoAcopio = new System.Windows.Forms.Label();
            this.lblGuiaDeRemision = new System.Windows.Forms.Label();
            this.gbLinea = new System.Windows.Forms.GroupBox();
            this.txtValidarFecha = new MyDataGridViewColumns.MyDataGridViewMaskedTextEditingControl();
            this.lblFechaHoraRegistroAAbastecimiento = new System.Windows.Forms.Label();
            this.txtFechaRegistroAAbastecimiento = new MyDataGridViewColumns.MyDataGridViewMaskedTextEditingControl();
            this.cboFormatoDeEmpaque = new Telerik.WinControls.UI.RadDropDownList();
            this.lblFormatoDeEmpaque = new System.Windows.Forms.Label();
            this.cboLinea = new Telerik.WinControls.UI.RadDropDownList();
            this.cboHoraCambioDeFormato = new Telerik.WinControls.UI.RadDropDownList();
            this.lblLinea = new System.Windows.Forms.Label();
            this.lblHoraCambioDeFormato = new System.Windows.Forms.Label();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.subMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnEditarRegistro = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimirEtiquetaPequena = new System.Windows.Forms.ToolStripMenuItem();
            this.btnVistaPreviaEtiquetaPequena = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnImprimirEtiquetaGrande = new System.Windows.Forms.ToolStripMenuItem();
            this.btnVistaPreviaEtiquetaGrande = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.bgwHilo = new System.ComponentModel.BackgroundWorker();
            this.gbAcciones.SuspendLayout();
            this.gbDatosDelTicket.SuspendLayout();
            this.gbLinea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboFormatoDeEmpaque)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLinea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboHoraCambioDeFormato)).BeginInit();
            this.subMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // stsBarraEstado
            // 
            this.stsBarraEstado.Location = new System.Drawing.Point(0, 305);
            this.stsBarraEstado.Name = "stsBarraEstado";
            this.stsBarraEstado.Size = new System.Drawing.Size(733, 22);
            this.stsBarraEstado.TabIndex = 207;
            // 
            // gbAcciones
            // 
            this.gbAcciones.Controls.Add(this.btnRegistrar);
            this.gbAcciones.Controls.Add(this.btnCancelar);
            this.gbAcciones.Location = new System.Drawing.Point(12, 250);
            this.gbAcciones.Name = "gbAcciones";
            this.gbAcciones.Size = new System.Drawing.Size(709, 50);
            this.gbAcciones.TabIndex = 206;
            this.gbAcciones.TabStop = false;
            this.gbAcciones.Text = "Acciones";
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnRegistrar.Location = new System.Drawing.Point(555, 19);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(103, 23);
            this.btnRegistrar.TabIndex = 16;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(55, 21);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(103, 23);
            this.btnCancelar.TabIndex = 17;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // gbDatosDelTicket
            // 
            this.gbDatosDelTicket.Controls.Add(this.txtLote);
            this.gbDatosDelTicket.Controls.Add(this.txtVariedad);
            this.gbDatosDelTicket.Controls.Add(this.txtCantidad);
            this.gbDatosDelTicket.Controls.Add(this.txtItemDetalle);
            this.gbDatosDelTicket.Controls.Add(this.txtFecha);
            this.gbDatosDelTicket.Controls.Add(this.txtTicket);
            this.gbDatosDelTicket.Controls.Add(this.txtTipoDeCultivo);
            this.gbDatosDelTicket.Controls.Add(this.txtlDocumentoAcopio);
            this.gbDatosDelTicket.Controls.Add(this.txtGuiaDeRemision);
            this.gbDatosDelTicket.Controls.Add(this.lblTicket);
            this.gbDatosDelTicket.Controls.Add(this.lblLote);
            this.gbDatosDelTicket.Controls.Add(this.lblVariedad);
            this.gbDatosDelTicket.Controls.Add(this.lblTipoDeCultivo);
            this.gbDatosDelTicket.Controls.Add(this.lblFecha);
            this.gbDatosDelTicket.Controls.Add(this.lblCantidad);
            this.gbDatosDelTicket.Controls.Add(this.lblItemDetalle);
            this.gbDatosDelTicket.Controls.Add(this.lblDocumentoAcopio);
            this.gbDatosDelTicket.Controls.Add(this.lblGuiaDeRemision);
            this.gbDatosDelTicket.Location = new System.Drawing.Point(12, 6);
            this.gbDatosDelTicket.Name = "gbDatosDelTicket";
            this.gbDatosDelTicket.Size = new System.Drawing.Size(710, 106);
            this.gbDatosDelTicket.TabIndex = 205;
            this.gbDatosDelTicket.TabStop = false;
            this.gbDatosDelTicket.Text = "Datos del Ticket";
            // 
            // txtLote
            // 
            this.txtLote.Location = new System.Drawing.Point(557, 78);
            this.txtLote.Name = "txtLote";
            this.txtLote.ReadOnly = true;
            this.txtLote.Size = new System.Drawing.Size(130, 20);
            this.txtLote.TabIndex = 30;
            // 
            // txtVariedad
            // 
            this.txtVariedad.Location = new System.Drawing.Point(336, 78);
            this.txtVariedad.Name = "txtVariedad";
            this.txtVariedad.ReadOnly = true;
            this.txtVariedad.Size = new System.Drawing.Size(130, 20);
            this.txtVariedad.TabIndex = 29;
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(556, 51);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.ReadOnly = true;
            this.txtCantidad.Size = new System.Drawing.Size(130, 20);
            this.txtCantidad.TabIndex = 28;
            // 
            // txtItemDetalle
            // 
            this.txtItemDetalle.Location = new System.Drawing.Point(336, 51);
            this.txtItemDetalle.Name = "txtItemDetalle";
            this.txtItemDetalle.ReadOnly = true;
            this.txtItemDetalle.Size = new System.Drawing.Size(130, 20);
            this.txtItemDetalle.TabIndex = 27;
            // 
            // txtFecha
            // 
            this.txtFecha.Location = new System.Drawing.Point(555, 25);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.ReadOnly = true;
            this.txtFecha.Size = new System.Drawing.Size(130, 20);
            this.txtFecha.TabIndex = 26;
            // 
            // txtTicket
            // 
            this.txtTicket.Location = new System.Drawing.Point(336, 25);
            this.txtTicket.Name = "txtTicket";
            this.txtTicket.ReadOnly = true;
            this.txtTicket.Size = new System.Drawing.Size(130, 20);
            this.txtTicket.TabIndex = 25;
            // 
            // txtTipoDeCultivo
            // 
            this.txtTipoDeCultivo.Location = new System.Drawing.Point(140, 80);
            this.txtTipoDeCultivo.Name = "txtTipoDeCultivo";
            this.txtTipoDeCultivo.ReadOnly = true;
            this.txtTipoDeCultivo.Size = new System.Drawing.Size(130, 20);
            this.txtTipoDeCultivo.TabIndex = 24;
            // 
            // txtlDocumentoAcopio
            // 
            this.txtlDocumentoAcopio.Location = new System.Drawing.Point(140, 51);
            this.txtlDocumentoAcopio.Name = "txtlDocumentoAcopio";
            this.txtlDocumentoAcopio.ReadOnly = true;
            this.txtlDocumentoAcopio.Size = new System.Drawing.Size(130, 20);
            this.txtlDocumentoAcopio.TabIndex = 23;
            // 
            // txtGuiaDeRemision
            // 
            this.txtGuiaDeRemision.Location = new System.Drawing.Point(140, 25);
            this.txtGuiaDeRemision.Name = "txtGuiaDeRemision";
            this.txtGuiaDeRemision.ReadOnly = true;
            this.txtGuiaDeRemision.Size = new System.Drawing.Size(130, 20);
            this.txtGuiaDeRemision.TabIndex = 22;
            // 
            // lblTicket
            // 
            this.lblTicket.AutoSize = true;
            this.lblTicket.Location = new System.Drawing.Point(286, 29);
            this.lblTicket.Name = "lblTicket";
            this.lblTicket.Size = new System.Drawing.Size(46, 13);
            this.lblTicket.TabIndex = 18;
            this.lblTicket.Text = "Ticket : ";
            this.lblTicket.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLote
            // 
            this.lblLote.AutoSize = true;
            this.lblLote.Location = new System.Drawing.Point(517, 82);
            this.lblLote.Name = "lblLote";
            this.lblLote.Size = new System.Drawing.Size(37, 13);
            this.lblLote.TabIndex = 21;
            this.lblLote.Text = "Lote : ";
            // 
            // lblVariedad
            // 
            this.lblVariedad.AutoSize = true;
            this.lblVariedad.Location = new System.Drawing.Point(281, 83);
            this.lblVariedad.Name = "lblVariedad";
            this.lblVariedad.Size = new System.Drawing.Size(55, 13);
            this.lblVariedad.TabIndex = 20;
            this.lblVariedad.Text = "Variedad :";
            this.lblVariedad.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTipoDeCultivo
            // 
            this.lblTipoDeCultivo.AutoSize = true;
            this.lblTipoDeCultivo.Location = new System.Drawing.Point(52, 83);
            this.lblTipoDeCultivo.Name = "lblTipoDeCultivo";
            this.lblTipoDeCultivo.Size = new System.Drawing.Size(83, 13);
            this.lblTipoDeCultivo.TabIndex = 18;
            this.lblTipoDeCultivo.Text = "Tipo de cultivo :";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(509, 28);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(43, 13);
            this.lblFecha.TabIndex = 19;
            this.lblFecha.Text = "Fecha :";
            this.lblFecha.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Location = new System.Drawing.Point(497, 54);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(58, 13);
            this.lblCantidad.TabIndex = 18;
            this.lblCantidad.Text = "Cantidad : ";
            this.lblCantidad.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblItemDetalle
            // 
            this.lblItemDetalle.AutoSize = true;
            this.lblItemDetalle.Location = new System.Drawing.Point(297, 54);
            this.lblItemDetalle.Name = "lblItemDetalle";
            this.lblItemDetalle.Size = new System.Drawing.Size(36, 13);
            this.lblItemDetalle.TabIndex = 2;
            this.lblItemDetalle.Text = "Item : ";
            this.lblItemDetalle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDocumentoAcopio
            // 
            this.lblDocumentoAcopio.AutoSize = true;
            this.lblDocumentoAcopio.Location = new System.Drawing.Point(16, 54);
            this.lblDocumentoAcopio.Name = "lblDocumentoAcopio";
            this.lblDocumentoAcopio.Size = new System.Drawing.Size(119, 13);
            this.lblDocumentoAcopio.TabIndex = 1;
            this.lblDocumentoAcopio.Text = "Documento de Acopio :";
            // 
            // lblGuiaDeRemision
            // 
            this.lblGuiaDeRemision.AutoSize = true;
            this.lblGuiaDeRemision.Location = new System.Drawing.Point(41, 28);
            this.lblGuiaDeRemision.Name = "lblGuiaDeRemision";
            this.lblGuiaDeRemision.Size = new System.Drawing.Size(93, 13);
            this.lblGuiaDeRemision.TabIndex = 0;
            this.lblGuiaDeRemision.Text = "Guía de remisión :";
            // 
            // gbLinea
            // 
            this.gbLinea.Controls.Add(this.txtValidarFecha);
            this.gbLinea.Controls.Add(this.lblFechaHoraRegistroAAbastecimiento);
            this.gbLinea.Controls.Add(this.txtFechaRegistroAAbastecimiento);
            this.gbLinea.Controls.Add(this.cboFormatoDeEmpaque);
            this.gbLinea.Controls.Add(this.lblFormatoDeEmpaque);
            this.gbLinea.Controls.Add(this.cboLinea);
            this.gbLinea.Controls.Add(this.cboHoraCambioDeFormato);
            this.gbLinea.Controls.Add(this.lblLinea);
            this.gbLinea.Controls.Add(this.lblHoraCambioDeFormato);
            this.gbLinea.Location = new System.Drawing.Point(12, 118);
            this.gbLinea.Name = "gbLinea";
            this.gbLinea.Size = new System.Drawing.Size(710, 126);
            this.gbLinea.TabIndex = 204;
            this.gbLinea.TabStop = false;
            this.gbLinea.Text = "Linea:";
            // 
            // txtValidarFecha
            // 
            this.txtValidarFecha.EditingControlDataGridView = null;
            this.txtValidarFecha.EditingControlFormattedValue = "  /  /       :";
            this.txtValidarFecha.EditingControlRowIndex = 0;
            this.txtValidarFecha.EditingControlValueChanged = true;
            this.txtValidarFecha.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txtValidarFecha.Location = new System.Drawing.Point(566, 16);
            this.txtValidarFecha.Mask = "00/00/0000 00:00";
            this.txtValidarFecha.Name = "txtValidarFecha";
            this.txtValidarFecha.P_EsEditable = false;
            this.txtValidarFecha.P_EsModificable = false;
            this.txtValidarFecha.P_ExigeInformacion = false;
            this.txtValidarFecha.P_Hora = null;
            this.txtValidarFecha.P_NombreColumna = null;
            this.txtValidarFecha.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtValidarFecha.Size = new System.Drawing.Size(121, 20);
            this.txtValidarFecha.TabIndex = 21;
            this.txtValidarFecha.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtValidarFecha.ValidatingType = typeof(System.DateTime);
            this.txtValidarFecha.Visible = false;
            // 
            // lblFechaHoraRegistroAAbastecimiento
            // 
            this.lblFechaHoraRegistroAAbastecimiento.AutoSize = true;
            this.lblFechaHoraRegistroAAbastecimiento.Location = new System.Drawing.Point(252, 101);
            this.lblFechaHoraRegistroAAbastecimiento.Name = "lblFechaHoraRegistroAAbastecimiento";
            this.lblFechaHoraRegistroAAbastecimiento.Size = new System.Drawing.Size(88, 13);
            this.lblFechaHoraRegistroAAbastecimiento.TabIndex = 20;
            this.lblFechaHoraRegistroAAbastecimiento.Text = "Hora de registro :";
            this.lblFechaHoraRegistroAAbastecimiento.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFechaRegistroAAbastecimiento
            // 
            this.txtFechaRegistroAAbastecimiento.EditingControlDataGridView = null;
            this.txtFechaRegistroAAbastecimiento.EditingControlFormattedValue = "  /  /       :";
            this.txtFechaRegistroAAbastecimiento.EditingControlRowIndex = 0;
            this.txtFechaRegistroAAbastecimiento.EditingControlValueChanged = true;
            this.txtFechaRegistroAAbastecimiento.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txtFechaRegistroAAbastecimiento.Location = new System.Drawing.Point(372, 98);
            this.txtFechaRegistroAAbastecimiento.Mask = "00/00/0000 00:00";
            this.txtFechaRegistroAAbastecimiento.Name = "txtFechaRegistroAAbastecimiento";
            this.txtFechaRegistroAAbastecimiento.P_EsEditable = false;
            this.txtFechaRegistroAAbastecimiento.P_EsModificable = false;
            this.txtFechaRegistroAAbastecimiento.P_ExigeInformacion = false;
            this.txtFechaRegistroAAbastecimiento.P_Hora = null;
            this.txtFechaRegistroAAbastecimiento.P_NombreColumna = null;
            this.txtFechaRegistroAAbastecimiento.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtFechaRegistroAAbastecimiento.Size = new System.Drawing.Size(121, 20);
            this.txtFechaRegistroAAbastecimiento.TabIndex = 18;
            this.txtFechaRegistroAAbastecimiento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFechaRegistroAAbastecimiento.ValidatingType = typeof(System.DateTime);
            // 
            // cboFormatoDeEmpaque
            // 
            this.cboFormatoDeEmpaque.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.cboFormatoDeEmpaque.Location = new System.Drawing.Point(255, 72);
            this.cboFormatoDeEmpaque.Name = "cboFormatoDeEmpaque";
            this.cboFormatoDeEmpaque.Size = new System.Drawing.Size(238, 20);
            this.cboFormatoDeEmpaque.TabIndex = 15;
            this.cboFormatoDeEmpaque.ThemeName = "VisualStudio2012Light";
            this.cboFormatoDeEmpaque.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.cboFormatoDeEmpaque_SelectedIndexChanged);
            // 
            // lblFormatoDeEmpaque
            // 
            this.lblFormatoDeEmpaque.AutoSize = true;
            this.lblFormatoDeEmpaque.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormatoDeEmpaque.Location = new System.Drawing.Point(181, 75);
            this.lblFormatoDeEmpaque.Name = "lblFormatoDeEmpaque";
            this.lblFormatoDeEmpaque.Size = new System.Drawing.Size(67, 13);
            this.lblFormatoDeEmpaque.TabIndex = 14;
            this.lblFormatoDeEmpaque.Text = "Empaque :";
            // 
            // cboLinea
            // 
            this.cboLinea.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.cboLinea.Location = new System.Drawing.Point(255, 19);
            this.cboLinea.Name = "cboLinea";
            this.cboLinea.Size = new System.Drawing.Size(238, 20);
            this.cboLinea.TabIndex = 11;
            this.cboLinea.ThemeName = "VisualStudio2012Light";
            this.cboLinea.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboLinea_KeyUp);
            this.cboLinea.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.cboLinea_SelectedIndexChanged);
            // 
            // cboHoraCambioDeFormato
            // 
            this.cboHoraCambioDeFormato.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.cboHoraCambioDeFormato.Location = new System.Drawing.Point(255, 45);
            this.cboHoraCambioDeFormato.Name = "cboHoraCambioDeFormato";
            this.cboHoraCambioDeFormato.Size = new System.Drawing.Size(238, 20);
            this.cboHoraCambioDeFormato.TabIndex = 13;
            this.cboHoraCambioDeFormato.ThemeName = "VisualStudio2012Light";
            this.cboHoraCambioDeFormato.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboHoraCambioDeFormato_KeyUp);
            this.cboHoraCambioDeFormato.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.cboHoraCambioDeFormato_SelectedIndexChanged);
            // 
            // lblLinea
            // 
            this.lblLinea.AutoSize = true;
            this.lblLinea.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLinea.Location = new System.Drawing.Point(202, 23);
            this.lblLinea.Name = "lblLinea";
            this.lblLinea.Size = new System.Drawing.Size(46, 13);
            this.lblLinea.TabIndex = 10;
            this.lblLinea.Text = "Linea :";
            // 
            // lblHoraCambioDeFormato
            // 
            this.lblHoraCambioDeFormato.AutoSize = true;
            this.lblHoraCambioDeFormato.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHoraCambioDeFormato.Location = new System.Drawing.Point(205, 48);
            this.lblHoraCambioDeFormato.Name = "lblHoraCambioDeFormato";
            this.lblHoraCambioDeFormato.Size = new System.Drawing.Size(42, 13);
            this.lblHoraCambioDeFormato.TabIndex = 12;
            this.lblHoraCambioDeFormato.Text = "Hora :";
            // 
            // subMenu
            // 
            this.subMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator4,
            this.btnEditarRegistro,
            this.toolStripSeparator1,
            this.btnImprimirEtiquetaPequena,
            this.btnVistaPreviaEtiquetaPequena,
            this.toolStripSeparator2,
            this.btnImprimirEtiquetaGrande,
            this.btnVistaPreviaEtiquetaGrande,
            this.toolStripSeparator3});
            this.subMenu.Name = "subMenu";
            this.subMenu.Size = new System.Drawing.Size(235, 158);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(231, 6);
            // 
            // btnEditarRegistro
            // 
            this.btnEditarRegistro.Enabled = false;
            this.btnEditarRegistro.Image = ((System.Drawing.Image)(resources.GetObject("btnEditarRegistro.Image")));
            this.btnEditarRegistro.Name = "btnEditarRegistro";
            this.btnEditarRegistro.Size = new System.Drawing.Size(234, 26);
            this.btnEditarRegistro.Text = "Editar";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(231, 6);
            // 
            // btnImprimirEtiquetaPequena
            // 
            this.btnImprimirEtiquetaPequena.Enabled = false;
            this.btnImprimirEtiquetaPequena.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimirEtiquetaPequena.Image")));
            this.btnImprimirEtiquetaPequena.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnImprimirEtiquetaPequena.Name = "btnImprimirEtiquetaPequena";
            this.btnImprimirEtiquetaPequena.Size = new System.Drawing.Size(234, 26);
            this.btnImprimirEtiquetaPequena.Text = "Imprimir Etiqueta Pequeña";
            // 
            // btnVistaPreviaEtiquetaPequena
            // 
            this.btnVistaPreviaEtiquetaPequena.Enabled = false;
            this.btnVistaPreviaEtiquetaPequena.Image = ((System.Drawing.Image)(resources.GetObject("btnVistaPreviaEtiquetaPequena.Image")));
            this.btnVistaPreviaEtiquetaPequena.Name = "btnVistaPreviaEtiquetaPequena";
            this.btnVistaPreviaEtiquetaPequena.Size = new System.Drawing.Size(234, 26);
            this.btnVistaPreviaEtiquetaPequena.Text = "Vista Previa Etiqueta Pequeña";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(231, 6);
            // 
            // btnImprimirEtiquetaGrande
            // 
            this.btnImprimirEtiquetaGrande.Enabled = false;
            this.btnImprimirEtiquetaGrande.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimirEtiquetaGrande.Image")));
            this.btnImprimirEtiquetaGrande.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnImprimirEtiquetaGrande.Name = "btnImprimirEtiquetaGrande";
            this.btnImprimirEtiquetaGrande.Size = new System.Drawing.Size(234, 26);
            this.btnImprimirEtiquetaGrande.Text = "Imprimir Etiqueta Grande";
            // 
            // btnVistaPreviaEtiquetaGrande
            // 
            this.btnVistaPreviaEtiquetaGrande.Enabled = false;
            this.btnVistaPreviaEtiquetaGrande.Image = ((System.Drawing.Image)(resources.GetObject("btnVistaPreviaEtiquetaGrande.Image")));
            this.btnVistaPreviaEtiquetaGrande.Name = "btnVistaPreviaEtiquetaGrande";
            this.btnVistaPreviaEtiquetaGrande.Size = new System.Drawing.Size(234, 26);
            this.btnVistaPreviaEtiquetaGrande.Text = "Vista Previa Etiqueta Grande";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(231, 6);
            // 
            // bgwHilo
            // 
            this.bgwHilo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwHilo_DoWork);
            this.bgwHilo.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwHilo_RunWorkerCompleted);
            // 
            // RegistrarTicketALineaDeAbastecimiento
            // 
            this.AcceptButton = this.btnRegistrar;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(733, 327);
            this.Controls.Add(this.stsBarraEstado);
            this.Controls.Add(this.gbAcciones);
            this.Controls.Add(this.gbDatosDelTicket);
            this.Controls.Add(this.gbLinea);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RegistrarTicketALineaDeAbastecimiento";
            this.Text = "Registrar ticket a línea de abastecimiento | Asignación manuel de lectura de tick" +
    "et";
            this.Load += new System.EventHandler(this.RegistrarTicketALineaDeAbastecimiento_Load);
            this.gbAcciones.ResumeLayout(false);
            this.gbDatosDelTicket.ResumeLayout(false);
            this.gbDatosDelTicket.PerformLayout();
            this.gbLinea.ResumeLayout(false);
            this.gbLinea.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboFormatoDeEmpaque)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLinea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboHoraCambioDeFormato)).EndInit();
            this.subMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip stsBarraEstado;
        private System.Windows.Forms.GroupBox gbAcciones;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox gbDatosDelTicket;
        private System.Windows.Forms.TextBox txtLote;
        private System.Windows.Forms.TextBox txtVariedad;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.TextBox txtItemDetalle;
        private System.Windows.Forms.TextBox txtFecha;
        private System.Windows.Forms.TextBox txtTicket;
        private System.Windows.Forms.TextBox txtTipoDeCultivo;
        private System.Windows.Forms.TextBox txtlDocumentoAcopio;
        private System.Windows.Forms.TextBox txtGuiaDeRemision;
        private System.Windows.Forms.Label lblTicket;
        private System.Windows.Forms.Label lblLote;
        private System.Windows.Forms.Label lblVariedad;
        private System.Windows.Forms.Label lblTipoDeCultivo;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Label lblItemDetalle;
        private System.Windows.Forms.Label lblDocumentoAcopio;
        private System.Windows.Forms.Label lblGuiaDeRemision;
        private System.Windows.Forms.GroupBox gbLinea;
        private System.Windows.Forms.Label lblFechaHoraRegistroAAbastecimiento;
        private MyDataGridViewColumns.MyDataGridViewMaskedTextEditingControl txtFechaRegistroAAbastecimiento;
        private Telerik.WinControls.UI.RadDropDownList cboFormatoDeEmpaque;
        private System.Windows.Forms.Label lblFormatoDeEmpaque;
        private Telerik.WinControls.UI.RadDropDownList cboLinea;
        private Telerik.WinControls.UI.RadDropDownList cboHoraCambioDeFormato;
        private System.Windows.Forms.Label lblLinea;
        private System.Windows.Forms.Label lblHoraCambioDeFormato;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ContextMenuStrip subMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem btnEditarRegistro;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem btnImprimirEtiquetaPequena;
        private System.Windows.Forms.ToolStripMenuItem btnVistaPreviaEtiquetaPequena;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem btnImprimirEtiquetaGrande;
        private System.Windows.Forms.ToolStripMenuItem btnVistaPreviaEtiquetaGrande;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private System.ComponentModel.BackgroundWorker bgwHilo;
        private MyDataGridViewColumns.MyDataGridViewMaskedTextEditingControl txtValidarFecha;
    }
}