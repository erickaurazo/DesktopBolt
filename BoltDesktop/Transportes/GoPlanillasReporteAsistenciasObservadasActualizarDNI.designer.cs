namespace Asistencia
{
    partial class GoPlanillasReporteAsistenciasObservadasActualizarDNI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoPlanillasReporteAsistenciasObservadasActualizarDNI));
            this.gbDestino = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDNI = new MyControlsDataBinding.Controles.MyTextBoxSearchSimple(this.components);
            this.btnDNIBuscar = new MyControlsDataBinding.Controles.MyButtonSearchSimple(this.components);
            this.txtNombres = new MyControlsDataBinding.Controles.MyTextBoxSearchSimple(this.components);
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.lblPlacaDestino = new System.Windows.Forms.Label();
            this.txtdniObservado = new MyControlsDataBinding.Controles.MyTextBoxSearchSimple(this.components);
            this.txtNombresObservador = new MyControlsDataBinding.Controles.MyTextBoxSearchSimple(this.components);
            this.gbDestino.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDestino
            // 
            this.gbDestino.Controls.Add(this.label1);
            this.gbDestino.Controls.Add(this.txtDNI);
            this.gbDestino.Controls.Add(this.btnDNIBuscar);
            this.gbDestino.Controls.Add(this.txtNombres);
            this.gbDestino.Controls.Add(this.btnCancelar);
            this.gbDestino.Controls.Add(this.btnActualizar);
            this.gbDestino.Controls.Add(this.lblPlacaDestino);
            this.gbDestino.Controls.Add(this.txtdniObservado);
            this.gbDestino.Controls.Add(this.txtNombresObservador);
            this.gbDestino.Location = new System.Drawing.Point(10, 3);
            this.gbDestino.Name = "gbDestino";
            this.gbDestino.Size = new System.Drawing.Size(617, 108);
            this.gbDestino.TabIndex = 203;
            this.gbDestino.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 207;
            this.label1.Text = "Colaborador :";
            // 
            // txtDNI
            // 
            this.txtDNI.BackColor = System.Drawing.Color.White;
            this.txtDNI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtDNI.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDNI.Location = new System.Drawing.Point(139, 45);
            this.txtDNI.MaxLength = 9;
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.P_BotonEnlace = this.btnDNIBuscar;
            this.txtDNI.P_BuscarSoloCodigoExacto = false;
            this.txtDNI.P_EsEditable = false;
            this.txtDNI.P_EsModificable = false;
            this.txtDNI.P_EsPrimaryKey = false;
            this.txtDNI.P_ExigeInformacion = false;
            this.txtDNI.P_NombreColumna = null;
            this.txtDNI.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtDNI.Size = new System.Drawing.Size(73, 20);
            this.txtDNI.TabIndex = 205;
            this.txtDNI.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnDNIBuscar
            // 
            this.btnDNIBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnDNIBuscar.Image")));
            this.btnDNIBuscar.Location = new System.Drawing.Point(111, 43);
            this.btnDNIBuscar.Name = "btnDNIBuscar";
            this.btnDNIBuscar.P_CampoCodigo = "rtrim(IDCODIGOGENERAL)";
            this.btnDNIBuscar.P_CampoDescripcion = "rtrim(NOMBRES)";
            this.btnDNIBuscar.P_EsEditable = true;
            this.btnDNIBuscar.P_EsModificable = true;
            this.btnDNIBuscar.P_FilterByTextBox = null;
            this.btnDNIBuscar.P_TablaConsulta = "ASJ_LISTA_PERSONAL_ASISTENCIA";
            this.btnDNIBuscar.P_TextBoxCodigo = this.txtDNI;
            this.btnDNIBuscar.P_TextBoxDescripcion = this.txtNombres;
            this.btnDNIBuscar.P_TituloFormulario = "Buscar Personal";
            this.btnDNIBuscar.Size = new System.Drawing.Size(24, 23);
            this.btnDNIBuscar.TabIndex = 204;
            this.btnDNIBuscar.UseVisualStyleBackColor = true;
            // 
            // txtNombres
            // 
            this.txtNombres.BackColor = System.Drawing.Color.White;
            this.txtNombres.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtNombres.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtNombres.Location = new System.Drawing.Point(218, 45);
            this.txtNombres.Name = "txtNombres";
            this.txtNombres.P_BotonEnlace = this.btnDNIBuscar;
            this.txtNombres.P_BuscarSoloCodigoExacto = false;
            this.txtNombres.P_EsEditable = false;
            this.txtNombres.P_EsModificable = false;
            this.txtNombres.P_EsPrimaryKey = false;
            this.txtNombres.P_ExigeInformacion = false;
            this.txtNombres.P_NombreColumna = null;
            this.txtNombres.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtNombres.ReadOnly = true;
            this.txtNombres.Size = new System.Drawing.Size(393, 20);
            this.txtNombres.TabIndex = 206;
            this.txtNombres.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCancelar.Location = new System.Drawing.Point(422, 74);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(92, 26);
            this.btnCancelar.TabIndex = 203;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnActualizar.Location = new System.Drawing.Point(520, 74);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(91, 26);
            this.btnActualizar.TabIndex = 202;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // lblPlacaDestino
            // 
            this.lblPlacaDestino.AutoSize = true;
            this.lblPlacaDestino.Location = new System.Drawing.Point(18, 22);
            this.lblPlacaDestino.Name = "lblPlacaDestino";
            this.lblPlacaDestino.Size = new System.Drawing.Size(87, 13);
            this.lblPlacaDestino.TabIndex = 201;
            this.lblPlacaDestino.Text = "DNI Observado :";
            // 
            // txtdniObservado
            // 
            this.txtdniObservado.BackColor = System.Drawing.Color.White;
            this.txtdniObservado.Enabled = false;
            this.txtdniObservado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtdniObservado.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtdniObservado.Location = new System.Drawing.Point(139, 19);
            this.txtdniObservado.MaxLength = 30;
            this.txtdniObservado.Name = "txtdniObservado";
            this.txtdniObservado.P_BotonEnlace = null;
            this.txtdniObservado.P_BuscarSoloCodigoExacto = false;
            this.txtdniObservado.P_EsEditable = false;
            this.txtdniObservado.P_EsModificable = false;
            this.txtdniObservado.P_EsPrimaryKey = false;
            this.txtdniObservado.P_ExigeInformacion = false;
            this.txtdniObservado.P_NombreColumna = null;
            this.txtdniObservado.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtdniObservado.Size = new System.Drawing.Size(73, 20);
            this.txtdniObservado.TabIndex = 199;
            this.txtdniObservado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtNombresObservador
            // 
            this.txtNombresObservador.BackColor = System.Drawing.Color.White;
            this.txtNombresObservador.Enabled = false;
            this.txtNombresObservador.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtNombresObservador.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtNombresObservador.Location = new System.Drawing.Point(218, 19);
            this.txtNombresObservador.Name = "txtNombresObservador";
            this.txtNombresObservador.P_BotonEnlace = null;
            this.txtNombresObservador.P_BuscarSoloCodigoExacto = false;
            this.txtNombresObservador.P_EsEditable = false;
            this.txtNombresObservador.P_EsModificable = false;
            this.txtNombresObservador.P_EsPrimaryKey = false;
            this.txtNombresObservador.P_ExigeInformacion = false;
            this.txtNombresObservador.P_NombreColumna = null;
            this.txtNombresObservador.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtNombresObservador.ReadOnly = true;
            this.txtNombresObservador.Size = new System.Drawing.Size(393, 20);
            this.txtNombresObservador.TabIndex = 200;
            this.txtNombresObservador.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // GoPlanillasReporteAsistenciasObservadasActualizarDNI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(639, 114);
            this.Controls.Add(this.gbDestino);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GoPlanillasReporteAsistenciasObservadasActualizarDNI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Movimiento|  Registro de asistencia del personal|  Actualizar DNI observado";
            this.Load += new System.EventHandler(this.ActualizarDNIObservado_Load);
            this.gbDestino.ResumeLayout(false);
            this.gbDestino.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDestino;
        private System.Windows.Forms.Label label1;
        private MyControlsDataBinding.Controles.MyTextBoxSearchSimple txtDNI;
        private MyControlsDataBinding.Controles.MyButtonSearchSimple btnDNIBuscar;
        private MyControlsDataBinding.Controles.MyTextBoxSearchSimple txtNombres;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Label lblPlacaDestino;
        private MyControlsDataBinding.Controles.MyTextBoxSearchSimple txtdniObservado;
        private MyControlsDataBinding.Controles.MyTextBoxSearchSimple txtNombresObservador;
    }
}