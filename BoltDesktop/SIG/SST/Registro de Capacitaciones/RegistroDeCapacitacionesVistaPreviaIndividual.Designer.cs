namespace ComparativoHorasVisualSATNISIRA.SIG.SST.Registro_de_Capacitaciones
{
    partial class RegistroDeCapacitacionesVistaPreviaIndividual
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistroDeCapacitacionesVistaPreviaIndividual));
            this.crAgrupado = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crAgrupado
            // 
            this.crAgrupado.ActiveViewIndex = -1;
            this.crAgrupado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crAgrupado.Cursor = System.Windows.Forms.Cursors.Default;
            this.crAgrupado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crAgrupado.Location = new System.Drawing.Point(0, 0);
            this.crAgrupado.Name = "crAgrupado";
            this.crAgrupado.Size = new System.Drawing.Size(943, 539);
            this.crAgrupado.TabIndex = 3;
            this.crAgrupado.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // RegistroDeCapacitacionesVistaPreviaIndividual
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(943, 539);
            this.Controls.Add(this.crAgrupado);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RegistroDeCapacitacionesVistaPreviaIndividual";
            this.Text = "Vista previa";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.RegistroDeCapacitacionesVistaPreviaIndividual_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crAgrupado;
    }
}