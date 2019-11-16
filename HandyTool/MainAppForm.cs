using HandyTool.Commands;
using HandyTool.Components.CustomPanels;
using HandyTool.Components.WorkerPanels;
using HandyTool.Currency.Services;
using HandyTool.Logging;
using HandyTool.Properties;

using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HandyTool
{
    public partial class MainAppForm : Form
    {
        //################################################################################
        #region Fields

        private AboutPanel m_AboutPanel;
        private TitlePanel m_TitlePanel;
        private CurrencyPanel m_EurTryPanel;
        private CurrencyPanel m_UsdTryPanel;
        private CurrencyPanel m_EurUsdPanel;
        private CommandPanel m_ProcessExecuter;
        private CommandPanel m_ProcessKiller;
        private HourPanel m_WorkHour;
        private ToolbarPanel m_Toolbar;

        #endregion

        //################################################################################
        #region Constructor

        public MainAppForm()
        {
            Logger = new LogWriter();

            InitializeComponent();
            InitializePanels();
        }

        #endregion

        //################################################################################
        #region Properties

        internal LogWriter Logger { get; }

        #endregion

        //################################################################################
        #region Internal Implementation

        internal void SaveSettingsAndResetForm()
        {
            Settings.Default.Save();

            ResetPanelPositions();
            SetFormHeight();
        }

        #endregion

        //################################################################################
        #region Event Handler Implementation

        private void Form_Load(object sender, System.EventArgs e)
        {
            ResetPanelPositions();
            SetFormHeight();
            SetFormPosition();

            Opacity = Settings.Default.Opacity;
        }

        private void CurrencySwitch_Click(object sender, System.EventArgs e)
        {
            m_EurTryPanel.Visible = !m_EurTryPanel.Visible;
            m_EurUsdPanel.Visible = !m_EurUsdPanel.Visible;

            Settings.Default.CurrencySwitch = !Settings.Default.CurrencySwitch;
            SaveSettingsAndResetForm();
        }

        private void ToolsetSwitch_Click(object sender, System.EventArgs e)
        {
            m_ProcessExecuter.Visible = !m_ProcessExecuter.Visible;
            m_ProcessKiller.Visible = !m_ProcessKiller.Visible;

            Settings.Default.ToolsetSwitch = !Settings.Default.ToolsetSwitch;
            SaveSettingsAndResetForm();
        }

        private void WorkHourSwitch_Click(object sender, System.EventArgs e)
        {
            m_WorkHour.Visible = !m_WorkHour.Visible;

            Settings.Default.WorkHourSwitch = !Settings.Default.WorkHourSwitch;
            SaveSettingsAndResetForm();
        }

        #endregion

        //################################################################################
        #region Private Implementation

        private void InitializePanels()
        {
            bgPanel = new Panel();
            bgPanel.Name = "xxx";
            bgPanel.Width = 185;
            bgPanel.BackColor = Color.Black;
            bgPanel.Location = new Point(0, 0);
            bgPanel.SendToBack();
            bgPanel.Visible = true;

            // todo: consider creating group panel for currencies and tools

            //-- About Panel -------------------------------------------------------------
            m_AboutPanel = new AboutPanel(this);

            //-- Title Panel -------------------------------------------------------------
            m_TitlePanel = new TitlePanel(this);

            //-- Currency Panels ---------------------------------------------------------
            m_EurTryPanel = new CurrencyPanel(Yahoo.EurTry, this);
            m_UsdTryPanel = new CurrencyPanel(Yahoo.UsdTry, this);
            m_EurUsdPanel = new CurrencyPanel(Yahoo.EurUsd, this, 5000);

            //-- Process Panels ----------------------------------------------------------
            m_ProcessExecuter = new CommandPanel(new ProcessStarter(), this, "Start Process");
            m_ProcessKiller = new CommandPanel(new ProcessKiller(), this, "Kill TIA Processes");

            //-- WorkHour Panel ----------------------------------------------------------
            m_WorkHour = new HourPanel(this);

            //-- Toolbar Panel -----------------------------------------------------------
            m_Toolbar = new ToolbarPanel(this);
            m_Toolbar.CurrencySwitch.Click += CurrencySwitch_Click;
            m_Toolbar.WorkHourSwitch.Click += WorkHourSwitch_Click;
            m_Toolbar.ToolsetSwitch.Click += ToolsetSwitch_Click;

            //-- Add Panels in order -----------------------------------------------------
            Controls.Add(m_AboutPanel);

            Controls.Add(m_TitlePanel);
            Controls.Add(m_Toolbar);
            Controls.Add(m_WorkHour);
            Controls.Add(m_EurTryPanel);
            Controls.Add(m_UsdTryPanel);
            Controls.Add(m_EurUsdPanel);
            Controls.Add(m_ProcessExecuter);
            Controls.Add(m_ProcessKiller);

            Controls.Add(bgPanel);
        }

        Panel bgPanel;

        private void SetFormHeight()
        {
            var height = 0;
            foreach (var panel in Controls.OfType<Panel>())
            {
                if (panel.Visible && !panel.Name.Equals("xxx"))
                {
                    height += panel.Height + 1;
                }
            }

            Height = height + 1;
            bgPanel.Height = Height;
        }

        private void ResetPanelPositions()
        {
            int posY = 0;

            Control previousControl = null;
            foreach (Control control in Controls)
            {
                if (control.Visible && !control.Name.Equals("xxx"))
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

        private void SetFormPosition()
        {
#if DEBUG == false
            var marginRight = 30;
            var marginBottom = 60;
            var screenW = Screen.PrimaryScreen.Bounds.Width;
            var screenH = Screen.PrimaryScreen.Bounds.Height;

            Top = screenH - Height - marginBottom;
            Left = screenW - Width - marginRight;
#endif
        }

        #endregion
    }
}
