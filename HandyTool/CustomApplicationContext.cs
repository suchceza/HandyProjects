using HandyTool.Components;
using HandyTool.Properties;

using System;
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

#if DEBUG
            Bitmap icon = Resources.Tool;
#else
            Bitmap icon = Resources.Logo;
#endif

            ((MainAppForm)MainForm).CustomContextMenu = contextMenu;

            s_NotifyIcon = new NotifyIcon()
            {
                Icon = Icon.FromHandle(icon.GetHicon()),
                ContextMenu = contextMenu,
                Visible = true
            };

            s_NotifyIcon.DoubleClick += NotifyIcon_DoubleClick;
            s_NotifyIcon.MouseMove += NotifyIcon_MouseMouse;
        }

        private void NotifyIcon_MouseMouse(object sender, MouseEventArgs e)
        {
            string currencyInfos = string.Empty;
            string workHourInfo = string.Empty;

            foreach (var panel in MainForm.Controls)
            {
                if (panel is CurrencyPanel)
                {
                    currencyInfos += $"{((CurrencyPanel)panel).CurrencyName}: {((CurrencyPanel)panel).CurrentRateValue}\n";
                }

                if (panel is HourPanel)
                {
                    workHourInfo = $"Elapsed: {((HourPanel)panel).ElapsedTime}";
                }
            }

            s_NotifyIcon.Text = $@"{currencyInfos}{workHourInfo}";
        }

        #endregion

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            MainForm.Visible = !MainForm.Visible;
        }

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
