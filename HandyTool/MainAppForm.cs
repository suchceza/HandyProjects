using HandyTool.Components;
using HandyTool.Currency;

using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HandyTool
{
    public partial class MainAppForm : Form
    {
        //################################################################################
        #region Constructor

        public MainAppForm()
        {
            InitializeComponent();
            InitializePanels();
        }

        #endregion

        //################################################################################
        #region Properties

        public ContextMenu CustomContextMenu { get; set; }

        #endregion

        //################################################################################
        #region Event Implementation

        private void Form_Load(object sender, System.EventArgs e)
        {
            ResetPanelPositions();
            SetFormHeight();
            SetFormPosition();
            //Enabled = false;
        }

        #endregion

        //################################################################################
        #region Internal Implementation

        internal void SetFormHeight()
        {
            var height = 0;
            foreach (var panel in Controls.OfType<Panel>())
            {
                if (panel.Visible)
                {
                    height += panel.Height + 1;
                }
            }

            Height = height + 1;
        }

        internal void ResetPanelPositions()
        {
            int posY = 0;

            Control previousControl = null;
            foreach (Control control in Controls)
            {
                if (control.Visible)
                {
                    if (posY < 1)
                    {
                        posY = 1;
                    }
                    else
                    {
                        if (previousControl != null)
                            posY += 1 + previousControl.Height;
                    }

                    control.Location = new Point(1, posY);
                    previousControl = control;
                }
            }
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

            var eurUsdPanel = new CurrencyPanel(new EurUsdCurrency(), this, 5000);
            Controls.Add(eurUsdPanel);

            //var eurBgnPanel = new CurrencyPanel(new EurBgnCurrency(), this, 10000);
            //Controls.Add(eurBgnPanel);

            //var bgnTryPanel = new CurrencyPanel(new BgnTryCurrency(), this, 10000);
            //Controls.Add(bgnTryPanel);

            var hourPanel = new HourPanel(this);
            Controls.Add(hourPanel);

            var toolbarPanel = new ToolbarPanel(this);
            toolbarPanel.CurrencyPanels = new Panel[] { eurTryPanel, eurUsdPanel/*, eurBgnPanel, bgnTryPanel*/ };
            toolbarPanel.WorkHourPanel = hourPanel;
            toolbarPanel.ToolsetPanel = null;
            Controls.Add(toolbarPanel);
        }

        private void SetFormPosition()
        {
            //StartPosition = FormStartPosition.CenterScreen;
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
