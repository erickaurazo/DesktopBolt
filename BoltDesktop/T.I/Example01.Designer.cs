namespace ComparativoHorasVisualSATNISIRA.T.I
{
    partial class Example01
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
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.PanelResultado = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(77, 377);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            // 
            // btnGenerar
            // 
            this.btnGenerar.Location = new System.Drawing.Point(77, 348);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(75, 23);
            this.btnGenerar.TabIndex = 1;
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // txtValor
            // 
            this.txtValor.Location = new System.Drawing.Point(77, 322);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(275, 20);
            this.txtValor.TabIndex = 2;
            // 
            // PanelResultado
            // 
            this.PanelResultado.Location = new System.Drawing.Point(77, 61);
            this.PanelResultado.Name = "PanelResultado";
            this.PanelResultado.Size = new System.Drawing.Size(94, 63);
            this.PanelResultado.TabIndex = 3;
            // 
            // Example01
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 487);
            this.Controls.Add(this.PanelResultado);
            this.Controls.Add(this.txtValor);
            this.Controls.Add(this.btnGenerar);
            this.Controls.Add(this.btnGuardar);
            this.Name = "Example01";
            this.Text = "Example01";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.Panel PanelResultado;
    }
}