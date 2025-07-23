namespace SistemaTransporte
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
            this.btnAsignacion = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnRuta = new System.Windows.Forms.Button();
            this.btnAutobus = new System.Windows.Forms.Button();
            this.btnConductor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAsignacion
            // 
            this.btnAsignacion.BackColor = System.Drawing.Color.Red;
            this.btnAsignacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAsignacion.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAsignacion.Font = new System.Drawing.Font("MV Boli", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAsignacion.ForeColor = System.Drawing.Color.White;
            this.btnAsignacion.Location = new System.Drawing.Point(32, 292);
            this.btnAsignacion.Margin = new System.Windows.Forms.Padding(2);
            this.btnAsignacion.Name = "btnAsignacion";
            this.btnAsignacion.Size = new System.Drawing.Size(276, 53);
            this.btnAsignacion.TabIndex = 25;
            this.btnAsignacion.Text = "Asignacion";
            this.btnAsignacion.UseVisualStyleBackColor = false;
            this.btnAsignacion.Click += new System.EventHandler(this.btnAsignacion_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.Red;
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSalir.Font = new System.Drawing.Font("MV Boli", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Location = new System.Drawing.Point(32, 379);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(276, 50);
            this.btnSalir.TabIndex = 24;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnRuta
            // 
            this.btnRuta.BackColor = System.Drawing.Color.Red;
            this.btnRuta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRuta.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRuta.Font = new System.Drawing.Font("MV Boli", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRuta.ForeColor = System.Drawing.Color.White;
            this.btnRuta.Location = new System.Drawing.Point(32, 206);
            this.btnRuta.Margin = new System.Windows.Forms.Padding(2);
            this.btnRuta.Name = "btnRuta";
            this.btnRuta.Size = new System.Drawing.Size(276, 53);
            this.btnRuta.TabIndex = 23;
            this.btnRuta.Text = "Ruta";
            this.btnRuta.UseVisualStyleBackColor = false;
            this.btnRuta.Click += new System.EventHandler(this.btnRuta_Click);
            // 
            // btnAutobus
            // 
            this.btnAutobus.BackColor = System.Drawing.Color.Red;
            this.btnAutobus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAutobus.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAutobus.Font = new System.Drawing.Font("MV Boli", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAutobus.ForeColor = System.Drawing.Color.White;
            this.btnAutobus.Location = new System.Drawing.Point(32, 118);
            this.btnAutobus.Margin = new System.Windows.Forms.Padding(2);
            this.btnAutobus.Name = "btnAutobus";
            this.btnAutobus.Size = new System.Drawing.Size(276, 53);
            this.btnAutobus.TabIndex = 22;
            this.btnAutobus.Text = "Autobus";
            this.btnAutobus.UseVisualStyleBackColor = false;
            this.btnAutobus.Click += new System.EventHandler(this.btnAutobus_Click);
            // 
            // btnConductor
            // 
            this.btnConductor.BackColor = System.Drawing.Color.Red;
            this.btnConductor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConductor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConductor.Font = new System.Drawing.Font("MV Boli", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConductor.ForeColor = System.Drawing.Color.White;
            this.btnConductor.Location = new System.Drawing.Point(32, 28);
            this.btnConductor.Margin = new System.Windows.Forms.Padding(2);
            this.btnConductor.Name = "btnConductor";
            this.btnConductor.Size = new System.Drawing.Size(276, 53);
            this.btnConductor.TabIndex = 21;
            this.btnConductor.Text = "Conductor";
            this.btnConductor.UseVisualStyleBackColor = false;
            this.btnConductor.Click += new System.EventHandler(this.btnConductor_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(335, 450);
            this.Controls.Add(this.btnAsignacion);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnRuta);
            this.Controls.Add(this.btnAutobus);
            this.Controls.Add(this.btnConductor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAsignacion;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnRuta;
        private System.Windows.Forms.Button btnAutobus;
        private System.Windows.Forms.Button btnConductor;
    }
}

