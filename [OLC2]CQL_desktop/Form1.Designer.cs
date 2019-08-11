namespace _OLC2_CQL_desktop
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtEntrada = new System.Windows.Forms.TextBox();
            this.btnCargarArchivo = new System.Windows.Forms.Button();
            this.btnLimpair = new System.Windows.Forms.Button();
            this.btnAnalizar = new System.Windows.Forms.Button();
            this.txtSalida = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtEntrada
            // 
            this.txtEntrada.Location = new System.Drawing.Point(12, 12);
            this.txtEntrada.Multiline = true;
            this.txtEntrada.Name = "txtEntrada";
            this.txtEntrada.Size = new System.Drawing.Size(585, 177);
            this.txtEntrada.TabIndex = 0;
            // 
            // btnCargarArchivo
            // 
            this.btnCargarArchivo.Location = new System.Drawing.Point(12, 195);
            this.btnCargarArchivo.Name = "btnCargarArchivo";
            this.btnCargarArchivo.Size = new System.Drawing.Size(96, 23);
            this.btnCargarArchivo.TabIndex = 1;
            this.btnCargarArchivo.Text = "Cargar Archivo";
            this.btnCargarArchivo.UseVisualStyleBackColor = true;
            this.btnCargarArchivo.Click += new System.EventHandler(this.btnCargarArchivo_Click);
            // 
            // btnLimpair
            // 
            this.btnLimpair.Location = new System.Drawing.Point(114, 195);
            this.btnLimpair.Name = "btnLimpair";
            this.btnLimpair.Size = new System.Drawing.Size(96, 23);
            this.btnLimpair.TabIndex = 2;
            this.btnLimpair.Text = "Limpiar";
            this.btnLimpair.UseVisualStyleBackColor = true;
            this.btnLimpair.Click += new System.EventHandler(this.btnLimpair_Click);
            // 
            // btnAnalizar
            // 
            this.btnAnalizar.Location = new System.Drawing.Point(216, 195);
            this.btnAnalizar.Name = "btnAnalizar";
            this.btnAnalizar.Size = new System.Drawing.Size(96, 23);
            this.btnAnalizar.TabIndex = 3;
            this.btnAnalizar.Text = "Analizar";
            this.btnAnalizar.UseVisualStyleBackColor = true;
            this.btnAnalizar.Click += new System.EventHandler(this.btnAnalizar_Click);
            // 
            // txtSalida
            // 
            this.txtSalida.BackColor = System.Drawing.SystemColors.Desktop;
            this.txtSalida.Enabled = false;
            this.txtSalida.ForeColor = System.Drawing.SystemColors.Menu;
            this.txtSalida.Location = new System.Drawing.Point(12, 224);
            this.txtSalida.Multiline = true;
            this.txtSalida.Name = "txtSalida";
            this.txtSalida.Size = new System.Drawing.Size(585, 177);
            this.txtSalida.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 410);
            this.Controls.Add(this.txtSalida);
            this.Controls.Add(this.btnAnalizar);
            this.Controls.Add(this.btnLimpair);
            this.Controls.Add(this.btnCargarArchivo);
            this.Controls.Add(this.txtEntrada);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtEntrada;
        private System.Windows.Forms.Button btnCargarArchivo;
        private System.Windows.Forms.Button btnLimpair;
        private System.Windows.Forms.Button btnAnalizar;
        private System.Windows.Forms.TextBox txtSalida;
    }
}

