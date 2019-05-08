using HandyTool.Properties;
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

            MainForm mainForm = new MainForm();
            Application.Run(new CustomApplicationContext(mainForm));
        }
    }

    public class CustomApplicationContext : ApplicationContext
    {
        private NotifyIcon m_NotifyIcon;

        public CustomApplicationContext(Form mainForm)
        {
            MainForm = mainForm;
            m_NotifyIcon = new NotifyIcon()
            {
                Icon = Resources.AppIcon,
                ContextMenu = new ContextMenu(new MenuItem[] { new MenuItem("Exit", Exit)}),
                Visible = true
            };
        }

        private void Exit(object sender, EventArgs args)
        {
            m_NotifyIcon.Visible = false;
            Application.Exit();
        }
    }
}
