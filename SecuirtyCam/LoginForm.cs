using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecuirtyCam
{
    public partial class LoginForm : Form
    {
        public static string username;
        
        public static string ConnectionString = "Data Source=UMARFAROOQ\\SQLEXPRESS;Initial Catalog=SecurityCam;Integrated Security=True;Encrypt=False;";
        public LoginForm()
        {
            InitializeComponent();
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Login()
        {
            username = USERNAMEBOX.Text;
            string password = PASSWORDBOX.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill in both fields.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!IsValidEmail(username) && username != "admin")
            {
                MessageBox.Show("Invalid email format. Use @ and gmail.com", "Email Syntax Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (UserExist(username))
            {
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                    {
                        sqlConnection.Open();

                        string query = "SELECT Email, Password, Type, Approval FROM Login WHERE Email = @Username AND Password = @Password";
                        using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                        {
                            sqlCommand.Parameters.AddWithValue("@Username", username);
                            sqlCommand.Parameters.AddWithValue("@Password", password);

                            using (SqlDataReader reader = sqlCommand.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string userType = reader["Type"].ToString();
                                    bool isApproved = Convert.ToBoolean(reader["Approval"]);

                                    if (!isApproved)
                                    {
                                        MessageBox.Show("You don't have permission to log in. Please wait for admin approval.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }

                                    if (userType == "admin")
                                    {
                                        Hide();
                                        using (AdminDashboard mainMenu = new AdminDashboard())
                                        {
                                            mainMenu.ShowDialog();
                                        }
                                    }
                                    else if (userType == "user")
                                    {
                                        Hide();
                                        using (UserDashboard userDashboard = new UserDashboard())
                                        {
                                            userDashboard.ShowDialog();
                                        }
                                    }

                                    Application.Exit();
                                    ClearData();
                                }
                                else
                                {
                                    MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("User does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loginbutton_Click(object sender, EventArgs e)
        {

            Login();
        }

        private bool UserExist(string Username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM login WHERE email = @UserEmail";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserEmail", Username);

                        int count = (int)command.ExecuteScalar();

                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while checking user existence: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
                PASSWORDBOX.PasswordChar = checkBox1.Checked ? '\0' : '*';
        }

        private void PASSWORDBOX_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }

        private bool IsValidEmail(string email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(email, "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$");
        }

        private void ClearData()
        {
            USERNAMEBOX.Text = null;
            PASSWORDBOX.Text = null;
        }

        private void Exitbtn_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you want to sure to Exit?", "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

                System.Diagnostics.Process.GetCurrentProcess().Kill();
                Application.Exit();
            }
        }
       

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignupForm signupForm = new SignupForm();
            signupForm.ShowDialog();

        }

        private void USERNAMEBOX_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void PASSWORDBOX_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
