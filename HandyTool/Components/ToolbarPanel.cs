using HandyTool.Components.Custom;
using HandyTool.Properties;

using System.Drawing;
using System.Windows.Forms;

namespace HandyTool.Components
{
    class ToolbarPanel : CustomPanelBase
    {
        //################################################################################
        #region Fields

        private Label m_CurrencySwitchLabel;
        private Label m_ToolsetSwitchLabel;
        private ImageLabel m_WorkHourSwitchLabel;

        #endregion

        //################################################################################
        #region Constructor

        public ToolbarPanel(Control parentControl) : base(parentControl)
        {
            InitializeComponents();
            Paint += PaintBorder;
        }

        #endregion

        //################################################################################
        #region Properties

        public Panel[] CurrencyPanels { get; set; }

        public Panel ToolsetPanel { get; set; }

        public Panel WorkHourPanel { get; set; }

        #endregion

        //################################################################################
        #region Protected Implementation

        protected sealed override void InitializeComponents()
        {
            Name = "ToolPanel";
            TabIndex = 0;
            TabStop = false;
            Size = new Size(ParentControl.Width - 2, 22);

            #region Currency Switch Label

            var isCurrencySwitchedOn = Settings.Default.CurrencySwitch;
            m_CurrencySwitchLabel = new ImageLabel(this, 2, true, isCurrencySwitchedOn)
            {
                BackgroundImage = Resources.Money,
                Size = new Size(18, 18)
            };

            m_CurrencySwitchLabel.Click += CurrencySwitchLabel_Click;

            Controls.Add(m_CurrencySwitchLabel);

            #endregion

            #region Toolset Switch Label

            m_ToolsetSwitchLabel = new ImageLabel(this, 2, true)
            {
                BackgroundImage = Resources.Tool,
                Size = new Size(18, 18)
            };

            Controls.Add(m_ToolsetSwitchLabel);

            #endregion

            #region WorkHour Switch Label

            var isWorkHourSwitchedOn = Settings.Default.WorkHourSwitch;
            m_WorkHourSwitchLabel = new ImageLabel(this, 2, true, isWorkHourSwitchedOn)
            {
                BackgroundImage = Resources.Time,
                Size = new Size(18, 18)
            };

            m_WorkHourSwitchLabel.Click += HourActivateLabel_Click;

            Controls.Add(m_WorkHourSwitchLabel);

            #endregion
        }

        #endregion

        //################################################################################
        #region Private Implementation

        private void CurrencySwitchLabel_Click(object sender, System.EventArgs e)
        {
            foreach (var panel in CurrencyPanels)
            {
                panel.Visible = !panel.Visible;
            }

            Settings.Default.CurrencySwitch = !Settings.Default.CurrencySwitch;
            Settings.Default.Save();

            ((MainAppForm)ParentControl).ResetPanelPositions();
            ((MainAppForm)ParentControl).SetFormHeight();
        }

        private void HourActivateLabel_Click(object sender, System.EventArgs e)
        {
            WorkHourPanel.Visible = !WorkHourPanel.Visible;

            Settings.Default.WorkHourSwitch = !Settings.Default.WorkHourSwitch;
            Settings.Default.Save();

            ((MainAppForm)ParentControl).ResetPanelPositions();
            ((MainAppForm)ParentControl).SetFormHeight();
        }

        #endregion
    }
}
