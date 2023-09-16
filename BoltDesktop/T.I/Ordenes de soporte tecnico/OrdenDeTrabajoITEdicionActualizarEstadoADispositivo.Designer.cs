namespace ComparativoHorasVisualSATNISIRA.T.I.Ordenes_de_soporte_tecnico
{
    partial class OrdenDeTrabajoITEdicionActualizarEstadoADispositivo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrdenDeTrabajoITEdicionActualizarEstadoADispositivo));
            this.gbReasignarCaso = new System.Windows.Forms.GroupBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblDocumento = new System.Windows.Forms.Label();
            this.cboDocumento = new System.Windows.Forms.ComboBox();
            this.txtCorrelativo = new System.Windows.Forms.TextBox();
            this.cboSerie = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEstado = new System.Windows.Forms.TextBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.txtFecha = new MyDataGridViewColumns.MyDataGridViewMaskedTextEditingControl();
            this.gbColaborador = new System.Windows.Forms.GroupBox();
            this.txtComentario03 = new MyControlsDataBinding.Controles.MyTextBox(this.components);
            this.txtComentario02 = new MyControlsDataBinding.Controles.MyTextBox(this.components);
            this.txtComentario01 = new MyControlsDataBinding.Controles.MyTextBox(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.cboEstadoDispositivo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDispositivo = new System.Windows.Forms.TextBox();
            this.stsBarraEstado = new System.Windows.Forms.StatusStrip();
            this.progressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.bgwHilo = new System.ComponentModel.BackgroundWorker();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.subMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnAnularRegistro = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEditarRegistro = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEliminarRegistro = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnVerDispositivo = new System.Windows.Forms.ToolStripMenuItem();
            this.btnVerHistorial = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAsociarAreaDeTrabajo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.gbReasignarCaso.SuspendLayout();
            this.gbColaborador.SuspendLayout();
            this.stsBarraEstado.SuspendLayout();
            this.subMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbReasignarCaso
            // 
            this.gbReasignarCaso.Controls.Add(this.txtDispositivo);
            this.gbReasignarCaso.Controls.Add(this.txtCodigo);
            this.gbReasignarCaso.Controls.Add(this.label9);
            this.gbReasignarCaso.Controls.Add(this.lblDocumento);
            this.gbReasignarCaso.Controls.Add(this.cboDocumento);
            this.gbReasignarCaso.Controls.Add(this.txtCorrelativo);
            this.gbReasignarCaso.Controls.Add(this.cboSerie);
            this.gbReasignarCaso.Controls.Add(this.label1);
            this.gbReasignarCaso.Controls.Add(this.txtEstado);
            this.gbReasignarCaso.Controls.Add(this.lblEstado);
            this.gbReasignarCaso.Controls.Add(this.txtFecha);
            this.gbReasignarCaso.Location = new System.Drawing.Point(12, 12);
            this.gbReasignarCaso.Name = "gbReasignarCaso";
            this.gbReasignarCaso.Size = new System.Drawing.Size(880, 70);
            this.gbReasignarCaso.TabIndex = 206;
            this.gbReasignarCaso.TabStop = false;
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.Color.White;
            this.txtCodigo.Location = new System.Drawing.Point(50, 0);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.ReadOnly = true;
            this.txtCodigo.Size = new System.Drawing.Size(34, 20);
            this.txtCodigo.TabIndex = 259;
            this.txtCodigo.Text = "0";
            this.txtCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigo.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(2, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 258;
            this.label9.Text = "Codigo";
            this.label9.Visible = false;
            // 
            // lblDocumento
            // 
            this.lblDocumento.AutoSize = true;
            this.lblDocumento.Location = new System.Drawing.Point(469, 24);
            this.lblDocumento.Name = "lblDocumento";
            this.lblDocumento.Size = new System.Drawing.Size(68, 13);
            this.lblDocumento.TabIndex = 261;
            this.lblDocumento.Text = "Documento :";
            // 
            // cboDocumento
            // 
            this.cboDocumento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDocumento.FormattingEnabled = true;
            this.cboDocumento.Location = new System.Drawing.Point(540, 19);
            this.cboDocumento.Name = "cboDocumento";
            this.cboDocumento.Size = new System.Drawing.Size(50, 21);
            this.cboDocumento.TabIndex = 260;
            // 
            // txtCorrelativo
            // 
            this.txtCorrelativo.BackColor = System.Drawing.Color.White;
            this.txtCorrelativo.Location = new System.Drawing.Point(658, 20);
            this.txtCorrelativo.MaxLength = 7;
            this.txtCorrelativo.Name = "txtCorrelativo";
            this.txtCorrelativo.ReadOnly = true;
            this.txtCorrelativo.Size = new System.Drawing.Size(72, 20);
            this.txtCorrelativo.TabIndex = 259;
            this.txtCorrelativo.Text = "0000000";
            this.txtCorrelativo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cboSerie
            // 
            this.cboSerie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSerie.FormattingEnabled = true;
            this.cboSerie.Location = new System.Drawing.Point(596, 19);
            this.cboSerie.Name = "cboSerie";
            this.cboSerie.Size = new System.Drawing.Size(56, 21);
            this.cboSerie.TabIndex = 258;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(751, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 257;
            this.label1.Text = "Fecha :";
            // 
            // txtEstado
            // 
            this.txtEstado.BackColor = System.Drawing.Color.White;
            this.txtEstado.Location = new System.Drawing.Point(87, 19);
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.ReadOnly = true;
            this.txtEstado.Size = new System.Drawing.Size(332, 20);
            this.txtEstado.TabIndex = 256;
            this.txtEstado.Text = "PENDIENTE";
            this.txtEstado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(37, 22);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(46, 13);
            this.lblEstado.TabIndex = 255;
            this.lblEstado.Text = "Estado :";
            // 
            // txtFecha
            // 
            this.txtFecha.EditingControlDataGridView = null;
            this.txtFecha.EditingControlFormattedValue = "30/04/2020";
            this.txtFecha.EditingControlRowIndex = 0;
            this.txtFecha.EditingControlValueChanged = true;
            this.txtFecha.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txtFecha.Location = new System.Drawing.Point(800, 18);
            this.txtFecha.Mask = "00/00/0000";
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.P_EsEditable = false;
            this.txtFecha.P_EsModificable = false;
            this.txtFecha.P_ExigeInformacion = false;
            this.txtFecha.P_Hora = null;
            this.txtFecha.P_NombreColumna = null;
            this.txtFecha.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtFecha.ReadOnly = true;
            this.txtFecha.Size = new System.Drawing.Size(72, 20);
            this.txtFecha.TabIndex = 254;
            this.txtFecha.Text = "30042020";
            this.txtFecha.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFecha.ValidatingType = typeof(System.DateTime);
            // 
            // gbColaborador
            // 
            this.gbColaborador.Controls.Add(this.txtComentario03);
            this.gbColaborador.Controls.Add(this.txtComentario02);
            this.gbColaborador.Controls.Add(this.txtComentario01);
            this.gbColaborador.Controls.Add(this.label4);
            this.gbColaborador.Controls.Add(this.label3);
            this.gbColaborador.Controls.Add(this.label2);
            this.gbColaborador.Location = new System.Drawing.Point(12, 84);
            this.gbColaborador.Name = "gbColaborador";
            this.gbColaborador.Size = new System.Drawing.Size(886, 242);
            this.gbColaborador.TabIndex = 207;
            this.gbColaborador.TabStop = false;
            // 
            // txtComentario03
            // 
            this.txtComentario03.Enabled = false;
            this.txtComentario03.Location = new System.Drawing.Point(18, 180);
            this.txtComentario03.MaxLength = 1500;
            this.txtComentario03.Multiline = true;
            this.txtComentario03.Name = "txtComentario03";
            this.txtComentario03.P_EsEditable = false;
            this.txtComentario03.P_EsModificable = false;
            this.txtComentario03.P_EsPrimaryKey = false;
            this.txtComentario03.P_ExigeInformacion = false;
            this.txtComentario03.P_NombreColumna = null;
            this.txtComentario03.P_NroDecimales = 0;
            this.txtComentario03.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtComentario03.P_ToCopyInsertLeft = null;
            this.txtComentario03.ReadOnly = true;
            this.txtComentario03.Size = new System.Drawing.Size(862, 58);
            this.txtComentario03.TabIndex = 269;
            // 
            // txtComentario02
            // 
            this.txtComentario02.Enabled = false;
            this.txtComentario02.Location = new System.Drawing.Point(18, 99);
            this.txtComentario02.MaxLength = 1500;
            this.txtComentario02.Multiline = true;
            this.txtComentario02.Name = "txtComentario02";
            this.txtComentario02.P_EsEditable = false;
            this.txtComentario02.P_EsModificable = false;
            this.txtComentario02.P_EsPrimaryKey = false;
            this.txtComentario02.P_ExigeInformacion = false;
            this.txtComentario02.P_NombreColumna = null;
            this.txtComentario02.P_NroDecimales = 0;
            this.txtComentario02.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtComentario02.P_ToCopyInsertLeft = null;
            this.txtComentario02.ReadOnly = true;
            this.txtComentario02.Size = new System.Drawing.Size(862, 57);
            this.txtComentario02.TabIndex = 268;
            // 
            // txtComentario01
            // 
            this.txtComentario01.Enabled = false;
            this.txtComentario01.Location = new System.Drawing.Point(18, 27);
            this.txtComentario01.MaxLength = 1500;
            this.txtComentario01.Multiline = true;
            this.txtComentario01.Name = "txtComentario01";
            this.txtComentario01.P_EsEditable = false;
            this.txtComentario01.P_EsModificable = false;
            this.txtComentario01.P_EsPrimaryKey = false;
            this.txtComentario01.P_ExigeInformacion = false;
            this.txtComentario01.P_NombreColumna = null;
            this.txtComentario01.P_NroDecimales = 0;
            this.txtComentario01.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtComentario01.P_ToCopyInsertLeft = null;
            this.txtComentario01.ReadOnly = true;
            this.txtComentario01.Size = new System.Drawing.Size(862, 47);
            this.txtComentario01.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 266;
            this.label4.Text = "Comentario 03 :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 265;
            this.label3.Text = "Comentario 02 :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 264;
            this.label2.Text = "Comentario 01 :";
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(297, 358);
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
            this.btnRegistrar.Location = new System.Drawing.Point(455, 358);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(109, 23);
            this.btnRegistrar.TabIndex = 264;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // cboEstadoDispositivo
            // 
            this.cboEstadoDispositivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstadoDispositivo.FormattingEnabled = true;
            this.cboEstadoDispositivo.Location = new System.Drawing.Point(262, 333);
            this.cboEstadoDispositivo.Name = "cboEstadoDispositivo";
            this.cboEstadoDispositivo.Size = new System.Drawing.Size(391, 21);
            this.cboEstadoDispositivo.TabIndex = 266;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(141, 336);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 13);
            this.label5.TabIndex = 267;
            this.label5.Text = "Estado del dispositivo :";
            // 
            // txtDispositivo
            // 
            this.txtDispositivo.BackColor = System.Drawing.Color.White;
            this.txtDispositivo.Location = new System.Drawing.Point(87, 45);
            this.txtDispositivo.Name = "txtDispositivo";
            this.txtDispositivo.ReadOnly = true;
            this.txtDispositivo.Size = new System.Drawing.Size(785, 20);
            this.txtDispositivo.TabIndex = 268;
            this.txtDispositivo.Text = "Equipo de cómputo Lenovo Thinkpad M720";
            // 
            // stsBarraEstado
            // 
            this.stsBarraEstado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar1});
            this.stsBarraEstado.Location = new System.Drawing.Point(0, 383);
            this.stsBarraEstado.Name = "stsBarraEstado";
            this.stsBarraEstado.Size = new System.Drawing.Size(910, 22);
            this.stsBarraEstado.TabIndex = 269;
            // 
            // progressBar1
            // 
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(700, 16);
            // 
            // bgwHilo
            // 
            this.bgwHilo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwHilo_DoWork);
            this.bgwHilo.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwHilo_RunWorkerCompleted);
            // 
            // subMenu
            // 
            this.subMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAnularRegistro,
            this.btnEditarRegistro,
            this.btnEliminarRegistro,
            this.toolStripSeparator1,
            this.btnVerDispositivo,
            this.btnVerHistorial,
            this.toolStripSeparator2,
            this.btnAsociarAreaDeTrabajo,
            this.toolStripSeparator3});
            this.subMenu.Name = "subMenu";
            this.subMenu.Size = new System.Drawing.Size(237, 154);
            // 
            // btnAnularRegistro
            // 
            this.btnAnularRegistro.Image = ((System.Drawing.Image)(resources.GetObject("btnAnularRegistro.Image")));
            this.btnAnularRegistro.Name = "btnAnularRegistro";
            this.btnAnularRegistro.Size = new System.Drawing.Size(236, 22);
            this.btnAnularRegistro.Text = "Anular";
            // 
            // btnEditarRegistro
            // 
            this.btnEditarRegistro.Image = ((System.Drawing.Image)(resources.GetObject("btnEditarRegistro.Image")));
            this.btnEditarRegistro.Name = "btnEditarRegistro";
            this.btnEditarRegistro.Size = new System.Drawing.Size(236, 22);
            this.btnEditarRegistro.Text = "Editar";
            // 
            // btnEliminarRegistro
            // 
            this.btnEliminarRegistro.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminarRegistro.Image")));
            this.btnEliminarRegistro.Name = "btnEliminarRegistro";
            this.btnEliminarRegistro.Size = new System.Drawing.Size(236, 22);
            this.btnEliminarRegistro.Text = "Eliminar";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(233, 6);
            // 
            // btnVerDispositivo
            // 
            this.btnVerDispositivo.Image = ((System.Drawing.Image)(resources.GetObject("btnVerDispositivo.Image")));
            this.btnVerDispositivo.Name = "btnVerDispositivo";
            this.btnVerDispositivo.Size = new System.Drawing.Size(236, 22);
            this.btnVerDispositivo.Text = "Ver dispositivo";
            // 
            // btnVerHistorial
            // 
            this.btnVerHistorial.Image = ((System.Drawing.Image)(resources.GetObject("btnVerHistorial.Image")));
            this.btnVerHistorial.Name = "btnVerHistorial";
            this.btnVerHistorial.Size = new System.Drawing.Size(236, 22);
            this.btnVerHistorial.Text = "Ver historial de mantenimiento";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(233, 6);
            // 
            // btnAsociarAreaDeTrabajo
            // 
            this.btnAsociarAreaDeTrabajo.Image = ((System.Drawing.Image)(resources.GetObject("btnAsociarAreaDeTrabajo.Image")));
            this.btnAsociarAreaDeTrabajo.Name = "btnAsociarAreaDeTrabajo";
            this.btnAsociarAreaDeTrabajo.Size = new System.Drawing.Size(236, 22);
            this.btnAsociarAreaDeTrabajo.Text = "Asociar a área de trabajo";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(233, 6);
            // 
            // OrdenDeTrabajoITEdicionActualizarEstadoADispositivo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(910, 405);
            this.Controls.Add(this.stsBarraEstado);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboEstadoDispositivo);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.gbColaborador);
            this.Controls.Add(this.gbReasignarCaso);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrdenDeTrabajoITEdicionActualizarEstadoADispositivo";
            this.Text = "Actualizar estado del dispositivo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OrdenDeTrabajoITEdicionActualizarEstadoADispositivo_FormClosing);
            this.Load += new System.EventHandler(this.OrdenDeTrabajoITEdicionActualizarEstadoADispositivo_Load);
            this.gbReasignarCaso.ResumeLayout(false);
            this.gbReasignarCaso.PerformLayout();
            this.gbColaborador.ResumeLayout(false);
            this.gbColaborador.PerformLayout();
            this.stsBarraEstado.ResumeLayout(false);
            this.stsBarraEstado.PerformLayout();
            this.subMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbReasignarCaso;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblDocumento;
        private System.Windows.Forms.ComboBox cboDocumento;
        private System.Windows.Forms.TextBox txtCorrelativo;
        private System.Windows.Forms.ComboBox cboSerie;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEstado;
        private System.Windows.Forms.Label lblEstado;
        private MyDataGridViewColumns.MyDataGridViewMaskedTextEditingControl txtFecha;
        private System.Windows.Forms.GroupBox gbColaborador;
        private MyControlsDataBinding.Controles.MyTextBox txtComentario03;
        private MyControlsDataBinding.Controles.MyTextBox txtComentario02;
        private MyControlsDataBinding.Controles.MyTextBox txtComentario01;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.ComboBox cboEstadoDispositivo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDispositivo;
        private System.Windows.Forms.StatusStrip stsBarraEstado;
        private System.Windows.Forms.ToolStripProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker bgwHilo;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ContextMenuStrip subMenu;
        private System.Windows.Forms.ToolStripMenuItem btnAnularRegistro;
        private System.Windows.Forms.ToolStripMenuItem btnEditarRegistro;
        private System.Windows.Forms.ToolStripMenuItem btnEliminarRegistro;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem btnVerDispositivo;
        private System.Windows.Forms.ToolStripMenuItem btnVerHistorial;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem btnAsociarAreaDeTrabajo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
    }
}