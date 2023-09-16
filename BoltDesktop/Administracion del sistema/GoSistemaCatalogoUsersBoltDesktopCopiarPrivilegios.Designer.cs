namespace ComparativoHorasVisualSATNISIRA.Administracion_del_sistema
{
    partial class GoSistemaCatalogoUsersBoltDesktopCopiarPrivilegios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoSistemaCatalogoUsersBoltDesktopCopiarPrivilegios));
            this.btnAccessFromOtherUser = new System.Windows.Forms.Button();
            this.btnUserFind = new MyControlsDataBinding.Controles.MyButtonSearchSimple(this.components);
            this.txtUserIdBase = new MyControlsDataBinding.Controles.MyTextBoxSearchSimple(this.components);
            this.txtUserNameBase = new MyControlsDataBinding.Controles.MyTextBoxSearchSimple(this.components);
            this.lblPersonalCode = new System.Windows.Forms.Label();
            this.txtUserId = new MyControlsDataBinding.Controles.MyTextBoxSearchSimple(this.components);
            this.txtUserName = new MyControlsDataBinding.Controles.MyTextBoxSearchSimple(this.components);
            this.stsBarraEstado = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl1UsuarioCodigo = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCodigoDeUsuario = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblEspacioEnBlanco = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl1NombreDelUsuario = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblNombreDelUsuario = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSeparador = new System.Windows.Forms.ToolStripStatusLabel();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.lblNumeroResultados = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.bgwHilo = new System.ComponentModel.BackgroundWorker();
            this.chkNotificar = new System.Windows.Forms.CheckBox();
            this.stsBarraEstado.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAccessFromOtherUser
            // 
            this.btnAccessFromOtherUser.Image = ((System.Drawing.Image)(resources.GetObject("btnAccessFromOtherUser.Image")));
            this.btnAccessFromOtherUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAccessFromOtherUser.Location = new System.Drawing.Point(499, 61);
            this.btnAccessFromOtherUser.Name = "btnAccessFromOtherUser";
            this.btnAccessFromOtherUser.Size = new System.Drawing.Size(134, 31);
            this.btnAccessFromOtherUser.TabIndex = 9;
            this.btnAccessFromOtherUser.Text = "Copiar privilegios";
            this.btnAccessFromOtherUser.UseVisualStyleBackColor = true;
            this.btnAccessFromOtherUser.Click += new System.EventHandler(this.btnAccessFromOtherUser_Click);
            // 
            // btnUserFind
            // 
            this.btnUserFind.Image = ((System.Drawing.Image)(resources.GetObject("btnUserFind.Image")));
            this.btnUserFind.Location = new System.Drawing.Point(80, 35);
            this.btnUserFind.Name = "btnUserFind";
            this.btnUserFind.P_CampoCodigo = "RTRIM(idusuario)";
            this.btnUserFind.P_CampoDescripcion = "rtrim(nombrecompleto)";
            this.btnUserFind.P_EsEditable = true;
            this.btnUserFind.P_EsModificable = true;
            this.btnUserFind.P_FilterByTextBox = null;
            this.btnUserFind.P_TablaConsulta = "asj_usuarios";
            this.btnUserFind.P_TextBoxCodigo = this.txtUserIdBase;
            this.btnUserFind.P_TextBoxDescripcion = this.txtUserNameBase;
            this.btnUserFind.P_TituloFormulario = ".. Busqueda";
            this.btnUserFind.Size = new System.Drawing.Size(24, 23);
            this.btnUserFind.TabIndex = 5;
            this.btnUserFind.UseVisualStyleBackColor = true;
            // 
            // txtUserIdBase
            // 
            this.txtUserIdBase.BackColor = System.Drawing.Color.White;
            this.txtUserIdBase.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUserIdBase.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtUserIdBase.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtUserIdBase.Location = new System.Drawing.Point(110, 35);
            this.txtUserIdBase.MaxLength = 20;
            this.txtUserIdBase.Name = "txtUserIdBase";
            this.txtUserIdBase.P_BotonEnlace = this.btnUserFind;
            this.txtUserIdBase.P_BuscarSoloCodigoExacto = false;
            this.txtUserIdBase.P_EsEditable = false;
            this.txtUserIdBase.P_EsModificable = false;
            this.txtUserIdBase.P_EsPrimaryKey = false;
            this.txtUserIdBase.P_ExigeInformacion = false;
            this.txtUserIdBase.P_NombreColumna = null;
            this.txtUserIdBase.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtUserIdBase.Size = new System.Drawing.Size(66, 20);
            this.txtUserIdBase.TabIndex = 31;
            this.txtUserIdBase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtUserNameBase
            // 
            this.txtUserNameBase.BackColor = System.Drawing.Color.White;
            this.txtUserNameBase.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUserNameBase.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtUserNameBase.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtUserNameBase.Location = new System.Drawing.Point(182, 35);
            this.txtUserNameBase.Name = "txtUserNameBase";
            this.txtUserNameBase.P_BotonEnlace = null;
            this.txtUserNameBase.P_BuscarSoloCodigoExacto = false;
            this.txtUserNameBase.P_EsEditable = false;
            this.txtUserNameBase.P_EsModificable = false;
            this.txtUserNameBase.P_EsPrimaryKey = false;
            this.txtUserNameBase.P_ExigeInformacion = false;
            this.txtUserNameBase.P_NombreColumna = null;
            this.txtUserNameBase.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtUserNameBase.ReadOnly = true;
            this.txtUserNameBase.Size = new System.Drawing.Size(451, 20);
            this.txtUserNameBase.TabIndex = 7;
            // 
            // lblPersonalCode
            // 
            this.lblPersonalCode.AutoSize = true;
            this.lblPersonalCode.Location = new System.Drawing.Point(14, 41);
            this.lblPersonalCode.Name = "lblPersonalCode";
            this.lblPersonalCode.Size = new System.Drawing.Size(59, 13);
            this.lblPersonalCode.TabIndex = 4;
            this.lblPersonalCode.Text = "Cód. Base:";
            // 
            // txtUserId
            // 
            this.txtUserId.BackColor = System.Drawing.Color.White;
            this.txtUserId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUserId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtUserId.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtUserId.Location = new System.Drawing.Point(110, 12);
            this.txtUserId.MaxLength = 20;
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.P_BotonEnlace = null;
            this.txtUserId.P_BuscarSoloCodigoExacto = false;
            this.txtUserId.P_EsEditable = false;
            this.txtUserId.P_EsModificable = false;
            this.txtUserId.P_EsPrimaryKey = false;
            this.txtUserId.P_ExigeInformacion = false;
            this.txtUserId.P_NombreColumna = null;
            this.txtUserId.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtUserId.ReadOnly = true;
            this.txtUserId.Size = new System.Drawing.Size(66, 20);
            this.txtUserId.TabIndex = 2;
            this.txtUserId.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtUserName
            // 
            this.txtUserName.BackColor = System.Drawing.Color.White;
            this.txtUserName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtUserName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtUserName.Location = new System.Drawing.Point(182, 12);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.P_BotonEnlace = null;
            this.txtUserName.P_BuscarSoloCodigoExacto = false;
            this.txtUserName.P_EsEditable = false;
            this.txtUserName.P_EsModificable = false;
            this.txtUserName.P_EsPrimaryKey = false;
            this.txtUserName.P_ExigeInformacion = false;
            this.txtUserName.P_NombreColumna = null;
            this.txtUserName.P_TipoDato = MyControlsDataBinding.Extensions.EnumTipoDato.Texto;
            this.txtUserName.ReadOnly = true;
            this.txtUserName.Size = new System.Drawing.Size(451, 20);
            this.txtUserName.TabIndex = 3;
            // 
            // stsBarraEstado
            // 
            this.stsBarraEstado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lbl1UsuarioCodigo,
            this.lblCodigoDeUsuario,
            this.lblEspacioEnBlanco,
            this.lbl1NombreDelUsuario,
            this.lblNombreDelUsuario,
            this.lblSeparador,
            this.ProgressBar,
            this.lblNumeroResultados});
            this.stsBarraEstado.Location = new System.Drawing.Point(0, 100);
            this.stsBarraEstado.Name = "stsBarraEstado";
            this.stsBarraEstado.Size = new System.Drawing.Size(646, 22);
            this.stsBarraEstado.TabIndex = 193;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // lbl1UsuarioCodigo
            // 
            this.lbl1UsuarioCodigo.Name = "lbl1UsuarioCodigo";
            this.lbl1UsuarioCodigo.Size = new System.Drawing.Size(50, 17);
            this.lbl1UsuarioCodigo.Text = "Usuario:";
            // 
            // lblCodigoDeUsuario
            // 
            this.lblCodigoDeUsuario.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblCodigoDeUsuario.Name = "lblCodigoDeUsuario";
            this.lblCodigoDeUsuario.Size = new System.Drawing.Size(0, 17);
            // 
            // lblEspacioEnBlanco
            // 
            this.lblEspacioEnBlanco.Name = "lblEspacioEnBlanco";
            this.lblEspacioEnBlanco.Size = new System.Drawing.Size(28, 17);
            this.lblEspacioEnBlanco.Text = "       ";
            // 
            // lbl1NombreDelUsuario
            // 
            this.lbl1NombreDelUsuario.Name = "lbl1NombreDelUsuario";
            this.lbl1NombreDelUsuario.Size = new System.Drawing.Size(62, 17);
            this.lbl1NombreDelUsuario.Text = "Nombres :";
            // 
            // lblNombreDelUsuario
            // 
            this.lblNombreDelUsuario.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblNombreDelUsuario.Name = "lblNombreDelUsuario";
            this.lblNombreDelUsuario.Size = new System.Drawing.Size(0, 17);
            // 
            // lblSeparador
            // 
            this.lblSeparador.Name = "lblSeparador";
            this.lblSeparador.Size = new System.Drawing.Size(28, 17);
            this.lblSeparador.Text = "       ";
            // 
            // ProgressBar
            // 
            this.ProgressBar.MarqueeAnimationSpeed = 25;
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(160, 16);
            this.ProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.ProgressBar.Visible = false;
            // 
            // lblNumeroResultados
            // 
            this.lblNumeroResultados.Name = "lblNumeroResultados";
            this.lblNumeroResultados.Size = new System.Drawing.Size(0, 17);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Código Personal :";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(408, 61);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(85, 31);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // bgwHilo
            // 
            this.bgwHilo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwHilo_DoWork);
            this.bgwHilo.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwHilo_RunWorkerCompleted);
            // 
            // chkNotificar
            // 
            this.chkNotificar.AutoSize = true;
            this.chkNotificar.Location = new System.Drawing.Point(19, 69);
            this.chkNotificar.Name = "chkNotificar";
            this.chkNotificar.Size = new System.Drawing.Size(157, 17);
            this.chkNotificar.TabIndex = 194;
            this.chkNotificar.Text = "Notificar cambios realizados";
            this.chkNotificar.UseVisualStyleBackColor = true;
            // 
            // GoSistemaCatalogoUsersCopiarPrivilegios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 122);
            this.Controls.Add(this.chkNotificar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stsBarraEstado);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.btnUserFind);
            this.Controls.Add(this.txtUserIdBase);
            this.Controls.Add(this.txtUserNameBase);
            this.Controls.Add(this.lblPersonalCode);
            this.Controls.Add(this.btnAccessFromOtherUser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GoSistemaCatalogoUsersCopiarPrivilegios";
            this.Text = "Usuarios del sistema | Copiar privilegios";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GoSistemaCatalogoUsersCopiarPrivilegios_FormClosing);
            this.Load += new System.EventHandler(this.GoSistemaCatalogoUsersCopiarPrivilegios_Load);
            this.stsBarraEstado.ResumeLayout(false);
            this.stsBarraEstado.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAccessFromOtherUser;
        private MyControlsDataBinding.Controles.MyButtonSearchSimple btnUserFind;
        private MyControlsDataBinding.Controles.MyTextBoxSearchSimple txtUserIdBase;
        private MyControlsDataBinding.Controles.MyTextBoxSearchSimple txtUserNameBase;
        private System.Windows.Forms.Label lblPersonalCode;
        private MyControlsDataBinding.Controles.MyTextBoxSearchSimple txtUserId;
        private MyControlsDataBinding.Controles.MyTextBoxSearchSimple txtUserName;
        private System.Windows.Forms.StatusStrip stsBarraEstado;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lbl1UsuarioCodigo;
        private System.Windows.Forms.ToolStripStatusLabel lblCodigoDeUsuario;
        private System.Windows.Forms.ToolStripStatusLabel lblEspacioEnBlanco;
        private System.Windows.Forms.ToolStripStatusLabel lbl1NombreDelUsuario;
        private System.Windows.Forms.ToolStripStatusLabel lblNombreDelUsuario;
        private System.Windows.Forms.ToolStripStatusLabel lblSeparador;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel lblNumeroResultados;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancelar;
        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private System.ComponentModel.BackgroundWorker bgwHilo;
        private System.Windows.Forms.CheckBox chkNotificar;
    }
}