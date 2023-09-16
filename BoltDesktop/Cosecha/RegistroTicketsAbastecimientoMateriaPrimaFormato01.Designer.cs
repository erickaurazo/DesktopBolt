namespace ComparativoHorasVisualSATNISIRA
{
    partial class ImpresionTicketsAbastecimientoMateriaPrimaImprimir
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImpresionTicketsAbastecimientoMateriaPrimaImprimir));
            this.crptFormato01 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
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
            this.crptFormato01.Size = new System.Drawing.Size(678, 414);
            this.crptFormato01.TabIndex = 0;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // ImpresionTicketsAbastecimientoMateriaPrimaImprimir
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(678, 414);
            this.Controls.Add(this.crptFormato01);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImpresionTicketsAbastecimientoMateriaPrimaImprimir";
            this.Text = "Impresión de tickets para abastecimiento | Formato 01";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ImpresionTicketsAbastecimientoMateriaPrimaImprimir_Load);
            this.ResumeLayout(false);

        }

        #endregion
        //private CrystalDecisions.Windows.Forms.CrystalReportViewer crvImprimir;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crptFormato01;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
    }
}