using Guna.UI2.WinForms;
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
    public partial class ViewAlert : Form
    {
        private static string ConnectionString = LoginForm.ConnectionString;
        public ViewAlert()
        {
            InitializeComponent();
        }

        private void ViewAlert_Load(object sender, EventArgs e)
        {
            LoadAlertsFromDatabase();
            
        }


        private void LoadAlertsFromDatabase()
        {
            try
            {
                // SQL query to fetch all alerts
                string query = "SELECT Id, Message,ImagePath, Timestamp FROM Alerts ORDER BY Timestamp DESC";
                // Fetch the data from the database
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        alertListBox.DataSource = dataTable;
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading alerts from database: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



       


    }
}
