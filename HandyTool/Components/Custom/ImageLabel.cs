using System;
using System.Drawing;
using System.Windows.Forms;

namespace HandyTool.Components.Custom
{
    internal sealed class ImageLabel : Label
    {
        //################################################################################
        #region Fields

        private readonly Control m_Parent;
        private readonly int m_Origin;
        private readonly bool m_IsSwitchable;

        private bool m_IsSwitchOn;
        private readonly Color m_SwitchOnColor = Color.FromArgb(255, 255, 255);
        private readonly Color m_SwitchOffColor = Color.FromArgb(68, 68, 68);

        #endregion

        //################################################################################
        #region Constructor

        public ImageLabel(Control parent, int origin, string tooltip, bool isSwitchable = false, bool isSwitchedOn = false)
        {
            m_Parent = parent;
            m_Origin = origin;
            m_IsSwitchable = isSwitchable;
            m_IsSwitchOn = isSwitchedOn;

            var toolTip = new ToolTip();
            toolTip.SetToolTip(this, tooltip);

            InitializeComponent();
        }

        #endregion

        //################################################################################
        #region Private Implementation

        private void InitializeComponent()
        {
            //Adjust style
            BackgroundImageLayout = ImageLayout.Center;
            BackColor = !m_IsSwitchOn && m_IsSwitchable ? m_SwitchOffColor : m_SwitchOnColor;
            Cursor = Cursors.Hand;

            //Adjust position and size
            Size = new Size(18, 18);
            Location = SetLocation();

            //Adjust events
            Click += OnClick;
        }

        private Point SetLocation()
        {
            int x = m_Origin;

            foreach (Control control in m_Parent.Controls)
            {
                x += 1 + control.Width;
            }

            return new Point(x, m_Origin);
        }

        private void OnClick(object sender, EventArgs e)
        {
            if (m_IsSwitchable)
            {
                m_IsSwitchOn = !m_IsSwitchOn;
                BackColor = m_IsSwitchOn ? m_SwitchOnColor : m_SwitchOffColor;
            }
        }

        #endregion
    }
}
