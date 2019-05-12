using HandyTool.Components.Custom;
using HandyTool.Properties;

using System.Drawing;
using System.Windows.Forms;

namespace HandyTool.Components
{
    class ToolbarPanel : Panel
    {
        //################################################################################
        #region Fields

        private readonly Control m_Parent;

        private Label m_RateActivateLabel;
        private Label m_ToolActivateLabel;
        private Label m_HourActivateLabel;

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
            Name = "ToolPanel";
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

            m_RateActivateLabel = new ImageLabel(this, 2, true)
            {
                BackgroundImage = Resources.Money,
                Size = new Size(18, 18)
            };

            Controls.Add(m_RateActivateLabel);

            #endregion

            #region Tool Activate Label

            m_ToolActivateLabel = new ImageLabel(this, 2, true)
            {
                BackgroundImage = Resources.Tool,
                Size = new Size(18, 18)
            };

            Controls.Add(m_ToolActivateLabel);

            #endregion

            #region Hour Activate Label

            m_HourActivateLabel = new ImageLabel(this, 2, true)
            {
                BackgroundImage = Resources.Time,
                Size = new Size(18, 18)
            };

            Controls.Add(m_HourActivateLabel);

            #endregion
        }

        private void PaintBorder(object sender, PaintEventArgs e)
        {
            Rectangle border = new Rectangle(new Point(0, 0), new Size(Width - 1, Height - 1));
            CreateGraphics().DrawRectangle(new Pen(Color.White, 1), border);
        }

        #endregion
    }
}
