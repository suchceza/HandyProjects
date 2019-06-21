using HandyTool.Components.BasePanels;
using HandyTool.Components.CustomPanels;
using HandyTool.Properties;

using System.Drawing;
using System.Windows.Forms;

namespace HandyTool.Components
{
    //todo: consider renaming of this class. it is not a tool, it is a command.
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

        public Panel[] ToolsetPanels { get; set; }

        public Panel WorkHourPanel { get; set; }

        #endregion

        internal void AddSubPanels()
        {
            ParentControl.Controls.AddRange(CurrencyPanels);
            ParentControl.Controls.AddRange(ToolsetPanels);
            //todo: consider to put work hour panel top of the tool panels
            ParentControl.Controls.Add(WorkHourPanel);
        }

        //################################################################################
        #region Protected Implementation

        protected sealed override void InitializeComponents()
        {
            Name = "ToolPanel";
            TabIndex = 0;
            TabStop = false;
            Size = new Size(ParentControl.Width - 2, 22);
            BackColor = Color.FromArgb(68, 68, 68);

            #region Currency Switch Label

            var isCurrencySwitchedOn = Settings.Default.CurrencySwitch;
            m_CurrencySwitchLabel = new ImageLabel(this, 2, "Show/Hide currency info", true, isCurrencySwitchedOn)
            {
                BackgroundImage = Resources.Money
            };

            m_CurrencySwitchLabel.Click += CurrencySwitch_Click;

            Controls.Add(m_CurrencySwitchLabel);

            #endregion

            #region Toolset Switch Label

            var isToolsetSwitchedOn = Settings.Default.ToolsetSwitch;
            m_ToolsetSwitchLabel = new ImageLabel(this, 2, "Show/Hide command tools", true, isToolsetSwitchedOn)
            {
                BackgroundImage = Resources.Tool
            };

            m_ToolsetSwitchLabel.Click += ToolsetSwitch_Click;

            Controls.Add(m_ToolsetSwitchLabel);

            #endregion

            #region WorkHour Switch Label

            var isWorkHourSwitchedOn = Settings.Default.WorkHourSwitch;
            m_WorkHourSwitchLabel = new ImageLabel(this, 2, "Show/Hide work hour info", true, isWorkHourSwitchedOn)
            {
                BackgroundImage = Resources.Time
            };

            m_WorkHourSwitchLabel.Click += WorkHourSwitch_Click;

            Controls.Add(m_WorkHourSwitchLabel);

            #endregion
        }

        #endregion

        //################################################################################
        #region Event Handler Methods

        private void CurrencySwitch_Click(object sender, System.EventArgs e)
        {
            foreach (var panel in CurrencyPanels)
            {
                panel.Visible = !panel.Visible;
            }

            Settings.Default.CurrencySwitch = !Settings.Default.CurrencySwitch;
            SaveSettingsAndResetForm();
        }

        private void ToolsetSwitch_Click(object sender, System.EventArgs e)
        {
            foreach (var panel in ToolsetPanels)
            {
                panel.Visible = !panel.Visible;
            }

            Settings.Default.ToolsetSwitch = !Settings.Default.ToolsetSwitch;
            SaveSettingsAndResetForm();
        }

        private void WorkHourSwitch_Click(object sender, System.EventArgs e)
        {
            WorkHourPanel.Visible = !WorkHourPanel.Visible;

            Settings.Default.WorkHourSwitch = !Settings.Default.WorkHourSwitch;
            SaveSettingsAndResetForm();
        }

        #endregion

        //################################################################################
        #region Private Implementation

        private void SaveSettingsAndResetForm()
        {
            Settings.Default.Save();

            ((MainAppForm)ParentControl).ResetPanelPositions();
            ((MainAppForm)ParentControl).SetFormHeight();
        }

        #endregion
    }
}
