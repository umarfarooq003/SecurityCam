namespace SecuirtyCam
{
    partial class VideoStream
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
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.videostreamloadbutton = new Guna.UI2.WinForms.Guna2Button();
            this.stopbutton = new Guna.UI2.WinForms.Guna2Button();
            this.alerttimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(583, 10);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(253, 47);
            this.guna2HtmlLabel1.TabIndex = 0;
            this.guna2HtmlLabel1.Text = "Video Streaming";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(12, 65);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1342, 549);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // videostreamloadbutton
            // 
            this.videostreamloadbutton.BackColor = System.Drawing.Color.DarkGray;
            this.videostreamloadbutton.BorderColor = System.Drawing.Color.SlateGray;
            this.videostreamloadbutton.BorderRadius = 20;
            this.videostreamloadbutton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.videostreamloadbutton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.videostreamloadbutton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.videostreamloadbutton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.videostreamloadbutton.FillColor = System.Drawing.Color.DarkCyan;
            this.videostreamloadbutton.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold);
            this.videostreamloadbutton.ForeColor = System.Drawing.Color.White;
            this.videostreamloadbutton.Location = new System.Drawing.Point(943, 620);
            this.videostreamloadbutton.Name = "videostreamloadbutton";
            this.videostreamloadbutton.Size = new System.Drawing.Size(190, 36);
            this.videostreamloadbutton.TabIndex = 40;
            this.videostreamloadbutton.Text = "Video Stream Load";
            this.videostreamloadbutton.Click += new System.EventHandler(this.videostreamloadbutton_Click);
            // 
            // stopbutton
            // 
            this.stopbutton.BackColor = System.Drawing.Color.DarkGray;
            this.stopbutton.BorderColor = System.Drawing.Color.SlateGray;
            this.stopbutton.BorderRadius = 20;
            this.stopbutton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.stopbutton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.stopbutton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.stopbutton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.stopbutton.FillColor = System.Drawing.Color.DarkCyan;
            this.stopbutton.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold);
            this.stopbutton.ForeColor = System.Drawing.Color.White;
            this.stopbutton.Location = new System.Drawing.Point(1164, 620);
            this.stopbutton.Name = "stopbutton";
            this.stopbutton.Size = new System.Drawing.Size(190, 36);
            this.stopbutton.TabIndex = 41;
            this.stopbutton.Text = "Stop Video Stream ";
            this.stopbutton.Click += new System.EventHandler(this.stopbutton_Click);
            // 
            // alerttimer
            // 
            this.alerttimer.Tick += new System.EventHandler(this.alerttimer_Tick);
            // 
            // VideoStream
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(1366, 740);
            this.Controls.Add(this.stopbutton);
            this.Controls.Add(this.videostreamloadbutton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "VideoStream";
            this.Text = "VideoStream";
            this.Load += new System.EventHandler(this.VideoStream_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Guna.UI2.WinForms.Guna2Button videostreamloadbutton;
        private Guna.UI2.WinForms.Guna2Button stopbutton;
        private System.Windows.Forms.Timer alerttimer;
    }
}