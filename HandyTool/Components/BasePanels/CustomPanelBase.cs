using HandyTool.Logging;

using System.Drawing;
using System.Windows.Forms;

namespace HandyTool.Components.BasePanels
{
    internal abstract class CustomPanelBase : Panel
    {
        //################################################################################
        #region Constructor

        protected CustomPanelBase(Control parentControl)
        {
            ParentControl = parentControl;
        }

        #endregion

        //################################################################################
        #region Properties

        protected Control ParentControl { get; }

        protected LogWriter Logger => ((MainAppForm)ParentControl).Logger;

        #endregion

        //################################################################################
        #region Abstract Methods

        protected abstract void InitializeComponents();

        #endregion

        //################################################################################
        #region Protected Implementation

        protected void AddControl(Control controlToAdd, string tooltip = null)
        {
            int x = 1;

            foreach (Control control in Controls)
            {
                x += control.Width + 1;
            }

            controlToAdd.Location = new Point(x, 1);
            Controls.Add(controlToAdd);
        }

        protected void PaintBorder(object sender, PaintEventArgs e)
        {
            Rectangle border = new Rectangle(new Point(0, 0), new Size(Width - 1, Height - 1));
            CreateGraphics().DrawRectangle(new Pen(Color.White, 1), border);
        }

        protected void ShowBalloonTip(string title, string message, ToolTipIcon toolTipIcon)
        {
            ToolTipIcon icon = toolTipIcon;
            int timeout = 2000;

            BalloonTipDisplay.Show(title, message, icon, timeout);
        }

        #endregion
    }
}
