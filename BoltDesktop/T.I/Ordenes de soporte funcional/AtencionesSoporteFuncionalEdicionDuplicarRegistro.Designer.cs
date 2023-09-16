namespace ComparativoHorasVisualSATNISIRA.T.I
{
    partial class AtencionesSoporteFuncionalEdicionDuplicarRegistro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AtencionesSoporteFuncionalEdicionDuplicarRegistro));
            this.gbColaborador = new System.Windows.Forms.GroupBox();
            this.txtPersonal = new MyControlsDataBinding.Controles.MyTextBoxSearchSimple(this.components);
            this.txtPersonalCodigo = new MyControlsDataBinding.Controles.MyTextBoxSearchSimple(this.components);
            this.btnPersonalBuscar = new MyControlsDataBinding.Controles.MyButtonSearchSimple(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnRegistrar = new System.Windows.Forms.Button();
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
            this.bgwHilo = new System.ComponentModel.BackgroundWorker();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.stsBarraEstado = new System.Windows.Forms.StatusStrip();
            this.progressBar1 = new System.Windows.Forms.ToolStripProgressBar();
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
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.gbColaborador.SuspendLayout();
            this.gbReasignarCaso.SuspendLayout();
            this.stsBarraEstado.SuspendLayout();
            this.subMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbColaborador
            // 
            this.gbColaborador.Controls.Add(this.txtPersonal);
            this.gbColaborador.Controls.Add(this.txtPersonalCodigo);
            this.gbColaborador.Controls.Add(this.btnPersonalBuscar);
            this.gbColaborador.Controls.Add(this.label3);
            this.gbColaborador.Controls.Add(this.btnCancelar);
            this.gbColaborador.Controls.Add(this.btnRegistrar);
            this.gbColaborador.Location = new System.Drawing.Point(12, 83);
            this.gbColaborador.Name = "gbColaborador";
            this.gbColaborador.Size = new System.Drawing.Size(428, 74);
            this.gbColaborador.TabIndex = 3;
            this.gbColaborador.TabStop = false;
            // 
            // txtPersonal
            // 
            this.txtPersonal.BackColor = System.Drawing.Color.White;
            this.txtPersonal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPersonal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtPersonal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtPersonal.Location = new System.Drawing.Point(153, 19);
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
            this.txtPersonal.Size = new System.Drawing.Size(269, 20);
            this.txtPersonal.TabIndex = 266;
            this.txtPersonal.Text = "AURAZO CARHUATANTA, ERICK";
            // 
            // txtPersonalCodigo
            // 
            this.txtPersonalCodigo.BackColor = System.Drawing.Color.White;
            this.txtPersonalCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPersonalCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtPersonalCodigo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtPersonalCodigo.Location = new System.Drawing.Point(89, 19);
            this.txtPersonalCodigo.MaxLength = 7;
            this.txtPersonalCodigo.Name = "txtPersonalCodigo";
            this.txtPersonalCodigo.P_BotonEnlace = this.btnPersonalBuscar;
            this.txtPersonalCodigo.P_BuscarSoloCodigoExacto = false;
            this.txtPersonalCodigo.P_EsEditable = true;
            this.txtPersonalCodigo.P_EsModificable = true;
            this.txtPersonalCodigo.P_EsPrimaryKey = false;
            this.txtPersonalCodigo.P_ExigeInformacion = false;
            this.txtPersonalCodigo.P_NombreColumna = null;
            this.txtPersonalCodigo.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtPersonalCodigo.Size = new System.Drawing.Size(58, 20);
            this.txtPersonalCodigo.TabIndex = 265;
            this.txtPersonalCodigo.Text = "100369";
            this.txtPersonalCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnPersonalBuscar
            // 
            this.btnPersonalBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnPersonalBuscar.Image")));
            this.btnPersonalBuscar.Location = new System.Drawing.Point(59, 18);
            this.btnPersonalBuscar.Name = "btnPersonalBuscar";
            this.btnPersonalBuscar.P_CampoCodigo = "rtrim(codigo)";
            this.btnPersonalBuscar.P_CampoDescripcion = "descripcion";
            this.btnPersonalBuscar.P_EsEditable = true;
            this.btnPersonalBuscar.P_EsModificable = true;
            this.btnPersonalBuscar.P_FilterByTextBox = null;
            this.btnPersonalBuscar.P_TablaConsulta = "SAS_ListadoPersonalConAsignacionDeEquipamientotecnologico order by descripcion";
            this.btnPersonalBuscar.P_TextBoxCodigo = this.txtPersonalCodigo;
            this.btnPersonalBuscar.P_TextBoxDescripcion = this.txtPersonal;
            this.btnPersonalBuscar.P_TituloFormulario = "... Buscar personal interno y extetno";
            this.btnPersonalBuscar.Size = new System.Drawing.Size(24, 23);
            this.btnPersonalBuscar.TabIndex = 267;
            this.btnPersonalBuscar.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 264;
            this.label3.Text = "Personal :";
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(87, 45);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(109, 23);
            this.btnCancelar.TabIndex = 263;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnRegistrar.Location = new System.Drawing.Point(279, 45);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(109, 23);
            this.btnRegistrar.TabIndex = 262;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // gbReasignarCaso
            // 
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
            this.gbReasignarCaso.Size = new System.Drawing.Size(428, 70);
            this.gbReasignarCaso.TabIndex = 2;
            this.gbReasignarCaso.TabStop = false;
            // 
            // txtCodigo
            // 
            this.txtCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.lblDocumento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDocumento.AutoSize = true;
            this.lblDocumento.Location = new System.Drawing.Point(16, 50);
            this.lblDocumento.Name = "lblDocumento";
            this.lblDocumento.Size = new System.Drawing.Size(68, 13);
            this.lblDocumento.TabIndex = 261;
            this.lblDocumento.Text = "Documento :";
            // 
            // cboDocumento
            // 
            this.cboDocumento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDocumento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDocumento.FormattingEnabled = true;
            this.cboDocumento.Location = new System.Drawing.Point(87, 45);
            this.cboDocumento.Name = "cboDocumento";
            this.cboDocumento.Size = new System.Drawing.Size(50, 21);
            this.cboDocumento.TabIndex = 260;
            // 
            // txtCorrelativo
            // 
            this.txtCorrelativo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCorrelativo.BackColor = System.Drawing.Color.White;
            this.txtCorrelativo.Location = new System.Drawing.Point(205, 46);
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
            this.cboSerie.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSerie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSerie.FormattingEnabled = true;
            this.cboSerie.Location = new System.Drawing.Point(143, 45);
            this.cboSerie.Name = "cboSerie";
            this.cboSerie.Size = new System.Drawing.Size(56, 21);
            this.cboSerie.TabIndex = 258;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(298, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 257;
            this.label1.Text = "Fecha :";
            // 
            // txtEstado
            // 
            this.txtEstado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.lblEstado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(37, 22);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(46, 13);
            this.lblEstado.TabIndex = 255;
            this.lblEstado.Text = "Estado :";
            // 
            // txtFecha
            // 
            this.txtFecha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFecha.EditingControlDataGridView = null;
            this.txtFecha.EditingControlFormattedValue = "30/04/2020";
            this.txtFecha.EditingControlRowIndex = 0;
            this.txtFecha.EditingControlValueChanged = true;
            this.txtFecha.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txtFecha.Location = new System.Drawing.Point(347, 44);
            this.txtFecha.Mask = "00/00/0000";
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.P_EsEditable = false;
            this.txtFecha.P_EsModificable = false;
            this.txtFecha.P_ExigeInformacion = false;
            this.txtFecha.P_Hora = null;
            this.txtFecha.P_NombreColumna = null;
            this.txtFecha.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtFecha.Size = new System.Drawing.Size(72, 20);
            this.txtFecha.TabIndex = 254;
            this.txtFecha.Text = "30042020";
            this.txtFecha.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFecha.ValidatingType = typeof(System.DateTime);
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
            this.stsBarraEstado.Location = new System.Drawing.Point(0, 159);
            this.stsBarraEstado.Name = "stsBarraEstado";
            this.stsBarraEstado.Size = new System.Drawing.Size(446, 22);
            this.stsBarraEstado.TabIndex = 198;
            // 
            // progressBar1
            // 
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(700, 16);
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
            // AtencionesSoporteFuncionalEdicionDuplicarRegistro
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(446, 181);
            this.Controls.Add(this.stsBarraEstado);
            this.Controls.Add(this.gbColaborador);
            this.Controls.Add(this.gbReasignarCaso);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AtencionesSoporteFuncionalEdicionDuplicarRegistro";
            this.Text = "Duplicar registro";
            this.Load += new System.EventHandler(this.AtencionesSoporteFuncionalEdicionDuplicarRegistro_Load);
            this.gbColaborador.ResumeLayout(false);
            this.gbColaborador.PerformLayout();
            this.gbReasignarCaso.ResumeLayout(false);
            this.gbReasignarCaso.PerformLayout();
            this.stsBarraEstado.ResumeLayout(false);
            this.stsBarraEstado.PerformLayout();
            this.subMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbColaborador;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnRegistrar;
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
        private MyControlsDataBinding.Controles.MyTextBoxSearchSimple txtPersonal;
        private MyControlsDataBinding.Controles.MyTextBoxSearchSimple txtPersonalCodigo;
        private MyControlsDataBinding.Controles.MyButtonSearchSimple btnPersonalBuscar;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker bgwHilo;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private System.Windows.Forms.StatusStrip stsBarraEstado;
        private System.Windows.Forms.ToolStripProgressBar progressBar1;
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
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}