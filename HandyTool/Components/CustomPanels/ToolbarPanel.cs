using HandyTool.Components.BasePanels;
using HandyTool.Properties;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace HandyTool.Components.CustomPanels
{
    class ToolbarPanel : CustomPanelBase
    {
        //################################################################################
        #region Fields

        private ImageButton m_CurrencySwitchLabel;
        private ImageButton m_ToolsetSwitchLabel;
        private ImageButton m_WorkHourSwitchLabel;

        #endregion

        //################################################################################
        #region Constructor

        public ToolbarPanel(Control parentControl) : base(parentControl)
        {
            InitializeComponents();
            Paint += PaintBorder;

            //Click += Panel_Click;
        }

        #endregion

        //################################################################################
        #region Properties

        public Label CurrencySwitch => m_CurrencySwitchLabel;

        public Label WorkHourSwitch => m_WorkHourSwitchLabel;

        public Label ToolsetSwitch => m_ToolsetSwitchLabel;

        #endregion

        //################################################################################
        #region Protected Implementation

        protected sealed override void InitializeComponents()
        {
            Name = "ToolPanel";
            TabIndex = 0;
            TabStop = false;
            Size = new Size(185 - 2, 22);
            BackColor = Color.FromArgb(68, 68, 68);

            #region Currency Switch Label

            var isCurrencySwitchedOn = Settings.Default.CurrencySwitch;
            m_CurrencySwitchLabel = new ImageButton(this, "Show/Hide currency info", true, isCurrencySwitchedOn)
            {
                BackgroundImage = Resources.Money
            };

            Controls.Add(m_CurrencySwitchLabel);

            #endregion

            #region Toolset Switch Label

            var isToolsetSwitchedOn = Settings.Default.ToolsetSwitch;
            m_ToolsetSwitchLabel = new ImageButton(this, "Show/Hide command tools", true, isToolsetSwitchedOn)
            {
                BackgroundImage = Resources.Tool
            };

            Controls.Add(m_ToolsetSwitchLabel);

            #endregion

            #region WorkHour Switch Label

            var isWorkHourSwitchedOn = Settings.Default.WorkHourSwitch;
            m_WorkHourSwitchLabel = new ImageButton(this, "Show/Hide work hour info", true, isWorkHourSwitchedOn)
            {
                BackgroundImage = Resources.Time
            };

            Controls.Add(m_WorkHourSwitchLabel);

            #endregion
        }

        #endregion

        private bool isSlide = false;

        private void Panel_Click(object sender, EventArgs e)
        {
            if (!isSlide)
            {
                for (int i = Location.X; i < 100; i++)
                {
                    Location = new Point(i, Location.Y);
                    Thread.Sleep(1);
                }

                isSlide = true;
            }
            else
            {
                for (int i = Location.X; i >= 1; i--)
                {
                    Location = new Point(i, Location.Y);
                    Thread.Sleep(1);
                }

                isSlide = false;
            }
        }
    }
}
