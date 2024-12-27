using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SecuirtyCam.LoginForm;

namespace SecuirtyCam
{
    public partial class Reportproblem : Form
    {

        private static string ConnectionString = LoginForm.ConnectionString;
        public Reportproblem()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg;*.jpeg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(ofd.FileName);
                    pictureBox1.Tag = ofd.FileName; // Store the file path for later use
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null || string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Please select an image and enter a description.");
                return;
            }

            // Convert image to byte array
            byte[] imageBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                imageBytes = ms.ToArray();
            }

            string description = txtDescription.Text;

          
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "INSERT INTO ProblemReports (ImageData, Description) VALUES (@image, @description)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@image", imageBytes);
                    cmd.Parameters.AddWithValue("@description", description);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            MessageBox.Show("Data saved successfully!");
        }
    }
}
