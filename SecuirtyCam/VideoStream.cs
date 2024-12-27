using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SecuirtyCam.UserDashboard;
using static SecuirtyCam.LoginForm;
using System.Net.Mail;
using System.Net;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;


namespace SecuirtyCam
{
    public partial class VideoStream : Form
    {
        private static string ConnectionString = LoginForm.ConnectionString;
        private VideoStreamService _videoService;
        private CancellationTokenSource _cancellationTokenSource;
        private Process _pythonProcess;
        private System.Windows.Forms.Timer alertTimer;
        private NotifyIcon notifyIcon; // Icon to show in the system tray
        private int notificationCount = 0;
        public static string receivermail = LoginForm.username;


        // Twilio credentials (replace with your actual account SID and auth token)
        private const string TwilioAccountSid = "AC312e8af33779e0c8358b53e2722749e7";
        private const string TwilioAuthToken = "80da540033d470b4acc499a560183025";
        private const string TwilioPhoneNumber = "+17752959796"; // Twilio phone number

        private const string ReceiverPhoneNumber = "+923262535009"; // Receiver's phone number
        public VideoStream()
        {
            InitializeComponent();
            _videoService = new VideoStreamService();
            _cancellationTokenSource = new CancellationTokenSource();
            
            // Initialize NotifyIcon (System Tray)
            InitializeNotifyIcon();
            Task.Run(() => CheckAlertsInBackground(_cancellationTokenSource.Token));
        }

        private void InitializeTimer()
        {
            alertTimer = new System.Windows.Forms.Timer();
            alertTimer.Interval = 10000; // 10 seconds
            alertTimer.Tick += alerttimer_Tick;  // Event handler for the Tick event
            alertTimer.Start();
        }

        // Initialize the NotifyIcon
        private void InitializeNotifyIcon()
        {   // Initialize NotifyIcon
            notifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Information,  // Set an icon (can be custom)
                Visible = true,                  // Make the icon visible in the system tray
                BalloonTipIcon = ToolTipIcon.Info, // Optionally set the icon type (Information, Warning, Error)
                BalloonTipTitle = "Security Alert",  // Set the default title for the BalloonTip
                BalloonTipText = "Waiting for alerts..." // Default message
            };

            // Register the BalloonTipClicked event to handle when the user clicks on the notification
            notifyIcon.BalloonTipClicked += NotifyIcon_BalloonTipClicked;
        }

        private void ShowNotification(string title, string message)
        {
            // Set the title and message of the balloon tip dynamically
            notifyIcon.BalloonTipTitle = title; // Set the title of the notification
            notifyIcon.BalloonTipText = message; // Set the text of the notification

            // Show the balloon tip with a 1-second duration
            notifyIcon.ShowBalloonTip(1000);
        }

