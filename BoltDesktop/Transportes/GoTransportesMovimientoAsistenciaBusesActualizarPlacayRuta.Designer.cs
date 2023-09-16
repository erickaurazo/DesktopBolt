namespace Asistencia
{
    partial class GoTransportesMovimientoAsistenciaBusesActualizarPlacayRuta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoTransportesMovimientoAsistenciaBusesActualizarPlacayRuta));
            this.gbRegistro = new System.Windows.Forms.GroupBox();
            this.txtRouteId = new System.Windows.Forms.TextBox();
            this.lblRuta = new System.Windows.Forms.Label();
            this.txtTipoAsistencia = new System.Windows.Forms.TextBox();
            this.lblTipoAsistencia = new System.Windows.Forms.Label();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.lblPlaca = new System.Windows.Forms.Label();
            this.txtEmpresaTransporte = new System.Windows.Forms.TextBox();
            this.lblEmpresaTransporte = new System.Windows.Forms.Label();
            this.txtFecha = new System.Windows.Forms.MaskedTextBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.txtEmpresaTransporteRUC = new System.Windows.Forms.TextBox();
            this.btnPlanillaBuscar = new MyControlsDataBinding.Controles.MyButtonSearchSimple(this.components);
            this.txtPlacaCodigoNuevo = new MyControlsDataBinding.Controles.MyTextBoxSearchSimple(this.components);
            this.txtRazonSocial = new MyControlsDataBinding.Controles.MyTextBoxSearchSimple(this.components);
            this.lblPlacaDestino = new System.Windows.Forms.Label();
            this.gbDestino = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRutaCodigo = new MyControlsDataBinding.Controles.MyTextBoxSearchSimple(this.components);
            this.btnRuta = new MyControlsDataBinding.Controles.MyButtonSearchSimple(this.components);
            this.txtRutaDescripcion = new MyControlsDataBinding.Controles.MyTextBoxSearchSimple(this.components);
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.bgwHilo = new System.ComponentModel.BackgroundWorker();
            this.gbRegistro.SuspendLayout();
            this.gbDestino.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbRegistro
            // 
            this.gbRegistro.Controls.Add(this.txtRouteId);
            this.gbRegistro.Controls.Add(this.lblRuta);
            this.gbRegistro.Controls.Add(this.txtTipoAsistencia);
            this.gbRegistro.Controls.Add(this.lblTipoAsistencia);
            this.gbRegistro.Controls.Add(this.txtPlaca);
            this.gbRegistro.Controls.Add(this.lblPlaca);
            this.gbRegistro.Controls.Add(this.txtEmpresaTransporte);
            this.gbRegistro.Controls.Add(this.lblEmpresaTransporte);
            this.gbRegistro.Controls.Add(this.txtFecha);
            this.gbRegistro.Controls.Add(this.lblFecha);
            this.gbRegistro.Controls.Add(this.txtEmpresaTransporteRUC);
            this.gbRegistro.Location = new System.Drawing.Point(11, 12);
            this.gbRegistro.Name = "gbRegistro";
            this.gbRegistro.Size = new System.Drawing.Size(633, 139);
            this.gbRegistro.TabIndex = 0;
            this.gbRegistro.TabStop = false;
            this.gbRegistro.Text = "Registro de asistencia.";
            // 
            // txtRouteId
            // 
            this.txtRouteId.BackColor = System.Drawing.SystemColors.Control;
            this.txtRouteId.Enabled = false;
            this.txtRouteId.Location = new System.Drawing.Point(65, 104);
            this.txtRouteId.Name = "txtRouteId";
            this.txtRouteId.Size = new System.Drawing.Size(31, 20);
            this.txtRouteId.TabIndex = 204;
            this.txtRouteId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblRuta
            // 
            this.lblRuta.AutoSize = true;
            this.lblRuta.Location = new System.Drawing.Point(102, 111);
            this.lblRuta.Name = "lblRuta";
            this.lblRuta.Size = new System.Drawing.Size(36, 13);
            this.lblRuta.TabIndex = 8;
            this.lblRuta.Text = "Ruta :";
            // 
            // txtTipoAsistencia
            // 
            this.txtTipoAsistencia.BackColor = System.Drawing.SystemColors.Control;
            this.txtTipoAsistencia.Enabled = false;
            this.txtTipoAsistencia.Location = new System.Drawing.Point(502, 20);
            this.txtTipoAsistencia.Name = "txtTipoAsistencia";
            this.txtTipoAsistencia.Size = new System.Drawing.Size(125, 20);
            this.txtTipoAsistencia.TabIndex = 7;
            this.txtTipoAsistencia.Text = "INGRESO";
            this.txtTipoAsistencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblTipoAsistencia
            // 
            this.lblTipoAsistencia.AutoSize = true;
            this.lblTipoAsistencia.Location = new System.Drawing.Point(397, 23);
            this.lblTipoAsistencia.Name = "lblTipoAsistencia";
            this.lblTipoAsistencia.Size = new System.Drawing.Size(99, 13);
            this.lblTipoAsistencia.TabIndex = 7;
            this.lblTipoAsistencia.Text = "Tipo de asistencia :";
            // 
            // txtPlaca
            // 
            this.txtPlaca.BackColor = System.Drawing.SystemColors.Control;
            this.txtPlaca.Enabled = false;
            this.txtPlaca.Location = new System.Drawing.Point(145, 80);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(125, 20);
            this.txtPlaca.TabIndex = 6;
            this.txtPlaca.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblPlaca
            // 
            this.lblPlaca.AutoSize = true;
            this.lblPlaca.Location = new System.Drawing.Point(99, 84);
            this.lblPlaca.Name = "lblPlaca";
            this.lblPlaca.Size = new System.Drawing.Size(40, 13);
            this.lblPlaca.TabIndex = 5;
            this.lblPlaca.Text = "Placa :";
            // 
            // txtEmpresaTransporte
            // 
            this.txtEmpresaTransporte.BackColor = System.Drawing.SystemColors.Control;
            this.txtEmpresaTransporte.Enabled = false;
            this.txtEmpresaTransporte.Location = new System.Drawing.Point(273, 53);
            this.txtEmpresaTransporte.Name = "txtEmpresaTransporte";
            this.txtEmpresaTransporte.Size = new System.Drawing.Size(354, 20);
            this.txtEmpresaTransporte.TabIndex = 4;
            this.txtEmpresaTransporte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblEmpresaTransporte
            // 
            this.lblEmpresaTransporte.AutoSize = true;
            this.lblEmpresaTransporte.Location = new System.Drawing.Point(20, 56);
            this.lblEmpresaTransporte.Name = "lblEmpresaTransporte";
            this.lblEmpresaTransporte.Size = new System.Drawing.Size(119, 13);
            this.lblEmpresaTransporte.TabIndex = 3;
            this.lblEmpresaTransporte.Text = "Empresa de transporte :";
            // 
            // txtFecha
            // 
            this.txtFecha.BackColor = System.Drawing.SystemColors.Control;
            this.txtFecha.Enabled = false;
            this.txtFecha.Location = new System.Drawing.Point(69, 23);
            this.txtFecha.Mask = "00/00/0000";
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(71, 20);
            this.txtFecha.TabIndex = 2;
            this.txtFecha.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFecha.ValidatingType = typeof(System.DateTime);
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(20, 26);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(43, 13);
            this.lblFecha.TabIndex = 1;
            this.lblFecha.Text = "Fecha :";
            // 
            // txtEmpresaTransporteRUC
            // 
            this.txtEmpresaTransporteRUC.BackColor = System.Drawing.SystemColors.Control;
            this.txtEmpresaTransporteRUC.Enabled = false;
            this.txtEmpresaTransporteRUC.Location = new System.Drawing.Point(145, 53);
            this.txtEmpresaTransporteRUC.Name = "txtEmpresaTransporteRUC";
            this.txtEmpresaTransporteRUC.Size = new System.Drawing.Size(125, 20);
            this.txtEmpresaTransporteRUC.TabIndex = 0;
            this.txtEmpresaTransporteRUC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnPlanillaBuscar
            // 
            this.btnPlanillaBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnPlanillaBuscar.Image")));
            this.btnPlanillaBuscar.Location = new System.Drawing.Point(111, 17);
            this.btnPlanillaBuscar.Name = "btnPlanillaBuscar";
            this.btnPlanillaBuscar.P_CampoCodigo = "rtrim(placa)";
            this.btnPlanillaBuscar.P_CampoDescripcion = "rtrim(nombreCorto)";
            this.btnPlanillaBuscar.P_EsEditable = true;
            this.btnPlanillaBuscar.P_EsModificable = true;
            this.btnPlanillaBuscar.P_FilterByTextBox = null;
            this.btnPlanillaBuscar.P_TablaConsulta = "sj_rhtransportista";
            this.btnPlanillaBuscar.P_TextBoxCodigo = this.txtPlacaCodigoNuevo;
            this.btnPlanillaBuscar.P_TextBoxDescripcion = this.txtRazonSocial;
            this.btnPlanillaBuscar.P_TituloFormulario = "";
            this.btnPlanillaBuscar.Size = new System.Drawing.Size(24, 23);
            this.btnPlanillaBuscar.TabIndex = 198;
            this.btnPlanillaBuscar.UseVisualStyleBackColor = true;
            // 
            // txtPlacaCodigoNuevo
            // 
            this.txtPlacaCodigoNuevo.BackColor = System.Drawing.Color.White;
            this.txtPlacaCodigoNuevo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtPlacaCodigoNuevo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtPlacaCodigoNuevo.Location = new System.Drawing.Point(139, 19);
            this.txtPlacaCodigoNuevo.MaxLength = 30;
            this.txtPlacaCodigoNuevo.Name = "txtPlacaCodigoNuevo";
            this.txtPlacaCodigoNuevo.P_BotonEnlace = this.btnPlanillaBuscar;
            this.txtPlacaCodigoNuevo.P_BuscarSoloCodigoExacto = false;
            this.txtPlacaCodigoNuevo.P_EsEditable = false;
            this.txtPlacaCodigoNuevo.P_EsModificable = false;
            this.txtPlacaCodigoNuevo.P_EsPrimaryKey = false;
            this.txtPlacaCodigoNuevo.P_ExigeInformacion = false;
            this.txtPlacaCodigoNuevo.P_NombreColumna = null;
            this.txtPlacaCodigoNuevo.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtPlacaCodigoNuevo.Size = new System.Drawing.Size(73, 20);
            this.txtPlacaCodigoNuevo.TabIndex = 199;
            this.txtPlacaCodigoNuevo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtRazonSocial
            // 
            this.txtRazonSocial.BackColor = System.Drawing.Color.White;
            this.txtRazonSocial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtRazonSocial.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtRazonSocial.Location = new System.Drawing.Point(218, 19);
            this.txtRazonSocial.Name = "txtRazonSocial";
            this.txtRazonSocial.P_BotonEnlace = this.btnPlanillaBuscar;
            this.txtRazonSocial.P_BuscarSoloCodigoExacto = false;
            this.txtRazonSocial.P_EsEditable = false;
            this.txtRazonSocial.P_EsModificable = false;
            this.txtRazonSocial.P_EsPrimaryKey = false;
            this.txtRazonSocial.P_ExigeInformacion = false;
            this.txtRazonSocial.P_NombreColumna = null;
            this.txtRazonSocial.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtRazonSocial.ReadOnly = true;
            this.txtRazonSocial.Size = new System.Drawing.Size(408, 20);
            this.txtRazonSocial.TabIndex = 200;
            this.txtRazonSocial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblPlacaDestino
            // 
            this.lblPlacaDestino.AutoSize = true;
            this.lblPlacaDestino.Location = new System.Drawing.Point(28, 22);
            this.lblPlacaDestino.Name = "lblPlacaDestino";
            this.lblPlacaDestino.Size = new System.Drawing.Size(77, 13);
            this.lblPlacaDestino.TabIndex = 201;
            this.lblPlacaDestino.Text = "Placa destino :";
            // 
            // gbDestino
            // 
            this.gbDestino.Controls.Add(this.label1);
            this.gbDestino.Controls.Add(this.txtRutaCodigo);
            this.gbDestino.Controls.Add(this.btnRuta);
            this.gbDestino.Controls.Add(this.txtRutaDescripcion);
            this.gbDestino.Controls.Add(this.btnCancelar);
            this.gbDestino.Controls.Add(this.btnActualizar);
            this.gbDestino.Controls.Add(this.lblPlacaDestino);
            this.gbDestino.Controls.Add(this.txtPlacaCodigoNuevo);
            this.gbDestino.Controls.Add(this.btnPlanillaBuscar);
            this.gbDestino.Controls.Add(this.txtRazonSocial);
            this.gbDestino.Location = new System.Drawing.Point(12, 154);
            this.gbDestino.Name = "gbDestino";
            this.gbDestino.Size = new System.Drawing.Size(632, 108);
            this.gbDestino.TabIndex = 202;
            this.gbDestino.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(69, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 207;
            this.label1.Text = "Ruta :";
            // 
            // txtRutaCodigo
            // 
            this.txtRutaCodigo.BackColor = System.Drawing.Color.White;
            this.txtRutaCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtRutaCodigo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtRutaCodigo.Location = new System.Drawing.Point(139, 45);
            this.txtRutaCodigo.MaxLength = 30;
            this.txtRutaCodigo.Name = "txtRutaCodigo";
            this.txtRutaCodigo.P_BotonEnlace = this.btnRuta;
            this.txtRutaCodigo.P_BuscarSoloCodigoExacto = false;
            this.txtRutaCodigo.P_EsEditable = false;
            this.txtRutaCodigo.P_EsModificable = false;
            this.txtRutaCodigo.P_EsPrimaryKey = false;
            this.txtRutaCodigo.P_ExigeInformacion = false;
            this.txtRutaCodigo.P_NombreColumna = null;
            this.txtRutaCodigo.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtRutaCodigo.Size = new System.Drawing.Size(73, 20);
            this.txtRutaCodigo.TabIndex = 205;
            this.txtRutaCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnRuta
            // 
            this.btnRuta.Image = ((System.Drawing.Image)(resources.GetObject("btnRuta.Image")));
            this.btnRuta.Location = new System.Drawing.Point(111, 43);
            this.btnRuta.Name = "btnRuta";
            this.btnRuta.P_CampoCodigo = "id";
            this.btnRuta.P_CampoDescripcion = "rtrim(descripcionCortaOrigen) + \' \' +upper(descripcionCortaDestino)";
            this.btnRuta.P_EsEditable = true;
            this.btnRuta.P_EsModificable = true;
            this.btnRuta.P_FilterByTextBox = null;
            this.btnRuta.P_TablaConsulta = "SJ_RHRuta";
            this.btnRuta.P_TextBoxCodigo = this.txtRutaCodigo;
            this.btnRuta.P_TextBoxDescripcion = this.txtRutaDescripcion;
            this.btnRuta.P_TituloFormulario = "";
            this.btnRuta.Size = new System.Drawing.Size(24, 23);
            this.btnRuta.TabIndex = 204;
            this.btnRuta.UseVisualStyleBackColor = true;
            // 
            // txtRutaDescripcion
            // 
            this.txtRutaDescripcion.BackColor = System.Drawing.Color.White;
            this.txtRutaDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtRutaDescripcion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtRutaDescripcion.Location = new System.Drawing.Point(218, 45);
            this.txtRutaDescripcion.Name = "txtRutaDescripcion";
            this.txtRutaDescripcion.P_BotonEnlace = this.btnRuta;
            this.txtRutaDescripcion.P_BuscarSoloCodigoExacto = false;
            this.txtRutaDescripcion.P_EsEditable = false;
            this.txtRutaDescripcion.P_EsModificable = false;
            this.txtRutaDescripcion.P_EsPrimaryKey = false;
            this.txtRutaDescripcion.P_ExigeInformacion = false;
            this.txtRutaDescripcion.P_NombreColumna = null;
            this.txtRutaDescripcion.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtRutaDescripcion.ReadOnly = true;
            this.txtRutaDescripcion.Size = new System.Drawing.Size(408, 20);
            this.txtRutaDescripcion.TabIndex = 206;
            this.txtRutaDescripcion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCancelar.Location = new System.Drawing.Point(408, 71);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(106, 26);
            this.btnCancelar.TabIndex = 203;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(520, 71);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(106, 26);
            this.btnActualizar.TabIndex = 202;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // txtRuta
            // 
            this.txtRuta.BackColor = System.Drawing.SystemColors.Control;
            this.txtRuta.Enabled = false;
            this.txtRuta.Location = new System.Drawing.Point(156, 120);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(482, 20);
            this.txtRuta.TabIndex = 203;
            this.txtRuta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // bgwHilo
            // 
            this.bgwHilo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwHilo_DoWork);
            this.bgwHilo.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwHilo_RunWorkerCompleted);
            // 
            // GoTransportesMovimientoAsistenciaBusesActualizarPlacayRuta
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(656, 274);
            this.Controls.Add(this.txtRuta);
            this.Controls.Add(this.gbDestino);
            this.Controls.Add(this.gbRegistro);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GoTransportesMovimientoAsistenciaBusesActualizarPlacayRuta";
            this.Text = "Movimiento|  Registro de asistencia|  Actualizar de placa y/o ruta";
            this.Load += new System.EventHandler(this.ActualizarPlaca_Load);
            this.gbRegistro.ResumeLayout(false);
            this.gbRegistro.PerformLayout();
            this.gbDestino.ResumeLayout(false);
            this.gbDestino.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbRegistro;
        private System.Windows.Forms.TextBox txtTipoAsistencia;
        private System.Windows.Forms.Label lblTipoAsistencia;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.Label lblPlaca;
        private System.Windows.Forms.TextBox txtEmpresaTransporte;
        private System.Windows.Forms.Label lblEmpresaTransporte;
        private System.Windows.Forms.MaskedTextBox txtFecha;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.TextBox txtEmpresaTransporteRUC;
        private MyControlsDataBinding.Controles.MyButtonSearchSimple btnPlanillaBuscar;
        private MyControlsDataBinding.Controles.MyTextBoxSearchSimple txtPlacaCodigoNuevo;
        private MyControlsDataBinding.Controles.MyTextBoxSearchSimple txtRazonSocial;
        private System.Windows.Forms.Label lblPlacaDestino;
        private System.Windows.Forms.GroupBox gbDestino;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Label lblRuta;
        private System.Windows.Forms.TextBox txtRuta;
        private System.ComponentModel.BackgroundWorker bgwHilo;
        private System.Windows.Forms.Label label1;
        private MyControlsDataBinding.Controles.MyTextBoxSearchSimple txtRutaCodigo;
        private MyControlsDataBinding.Controles.MyButtonSearchSimple btnRuta;
        private MyControlsDataBinding.Controles.MyTextBoxSearchSimple txtRutaDescripcion;
        private System.Windows.Forms.TextBox txtRouteId;
    }
}