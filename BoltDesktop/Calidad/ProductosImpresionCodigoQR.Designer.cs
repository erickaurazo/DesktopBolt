namespace ComparativoHorasVisualSATNISIRA.Almacen
{
    partial class ProductosImpresionCodigoQR
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductosImpresionCodigoQR));
            this.crImpresionQR = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crImpresionQR
            // 
            this.crImpresionQR.ActiveViewIndex = -1;
            this.crImpresionQR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crImpresionQR.Cursor = System.Windows.Forms.Cursors.Default;
            this.crImpresionQR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crImpresionQR.Location = new System.Drawing.Point(0, 0);
            this.crImpresionQR.Name = "crImpresionQR";
            this.crImpresionQR.Size = new System.Drawing.Size(597, 322);
            this.crImpresionQR.TabIndex = 2;
            this.crImpresionQR.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // ProductosImpresionCodigoQR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 322);
            this.Controls.Add(this.crImpresionQR);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProductosImpresionCodigoQR";
            this.Text = "Vista Previa del producto";
            this.Load += new System.EventHandler(this.ProductosImpresionCodigoBarras_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crImpresionQR;
    }
}