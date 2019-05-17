using HandyTool.Properties;

using System;
using System.Windows.Forms;

namespace HandyTool.Components
{
    internal class CustomContextMenu : ContextMenu
    {
        //################################################################################
        #region Fields

        private readonly Form m_MainForm;

        private readonly MenuItem m_Transparency;
        private readonly MenuItem m_AlwaysOnTop;
        private readonly MenuItem m_ShowHide;
        private readonly MenuItem m_Logs;
        private readonly MenuItem m_Settings;
        private readonly MenuItem m_About;
        private readonly MenuItem m_Exit;

        #endregion

        //################################################################################
        #region Constructor

        public CustomContextMenu(Form mainForm)
        {
            m_MainForm = mainForm;
            m_Transparency = new MenuItem("Opacity", OpacityOptions());
            m_AlwaysOnTop = new MenuItem("Always on top", AlwaysOnTop);
            m_ShowHide = new MenuItem("Show/Hide", ShowHide);
            m_Logs = new MenuItem("Logs", new[]
            {
                new MenuItem("Show Logs", ShowLogs),
                new MenuItem("Clear Logs", ClearLogs)
            });
            m_Settings = new MenuItem("Settings", AppSettings);
            m_About = new MenuItem("About", About);
            m_Exit = new MenuItem("Exit", Exit);

            InitializeComponent();
        }

        #endregion

        //################################################################################
        #region Private Implementation

        private void InitializeComponent()
        {
            SetOpacityCheckedStatus();
            m_AlwaysOnTop.Checked = Settings.Default.AlwaysOnTop;
            m_ShowHide.Text = @"Hide";

            m_MainForm.VisibleChanged += MainForm_VisibleChanged;

            MenuItems.Add(m_Transparency);
            MenuItems.Add(m_AlwaysOnTop);
            MenuItems.Add(m_ShowHide);
            MenuItems.Add("-");
            MenuItems.Add(m_Logs);
            MenuItems.Add("-");
            MenuItems.Add(m_Settings);
            MenuItems.Add(m_About);
            MenuItems.Add("-");
            MenuItems.Add(m_Exit);
        }

        private MenuItem[] OpacityOptions()
        {
            var options = new MenuItem[11];

            for (int i = 0; i <= 10; i++)
            {
                options[i] = new MenuItem($"%{i * 10}", SetOpacity);
                options[i].Tag = i * 10;
            }

            return options;
        }

        private void SetOpacityCheckedStatus()
        {
            var opacitySetting = Settings.Default.Opacity;
            foreach (MenuItem opacityOption in m_Transparency.MenuItems)
            {
                opacityOption.Checked = false;

                if ((int)opacityOption.Tag / 100D == opacitySetting)
                {
                    opacityOption.Checked = true;
                }
            }
        }

        #endregion

        //################################################################################
        #region Event Handler Methods

        private void SetOpacity(object sender, EventArgs e)
        {
            if (sender is MenuItem menuItem)
            {
                double opacity = menuItem.Index / 10D;
                m_MainForm.Opacity = opacity;

                Settings.Default.Opacity = opacity;
            }

            Settings.Default.Save();

            SetOpacityCheckedStatus();
        }

        private void AlwaysOnTop(object sender, EventArgs args)
        {
            m_MainForm.TopMost = !m_MainForm.TopMost;
            m_AlwaysOnTop.Checked = m_MainForm.TopMost;

            Settings.Default.AlwaysOnTop = m_MainForm.TopMost;
            Settings.Default.Save();
        }

        private void ShowHide(object sender, EventArgs args)
        {
            m_MainForm.Visible = !m_MainForm.Visible;
        }

        private void ShowLogs(object sender, EventArgs args)
        {
            ((MainAppForm)m_MainForm).Logger.ShowLogs();
        }

        private void ClearLogs(object sender, EventArgs args)
        {
            ((MainAppForm)m_MainForm).Logger.ClearLogs();
        }

        private void AppSettings(object sender, EventArgs args)
        {
            //todo: Implement ContextMenu Settings menu
        }

        private void About(object sender, EventArgs args)
        {
            foreach (var panel in m_MainForm.Controls)
            {
                if (panel is AboutPanel)
                {
                    var aboutPanel = (Panel)panel;
                    aboutPanel.Visible = !aboutPanel.Visible;
                    break;
                }
            }
        }

        private void Exit(object sender, EventArgs args)
        {
            CustomApplicationContext.Exit();
        }

        private void MainForm_VisibleChanged(object sender, EventArgs e)
        {
            m_ShowHide.Text = m_MainForm.Visible ? @"Hide" : @"Show";
        }

        #endregion
    }
}
