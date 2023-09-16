namespace ComparativoHorasVisualSATNISIRA.Produccion
{
    partial class RegistroTicketsAbastecimientoMateriaPrimaFormato03
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistroTicketsAbastecimientoMateriaPrimaFormato03));
            this.crptFormato02 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crptFormato02
            // 
            this.crptFormato02.ActiveViewIndex = -1;
            this.crptFormato02.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crptFormato02.Cursor = System.Windows.Forms.Cursors.Default;
            this.crptFormato02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crptFormato02.Location = new System.Drawing.Point(0, 0);
            this.crptFormato02.Name = "crptFormato02";
            this.crptFormato02.Size = new System.Drawing.Size(670, 425);
            this.crptFormato02.TabIndex = 2;
            // 
            // RegistroTicketsAbastecimientoMateriaPrimaFormato03
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 425);
            this.Controls.Add(this.crptFormato02);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RegistroTicketsAbastecimientoMateriaPrimaFormato03";
            this.Text = "Impresión de tickets para abastecimiento | Formato 03";
            this.Load += new System.EventHandler(this.RegistroTicketsAbastecimientoMateriaPrimaFormato03_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crptFormato02;
    }
}