namespace StarSpot
{
    partial class Radar
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
            this.RadarBox = new System.Windows.Forms.PictureBox();
            this.RadarTimer = new System.Windows.Forms.Timer(this.components);
            this.radar_label = new System.Windows.Forms.Label();
            this.radar_close_btn = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.RadarBox)).BeginInit();
            this.SuspendLayout();
            // 
            // RadarBox
            // 
            this.RadarBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(14)))), ((int)(((byte)(14)))));
            this.RadarBox.Location = new System.Drawing.Point(12, 45);
            this.RadarBox.Name = "RadarBox";
            this.RadarBox.Size = new System.Drawing.Size(326, 287);
            this.RadarBox.TabIndex = 0;
            this.RadarBox.TabStop = false;
            // 
            // RadarTimer
            // 
            this.RadarTimer.Enabled = true;
            this.RadarTimer.Interval = 350;
            this.RadarTimer.Tick += new System.EventHandler(this.RadarTimer_Tick);
            // 
            // radar_label
            // 
            this.radar_label.AutoSize = true;
            this.radar_label.BackColor = System.Drawing.Color.Transparent;
            this.radar_label.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.radar_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(116)))), ((int)(((byte)(221)))));
            this.radar_label.Location = new System.Drawing.Point(12, 9);
            this.radar_label.Name = "radar_label";
            this.radar_label.Size = new System.Drawing.Size(63, 28);
            this.radar_label.TabIndex = 2;
            this.radar_label.Text = "Radar";
            // 
            // radar_close_btn
            // 
            this.radar_close_btn.AutoSize = true;
            this.radar_close_btn.BackColor = System.Drawing.Color.Transparent;
            this.radar_close_btn.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radar_close_btn.ForeColor = System.Drawing.Color.White;
            this.radar_close_btn.Location = new System.Drawing.Point(318, 16);
            this.radar_close_btn.Name = "radar_close_btn";
            this.radar_close_btn.Size = new System.Drawing.Size(17, 17);
            this.radar_close_btn.TabIndex = 3;
            this.radar_close_btn.Text = "X";
            this.radar_close_btn.Click += new System.EventHandler(this.radar_close_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(223, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "[CPU INTENSIVE]";
            // 
            // Radar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(351, 344);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radar_close_btn);
            this.Controls.Add(this.radar_label);
            this.Controls.Add(this.RadarBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Radar";
            this.Opacity = 0.95D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Radar";
            this.TopMost = true;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Radar_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.RadarBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox RadarBox;
        private System.Windows.Forms.Timer RadarTimer;
        private System.Windows.Forms.Label radar_label;
        private System.Windows.Forms.Label radar_close_btn;
        private System.Windows.Forms.Label label1;
    }
}