using HandyTool.Properties;

using System;
using System.Windows.Forms;

namespace HandyTool
{
    internal class CustomContextMenu : ContextMenu
    {
        private readonly Form m_MainForm;

        private readonly MenuItem m_Transparency;
        private readonly MenuItem m_AlwaysOnTop;
        private readonly MenuItem m_ShowHide;
        private readonly MenuItem m_Exit;

        public CustomContextMenu(Form mainForm)
        {
            m_MainForm = mainForm;
            m_Transparency = new MenuItem("Transparency", TransparencyOptions());
            m_AlwaysOnTop = new MenuItem("Always on top", AlwaysOnTop);
            m_ShowHide = new MenuItem("Show/Hide", ShowHide);
            m_Exit = new MenuItem("Exit", Exit);

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            m_AlwaysOnTop.Checked = Settings.Default.AlwaysOnTop;
            m_ShowHide.Text = @"Hide";

            MenuItems.Add(m_Transparency);
            MenuItems.Add(m_AlwaysOnTop);
            MenuItems.Add(m_ShowHide);
            MenuItems.Add(m_Exit);
        }

        private MenuItem[] TransparencyOptions()
        {
            var options = new MenuItem[11];

            for (int i = 0; i <= 10; i++)
            {
                options[i] = new MenuItem($"%{i * 10}", SetTransparency);
            }

            return options;
        }

        private void SetTransparency(object sender, EventArgs e)
        {
            m_MainForm.Opacity = ((MenuItem)sender).Index / 10D;
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

            m_ShowHide.Text = m_MainForm.Visible ? @"Hide" : @"Show";
        }

        private void Exit(object sender, EventArgs args)
        {
            CustomApplicationContext.Exit();
        }
    }
}
