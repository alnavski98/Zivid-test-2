namespace zivid_test
{
    partial class Form2
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
            this.show_error_picture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.show_error_picture)).BeginInit();
            this.SuspendLayout();
            // 
            // show_error_picture
            // 
            this.show_error_picture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.show_error_picture.Location = new System.Drawing.Point(12, 12);
            this.show_error_picture.Name = "show_error_picture";
            this.show_error_picture.Size = new System.Drawing.Size(1920, 1200);
            this.show_error_picture.TabIndex = 0;
            this.show_error_picture.TabStop = false;
            this.show_error_picture.Click += new System.EventHandler(this.show_error_picture_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1898, 1050);
            this.Controls.Add(this.show_error_picture);
            this.MaximumSize = new System.Drawing.Size(1920, 1200);
            this.MinimumSize = new System.Drawing.Size(1918, 1018);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.show_error_picture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox show_error_picture;
    }
}