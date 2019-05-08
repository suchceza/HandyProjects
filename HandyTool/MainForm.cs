using HandyTool.Interfaces;
using HandyTool.RateSources;
using System.Linq;
using System.Windows.Forms;

namespace HandyTool
{
    public partial class MainForm : Form
    {
        private readonly IRateFetchService m_YahooService = new Yahoo();

        //################################################################################
        #region Constructor

        public MainForm()
        {
            InitializeComponent();
            SetFormHeight();
            SetFormPosition();
        }

        #endregion

        //################################################################################
        #region Private Implementation

        private void SetFormHeight()
        {
            var height = 0;
            foreach (var panel in Controls.OfType<Panel>())
            {
                height += panel.Height;
            }

            Height = height + 50;
        }

        private void SetFormPosition()
        {
            var marginRight = 30;
            var marginBottom = 60;
            var screenW = Screen.PrimaryScreen.Bounds.Width;
            var screenH = Screen.PrimaryScreen.Bounds.Height;

            Top = screenH - Height - marginBottom;
            Left = screenW - Width - marginRight;
        }

        #endregion
    }
}
