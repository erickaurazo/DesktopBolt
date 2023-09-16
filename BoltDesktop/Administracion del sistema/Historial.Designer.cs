namespace Asistencia
{
    partial class Historial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Historial));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bgwHilo = new System.ComponentModel.BackgroundWorker();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.gbHistorial = new System.Windows.Forms.GroupBox();
            this.dgvHistorial = new MyControlsDataBinding.Controles.MyDataGridViewDetails(this.components);
            this.chEvento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chMaquina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbHistorial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).BeginInit();
            this.SuspendLayout();
            // 
            // bgwHilo
            // 
            this.bgwHilo.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwHilo_DoWork);
            this.bgwHilo.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwHilo_RunWorkerCompleted);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.LightYellow;
            this.textBox1.Location = new System.Drawing.Point(2, 2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(496, 49);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // gbHistorial
            // 
            this.gbHistorial.Controls.Add(this.dgvHistorial);
            this.gbHistorial.Location = new System.Drawing.Point(2, 57);
            this.gbHistorial.Name = "gbHistorial";
            this.gbHistorial.Size = new System.Drawing.Size(499, 319);
            this.gbHistorial.TabIndex = 4;
            this.gbHistorial.TabStop = false;
            // 
            // dgvHistorial
            // 
            this.dgvHistorial.AllowUserToAddRows = false;
            this.dgvHistorial.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Honeydew;
            this.dgvHistorial.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvHistorial.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvHistorial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHistorial.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvHistorial.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvHistorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorial.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chEvento,
            this.chUsuario,
            this.chFecha,
            this.chMaquina});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Ivory;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHistorial.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvHistorial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistorial.GridColor = System.Drawing.SystemColors.Control;
            this.dgvHistorial.Location = new System.Drawing.Point(3, 16);
            this.dgvHistorial.Name = "dgvHistorial";
            this.dgvHistorial.P_EsEditable = false;
            this.dgvHistorial.P_FormatoDecimal = null;
            this.dgvHistorial.P_FormatoFecha = null;
            this.dgvHistorial.P_NombreColCorrelativa = null;
            this.dgvHistorial.P_NombreTabla = null;
            this.dgvHistorial.P_NumeroDigitos = 0;
            this.dgvHistorial.ReadOnly = true;
            this.dgvHistorial.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvHistorial.RowHeadersWidth = 34;
            this.dgvHistorial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistorial.Size = new System.Drawing.Size(493, 300);
            this.dgvHistorial.TabIndex = 0;
            // 
            // chEvento
            // 
            this.chEvento.DataPropertyName = "evento";
            this.chEvento.HeaderText = "Evento";
            this.chEvento.Name = "chEvento";
            this.chEvento.ReadOnly = true;
            // 
            // chUsuario
            // 
            this.chUsuario.DataPropertyName = "usuario";
            this.chUsuario.HeaderText = "usuario";
            this.chUsuario.Name = "chUsuario";
            this.chUsuario.ReadOnly = true;
            // 
            // chFecha
            // 
            this.chFecha.DataPropertyName = "fecha";
            this.chFecha.HeaderText = "Fecha";
            this.chFecha.Name = "chFecha";
            this.chFecha.ReadOnly = true;
            this.chFecha.Width = 120;
            // 
            // chMaquina
            // 
            this.chMaquina.DataPropertyName = "maquina";
            this.chMaquina.HeaderText = "Maquina";
            this.chMaquina.Name = "chMaquina";
            this.chMaquina.ReadOnly = true;
            this.chMaquina.Width = 130;
            // 
            // Historial
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(502, 379);
            this.Controls.Add(this.gbHistorial);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Historial";
            this.Text = "Historial";
            this.Load += new System.EventHandler(this.Historial_Load);
            this.gbHistorial.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bgwHilo;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox gbHistorial;
        private MyControlsDataBinding.Controles.MyDataGridViewDetails dgvHistorial;
        private System.Windows.Forms.DataGridViewTextBoxColumn chEvento;
        private System.Windows.Forms.DataGridViewTextBoxColumn chUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn chFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn chMaquina;
    }
}