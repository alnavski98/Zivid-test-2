namespace zivid_test
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
            this.btn_snapshot = new System.Windows.Forms.Button();
            this.connect = new System.Windows.Forms.Button();
            this.btn_assist_mode = new System.Windows.Forms.Button();
            this.ExposureTXT = new System.Windows.Forms.NumericUpDown();
            this.IrisTXT = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_update = new System.Windows.Forms.Button();
            this.LoggTXT = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_baseline = new System.Windows.Forms.Button();
            this.pictureCntTXT = new System.Windows.Forms.NumericUpDown();
            this.btn_connect_PLS = new System.Windows.Forms.Button();
            this.btn_disconnect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ExposureTXT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IrisTXT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCntTXT)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_snapshot
            // 
            this.btn_snapshot.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.btn_snapshot.Location = new System.Drawing.Point(218, 15);
            this.btn_snapshot.Name = "btn_snapshot";
            this.btn_snapshot.Size = new System.Drawing.Size(184, 75);
            this.btn_snapshot.TabIndex = 0;
            this.btn_snapshot.Text = "Snapshot";
            this.btn_snapshot.UseVisualStyleBackColor = true;
            this.btn_snapshot.Click += new System.EventHandler(this.btn_snapshot_Click);
            // 
            // connect
            // 
            this.connect.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.connect.Location = new System.Drawing.Point(12, 15);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(184, 75);
            this.connect.TabIndex = 1;
            this.connect.Text = "Connect";
            this.connect.UseVisualStyleBackColor = true;
            this.connect.Click += new System.EventHandler(this.connect_Click);
            // 
            // btn_assist_mode
            // 
            this.btn_assist_mode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.btn_assist_mode.Location = new System.Drawing.Point(441, 15);
            this.btn_assist_mode.Name = "btn_assist_mode";
            this.btn_assist_mode.Size = new System.Drawing.Size(184, 75);
            this.btn_assist_mode.TabIndex = 2;
            this.btn_assist_mode.Text = "Assist Mode";
            this.btn_assist_mode.UseVisualStyleBackColor = true;
            this.btn_assist_mode.Click += new System.EventHandler(this.btn_assist_mode_Click_1);
            // 
            // ExposureTXT
            // 
            this.ExposureTXT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ExposureTXT.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ExposureTXT.Location = new System.Drawing.Point(16, 162);
            this.ExposureTXT.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.ExposureTXT.Minimum = new decimal(new int[] {
            8333,
            0,
            0,
            0});
            this.ExposureTXT.Name = "ExposureTXT";
            this.ExposureTXT.Size = new System.Drawing.Size(180, 35);
            this.ExposureTXT.TabIndex = 4;
            this.ExposureTXT.Value = new decimal(new int[] {
            8333,
            0,
            0,
            0});
            // 
            // IrisTXT
            // 
            this.IrisTXT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.IrisTXT.Location = new System.Drawing.Point(218, 162);
            this.IrisTXT.Maximum = new decimal(new int[] {
            72,
            0,
            0,
            0});
            this.IrisTXT.Name = "IrisTXT";
            this.IrisTXT.Size = new System.Drawing.Size(184, 35);
            this.IrisTXT.TabIndex = 5;
            this.IrisTXT.Value = new decimal(new int[] {
            22,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(12, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 29);
            this.label1.TabIndex = 7;
            this.label1.Text = "Exposure time:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(213, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 29);
            this.label2.TabIndex = 8;
            this.label2.Text = "Iris:";
            // 
            // btn_update
            // 
            this.btn_update.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btn_update.Location = new System.Drawing.Point(12, 209);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(184, 72);
            this.btn_update.TabIndex = 9;
            this.btn_update.Text = "Apply settings";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // LoggTXT
            // 
            this.LoggTXT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LoggTXT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoggTXT.Location = new System.Drawing.Point(16, 348);
            this.LoggTXT.Multiline = true;
            this.LoggTXT.Name = "LoggTXT";
            this.LoggTXT.ReadOnly = true;
            this.LoggTXT.Size = new System.Drawing.Size(660, 235);
            this.LoggTXT.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 315);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 29);
            this.label3.TabIndex = 11;
            this.label3.Text = "Status:";
            // 
            // btn_baseline
            // 
            this.btn_baseline.Location = new System.Drawing.Point(494, 162);
            this.btn_baseline.Name = "btn_baseline";
            this.btn_baseline.Size = new System.Drawing.Size(123, 34);
            this.btn_baseline.TabIndex = 12;
            this.btn_baseline.Text = "Baseline";
            this.btn_baseline.UseVisualStyleBackColor = true;
            this.btn_baseline.Click += new System.EventHandler(this.btn_baseline_Click);
            // 
            // pictureCntTXT
            // 
            this.pictureCntTXT.Location = new System.Drawing.Point(494, 192);
            this.pictureCntTXT.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.pictureCntTXT.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pictureCntTXT.Name = "pictureCntTXT";
            this.pictureCntTXT.Size = new System.Drawing.Size(120, 26);
            this.pictureCntTXT.TabIndex = 13;
            this.pictureCntTXT.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.pictureCntTXT.ValueChanged += new System.EventHandler(this.pictureCntTXT_ValueChanged);
            // 
            // btn_connect_PLS
            // 
            this.btn_connect_PLS.Location = new System.Drawing.Point(494, 252);
            this.btn_connect_PLS.Name = "btn_connect_PLS";
            this.btn_connect_PLS.Size = new System.Drawing.Size(123, 37);
            this.btn_connect_PLS.TabIndex = 14;
            this.btn_connect_PLS.Text = "Connect PLS";
            this.btn_connect_PLS.UseVisualStyleBackColor = true;
            this.btn_connect_PLS.Click += new System.EventHandler(this.btn_connect_PLS_Click);
            // 
            // btn_disconnect
            // 
            this.btn_disconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_disconnect.Location = new System.Drawing.Point(229, 215);
            this.btn_disconnect.Name = "btn_disconnect";
            this.btn_disconnect.Size = new System.Drawing.Size(229, 74);
            this.btn_disconnect.TabIndex = 15;
            this.btn_disconnect.Text = "DISCONNECT!";
            this.btn_disconnect.UseVisualStyleBackColor = true;
            this.btn_disconnect.Click += new System.EventHandler(this.btn_disconnect_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 611);
            this.Controls.Add(this.btn_disconnect);
            this.Controls.Add(this.btn_connect_PLS);
            this.Controls.Add(this.pictureCntTXT);
            this.Controls.Add(this.btn_baseline);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LoggTXT);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IrisTXT);
            this.Controls.Add(this.ExposureTXT);
            this.Controls.Add(this.btn_assist_mode);
            this.Controls.Add(this.connect);
            this.Controls.Add(this.btn_snapshot);
            this.MaximumSize = new System.Drawing.Size(1916, 1071);
            this.MinimumSize = new System.Drawing.Size(708, 641);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ExposureTXT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IrisTXT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCntTXT)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button btn_snapshot;
        private System.Windows.Forms.Button connect;
        private System.Windows.Forms.Button btn_assist_mode;
        private System.Windows.Forms.NumericUpDown ExposureTXT;
        private System.Windows.Forms.NumericUpDown IrisTXT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_update;
        public System.Windows.Forms.TextBox LoggTXT;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button btn_baseline;
        private System.Windows.Forms.NumericUpDown pictureCntTXT;
        private System.Windows.Forms.Button btn_connect_PLS;
        private System.Windows.Forms.Button btn_disconnect;
    }
}

