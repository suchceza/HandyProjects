using HandyTool.Components.Custom;

using System.Drawing;
using System.Windows.Forms;

namespace HandyTool.Components
{
    internal class AboutPanel : CustomPanelBase
    {
        //################################################################################
        #region Fields

        #endregion

        //################################################################################
        #region Constructor

        public AboutPanel(Control parentControl) : base(parentControl)
        {
            InitializeComponents();
        }

        #endregion

        //################################################################################
        #region Protected Implementation

        protected sealed override void InitializeComponents()
        {
            Name = "AboutPanel";
            TabIndex = 0;
            TabStop = false;
            Size = new Size(ParentControl.Width - 2, ParentControl.Width - 2);
            Location = new Point(0, 0);
            BackColor = Color.Yellow;
            Visible = false;
        }

        #endregion
    }
}
