using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecuirtyCam
{


    public static class ErrorHandlers
    {
        public static void LogError(Exception ex)
        {
            // Log the error to a file or console
            Console.WriteLine($"Error: {ex.Message}");
        }

        public static void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

}
