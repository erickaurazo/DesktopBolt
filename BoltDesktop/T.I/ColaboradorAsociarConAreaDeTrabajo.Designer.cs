namespace ComparativoHorasVisualSATNISIRA.T.I
{
    partial class ColaboradorAsociarConAreaDeTrabajo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColaboradorAsociarConAreaDeTrabajo));
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.txtIdCodigoGeneral = new MyControlsDataBinding.Controles.MyTextBoxSearchSimple(this.components);
            this.btnPersonalBuscar = new MyControlsDataBinding.Controles.MyButtonSearchSimple(this.components);
            this.txtNombres = new MyControlsDataBinding.Controles.MyTextBoxSearchSimple(this.components);
            this.gbPersonalArea = new System.Windows.Forms.GroupBox();
            this.btnGrabar = new Telerik.WinControls.UI.RadButton();
            this.btnCancelar = new Telerik.WinControls.UI.RadButton();
            this.txtArea = new MyControlsDataBinding.Controles.MyTextBoxSearchSimple(this.components);
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.myButtonSearchSimple2 = new MyControlsDataBinding.Controles.MyButtonSearchSimple(this.components);
            this.txtAreaCodigo = new MyControlsDataBinding.Controles.MyTextBoxSearchSimple(this.components);
            this.txtGerencia = new MyControlsDataBinding.Controles.MyTextBoxSearchSimple(this.components);
            this.lblSubGerencia = new Telerik.WinControls.UI.RadLabel();
            this.myButtonSearchSimple1 = new MyControlsDataBinding.Controles.MyButtonSearchSimple(this.components);
            this.txtGerenciaCodigo = new MyControlsDataBinding.Controles.MyTextBoxSearchSimple(this.components);
            this.chkEsJefe = new System.Windows.Forms.CheckBox();
            this.chkEsGerente = new System.Windows.Forms.CheckBox();
            this.bgwHilo = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            this.gbPersonalArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnGrabar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancelar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSubGerencia)).BeginInit();
            this.SuspendLayout();
            // 
            // radLabel4
            // 
            this.radLabel4.Location = new System.Drawing.Point(23, 20);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(54, 18);
            this.radLabel4.TabIndex = 235;
            this.radLabel4.Text = "Personal :";
            // 
            // txtIdCodigoGeneral
            // 
            this.txtIdCodigoGeneral.BackColor = System.Drawing.Color.White;
            this.txtIdCodigoGeneral.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIdCodigoGeneral.Enabled = false;
            this.txtIdCodigoGeneral.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtIdCodigoGeneral.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtIdCodigoGeneral.Location = new System.Drawing.Point(144, 19);
            this.txtIdCodigoGeneral.MaxLength = 16;
            this.txtIdCodigoGeneral.Name = "txtIdCodigoGeneral";
            this.txtIdCodigoGeneral.P_BotonEnlace = this.btnPersonalBuscar;
            this.txtIdCodigoGeneral.P_BuscarSoloCodigoExacto = false;
            this.txtIdCodigoGeneral.P_EsEditable = true;
            this.txtIdCodigoGeneral.P_EsModificable = true;
            this.txtIdCodigoGeneral.P_EsPrimaryKey = false;
            this.txtIdCodigoGeneral.P_ExigeInformacion = false;
            this.txtIdCodigoGeneral.P_NombreColumna = null;
            this.txtIdCodigoGeneral.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtIdCodigoGeneral.Size = new System.Drawing.Size(91, 20);
            this.txtIdCodigoGeneral.TabIndex = 236;
            this.txtIdCodigoGeneral.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnPersonalBuscar
            // 
            this.btnPersonalBuscar.Enabled = false;
            this.btnPersonalBuscar.Image = ((System.Drawing.Image)(resources.GetObject("btnPersonalBuscar.Image")));
            this.btnPersonalBuscar.Location = new System.Drawing.Point(113, 17);
            this.btnPersonalBuscar.Name = "btnPersonalBuscar";
            this.btnPersonalBuscar.P_CampoCodigo = "rtrim(codigo)";
            this.btnPersonalBuscar.P_CampoDescripcion = "rtrim(nombres)";
            this.btnPersonalBuscar.P_EsEditable = true;
            this.btnPersonalBuscar.P_EsModificable = true;
            this.btnPersonalBuscar.P_FilterByTextBox = null;
            this.btnPersonalBuscar.P_TablaConsulta = "SAS_ListadoPersonalEmpresaYExterno order by codigo";
            this.btnPersonalBuscar.P_TextBoxCodigo = this.txtIdCodigoGeneral;
            this.btnPersonalBuscar.P_TextBoxDescripcion = this.txtNombres;
            this.btnPersonalBuscar.P_TituloFormulario = ".. Buscar colores de productos";
            this.btnPersonalBuscar.Size = new System.Drawing.Size(25, 23);
            this.btnPersonalBuscar.TabIndex = 238;
            this.btnPersonalBuscar.UseVisualStyleBackColor = true;
            // 
            // txtNombres
            // 
            this.txtNombres.BackColor = System.Drawing.Color.White;
            this.txtNombres.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombres.Enabled = false;
            this.txtNombres.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtNombres.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtNombres.Location = new System.Drawing.Point(241, 19);
            this.txtNombres.Name = "txtNombres";
            this.txtNombres.P_BotonEnlace = null;
            this.txtNombres.P_BuscarSoloCodigoExacto = false;
            this.txtNombres.P_EsEditable = false;
            this.txtNombres.P_EsModificable = false;
            this.txtNombres.P_EsPrimaryKey = false;
            this.txtNombres.P_ExigeInformacion = false;
            this.txtNombres.P_NombreColumna = null;
            this.txtNombres.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtNombres.ReadOnly = true;
            this.txtNombres.Size = new System.Drawing.Size(337, 20);
            this.txtNombres.TabIndex = 237;
            // 
            // gbPersonalArea
            // 
            this.gbPersonalArea.Controls.Add(this.chkEsGerente);
            this.gbPersonalArea.Controls.Add(this.chkEsJefe);
            this.gbPersonalArea.Controls.Add(this.btnGrabar);
            this.gbPersonalArea.Controls.Add(this.btnCancelar);
            this.gbPersonalArea.Controls.Add(this.txtArea);
            this.gbPersonalArea.Controls.Add(this.radLabel2);
            this.gbPersonalArea.Controls.Add(this.myButtonSearchSimple2);
            this.gbPersonalArea.Controls.Add(this.txtAreaCodigo);
            this.gbPersonalArea.Controls.Add(this.txtGerencia);
            this.gbPersonalArea.Controls.Add(this.lblSubGerencia);
            this.gbPersonalArea.Controls.Add(this.myButtonSearchSimple1);
            this.gbPersonalArea.Controls.Add(this.txtGerenciaCodigo);
            this.gbPersonalArea.Controls.Add(this.txtNombres);
            this.gbPersonalArea.Controls.Add(this.radLabel4);
            this.gbPersonalArea.Controls.Add(this.btnPersonalBuscar);
            this.gbPersonalArea.Controls.Add(this.txtIdCodigoGeneral);
            this.gbPersonalArea.Location = new System.Drawing.Point(12, 12);
            this.gbPersonalArea.Name = "gbPersonalArea";
            this.gbPersonalArea.Size = new System.Drawing.Size(621, 173);
            this.gbPersonalArea.TabIndex = 239;
            this.gbPersonalArea.TabStop = false;
            this.gbPersonalArea.Text = "Asociar colaborador con área de trabajo";
            // 
            // btnGrabar
            // 
            this.btnGrabar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGrabar.Image = ((System.Drawing.Image)(resources.GetObject("btnGrabar.Image")));
            this.btnGrabar.ImageAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGrabar.Location = new System.Drawing.Point(334, 140);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(102, 24);
            this.btnGrabar.TabIndex = 248;
            this.btnGrabar.Text = "Grabar";
            this.btnGrabar.ThemeName = "Windows8";
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.Location = new System.Drawing.Point(226, 140);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(102, 24);
            this.btnCancelar.TabIndex = 247;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.ThemeName = "Windows8";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtArea
            // 
            this.txtArea.BackColor = System.Drawing.Color.White;
            this.txtArea.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtArea.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtArea.Location = new System.Drawing.Point(241, 71);
            this.txtArea.Name = "txtArea";
            this.txtArea.P_BotonEnlace = null;
            this.txtArea.P_BuscarSoloCodigoExacto = false;
            this.txtArea.P_EsEditable = false;
            this.txtArea.P_EsModificable = false;
            this.txtArea.P_EsPrimaryKey = false;
            this.txtArea.P_ExigeInformacion = false;
            this.txtArea.P_NombreColumna = null;
            this.txtArea.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtArea.ReadOnly = true;
            this.txtArea.Size = new System.Drawing.Size(337, 20);
            this.txtArea.TabIndex = 245;
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(23, 72);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(89, 18);
            this.radLabel2.TabIndex = 243;
            this.radLabel2.Text = "Area de trabajo :";
            // 
            // myButtonSearchSimple2
            // 
            this.myButtonSearchSimple2.Image = ((System.Drawing.Image)(resources.GetObject("myButtonSearchSimple2.Image")));
            this.myButtonSearchSimple2.Location = new System.Drawing.Point(112, 69);
            this.myButtonSearchSimple2.Name = "myButtonSearchSimple2";
            this.myButtonSearchSimple2.P_CampoCodigo = "rtrim(idarea)";
            this.myButtonSearchSimple2.P_CampoDescripcion = "rtrim(descripcion)";
            this.myButtonSearchSimple2.P_EsEditable = true;
            this.myButtonSearchSimple2.P_EsModificable = true;
            this.myButtonSearchSimple2.P_FilterByTextBox = null;
            this.myButtonSearchSimple2.P_TablaConsulta = "areas";
            this.myButtonSearchSimple2.P_TextBoxCodigo = this.txtAreaCodigo;
            this.myButtonSearchSimple2.P_TextBoxDescripcion = this.txtArea;
            this.myButtonSearchSimple2.P_TituloFormulario = ".. Buscar areas de trabajo";
            this.myButtonSearchSimple2.Size = new System.Drawing.Size(25, 23);
            this.myButtonSearchSimple2.TabIndex = 246;
            this.myButtonSearchSimple2.UseVisualStyleBackColor = true;
            // 
            // txtAreaCodigo
            // 
            this.txtAreaCodigo.BackColor = System.Drawing.Color.White;
            this.txtAreaCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAreaCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtAreaCodigo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtAreaCodigo.Location = new System.Drawing.Point(144, 71);
            this.txtAreaCodigo.MaxLength = 16;
            this.txtAreaCodigo.Name = "txtAreaCodigo";
            this.txtAreaCodigo.P_BotonEnlace = this.myButtonSearchSimple2;
            this.txtAreaCodigo.P_BuscarSoloCodigoExacto = false;
            this.txtAreaCodigo.P_EsEditable = true;
            this.txtAreaCodigo.P_EsModificable = true;
            this.txtAreaCodigo.P_EsPrimaryKey = false;
            this.txtAreaCodigo.P_ExigeInformacion = false;
            this.txtAreaCodigo.P_NombreColumna = null;
            this.txtAreaCodigo.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtAreaCodigo.Size = new System.Drawing.Size(91, 20);
            this.txtAreaCodigo.TabIndex = 244;
            this.txtAreaCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtGerencia
            // 
            this.txtGerencia.BackColor = System.Drawing.Color.White;
            this.txtGerencia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGerencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtGerencia.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtGerencia.Location = new System.Drawing.Point(241, 45);
            this.txtGerencia.Name = "txtGerencia";
            this.txtGerencia.P_BotonEnlace = null;
            this.txtGerencia.P_BuscarSoloCodigoExacto = false;
            this.txtGerencia.P_EsEditable = false;
            this.txtGerencia.P_EsModificable = false;
            this.txtGerencia.P_EsPrimaryKey = false;
            this.txtGerencia.P_ExigeInformacion = false;
            this.txtGerencia.P_NombreColumna = null;
            this.txtGerencia.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtGerencia.ReadOnly = true;
            this.txtGerencia.Size = new System.Drawing.Size(337, 20);
            this.txtGerencia.TabIndex = 241;
            // 
            // lblSubGerencia
            // 
            this.lblSubGerencia.Location = new System.Drawing.Point(23, 46);
            this.lblSubGerencia.Name = "lblSubGerencia";
            this.lblSubGerencia.Size = new System.Drawing.Size(55, 18);
            this.lblSubGerencia.TabIndex = 239;
            this.lblSubGerencia.Text = "Gerencia :";
            // 
            // myButtonSearchSimple1
            // 
            this.myButtonSearchSimple1.Image = ((System.Drawing.Image)(resources.GetObject("myButtonSearchSimple1.Image")));
            this.myButtonSearchSimple1.Location = new System.Drawing.Point(113, 43);
            this.myButtonSearchSimple1.Name = "myButtonSearchSimple1";
            this.myButtonSearchSimple1.P_CampoCodigo = "rtrim(id)";
            this.myButtonSearchSimple1.P_CampoDescripcion = "rtrim(descripcion)";
            this.myButtonSearchSimple1.P_EsEditable = true;
            this.myButtonSearchSimple1.P_EsModificable = true;
            this.myButtonSearchSimple1.P_FilterByTextBox = null;
            this.myButtonSearchSimple1.P_TablaConsulta = "SAS_ColaboradorGerencia order by descripcion";
            this.myButtonSearchSimple1.P_TextBoxCodigo = this.txtGerenciaCodigo;
            this.myButtonSearchSimple1.P_TextBoxDescripcion = this.txtGerencia;
            this.myButtonSearchSimple1.P_TituloFormulario = ".. Buscar gerencias";
            this.myButtonSearchSimple1.Size = new System.Drawing.Size(25, 23);
            this.myButtonSearchSimple1.TabIndex = 242;
            this.myButtonSearchSimple1.UseVisualStyleBackColor = true;
            // 
            // txtGerenciaCodigo
            // 
            this.txtGerenciaCodigo.BackColor = System.Drawing.Color.White;
            this.txtGerenciaCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGerenciaCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtGerenciaCodigo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtGerenciaCodigo.Location = new System.Drawing.Point(144, 45);
            this.txtGerenciaCodigo.MaxLength = 16;
            this.txtGerenciaCodigo.Name = "txtGerenciaCodigo";
            this.txtGerenciaCodigo.P_BotonEnlace = this.myButtonSearchSimple1;
            this.txtGerenciaCodigo.P_BuscarSoloCodigoExacto = false;
            this.txtGerenciaCodigo.P_EsEditable = true;
            this.txtGerenciaCodigo.P_EsModificable = true;
            this.txtGerenciaCodigo.P_EsPrimaryKey = false;
            this.txtGerenciaCodigo.P_ExigeInformacion = false;
            this.txtGerenciaCodigo.P_NombreColumna = null;
            this.txtGerenciaCodigo.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtGerenciaCodigo.Size = new System.Drawing.Size(91, 20);
            this.txtGerenciaCodigo.TabIndex = 240;
            this.txtGerenciaCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkEsJefe
            // 
            this.chkEsJefe.AutoSize = true;
            this.chkEsJefe.Location = new System.Drawing.Point(144, 106);
            this.chkEsJefe.Name = "chkEsJefe";
            this.chkEsJefe.Size = new System.Drawing.Size(61, 17);
            this.chkEsJefe.TabIndex = 249;
            this.chkEsJefe.Text = "Es Jefe";
            this.chkEsJefe.UseVisualStyleBackColor = true;
            // 
            // chkEsGerente
            // 
            this.chkEsGerente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkEsGerente.AutoSize = true;
            this.chkEsGerente.Location = new System.Drawing.Point(417, 106);
            this.chkEsGerente.Name = "chkEsGerente";
            this.chkEsGerente.Size = new System.Drawing.Size(161, 17);
            this.chkEsGerente.TabIndex = 250;
            this.chkEsGerente.Text = "Es Gerente y/o Sub Gerente";
            this.chkEsGerente.UseVisualStyleBackColor = true;
            // 
            // bgwHilo
            // 
            this.bgwHilo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwHilo_DoWork);
            this.bgwHilo.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwHilo_RunWorkerCompleted);
            // 
            // ColaboradorAsociarConAreaDeTrabajo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(650, 197);
            this.Controls.Add(this.gbPersonalArea);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColaboradorAsociarConAreaDeTrabajo";
            this.Text = "Colabolador | Asociar a trabajar con Gerencia y con Área de trabajo";
            this.Load += new System.EventHandler(this.ColaboradorAsociarConAreaDeTrabajo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            this.gbPersonalArea.ResumeLayout(false);
            this.gbPersonalArea.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnGrabar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancelar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSubGerencia)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel radLabel4;
        private MyControlsDataBinding.Controles.MyTextBoxSearchSimple txtIdCodigoGeneral;
        private MyControlsDataBinding.Controles.MyButtonSearchSimple btnPersonalBuscar;
        private MyControlsDataBinding.Controles.MyTextBoxSearchSimple txtNombres;
        private System.Windows.Forms.GroupBox gbPersonalArea;
        private MyControlsDataBinding.Controles.MyTextBoxSearchSimple txtArea;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private MyControlsDataBinding.Controles.MyButtonSearchSimple myButtonSearchSimple2;
        private MyControlsDataBinding.Controles.MyTextBoxSearchSimple txtAreaCodigo;
        private MyControlsDataBinding.Controles.MyTextBoxSearchSimple txtGerencia;
        private Telerik.WinControls.UI.RadLabel lblSubGerencia;
        private MyControlsDataBinding.Controles.MyButtonSearchSimple myButtonSearchSimple1;
        private MyControlsDataBinding.Controles.MyTextBoxSearchSimple txtGerenciaCodigo;
        private Telerik.WinControls.UI.RadButton btnGrabar;
        private Telerik.WinControls.UI.RadButton btnCancelar;
        private System.Windows.Forms.CheckBox chkEsGerente;
        private System.Windows.Forms.CheckBox chkEsJefe;
        private System.ComponentModel.BackgroundWorker bgwHilo;
    }
}