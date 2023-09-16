namespace ComparativoHorasVisualSATNISIRA.Produccion
{
    partial class CantidadDeTickets
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CantidadDeTickets));
            this.txtCantidad = new Telerik.WinControls.UI.RadSpinEditor();
            this.txtTipoTicket = new System.Windows.Forms.TextBox();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.lblDescripcion = new Telerik.WinControls.UI.RadLabel();
            this.btnGenerarTicketConvencionales = new Telerik.WinControls.UI.RadButton();
            this.stsBarraEstado = new System.Windows.Forms.StatusStrip();
            this.progressBarMain = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripProgressBar2 = new System.Windows.Forms.ToolStripProgressBar();
            this.bgwHilo = new System.ComponentModel.BackgroundWorker();
            this.btnOk = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDescripcion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGenerarTicketConvencionales)).BeginInit();
            this.stsBarraEstado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(83, 37);
            this.txtCantidad.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.txtCantidad.Name = "txtCantidad";
            // 
            // 
            // 
            this.txtCantidad.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.txtCantidad.Size = new System.Drawing.Size(50, 20);
            this.txtCantidad.TabIndex = 265;
            this.txtCantidad.TabStop = false;
            this.txtCantidad.TextAlignment = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCantidad.ThemeName = "Windows8";
            this.txtCantidad.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // txtTipoTicket
            // 
            this.txtTipoTicket.Location = new System.Drawing.Point(83, 12);
            this.txtTipoTicket.MaxLength = 255;
            this.txtTipoTicket.Name = "txtTipoTicket";
            this.txtTipoTicket.Size = new System.Drawing.Size(279, 20);
            this.txtTipoTicket.TabIndex = 264;
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(36, 14);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(34, 18);
            this.radLabel2.TabIndex = 263;
            this.radLabel2.Text = "Tipo :";
            this.radLabel2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.Location = new System.Drawing.Point(7, 39);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(57, 18);
            this.lblDescripcion.TabIndex = 262;
            this.lblDescripcion.Text = "Cantidad :";
            // 
            // btnGenerarTicketConvencionales
            // 
            this.btnGenerarTicketConvencionales.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerarTicketConvencionales.Image")));
            this.btnGenerarTicketConvencionales.Location = new System.Drawing.Point(12, 84);
            this.btnGenerarTicketConvencionales.Name = "btnGenerarTicketConvencionales";
            this.btnGenerarTicketConvencionales.Size = new System.Drawing.Size(182, 24);
            this.btnGenerarTicketConvencionales.TabIndex = 266;
            this.btnGenerarTicketConvencionales.Text = "Generar Tickets";
            this.btnGenerarTicketConvencionales.ThemeName = "Windows8";
            this.btnGenerarTicketConvencionales.Click += new System.EventHandler(this.btnGenerarTicketConvencionales_Click);
            // 
            // stsBarraEstado
            // 
            this.stsBarraEstado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBarMain,
            this.toolStripProgressBar1,
            this.toolStripProgressBar2});
            this.stsBarraEstado.Location = new System.Drawing.Point(0, 123);
            this.stsBarraEstado.Name = "stsBarraEstado";
            this.stsBarraEstado.Size = new System.Drawing.Size(393, 22);
            this.stsBarraEstado.TabIndex = 267;
            // 
            // progressBarMain
            // 
            this.progressBarMain.Name = "progressBarMain";
            this.progressBarMain.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(600, 16);
            // 
            // toolStripProgressBar2
            // 
            this.toolStripProgressBar2.Name = "toolStripProgressBar2";
            this.toolStripProgressBar2.Size = new System.Drawing.Size(600, 16);
            // 
            // bgwHilo
            // 
            this.bgwHilo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwHilo_DoWork);
            this.bgwHilo.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwHilo_RunWorkerCompleted);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.Image")));
            this.btnOk.Location = new System.Drawing.Point(271, 84);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(91, 24);
            this.btnOk.TabIndex = 268;
            this.btnOk.Text = "Ok.";
            this.btnOk.ThemeName = "Windows8";
            // 
            // CantidadDeTickets
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(393, 145);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.stsBarraEstado);
            this.Controls.Add(this.btnGenerarTicketConvencionales);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.txtTipoTicket);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.lblDescripcion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CantidadDeTickets";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ingresar Cantidad de Tickets a Generar";
            this.Load += new System.EventHandler(this.CantidadDeTickets_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDescripcion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGenerarTicketConvencionales)).EndInit();
            this.stsBarraEstado.ResumeLayout(false);
            this.stsBarraEstado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadSpinEditor txtCantidad;
        private System.Windows.Forms.TextBox txtTipoTicket;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel lblDescripcion;
        private Telerik.WinControls.UI.RadButton btnGenerarTicketConvencionales;
        private System.Windows.Forms.StatusStrip stsBarraEstado;
        private System.Windows.Forms.ToolStripProgressBar progressBarMain;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar2;
        private System.ComponentModel.BackgroundWorker bgwHilo;
        private Telerik.WinControls.UI.RadButton btnOk;
    }
}