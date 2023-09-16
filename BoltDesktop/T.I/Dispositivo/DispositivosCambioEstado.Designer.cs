namespace ComparativoHorasVisualSATNISIRA.T.I
{
    partial class DispositivosCambioEstado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DispositivosCambioEstado));
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.txtEstado = new System.Windows.Forms.TextBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.gbDatosDispositivo = new System.Windows.Forms.GroupBox();
            this.gbMotivoCambio = new System.Windows.Forms.GroupBox();
            this.txtMotivo = new System.Windows.Forms.TextBox();
            this.cboMotivo = new System.Windows.Forms.ComboBox();
            this.lblTipoDispositivo = new System.Windows.Forms.Label();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.gbDatosDispositivo.SuspendLayout();
            this.gbMotivoCambio.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(67, 45);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.ReadOnly = true;
            this.txtNombre.Size = new System.Drawing.Size(532, 20);
            this.txtNombre.TabIndex = 15;
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtCodigo.Location = new System.Drawing.Point(67, 24);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.ReadOnly = true;
            this.txtCodigo.Size = new System.Drawing.Size(156, 20);
            this.txtCodigo.TabIndex = 14;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(7, 45);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(50, 13);
            this.lblNombre.TabIndex = 13;
            this.lblNombre.Text = "Nombre :";
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(13, 27);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(43, 13);
            this.lblCodigo.TabIndex = 12;
            this.lblCodigo.Text = "Código:";
            // 
            // txtEstado
            // 
            this.txtEstado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEstado.Location = new System.Drawing.Point(378, 19);
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.ReadOnly = true;
            this.txtEstado.Size = new System.Drawing.Size(221, 20);
            this.txtEstado.TabIndex = 17;
            // 
            // lblEstado
            // 
            this.lblEstado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(324, 22);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(46, 13);
            this.lblEstado.TabIndex = 16;
            this.lblEstado.Text = "Estado :";
            // 
            // gbDatosDispositivo
            // 
            this.gbDatosDispositivo.Controls.Add(this.txtEstado);
            this.gbDatosDispositivo.Controls.Add(this.lblCodigo);
            this.gbDatosDispositivo.Controls.Add(this.lblEstado);
            this.gbDatosDispositivo.Controls.Add(this.lblNombre);
            this.gbDatosDispositivo.Controls.Add(this.txtNombre);
            this.gbDatosDispositivo.Controls.Add(this.txtCodigo);
            this.gbDatosDispositivo.Location = new System.Drawing.Point(12, 12);
            this.gbDatosDispositivo.Name = "gbDatosDispositivo";
            this.gbDatosDispositivo.Size = new System.Drawing.Size(605, 81);
            this.gbDatosDispositivo.TabIndex = 18;
            this.gbDatosDispositivo.TabStop = false;
            this.gbDatosDispositivo.Text = "Datos del dispositivo";
            // 
            // gbMotivoCambio
            // 
            this.gbMotivoCambio.Controls.Add(this.txtMotivo);
            this.gbMotivoCambio.Controls.Add(this.cboMotivo);
            this.gbMotivoCambio.Controls.Add(this.lblTipoDispositivo);
            this.gbMotivoCambio.Location = new System.Drawing.Point(12, 99);
            this.gbMotivoCambio.Name = "gbMotivoCambio";
            this.gbMotivoCambio.Size = new System.Drawing.Size(608, 108);
            this.gbMotivoCambio.TabIndex = 19;
            this.gbMotivoCambio.TabStop = false;
            this.gbMotivoCambio.Text = "Motivo de cambio";
            // 
            // txtMotivo
            // 
            this.txtMotivo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMotivo.Location = new System.Drawing.Point(16, 46);
            this.txtMotivo.Multiline = true;
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.Size = new System.Drawing.Size(583, 56);
            this.txtMotivo.TabIndex = 19;
            // 
            // cboMotivo
            // 
            this.cboMotivo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMotivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMotivo.FormattingEnabled = true;
            this.cboMotivo.Location = new System.Drawing.Point(178, 19);
            this.cboMotivo.Name = "cboMotivo";
            this.cboMotivo.Size = new System.Drawing.Size(297, 21);
            this.cboMotivo.TabIndex = 17;
            // 
            // lblTipoDispositivo
            // 
            this.lblTipoDispositivo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTipoDispositivo.AutoSize = true;
            this.lblTipoDispositivo.Location = new System.Drawing.Point(127, 23);
            this.lblTipoDispositivo.Name = "lblTipoDispositivo";
            this.lblTipoDispositivo.Size = new System.Drawing.Size(45, 13);
            this.lblTipoDispositivo.TabIndex = 16;
            this.lblTipoDispositivo.Text = "Motivo :";
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfirmar.Location = new System.Drawing.Point(545, 207);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(75, 23);
            this.btnConfirmar.TabIndex = 20;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Location = new System.Drawing.Point(464, 207);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 21;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(12, 207);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 22;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Visible = false;
            // 
            // DispositivosCambioEstado
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btnSalir;
            this.ClientSize = new System.Drawing.Size(632, 233);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.gbMotivoCambio);
            this.Controls.Add(this.gbDatosDispositivo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DispositivosCambioEstado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dispositivos | Registrar Cambio de Estado";
            this.Load += new System.EventHandler(this.DispositivosCambioEstado_Load);
            this.gbDatosDispositivo.ResumeLayout(false);
            this.gbDatosDispositivo.PerformLayout();
            this.gbMotivoCambio.ResumeLayout(false);
            this.gbMotivoCambio.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.TextBox txtEstado;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.GroupBox gbDatosDispositivo;
        private System.Windows.Forms.GroupBox gbMotivoCambio;
        private System.Windows.Forms.ComboBox cboMotivo;
        private System.Windows.Forms.Label lblTipoDispositivo;
        private System.Windows.Forms.TextBox txtMotivo;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnOk;
    }
}