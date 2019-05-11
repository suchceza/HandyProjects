using HandyTool.Properties;

using System;
using System.Drawing;
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

            MainForm mainForm = new MainForm();
            Application.Run(new CustomApplicationContext(mainForm));
        }
    }

    public class CustomApplicationContext : ApplicationContext
    {
        private static NotifyIcon s_NotifyIcon;

        public CustomApplicationContext(Form mainForm)
        {
            MainForm = mainForm;
            Bitmap icon = Resources.Logo;

            s_NotifyIcon = new NotifyIcon()
            {
                Icon = Icon.FromHandle(icon.GetHicon()),
                ContextMenu = new ContextMenu(new[] { new MenuItem("Exit", Exit) }),
                Visible = true
            };
        }

        private void Exit(object sender, EventArgs args)
        {
            Exit();
        }

        public static void Exit()
        {
            s_NotifyIcon.Visible = false;
            Application.Exit();
        }
    }
}
