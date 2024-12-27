using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SecuirtyCam.LoginForm;

namespace SecuirtyCam
{
    public partial class notification : Form
    {
        private static string ConnectionString = LoginForm.ConnectionString;
        public notification()
        {
            InitializeComponent();
        }

        private void alertListBox_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the user clicked on the "View" column (assuming column 4 is "View")
            if (e.ColumnIndex == 4) // Assuming column 4 is "View"
            {
                int notificationId = Convert.ToInt32(alertListBox.Rows[e.RowIndex].Cells[0].Value);
                string imagePath = alertListBox.Rows[e.RowIndex].Cells[4].Value.ToString(); // Column 4 should hold ImagePath
                DisplayImage(imagePath);
                OpenNotificationDetail(notificationId);
            }
            // Check if the clicked column is the "Delete" button column (column index of Delete button column)
            // Check if the clicked column is the "Delete" button column
            else if (e.ColumnIndex == alertListBox.Columns["DeleteButton"].Index) // Delete button column
            {
                // Get the notification Id from the first column (assumed to be the Id column)
                int notificationId = Convert.ToInt32(alertListBox.Rows[e.RowIndex].Cells[0].Value);

                // Ask for user confirmation before deleting
                DialogResult result = MessageBox.Show("Are you sure you want to delete this notification?",
                                                      "Confirm Deletion",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Proceed to delete the notification
                    DeleteNotification(notificationId);
                }
            }
        }

        private void notification_Load(object sender, EventArgs e)
        {
            LoadNotifications();
            // Attach CellFormatting event for conditional formatting
            alertListBox.RowsAdded += alertListBox_RowsAdded;

        }

        private void alertListBox_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            foreach (DataGridViewRow row in alertListBox.Rows)
            {
                if (row.Cells["Status"].Value?.ToString() == "Unread")
                {
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }


        private void LoadNotifications()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    // Query to fetch notifications (now including ImagePath and IsRead)
                    string query = "SELECT Id,Message, Timestamp,ImagePath, IsRead FROM Alerts ORDER BY Timestamp DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        alertListBox.Rows.Clear(); // Clear existing rows

                        if (reader.HasRows)
                        {
                            // Loop through the notifications and add them to the DataGridView
                            while (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["Id"]);
                                string timestamp = reader["Timestamp"].ToString();
                                string message = reader["Message"].ToString();
                                string imagePath = reader["ImagePath"].ToString();
                                bool isRead = Convert.ToBoolean(reader["IsRead"]);

                                // Add the notification to the DataGridView
                                alertListBox.Rows.Add(id, timestamp, message, isRead ? "Read" : "Unread", imagePath);

                                // Optionally, display the image of the first notification or any logic you prefer
                                if (id == 1) // Just an example; you could change this logic
                                {
                                    DisplayImage(imagePath); // Call the DisplayImage method to display the image
                                }

                                // Check and apply the style for unread rows
                                if (!isRead)
                                {
                                    int rowIndex = alertListBox.Rows.Count - 1;
                                    alertListBox.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightYellow;
                                    alertListBox.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Black;
                                }
                            }
                        }
                        else
                        {
                            // If no notifications are found, display a default image
                            DisplayImage("path/to/default/image.jpg");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading notifications: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void OpenNotificationDetail(int notificationId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    string query = "UPDATE Alerts SET IsRead = 1 WHERE Id = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", notificationId);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadNotifications(); // Reload to update the read status
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating notification: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void DeleteNotification(int notificationId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    // SQL query to delete the notification from the database
                    string query = "DELETE FROM Alerts WHERE Id = @Id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add the Id parameter to prevent SQL injection
                        cmd.Parameters.AddWithValue("@Id", notificationId);

                        // Execute the query to delete the record
                        cmd.ExecuteNonQuery();
                    }
                }

                // Notify the user of successful deletion
                MessageBox.Show("Notification deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reload the notifications to reflect the changes in the DataGridView
                LoadNotifications();
            }
            catch (Exception ex)
            {
                // Handle any errors that might occur during the deletion
                MessageBox.Show($"Error deleting notification: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DisplayImage(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                // Display the image in the PictureBox (ensure the PictureBox is present on the form)
                PictureBox1.Image = Image.FromFile(imagePath);
            }
            else
            {
                MessageBox.Show("Image not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Exitbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
