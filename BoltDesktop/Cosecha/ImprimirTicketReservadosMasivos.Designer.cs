namespace ComparativoHorasVisualSATNISIRA.Produccion
{
    partial class ImprimirTicketReservadosMasivos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImprimirTicketReservadosMasivos));
            this.crptFormato01 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crptFormato01
            // 
            this.crptFormato01.ActiveViewIndex = -1;
            this.crptFormato01.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crptFormato01.Cursor = System.Windows.Forms.Cursors.Default;
            this.crptFormato01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crptFormato01.Location = new System.Drawing.Point(0, 0);
            this.crptFormato01.Name = "crptFormato01";
            this.crptFormato01.Size = new System.Drawing.Size(615, 420);
            this.crptFormato01.TabIndex = 2;
            // 
            // ImprimirTicketReservadosMasivos
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(615, 420);
            this.Controls.Add(this.crptFormato01);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImprimirTicketReservadosMasivos";
            this.Text = "Vista previa | Tickets reservado pendientes de impresión.";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ImprimirTicketReservadosMasivos_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crptFormato01;
    }
}