using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using HospitalManagement.UI;
using HospitalManagement.Utils;

namespace HospitalManagement
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Set Vietnamese culture for date format (dd/MM/yyyy)
            CultureInfo vietnameseCulture = new CultureInfo("vi-VN");
            Thread.CurrentThread.CurrentCulture = vietnameseCulture;
            Thread.CurrentThread.CurrentUICulture = vietnameseCulture;
            Application.CurrentCulture = vietnameseCulture;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Initialize global DevExpress theming
            AppTheme.Initialize();
            
            // Ensure DB schema is up to date
            HospitalManagement.Utils.DatabaseInitializer.Initialize();

            // Self-Protection: Encrypt connection string if needed
            HospitalManagement.Utils.ConfigHelper.EncryptConnectionString();

            Application.Run(new LoginForm());
        }
    }
}
