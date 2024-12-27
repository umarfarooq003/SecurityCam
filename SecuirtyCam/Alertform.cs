using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecuirtyCam
{
    public partial class Alertform : Form
    {
        public Alertform()
        {
            InitializeComponent();
        }

        private void Alertform_Load(object sender, EventArgs e)
        {

        }

        // Constructor that takes the alert message and image path
        public Alertform(string alertMessage, string imagePath)
        {
            InitializeComponent();

            // Set the alert message in the label (Assuming you have a label named lblAlertMessage)
            lblAlertMessage.Text = alertMessage;

            // Check if the image path exists
            if (File.Exists(imagePath))
            {
                try
                {
                    // Load the image into the PictureBox (Assuming you have a PictureBox named pictureBox1)
                    pictureBox1.Image = Image.FromFile(imagePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Image file not found at path: " + imagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Exitbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
