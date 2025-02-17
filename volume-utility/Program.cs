using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace volume_utility
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Detect the OS language and set the application's culture accordingly
            var osCulture = CultureInfo.InstalledUICulture;
            if (osCulture.Name.StartsWith("ja"))
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("ja-JP");
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            }

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.Run(new Main());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Debug.WriteLine(e);
        }
    }
}
