using HandyTool.Properties;

using System.Drawing;
using System.Windows.Forms;

namespace HandyTool
{
    public class CustomApplicationContext : ApplicationContext
    {
        //################################################################################
        #region Fields

        private static NotifyIcon s_NotifyIcon;

        #endregion

        //################################################################################
        #region Constructor

        public CustomApplicationContext(Form mainForm)
        {
            MainForm = mainForm;
            CustomContextMenu contextMenu = new CustomContextMenu(MainForm);
            Bitmap icon = Resources.Logo;

            ((MainAppForm)MainForm).CustomContextMenu = contextMenu;

            s_NotifyIcon = new NotifyIcon()
            {
                Icon = Icon.FromHandle(icon.GetHicon()),
                ContextMenu = contextMenu,
                Visible = true
            };
        }

        #endregion

        //################################################################################
        #region Internal Implementation

        internal static void Exit()
        {
            s_NotifyIcon.Visible = false;
            Application.Exit();
        }

        #endregion
    }
}
