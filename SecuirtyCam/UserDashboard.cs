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
    public partial class UserDashboard : Form
    {
        private string ConnectionString = LoginForm.ConnectionString;
        public static string username = LoginForm.username;

        public void loadform(object form)
        {
            // Ensure the mainpanel has no other controls
            if (this.mainpanel.Controls.Count > 0)
                this.mainpanel.Controls.RemoveAt(0);

            // Safely cast the input object to Form
            if (form is Form f)
            {
                f.TopLevel = false; // Embed the form within the panel
                f.Dock = DockStyle.Fill; // Ensure the form fills the panel
                this.mainpanel.Controls.Add(f); // Add the form to the panel
                this.mainpanel.Tag = f; // Optionally store a reference to the loaded form
                f.Show(); // Display the form
            }
            else
            {
                // Log or handle the case where the provided object is not a form
                throw new ArgumentException("The provided object is not a Form.");
            }
        }

        public UserDashboard()
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

        private void profitDistributionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadform(new VideoStream());
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadform(new ViewAlert());
        }

        private void returnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();

            toolTip.AutoPopDelay = 5000; // Show tooltip for 5 seconds
            toolTip.InitialDelay = 500;  // Delay of 500ms before the tooltip appears

            loadform(new notificationalert()); // Correct way to load a form

            returnToolStripMenuItem.Text = "Checked!";

            Timer resetTimer = new Timer();
            resetTimer.Interval = 2000; // 2 seconds
            resetTimer.Tick += (s, args) =>
            {
                returnToolStripMenuItem.Text = "Receive Notification"; // Reset text
                resetTimer.Stop(); // Stop the timer
            };
            resetTimer.Start();
        }

        private void defectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadform(new Reportproblem());
        }

       
    }
}
