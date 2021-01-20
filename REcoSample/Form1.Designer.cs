namespace REcoSample
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.VisualizaImagen = new System.Windows.Forms.PictureBox();
            this.lblHoraSystemaBig = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.VisualizaImagen)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 44);
            this.label1.TabIndex = 0;
            // 
            // VisualizaImagen
            // 
            this.VisualizaImagen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.VisualizaImagen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VisualizaImagen.Image = global::REcoSample.Properties.Resources.RomanUI;
            this.VisualizaImagen.Location = new System.Drawing.Point(0, 0);
            this.VisualizaImagen.Name = "VisualizaImagen";
            this.VisualizaImagen.Size = new System.Drawing.Size(200, 200);
            this.VisualizaImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.VisualizaImagen.TabIndex = 78;
            this.VisualizaImagen.TabStop = false;
            // 
            // lblHoraSystemaBig
            // 
            this.lblHoraSystemaBig.AutoSize = true;
            this.lblHoraSystemaBig.BackColor = System.Drawing.Color.Black;
            this.lblHoraSystemaBig.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.lblHoraSystemaBig.ForeColor = System.Drawing.Color.White;
            this.lblHoraSystemaBig.Location = new System.Drawing.Point(49, 91);
            this.lblHoraSystemaBig.Name = "lblHoraSystemaBig";
            this.lblHoraSystemaBig.Size = new System.Drawing.Size(98, 25);
            this.lblHoraSystemaBig.TabIndex = 227;
            this.lblHoraSystemaBig.Text = "00:00:00";
            this.lblHoraSystemaBig.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(200, 200);
            this.Controls.Add(this.lblHoraSystemaBig);
            this.Controls.Add(this.VisualizaImagen);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.VisualizaImagen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox VisualizaImagen;
        private System.Windows.Forms.Label lblHoraSystemaBig;
    }
}

