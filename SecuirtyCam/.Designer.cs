namespace SecuirtyCam
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.guna2CirclePictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.Exitbtn = new Guna.UI2.WinForms.Guna2Button();
            this.loginbutton = new Guna.UI2.WinForms.Guna2Button();
            this.PASSWORDBOX = new Guna.UI2.WinForms.Guna2TextBox();
            this.USERNAMEBOX = new Guna.UI2.WinForms.Guna2TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2CirclePictureBox1
            // 
            this.guna2CirclePictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("guna2CirclePictureBox1.Image")));
            this.guna2CirclePictureBox1.ImageRotate = 0F;
            this.guna2CirclePictureBox1.Location = new System.Drawing.Point(601, 84);
            this.guna2CirclePictureBox1.Name = "guna2CirclePictureBox1";
            this.guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CirclePictureBox1.Size = new System.Drawing.Size(139, 132);
            this.guna2CirclePictureBox1.TabIndex = 30;
            this.guna2CirclePictureBox1.TabStop = false;
            this.guna2CirclePictureBox1.Click += new System.EventHandler(this.guna2CirclePictureBox1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.guna2HtmlLabel1);
            this.panel1.Location = new System.Drawing.Point(482, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(337, 65);
            this.panel1.TabIndex = 29;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold);
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(126, 7);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(83, 38);
            this.guna2HtmlLabel1.TabIndex = 2;
            this.guna2HtmlLabel1.Text = "Login";
            this.guna2HtmlLabel1.Click += new System.EventHandler(this.guna2HtmlLabel1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.ForeColor = System.Drawing.Color.Black;
            this.checkBox1.Location = new System.Drawing.Point(536, 321);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(130, 23);
            this.checkBox1.TabIndex = 35;
            this.checkBox1.Text = "Show Password";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Exitbtn
            // 
            this.Exitbtn.BorderColor = System.Drawing.Color.Blue;
            this.Exitbtn.BorderRadius = 20;
            this.Exitbtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Exitbtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Exitbtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Exitbtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Exitbtn.FillColor = System.Drawing.Color.Red;
            this.Exitbtn.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Exitbtn.ForeColor = System.Drawing.Color.White;
            this.Exitbtn.Location = new System.Drawing.Point(528, 401);
            this.Exitbtn.Name = "Exitbtn";
            this.Exitbtn.Size = new System.Drawing.Size(283, 45);
            this.Exitbtn.TabIndex = 34;
            this.Exitbtn.Text = "Exit";
            this.Exitbtn.Click += new System.EventHandler(this.Exitbtn_Click);
            // 
            // loginbutton
            // 
            this.loginbutton.BorderColor = System.Drawing.Color.Blue;
            this.loginbutton.BorderRadius = 20;
            this.loginbutton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.loginbutton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.loginbutton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.loginbutton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.loginbutton.FillColor = System.Drawing.Color.Blue;
            this.loginbutton.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginbutton.ForeColor = System.Drawing.Color.White;
            this.loginbutton.Location = new System.Drawing.Point(528, 350);
            this.loginbutton.Name = "loginbutton";
            this.loginbutton.Size = new System.Drawing.Size(283, 45);
            this.loginbutton.TabIndex = 33;
            this.loginbutton.Text = "Login";
            this.loginbutton.Click += new System.EventHandler(this.loginbutton_Click);
            // 
            // PASSWORDBOX
            // 
            this.PASSWORDBOX.Animated = true;
            this.PASSWORDBOX.AutoRoundedCorners = true;
            this.PASSWORDBOX.BorderRadius = 17;
            this.PASSWORDBOX.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.PASSWORDBOX.DefaultText = "";
            this.PASSWORDBOX.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.PASSWORDBOX.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.PASSWORDBOX.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PASSWORDBOX.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.PASSWORDBOX.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PASSWORDBOX.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.PASSWORDBOX.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.PASSWORDBOX.IconLeft = ((System.Drawing.Image)(resources.GetObject("PASSWORDBOX.IconLeft")));
            this.PASSWORDBOX.IconLeftSize = new System.Drawing.Size(25, 25);
            this.PASSWORDBOX.IconRightSize = new System.Drawing.Size(50, 20);
            this.PASSWORDBOX.Location = new System.Drawing.Point(536, 278);
            this.PASSWORDBOX.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PASSWORDBOX.Name = "PASSWORDBOX";
            this.PASSWORDBOX.PasswordChar = '●';
            this.PASSWORDBOX.PlaceholderText = "Enter Password";
            this.PASSWORDBOX.SelectedText = "";
            this.PASSWORDBOX.Size = new System.Drawing.Size(283, 36);
            this.PASSWORDBOX.TabIndex = 32;
            this.PASSWORDBOX.TextChanged += new System.EventHandler(this.PASSWORDBOX_TextChanged);
            this.PASSWORDBOX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PASSWORDBOX_KeyDown);
            // 
            // USERNAMEBOX
            // 
            this.USERNAMEBOX.Animated = true;
            this.USERNAMEBOX.AutoRoundedCorners = true;
            this.USERNAMEBOX.BorderRadius = 17;
            this.USERNAMEBOX.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.USERNAMEBOX.DefaultText = "";
            this.USERNAMEBOX.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.USERNAMEBOX.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.USERNAMEBOX.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.USERNAMEBOX.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.USERNAMEBOX.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.USERNAMEBOX.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.USERNAMEBOX.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.USERNAMEBOX.IconLeft = ((System.Drawing.Image)(resources.GetObject("USERNAMEBOX.IconLeft")));
            this.USERNAMEBOX.IconLeftSize = new System.Drawing.Size(25, 25);
            this.USERNAMEBOX.IconRightSize = new System.Drawing.Size(30, 30);
            this.USERNAMEBOX.Location = new System.Drawing.Point(536, 223);
            this.USERNAMEBOX.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.USERNAMEBOX.Name = "USERNAMEBOX";
            this.USERNAMEBOX.PasswordChar = '\0';
            this.USERNAMEBOX.PlaceholderText = "Enter Username";
            this.USERNAMEBOX.SelectedText = "";
            this.USERNAMEBOX.Size = new System.Drawing.Size(283, 36);
            this.USERNAMEBOX.TabIndex = 31;
            this.USERNAMEBOX.TextChanged += new System.EventHandler(this.USERNAMEBOX_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(459, 492);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 28;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkColor = System.Drawing.Color.White;
            this.linkLabel1.Location = new System.Drawing.Point(689, 463);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(110, 20);
            this.linkLabel1.TabIndex = 42;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Sign Up now";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(562, 463);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 20);
            this.label1.TabIndex = 41;
            this.label1.Text = "Not a member?";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(849, 492);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.guna2CirclePictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.Exitbtn);
            this.Controls.Add(this.loginbutton);
            this.Controls.Add(this.PASSWORDBOX);
            this.Controls.Add(this.USERNAMEBOX);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginForm";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox1;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private System.Windows.Forms.CheckBox checkBox1;
        private Guna.UI2.WinForms.Guna2Button Exitbtn;
        private Guna.UI2.WinForms.Guna2Button loginbutton;
        private Guna.UI2.WinForms.Guna2TextBox PASSWORDBOX;
        private Guna.UI2.WinForms.Guna2TextBox USERNAMEBOX;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label1;
    }
}

