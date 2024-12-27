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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static SecuirtyCam.LoginForm;

namespace SecuirtyCam
{
    public partial class SignupForm : Form
    {

        private string ConnectionString = LoginForm.ConnectionString;
        public SignupForm()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Check if LoginForm is already open
            LoginForm loginForm = Application.OpenForms.OfType<LoginForm>().FirstOrDefault();

            if (loginForm == null)
            {
                // If not open, create and show it
                loginForm = new LoginForm();
                loginForm.Show();
            }
            else
            {
                // If already open, bring it to the front
                loginForm.BringToFront();
            }

            // Close the current form
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private bool IsValidEmail(string email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(email, "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$");
        }

        private void loginbutton_Click(object sender, EventArgs e)
        {
            string FirstName = FirstNametxtbox.Text;
            string LastName = LastNametxtbox.Text;
            string Email = emailtxtbox.Text;
            string password = passwordtxtbox.Text;
            string confirmPassword = confirmpasswordtxtbox.Text;
            string type = "user";
            bool approval = false; // Default approval status is false.

            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) ||
                string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if passwords match
            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsValidEmail(Email))
            {
                MessageBox.Show("Invalid email format. Use @ and gmail.com", "Email Syntax Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    // SQL query to insert data
                    string query = @"INSERT INTO Login (FirstName, LastName, Email, Password, Type, Approval)
                                     VALUES (@FirstName, @LastName, @Email, @Password, @Type, @Approval)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters to prevent SQL injection
                        cmd.Parameters.AddWithValue("@FirstName", FirstName);
                        cmd.Parameters.AddWithValue("@LastName", LastName);
                        cmd.Parameters.AddWithValue("@Email", Email);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@Type", type);
                        cmd.Parameters.AddWithValue("@Approval", approval);

                        // Execute the query
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("User registered successfully. Approval pending from admin.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            passwordtxtbox.PasswordChar = checkBox1.Checked ? '\0' : '*';
            confirmpasswordtxtbox.PasswordChar = checkBox1.Checked ? '\0' : '*';
        }

        private void ClearData()
        {
            FirstNametxtbox.Text = null;
            LastNametxtbox.Text= null;
            emailtxtbox.Text = null;
            passwordtxtbox.Text = null;
            confirmpasswordtxtbox.Text= null;
        }
    }
}
