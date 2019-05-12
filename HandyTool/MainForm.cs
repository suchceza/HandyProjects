using HandyTool.Components;
using HandyTool.Currency;

using System.Linq;
using System.Windows.Forms;

namespace HandyTool
{
    public partial class MainForm : Form
    {
        //################################################################################
        #region Constructor

        public MainForm()
        {
            InitializeComponent();
            InitializePanels();
            SetFormHeight();
            SetFormPosition();
        }

        #endregion

        //################################################################################
        #region Private Implementation

        private void InitializePanels()
        {
            var titlePanel = new TitlePanel(this);
            Controls.Add(titlePanel);

            var eurTryPanel = new CurrencyPanel(new EurTryCurrency(), this);
            Controls.Add(eurTryPanel);

            var eurUsdPanel = new CurrencyPanel(new EurUsdCurrency(), this, 1000);
            Controls.Add(eurUsdPanel);

            //var eurBgnPanel = new CurrencyPanel(new EurBgnCurrency(), this, 1000);
            //Controls.Add(eurBgnPanel);

            //var bgnTryPanel = new CurrencyPanel(new BgnTryCurrency(), this, 1000);
            //Controls.Add(bgnTryPanel);

            var hourPanel = new HourPanel(this);
            Controls.Add(hourPanel);

            var toolbarPanel = new ToolbarPanel(this);
            Controls.Add(toolbarPanel);
        }

        private void SetFormHeight()
        {
            var height = 0;
            foreach (var panel in Controls.OfType<Panel>())
            {
                height += panel.Height + 1;
            }

            Height = height + 1;
        }

        private void SetFormPosition()
        {
            StartPosition = FormStartPosition.CenterScreen;
            //var marginRight = 30;
            //var marginBottom = 60;
            //var screenW = Screen.PrimaryScreen.Bounds.Width;
            //var screenH = Screen.PrimaryScreen.Bounds.Height;

            //Top = screenH - Height - marginBottom;
            //Left = screenW - Width - marginRight;
        }

        #endregion
    }
}
