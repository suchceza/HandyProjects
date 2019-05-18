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
            Location = new Point(1, 1);
            BackColor = Color.Yellow;
            Visible = false;
            VisibleChanged += AboutPanel_VisibleChanged;
            Click += AboutPanel_Click;
        }

        private void AboutPanel_Click(object sender, System.EventArgs e)
        {
            Visible = false;
        }

        private void AboutPanel_VisibleChanged(object sender, System.EventArgs e)
        {
            Size = new Size(ParentControl.Width - 2, ParentControl.Height - 2);
        }

        #endregion
    }
}
