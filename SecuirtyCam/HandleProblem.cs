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
    public partial class HandleProblem : Form
    {
        private static string ConnectionString = LoginForm.ConnectionString;
        public HandleProblem()
        {
            InitializeComponent();
            LoadDataIntoGrid();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void HandleProblem_Load(object sender, EventArgs e)
        {

        }

        private void LoadDataIntoGrid()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "SELECT Id, Description, ReportDate FROM ProblemReports"; // Exclude ImageData for better performance.
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.DataSource = dt;
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row is clicked.
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Retrieve the ID of the selected record.
                int id = Convert.ToInt32(selectedRow.Cells["Id"].Value); // Assuming 'Id' column is present.

                // Fetch the full record, including the image, from the database.
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    string query = "SELECT ImageData, Description FROM ProblemReports WHERE Id = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Load the description.
                                txtDescription.Text = reader["Description"].ToString();

                                // Load the image into the PictureBox.
                                byte[] imageBytes = (byte[])reader["ImageData"];
                                using (MemoryStream ms = new MemoryStream(imageBytes))
                                {
                                    pictureBox1.Image = Image.FromStream(ms);
                                }
                            }
                        }
                        conn.Close();
                    }
                }
            }
        }
    }
}
