using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SecuirtyCam.LoginForm;

namespace SecuirtyCam
{
    public partial class Approval : Form
    {
        private string ConnectionString = LoginForm.ConnectionString;
        public Approval()
        {
            InitializeComponent();
        }

        private void Approval_Load(object sender, EventArgs e)
        {
            LoadData();
        }


        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    string query = "SELECT Id, FirstName, LastName, Email, Approval FROM Login WHERE Type = 'user'";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        dataGridView1.DataSource = table;

                        // Add an update button column if it does not already exist
                        if (!dataGridView1.Columns.Contains("Update"))
                        {
                            DataGridViewButtonColumn updateButton = new DataGridViewButtonColumn
                            {
                                Name = "Update",
                                HeaderText = "Action",
                                Text = "Approve",
                                UseColumnTextForButtonValue = true
                            };
                            dataGridView1.Columns.Add(updateButton);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is from the Update button column
            if (e.ColumnIndex == dataGridView1.Columns["Update"].Index && e.RowIndex >= 0)
            {
                int userId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);

                UpdateApprovalStatus(userId);
            }
        }

        private void UpdateApprovalStatus(int userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    string query = "UPDATE Login SET Approval = 1 WHERE Id = @UserId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("User approved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData(); // Refresh the grid
                        }
                        else
                        {
                            MessageBox.Show("Failed to update approval status.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating approval status: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
