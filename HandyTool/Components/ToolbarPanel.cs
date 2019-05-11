using HandyTool.Properties;

using System.Drawing;
using System.Windows.Forms;

namespace HandyTool.Components
{
    class ToolbarPanel : Panel
    {
        //################################################################################
        #region Fields

        private const int c_Padding = 1;

        private readonly Control m_Parent;

        private readonly Label m_RateActivateLabel = new Label();
        private readonly Label m_ToolActivateLabel = new Label();
        private readonly Label m_HourActivateLabel = new Label();

        private bool m_IsRateActive = true;
        private bool m_IsToolActive = true;
        private bool m_IsHourActive = true;

        #endregion

        //################################################################################
        #region Constructor

        public ToolbarPanel(Control parent)
        {
            m_Parent = parent;

            SetPanelPosition();

            InitializeComponents();
            Paint += PaintBorder;
        }

        #endregion

        //################################################################################
        #region Private Implementation

        private void SetPanelPosition()
        {
            Name = "CurrencyPanel";
            TabIndex = 0;
            TabStop = false;

            int posY = 1;

            for (int i = 0; i < m_Parent.Controls.Count; i++)
            {
                posY += 1 + m_Parent.Controls[i].Height;
            }

            Location = new Point(1, posY);
            Size = new Size(m_Parent.Width - 2, 22);
        }

        private void InitializeComponents()
        {
            #region Rate Activate Label

            //Position/Size Stuff
            m_RateActivateLabel.Location = new Point(2, 2);
            m_RateActivateLabel.Size = new Size(18, 18);

            //Alignment Stuff
            m_RateActivateLabel.Padding = new Padding(c_Padding);
            m_RateActivateLabel.TextAlign = ContentAlignment.MiddleCenter;

            //Style Stuff
            m_RateActivateLabel.BackgroundImage = Resources.Money;
            m_RateActivateLabel.BackgroundImageLayout = ImageLayout.Center;
            m_RateActivateLabel.BackColor = Color.FromArgb(255, 255, 255);
            m_RateActivateLabel.Cursor = Cursors.Hand;

            //Event Stuff
            m_RateActivateLabel.Click += RateActivateLabel_Click;

            Controls.Add(m_RateActivateLabel);

            #endregion

            #region Tool Activate Label

            //Position/Size Stuff
            m_ToolActivateLabel.Location = new Point(m_RateActivateLabel.Width + 3, 2);
            m_ToolActivateLabel.Size = new Size(18, 18);

            //Alignment Stuff
            m_ToolActivateLabel.Padding = new Padding(c_Padding);
            m_ToolActivateLabel.TextAlign = ContentAlignment.MiddleCenter;

            //Style Stuff
            m_ToolActivateLabel.BackgroundImage = Resources.Tool;
            m_ToolActivateLabel.BackgroundImageLayout = ImageLayout.Center;
            m_ToolActivateLabel.BackColor = Color.FromArgb(255, 255, 255);
            m_ToolActivateLabel.Cursor = Cursors.Hand;

            //Event Stuff
            m_ToolActivateLabel.Click += ToolActivateLabel_Click;

            Controls.Add(m_ToolActivateLabel);

            #endregion

            #region Hour Activate Label

            //Position/Size Stuff
            m_HourActivateLabel.Location = new Point(m_RateActivateLabel.Width + m_ToolActivateLabel.Width + 4, 2);
            m_HourActivateLabel.Size = new Size(18, 18);

            //Alignment Stuff
            m_HourActivateLabel.Padding = new Padding(c_Padding);
            m_HourActivateLabel.TextAlign = ContentAlignment.MiddleCenter;

            //Style Stuff
            m_HourActivateLabel.BackgroundImage = Resources.Time;
            m_HourActivateLabel.BackgroundImageLayout = ImageLayout.Center;
            m_HourActivateLabel.BackColor = Color.FromArgb(255, 255, 255);
            m_HourActivateLabel.Cursor = Cursors.Hand;

            //Event Stuff
            m_HourActivateLabel.Click += HourActivateLabel_Click;

            Controls.Add(m_HourActivateLabel);

            #endregion
        }

        private void HourActivateLabel_Click(object sender, System.EventArgs e)
        {
            m_IsHourActive = !m_IsHourActive;
            SwitchOnOff(m_HourActivateLabel, m_IsHourActive);
        }

        private void ToolActivateLabel_Click(object sender, System.EventArgs e)
        {
            m_IsToolActive = !m_IsToolActive;
            SwitchOnOff(m_ToolActivateLabel, m_IsToolActive);
        }

        private void RateActivateLabel_Click(object sender, System.EventArgs e)
        {
            m_IsRateActive = !m_IsRateActive;
            SwitchOnOff(m_RateActivateLabel, m_IsRateActive);
        }

        private void SwitchOnOff(Control control, bool switchKey)
        {
            control.BackColor = switchKey ? Color.White : Color.Black;
        }

        private void PaintBorder(object sender, PaintEventArgs e)
        {
            Rectangle border = new Rectangle(new Point(0, 0), new Size(Width - 1, Height - 1));
            CreateGraphics().DrawRectangle(new Pen(Color.White, 1), border);
        }

        #endregion
    }
}
