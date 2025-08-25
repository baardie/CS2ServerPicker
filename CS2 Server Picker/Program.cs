using System.Security.Principal;

namespace CS2_Server_Picker
{
    /// <summary>
    /// Application entry point. Ensures elevation before launching UI.
    /// </summary>
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Initialize WinForms application settings (high DPI, visual styles, etc.)
            ApplicationConfiguration.Initialize();

            // Check for admin rights before proceeding
            if (!IsAdministrator())
            {
                MessageBox.Show(
                    "This program requires administrator privileges to manage Windows Firewall rules.",
                    "Elevation required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return; // Exit if not elevated
            }

            // Launch main UI
            Application.Run(new MainForm());
        }

        /// <summary>
        /// Determines whether the current process is running with administrator privileges.
        /// </summary>
        static bool IsAdministrator()
        {
            using var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
