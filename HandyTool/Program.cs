using System;
using System.Windows.Forms;

namespace HandyTool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainAppForm mainForm = new MainAppForm();
            Application.Run(new CustomApplicationContext(mainForm));
        }
    }
}
