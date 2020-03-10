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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btn_snapshot = new System.Windows.Forms.Button();
            this.btn_connect = new System.Windows.Forms.Button();
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
            this.btn_load_baselines = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Disconnect_PLS = new System.Windows.Forms.Button();
            this.btn_apply_median_filter = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ExposureTXT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IrisTXT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCntTXT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_snapshot
            // 
            this.btn_snapshot.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.btn_snapshot.Location = new System.Drawing.Point(593, 71);
            this.btn_snapshot.Name = "btn_snapshot";
            this.btn_snapshot.Size = new System.Drawing.Size(184, 75);
            this.btn_snapshot.TabIndex = 0;
            this.btn_snapshot.Text = "Snapshot";
            this.btn_snapshot.UseVisualStyleBackColor = true;
            this.btn_snapshot.Click += new System.EventHandler(this.btn_snapshot_Click);
            // 
            // btn_connect
            // 
            this.btn_connect.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.btn_connect.Location = new System.Drawing.Point(12, 72);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(184, 74);
            this.btn_connect.TabIndex = 1;
            this.btn_connect.Text = "Connect";
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // btn_assist_mode
            // 
            this.btn_assist_mode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.btn_assist_mode.Location = new System.Drawing.Point(785, 70);
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
            this.ExposureTXT.Location = new System.Drawing.Point(243, 101);
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
            this.ExposureTXT.Size = new System.Drawing.Size(138, 35);
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
            this.IrisTXT.Location = new System.Drawing.Point(243, 178);
            this.IrisTXT.Maximum = new decimal(new int[] {
            72,
            0,
            0,
            0});
            this.IrisTXT.Name = "IrisTXT";
            this.IrisTXT.Size = new System.Drawing.Size(137, 35);
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
            this.label1.Location = new System.Drawing.Point(236, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 29);
            this.label1.TabIndex = 7;
            this.label1.Text = "Exposure time:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(240, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 29);
            this.label2.TabIndex = 8;
            this.label2.Text = "Iris:";
            // 
            // btn_update
            // 
            this.btn_update.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.btn_update.Location = new System.Drawing.Point(243, 223);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(166, 100);
            this.btn_update.TabIndex = 9;
            this.btn_update.Text = "Apply settings";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // LoggTXT
            // 
            this.LoggTXT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoggTXT.Location = new System.Drawing.Point(25, 355);
            this.LoggTXT.Multiline = true;
            this.LoggTXT.Name = "LoggTXT";
            this.LoggTXT.ReadOnly = true;
            this.LoggTXT.Size = new System.Drawing.Size(421, 278);
            this.LoggTXT.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(20, 323);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 29);
            this.label3.TabIndex = 11;
            this.label3.Text = "Status:";
            // 
            // btn_baseline
            // 
            this.btn_baseline.Location = new System.Drawing.Point(462, 71);
            this.btn_baseline.Name = "btn_baseline";
            this.btn_baseline.Size = new System.Drawing.Size(120, 34);
            this.btn_baseline.TabIndex = 12;
            this.btn_baseline.Text = "Baseline";
            this.btn_baseline.UseVisualStyleBackColor = true;
            this.btn_baseline.Click += new System.EventHandler(this.btn_baseline_Click);
            // 
            // pictureCntTXT
            // 
            this.pictureCntTXT.Location = new System.Drawing.Point(462, 100);
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
            this.btn_connect_PLS.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_connect_PLS.Location = new System.Drawing.Point(1018, 70);
            this.btn_connect_PLS.Name = "btn_connect_PLS";
            this.btn_connect_PLS.Size = new System.Drawing.Size(230, 74);
            this.btn_connect_PLS.TabIndex = 14;
            this.btn_connect_PLS.Text = "Connect PLC";
            this.btn_connect_PLS.UseVisualStyleBackColor = true;
            this.btn_connect_PLS.Click += new System.EventHandler(this.btn_connect_PLS_Click);
            // 
            // btn_disconnect
            // 
            this.btn_disconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_disconnect.Location = new System.Drawing.Point(12, 156);
            this.btn_disconnect.Name = "btn_disconnect";
            this.btn_disconnect.Size = new System.Drawing.Size(184, 74);
            this.btn_disconnect.TabIndex = 15;
            this.btn_disconnect.Text = "Disconnect";
            this.btn_disconnect.UseVisualStyleBackColor = true;
            this.btn_disconnect.Click += new System.EventHandler(this.btn_disconnect_Click);
            // 
            // btn_load_baselines
            // 
            this.btn_load_baselines.Location = new System.Drawing.Point(462, 156);
            this.btn_load_baselines.Name = "btn_load_baselines";
            this.btn_load_baselines.Size = new System.Drawing.Size(146, 35);
            this.btn_load_baselines.TabIndex = 16;
            this.btn_load_baselines.Text = "Load baselines";
            this.btn_load_baselines.UseVisualStyleBackColor = true;
            this.btn_load_baselines.Click += new System.EventHandler(this.btn_load_baselines_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(628, 155);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(163, 35);
            this.button1.TabIndex = 17;
            this.button1.Text = "Test write image";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Disconnect_PLS
            // 
            this.Disconnect_PLS.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.Disconnect_PLS.Location = new System.Drawing.Point(1018, 156);
            this.Disconnect_PLS.Name = "Disconnect_PLS";
            this.Disconnect_PLS.Size = new System.Drawing.Size(230, 74);
            this.Disconnect_PLS.TabIndex = 18;
            this.Disconnect_PLS.Text = "Disconnect PLC";
            this.Disconnect_PLS.UseVisualStyleBackColor = true;
            this.Disconnect_PLS.Click += new System.EventHandler(this.Disconnect_PLS_Click);
            // 
            // btn_apply_median_filter
            // 
            this.btn_apply_median_filter.Location = new System.Drawing.Point(812, 155);
            this.btn_apply_median_filter.Name = "btn_apply_median_filter";
            this.btn_apply_median_filter.Size = new System.Drawing.Size(157, 35);
            this.btn_apply_median_filter.TabIndex = 19;
            this.btn_apply_median_filter.Text = "Apply median filter";
            this.btn_apply_median_filter.UseVisualStyleBackColor = true;
            this.btn_apply_median_filter.Click += new System.EventHandler(this.btn_apply_median_filter_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label4.Location = new System.Drawing.Point(36, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 40);
            this.label4.TabIndex = 20;
            this.label4.Text = "Camera";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label5.Location = new System.Drawing.Point(234, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 40);
            this.label5.TabIndex = 21;
            this.label5.Text = "Settings";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label6.Location = new System.Drawing.Point(576, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(265, 40);
            this.label6.TabIndex = 22;
            this.label6.Text = "Taking Pictures";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label7.Location = new System.Drawing.Point(1093, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 40);
            this.label7.TabIndex = 23;
            this.label7.Text = "PLC";
            // 
            // chart2
            // 
            chartArea1.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart2.Legends.Add(legend1);
            this.chart2.Location = new System.Drawing.Point(491, 269);
            this.chart2.Name = "chart2";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.LegendText = " ";
            series1.LegendToolTip = "white";
            series1.MarkerBorderColor = System.Drawing.Color.Transparent;
            series1.Name = "Series1";
            this.chart2.Series.Add(series1);
            this.chart2.Size = new System.Drawing.Size(757, 364);
            this.chart2.TabIndex = 25;
            this.chart2.Text = "chart2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label8.Location = new System.Drawing.Point(486, 237);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(315, 29);
            this.label8.TabIndex = 26;
            this.label8.Text = "Live chart of \"errornumbers\":";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1265, 664);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_apply_median_filter);
            this.Controls.Add(this.Disconnect_PLS);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_load_baselines);
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
            this.Controls.Add(this.btn_connect);
            this.Controls.Add(this.btn_snapshot);
            this.MaximumSize = new System.Drawing.Size(1980, 1000);
            this.MinimumSize = new System.Drawing.Size(1003, 720);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.ExposureTXT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IrisTXT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCntTXT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_snapshot;
        private System.Windows.Forms.Button btn_connect;
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
        private System.Windows.Forms.Button btn_load_baselines;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Disconnect_PLS;
        private System.Windows.Forms.Button btn_apply_median_filter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.Label label8;
    }
}