        private void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            notification notification = new notification();
            notification.ShowDialog();
        }
            private  void VideoStream_Load(object sender, EventArgs e)
        {
            
        }

        private async  void VideoStream_FormClosing(object sender, FormClosingEventArgs e)
        {
            alertTimer?.Stop(); // Stop the timer
            await StopStreamAsync(); // Ensure resources are released
            notifyIcon?.Dispose(); // Dispose of system tray icon
        }

        private void commandtxtbox_TextChanged(object sender, EventArgs e)
        {

        }

  

        private async void scriptcode()
        {
            if (_pythonProcess != null && !_pythonProcess.HasExited)
            {
                MessageBox.Show("Python process is already running.");
                return;
            }

            try
            {
                // Define the path to the Python executable
                string pythonPath = "python.exe"; // Ensure python.exe is in the PATH, else provide full path

                // Define the path to the Python script and snapshot directory
                string scriptPath = @"E:\Profit Distribution\SecuirtyCam\Python\securitycam.py";
                string snapshotDirectory = @"E:\Profit Distribution\SecuirtyCam\SecuirtyCam\bin\Debug\snapshots";

                // Prepare arguments
                string arguments = $"\"{scriptPath}\" \"{snapshotDirectory}\"";

                // Setup the process start information
                ProcessStartInfo processStartInfo = new ProcessStartInfo()
                {
                    FileName = pythonPath,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    WorkingDirectory = @"E:\Profit Distribution\SecuirtyCam\SecuirtyCam\bin\Debug"
                };


          

                // Initialize the process
                _pythonProcess = new Process()
                {
                    StartInfo = processStartInfo,
                    EnableRaisingEvents = true
                };

                // Subscribe to Exited event
                _pythonProcess.Exited += (senderProc, argsProc) =>
                {
                    if (_pythonProcess.ExitCode == 0)
                    {
                        // Process exited successfully
                        MessageBox.Show("Python process exited successfully.", "Process Exited",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Process exited with error
                        MessageBox.Show($"Python process exited with code {_pythonProcess.ExitCode}.",
                            "Process Exited with Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                
               

                // Subscribe to output and error data received events
                _pythonProcess.OutputDataReceived += (senderProc, argsProc) =>
                {
                    if (!string.IsNullOrEmpty(argsProc.Data))
                    {
                        Console.WriteLine($"Python Output: {argsProc.Data}");
                    }
                };

                _pythonProcess.ErrorDataReceived += (senderProc, argsProc) =>
                {
                    if (!string.IsNullOrEmpty(argsProc.Data))
                    {
                        Console.WriteLine($"Python Error: {argsProc.Data}");
                    }
                };

                // Start the process
                _pythonProcess.Start();

                // Begin asynchronous read
                _pythonProcess.BeginOutputReadLine();
                _pythonProcess.BeginErrorReadLine();

                // Inform the user
                MessageBox.Show("Python process started successfully.", "Process Started",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Display error if something goes wrong
                MessageBox.Show($"Error running the command: {ex.Message}",
                                 "Error",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);
            }
        }


       



        private async void videostreamloadbutton_Click(object sender, EventArgs e)
        {
            try
            {
                // Start the Python script
                scriptcode();

                // Introduce a 10-second delay before proceeding to the next function
                await Task.Delay(10000);
                // Load the video stream
                InitializeTimer();
                await videostreamload();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading video stream: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private async Task videostreamload()
        {
            try
            {
                // Start fetching video stream and show in PictureBox
                await _videoService.StartVideoStream(pictureBox1, "http://127.0.0.1:5000/video_feed", _cancellationTokenSource.Token);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting video stream: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private async void stopbutton_Click(object sender, EventArgs e)
        {
            // Stop the alert timer
            alertTimer?.Stop();

            // Cancel the background alert check task
            _cancellationTokenSource.Cancel();

            // Stop the video stream and Python process
            await StopStreamAsync();

            // Clear the PictureBox image
            pictureBox1.Image = null;




        }





        private async Task StopStreamAsync()
        {
            try
            {
                // Send shutdown command to Flask server
                using (var httpClient = new HttpClient())
                {
                    var shutdownUri = new Uri("http://127.0.0.1:5000/shutdown");
                    HttpResponseMessage response = await httpClient.PostAsync(shutdownUri, null);

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"Failed to send shutdown command. Status code: {response.StatusCode}",
                                        "Error",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Shutdown command sent successfully.", "Shutdown",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // Wait for the Python process to exit gracefully
                if (_pythonProcess != null && !_pythonProcess.HasExited)
                {
                    // Wait asynchronously for up to 5 seconds for graceful shutdown
                    for (int i = 0; i < 5; i++)
                    {
                        if (_pythonProcess.HasExited)
                        {
                            Console.WriteLine("Python process terminated gracefully.");
                            break;
                        }
                        await Task.Delay(1000); // Wait for 1 second
                    }

                    // Forcefully kill the Python process if it hasn't exited
                    if (!_pythonProcess.HasExited)
                    {
                        MessageBox.Show("Python process did not exit gracefully. It will be forcefully terminated.", "Warning",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        try
                        {
                            _pythonProcess.Kill();
                            _pythonProcess.WaitForExit();
                            MessageBox.Show("Python process was forcefully terminated.", "Process Killed",
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error forcefully terminating Python process: {ex.Message}",
                                            "Error",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                        }
                    }
                }

                // Dispose resources
                _pythonProcess?.Dispose();
                _pythonProcess = null;

                // Reset cancellation token
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = new CancellationTokenSource();

                // Update UI
                Invoke((Action)(() =>
                {
                    videostreamloadbutton.Enabled = true;
                    stopbutton.Enabled = true;
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error stopping video stream: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }





        private void alerttimer_Tick(object sender, EventArgs e)
        {
            CheckForAlertsAsync();
            
        }

       





        // Dispose of the NotifyIcon when the form is closed
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (notifyIcon != null)
            {
                notifyIcon.Visible = false; // Hide the icon before closing
                notifyIcon.Dispose(); // Clean up the NotifyIcon
            }
        }


        private async Task CheckAlertsInBackground(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await CheckForAlertsAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error checking alerts: {ex.Message}");
                    break; // Exit the loop on any error
                }

                await Task.Delay(10000, token); // Respect the cancellation token during delay
            }
        }


        private async Task CheckForAlertsAsync()
        {


            if (_pythonProcess == null || _pythonProcess.HasExited)
            {
                Console.WriteLine("Skipping alert check because the server is stopped.");
                return;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync("http://127.0.0.1:5000/alert");
                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response from Flask server: {responseContent}");

                    var alertData = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseContent);

                    if (alertData.ContainsKey("alert") && (bool)alertData["alert"])
                    {
                        string alertMessage = alertData["message"].ToString();
                        string imagePath = alertData.ContainsKey("image_path") ? alertData["image_path"].ToString() : "";

                        SaveAlertToDatabase(alertMessage, imagePath);

                        // Show notification in system tray
                        notificationCount++;
                        ShowNotification($"Notification #{notificationCount}", alertMessage);

                        // Check if notification count equals 5, then send an email alert
                        if (notificationCount == 2)
                        {
                            SendEmailAlert(alertMessage,imagePath);
                            // SendSmsAlert(alertMessage);
                            //SendSmsAlert(alertMessage);
                            notificationCount = 0; // Reset counter after sending the alert
                        }

                        // Open alert form on UI thread
                      /*  this.Invoke((Action)(() =>
                        {
                            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                            {
                                var alertForm = new Alertform(alertMessage, imagePath);
                                alertForm.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("Snapshot image not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }));*/
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                //MessageBox.Show($"Server is down or unreachable. Error: {ex.Message}", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Server is down or unreachable. Error: {ex.Message}", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking for alerts: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void SaveAlertToDatabase(string message, string imagePath)
        {
            try
            {
                // Ensure the connection string is correct.
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    // Get the current timestamp with explicit formatting and invariant culture
                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

                    // Log the timestamp for debugging purposes
                    Console.WriteLine($"Timestamp: {timestamp}");

                    // SQL query to insert the alert into the database
                    string query = "INSERT INTO Alerts (Message, ImagePath, Timestamp) VALUES (@Message, @ImagePath, @Timestamp)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters to the SQL query
                        cmd.Parameters.AddWithValue("@Message", message);   // Message first
                        cmd.Parameters.AddWithValue("@ImagePath", imagePath); // ImagePath second
                        cmd.Parameters.AddWithValue("@Timestamp", timestamp); // Timestamp last

                        // Execute the insert command
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving alert to database: {ex.Message}");
            }
        }


        private void SendEmailAlert(string alertMessage, string imagePath)
        {
            try
            {
                // Set up the email client
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("rafaysabir0786@gmail.com", "hvuc bxnl mnqx ezyo"),
                    EnableSsl = true
                };

                // Create the email message
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("rafaysabir0786@gmail.com"),
                    Subject = "Security Alert Notification",
                    Body = $"Alert Message: {alertMessage}\n\nPlease check the attached snapshot for more details.",
                    IsBodyHtml = false
                };

                // Add recipient
                mailMessage.To.Add(receivermail);

                // Attach the image file if the file exists
                if (File.Exists(imagePath))
                {
                    Attachment imageAttachment = new Attachment(imagePath);
                    mailMessage.Attachments.Add(imageAttachment);
                }
                else
                {
                    MessageBox.Show("Image file not found. Sending email without attachment.", "Attachment Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Send the email
                smtpClient.Send(mailMessage);

                // Notify user in the UI
                MessageBox.Show("Email alert sent successfully with attachment.", "Email Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to send email alert: {ex.Message}", "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /*   private void SendEmailAlert(string alertMessage)
           {
               try
               {
                   // Set up the email client
                   SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                   {
                       Port = 587,
                       Credentials = new NetworkCredential("rafaysabir0786@gmail.com", "hvuc bxnl mnqx ezyo"),
                       EnableSsl = true
                   };

                   // Create the email message
                   MailMessage mailMessage = new MailMessage
                   {
                       From = new MailAddress("rafaysabir0786@gmail.com"),
                       Subject = "Security Alert Notification",
                       Body = $"Alert Message: {alertMessage}\n\nPlease check your system for more details.",
                       IsBodyHtml = false
                   };

                   // Add recipient
                   mailMessage.To.Add(receivermail);           
                   // Send the email
                   smtpClient.Send(mailMessage);

                   // Notify user in the UI
                   MessageBox.Show("Email alert sent successfully.", "Email Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
               }
               catch (Exception ex)
               {
                   MessageBox.Show($"Failed to send email alert: {ex.Message}", "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               }
           }*/



        private void SendSmsAlert(string alertMessage)
        {
            try
            {
                // Initialize Twilio client
                TwilioClient.Init(TwilioAccountSid, TwilioAuthToken);

                // Send SMS
                var message = MessageResource.Create(
                    to: new PhoneNumber(ReceiverPhoneNumber), // Receiver's phone number
                    from: new PhoneNumber(TwilioPhoneNumber), // Twilio phone number
                    body: $"Alert Message: {alertMessage}\n\nPlease check your system for more details."); // Alert message

                Console.WriteLine($"SMS sent: SID {message.Sid}");
                MessageBox.Show("SMS alert sent successfully.", "SMS Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to send SMS alert: {ex.Message}", "SMS Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
