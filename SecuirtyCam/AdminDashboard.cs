using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SecuirtyCam.LoginForm;

namespace SecuirtyCam
{
    public partial class AdminDashboard : Form
    {
        private string ConnectionString = LoginForm.ConnectionString;
        public void loadform(object Form)
        {
            if (this.mainpanel.Controls.Count > 0)
                this.mainpanel.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(f);
            this.mainpanel.Tag = f;
            f.Show();

        }
        public AdminDashboard()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you want to sure to Exit?", "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

                System.Diagnostics.Process.GetCurrentProcess().Kill();
                Application.Exit();
            }
        }

        private void defectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadform(new HandleProblem());
        }

        private void approvalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadform(new Approval());
        }
    }
}
