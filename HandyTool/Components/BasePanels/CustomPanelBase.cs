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

        protected Logger Logger => ((MainAppForm)ParentControl).Logger;

        #endregion

        //################################################################################
        #region Abstract Methods

        protected abstract void InitializeComponents();

        #endregion

        //################################################################################
        #region Protected Implementation

        protected void PaintBorder(object sender, PaintEventArgs e)
        {
            Rectangle border = new Rectangle(new Point(0, 0), new Size(Width - 1, Height - 1));
            CreateGraphics().DrawRectangle(new Pen(Color.White, 1), border);
        }

        #endregion
    }
}
