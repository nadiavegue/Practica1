namespace Pr1
{
    partial class RelojAnalogico
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
            this.SuspendLayout();
            // 
            // RelojAnalogico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.Location = new System.Drawing.Point(425, 295);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RelojAnalogico";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "RelojAnalógico";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Manecillas);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RelojAnalogico_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RelojAnalogico_MouseMove);
            this.Resize += new System.EventHandler(this.Cambio_tamaño);
            this.ResumeLayout(false);

        }

        #endregion
    }
}