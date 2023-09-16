namespace ComparativoHorasVisualSATNISIRA.T.I
{
    partial class SolicitudDeRenovaciónDeEquipoCelularActualizarEstadoDeSolicitud
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolicitudDeRenovaciónDeEquipoCelularActualizarEstadoDeSolicitud));
            this.gbCabecera = new System.Windows.Forms.GroupBox();
            this.txtMotivo = new System.Windows.Forms.TextBox();
            this.txtCodigoDocumento = new System.Windows.Forms.TextBox();
            this.txtSerie = new System.Windows.Forms.TextBox();
            this.txtCargo = new System.Windows.Forms.TextBox();
            this.btnCargo = new System.Windows.Forms.Label();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPersonal = new MyControlsDataBinding.Controles.MyTextBoxSearchSimple(this.components);
            this.txtPersonalCodigo = new MyControlsDataBinding.Controles.MyTextBoxSearchSimple(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.lblMotivo = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.txtCorrelativo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFecha = new MyDataGridViewColumns.MyDataGridViewMaskedTextEditingControl();
            this.lblDocumento = new System.Windows.Forms.Label();
            this.txtEstado = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbEstados = new System.Windows.Forms.GroupBox();
            this.lblEstadoAActualizar = new System.Windows.Forms.Label();
            this.cboEstado = new System.Windows.Forms.ComboBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.bgwHilo = new System.ComponentModel.BackgroundWorker();
            this.gbCabecera.SuspendLayout();
            this.gbEstados.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbCabecera
            // 
            this.gbCabecera.Controls.Add(this.txtMotivo);
            this.gbCabecera.Controls.Add(this.txtCodigoDocumento);
            this.gbCabecera.Controls.Add(this.txtSerie);
            this.gbCabecera.Controls.Add(this.txtCargo);
            this.gbCabecera.Controls.Add(this.btnCargo);
            this.gbCabecera.Controls.Add(this.txtDNI);
            this.gbCabecera.Controls.Add(this.label10);
            this.gbCabecera.Controls.Add(this.txtPersonal);
            this.gbCabecera.Controls.Add(this.txtPersonalCodigo);
            this.gbCabecera.Controls.Add(this.label3);
            this.gbCabecera.Controls.Add(this.lblMotivo);
            this.gbCabecera.Controls.Add(this.txtCodigo);
            this.gbCabecera.Controls.Add(this.txtCorrelativo);
            this.gbCabecera.Controls.Add(this.label9);
            this.gbCabecera.Controls.Add(this.txtFecha);
            this.gbCabecera.Controls.Add(this.lblDocumento);
            this.gbCabecera.Controls.Add(this.txtEstado);
            this.gbCabecera.Controls.Add(this.label1);
            this.gbCabecera.Location = new System.Drawing.Point(12, 12);
            this.gbCabecera.Name = "gbCabecera";
            this.gbCabecera.Size = new System.Drawing.Size(616, 168);
            this.gbCabecera.TabIndex = 0;
            this.gbCabecera.TabStop = false;
            this.gbCabecera.Text = "Datos de la solicitud";
            // 
            // txtMotivo
            // 
            this.txtMotivo.BackColor = System.Drawing.Color.White;
            this.txtMotivo.Location = new System.Drawing.Point(198, 77);
            this.txtMotivo.MaxLength = 7;
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.ReadOnly = true;
            this.txtMotivo.Size = new System.Drawing.Size(293, 20);
            this.txtMotivo.TabIndex = 287;
            this.txtMotivo.Text = "RENOVACIÓN";
            this.txtMotivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCodigoDocumento
            // 
            this.txtCodigoDocumento.BackColor = System.Drawing.Color.White;
            this.txtCodigoDocumento.Location = new System.Drawing.Point(198, 45);
            this.txtCodigoDocumento.MaxLength = 7;
            this.txtCodigoDocumento.Name = "txtCodigoDocumento";
            this.txtCodigoDocumento.ReadOnly = true;
            this.txtCodigoDocumento.Size = new System.Drawing.Size(48, 20);
            this.txtCodigoDocumento.TabIndex = 286;
            this.txtCodigoDocumento.Text = "REN";
            this.txtCodigoDocumento.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtSerie
            // 
            this.txtSerie.BackColor = System.Drawing.Color.White;
            this.txtSerie.Location = new System.Drawing.Point(252, 45);
            this.txtSerie.MaxLength = 7;
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.ReadOnly = true;
            this.txtSerie.Size = new System.Drawing.Size(48, 20);
            this.txtSerie.TabIndex = 285;
            this.txtSerie.Text = "0000";
            this.txtSerie.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCargo
            // 
            this.txtCargo.Location = new System.Drawing.Point(75, 139);
            this.txtCargo.Name = "txtCargo";
            this.txtCargo.ReadOnly = true;
            this.txtCargo.Size = new System.Drawing.Size(416, 20);
            this.txtCargo.TabIndex = 284;
            this.txtCargo.Text = "JEFE DE INNOVACION Y TRANSFORMACION DIGITAL";
            // 
            // btnCargo
            // 
            this.btnCargo.AutoSize = true;
            this.btnCargo.Location = new System.Drawing.Point(28, 141);
            this.btnCargo.Name = "btnCargo";
            this.btnCargo.Size = new System.Drawing.Size(41, 13);
            this.btnCargo.TabIndex = 283;
            this.btnCargo.Text = "Cargo :";
            // 
            // txtDNI
            // 
            this.txtDNI.Location = new System.Drawing.Point(527, 112);
            this.txtDNI.MaxLength = 10;
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.ReadOnly = true;
            this.txtDNI.Size = new System.Drawing.Size(74, 20);
            this.txtDNI.TabIndex = 282;
            this.txtDNI.Text = "45038264";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(493, 116);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 13);
            this.label10.TabIndex = 281;
            this.label10.Text = "DNI :";
            // 
            // txtPersonal
            // 
            this.txtPersonal.BackColor = System.Drawing.Color.White;
            this.txtPersonal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPersonal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtPersonal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtPersonal.Location = new System.Drawing.Point(167, 112);
            this.txtPersonal.Name = "txtPersonal";
            this.txtPersonal.P_BotonEnlace = null;
            this.txtPersonal.P_BuscarSoloCodigoExacto = false;
            this.txtPersonal.P_EsEditable = false;
            this.txtPersonal.P_EsModificable = false;
            this.txtPersonal.P_EsPrimaryKey = false;
            this.txtPersonal.P_ExigeInformacion = false;
            this.txtPersonal.P_NombreColumna = null;
            this.txtPersonal.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtPersonal.ReadOnly = true;
            this.txtPersonal.Size = new System.Drawing.Size(324, 20);
            this.txtPersonal.TabIndex = 279;
            this.txtPersonal.Text = "AURAZO CARHUATANTA, ERICK";
            // 
            // txtPersonalCodigo
            // 
            this.txtPersonalCodigo.BackColor = System.Drawing.Color.White;
            this.txtPersonalCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPersonalCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtPersonalCodigo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtPersonalCodigo.Location = new System.Drawing.Point(103, 112);
            this.txtPersonalCodigo.MaxLength = 7;
            this.txtPersonalCodigo.Name = "txtPersonalCodigo";
            this.txtPersonalCodigo.P_BotonEnlace = null;
            this.txtPersonalCodigo.P_BuscarSoloCodigoExacto = false;
            this.txtPersonalCodigo.P_EsEditable = true;
            this.txtPersonalCodigo.P_EsModificable = true;
            this.txtPersonalCodigo.P_EsPrimaryKey = false;
            this.txtPersonalCodigo.P_ExigeInformacion = false;
            this.txtPersonalCodigo.P_NombreColumna = null;
            this.txtPersonalCodigo.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtPersonalCodigo.Size = new System.Drawing.Size(58, 20);
            this.txtPersonalCodigo.TabIndex = 278;
            this.txtPersonalCodigo.Text = "100369";
            this.txtPersonalCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 277;
            this.label3.Text = "Personal :";
            // 
            // lblMotivo
            // 
            this.lblMotivo.AutoSize = true;
            this.lblMotivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMotivo.Location = new System.Drawing.Point(125, 77);
            this.lblMotivo.Name = "lblMotivo";
            this.lblMotivo.Size = new System.Drawing.Size(53, 13);
            this.lblMotivo.TabIndex = 276;
            this.lblMotivo.Text = "Motivo :";
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.Color.White;
            this.txtCodigo.Location = new System.Drawing.Point(57, 19);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.ReadOnly = true;
            this.txtCodigo.Size = new System.Drawing.Size(36, 20);
            this.txtCodigo.TabIndex = 266;
            this.txtCodigo.Text = "0";
            this.txtCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigo.Visible = false;
            // 
            // txtCorrelativo
            // 
            this.txtCorrelativo.BackColor = System.Drawing.Color.White;
            this.txtCorrelativo.Location = new System.Drawing.Point(306, 45);
            this.txtCorrelativo.MaxLength = 7;
            this.txtCorrelativo.Name = "txtCorrelativo";
            this.txtCorrelativo.ReadOnly = true;
            this.txtCorrelativo.Size = new System.Drawing.Size(55, 20);
            this.txtCorrelativo.TabIndex = 262;
            this.txtCorrelativo.Text = "0000000";
            this.txtCorrelativo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 265;
            this.label9.Text = "Codigo";
            this.label9.Visible = false;
            // 
            // txtFecha
            // 
            this.txtFecha.EditingControlDataGridView = null;
            this.txtFecha.EditingControlFormattedValue = "21/06/2022";
            this.txtFecha.EditingControlRowIndex = 0;
            this.txtFecha.EditingControlValueChanged = true;
            this.txtFecha.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txtFecha.Location = new System.Drawing.Point(419, 45);
            this.txtFecha.Mask = "00/00/0000";
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.P_EsEditable = false;
            this.txtFecha.P_EsModificable = false;
            this.txtFecha.P_ExigeInformacion = false;
            this.txtFecha.P_Hora = null;
            this.txtFecha.P_NombreColumna = null;
            this.txtFecha.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtFecha.Size = new System.Drawing.Size(72, 20);
            this.txtFecha.TabIndex = 258;
            this.txtFecha.Text = "21062022";
            this.txtFecha.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFecha.ValidatingType = typeof(System.DateTime);
            // 
            // lblDocumento
            // 
            this.lblDocumento.AutoSize = true;
            this.lblDocumento.Location = new System.Drawing.Point(119, 50);
            this.lblDocumento.Name = "lblDocumento";
            this.lblDocumento.Size = new System.Drawing.Size(68, 13);
            this.lblDocumento.TabIndex = 264;
            this.lblDocumento.Text = "Documento :";
            // 
            // txtEstado
            // 
            this.txtEstado.BackColor = System.Drawing.Color.White;
            this.txtEstado.Location = new System.Drawing.Point(122, 19);
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.ReadOnly = true;
            this.txtEstado.Size = new System.Drawing.Size(369, 20);
            this.txtEstado.TabIndex = 259;
            this.txtEstado.Text = "PENDIENTE";
            this.txtEstado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(367, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 260;
            this.label1.Text = "Fecha :";
            // 
            // gbEstados
            // 
            this.gbEstados.Controls.Add(this.lblEstadoAActualizar);
            this.gbEstados.Controls.Add(this.cboEstado);
            this.gbEstados.Location = new System.Drawing.Point(12, 186);
            this.gbEstados.Name = "gbEstados";
            this.gbEstados.Size = new System.Drawing.Size(616, 59);
            this.gbEstados.TabIndex = 1;
            this.gbEstados.TabStop = false;
            this.gbEstados.Text = "Actualización a estado de la solicitud :";
            // 
            // lblEstadoAActualizar
            // 
            this.lblEstadoAActualizar.AutoSize = true;
            this.lblEstadoAActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstadoAActualizar.Location = new System.Drawing.Point(11, 27);
            this.lblEstadoAActualizar.Name = "lblEstadoAActualizar";
            this.lblEstadoAActualizar.Size = new System.Drawing.Size(119, 13);
            this.lblEstadoAActualizar.TabIndex = 278;
            this.lblEstadoAActualizar.Text = "Estado de solicitud:";
            // 
            // cboEstado
            // 
            this.cboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstado.FormattingEnabled = true;
            this.cboEstado.Location = new System.Drawing.Point(136, 22);
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Size = new System.Drawing.Size(465, 21);
            this.cboEstado.TabIndex = 277;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(180, 251);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(109, 23);
            this.btnCancelar.TabIndex = 265;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnRegistrar.Location = new System.Drawing.Point(372, 251);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(109, 23);
            this.btnRegistrar.TabIndex = 264;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // bgwHilo
            // 
            this.bgwHilo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwHilo_DoWork);
            this.bgwHilo.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwHilo_RunWorkerCompleted);
            // 
            // SolicitudDeRenovaciónDeEquipoCelularActualizarEstadoDeSolicitud
            // 
            this.AcceptButton = this.btnRegistrar;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(636, 283);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.gbEstados);
            this.Controls.Add(this.gbCabecera);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SolicitudDeRenovaciónDeEquipoCelularActualizarEstadoDeSolicitud";
            this.Text = "Actualizar estado de solicitud";
            this.Load += new System.EventHandler(this.SolicitudDeRenovaciónDeEquipoCelularActualizarEstadoDeSolicitud_Load);
            this.gbCabecera.ResumeLayout(false);
            this.gbCabecera.PerformLayout();
            this.gbEstados.ResumeLayout(false);
            this.gbEstados.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbCabecera;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.TextBox txtCorrelativo;
        private System.Windows.Forms.Label label9;
        private MyDataGridViewColumns.MyDataGridViewMaskedTextEditingControl txtFecha;
        private System.Windows.Forms.Label lblDocumento;
        private System.Windows.Forms.TextBox txtEstado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMotivo;
        private System.Windows.Forms.TextBox txtCargo;
        private System.Windows.Forms.Label btnCargo;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.Label label10;
        private MyControlsDataBinding.Controles.MyTextBoxSearchSimple txtPersonal;
        private MyControlsDataBinding.Controles.MyTextBoxSearchSimple txtPersonalCodigo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbEstados;
        private System.Windows.Forms.Label lblEstadoAActualizar;
        private System.Windows.Forms.ComboBox cboEstado;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnRegistrar;
        private System.ComponentModel.BackgroundWorker bgwHilo;
        private System.Windows.Forms.TextBox txtMotivo;
        private System.Windows.Forms.TextBox txtCodigoDocumento;
        private System.Windows.Forms.TextBox txtSerie;
    }
}