using HandyTool.Commands;
using HandyTool.Components.BasePanels;
using HandyTool.Components.CustomPanels;
using HandyTool.Properties;
using HandyTool.Style;
using HandyTool.Style.Colors;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HandyTool.Components.WorkerPanels
{
    internal class CommandPanel : BackgroundWorkerPanel
    {
        //################################################################################
        #region Fields

        private readonly ICommand m_Command;
        private readonly string m_CommandName;

        private readonly Label m_BatchCommandLabel;
        private ImageLabel m_BatchCommandStartStopLabel;
        private ImageLabel m_BatchCommandSettingsLabel;

        #endregion

        //################################################################################
        #region Constructor

        public CommandPanel(ICommand command, Control parentControl, string commandName) : base(parentControl)
        {
            m_Command = command;
            m_CommandName = commandName;
            m_BatchCommandLabel = new Label();

            InitializeComponents();
            Paint += PaintBorder;
        }

        #endregion

        //################################################################################
        #region Protected Implementation

        protected override void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            m_Command.Execute();
        }

        protected override void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WriteLogsIfHappened(e.Error, $"Command[{m_CommandName}]", e.Cancelled, m_Command.Output);
        }

        protected sealed override void InitializeComponents()
        {
            Name = $@"CommandPanel_{m_CommandName}";
            TabIndex = 0;
            TabStop = false;
            Size = new Size(ParentControl.Width - 2, 22);
            Visible = Settings.Default.ToolsetSwitch;

            #region Process Label

            //Text Stuff
            m_BatchCommandLabel.Text = m_CommandName;
            m_BatchCommandLabel.Font = new Font(new FontFamily("Consolas"), 9, FontStyle.Bold);

            //Position/Size Stuff
            m_BatchCommandLabel.Location = new Point(2, 2);
            m_BatchCommandLabel.Size = new Size(141, 18);

            //Alignment Stuff
            m_BatchCommandLabel.Padding = new Padding(2);
            m_BatchCommandLabel.TextAlign = ContentAlignment.MiddleLeft;

            //Style Stuff
            Painter<Black>.Paint(m_BatchCommandLabel, PaintMode.Normal);

            //todo: open a context menu with different options when text label is clicked

            Controls.Add(m_BatchCommandLabel);

            #endregion

            #region Process Start/Stop

            m_BatchCommandStartStopLabel = new ImageLabel(this, 2, "Execute command")
            {
                BackgroundImage = Resources.RunProcess
            };

            //Event Stuff
            m_BatchCommandStartStopLabel.Click += RunProcess_Click;

            Controls.Add(m_BatchCommandStartStopLabel);

            #endregion

            #region Process Settings

            m_BatchCommandSettingsLabel = new ImageLabel(this, 2, "Command settings")
            {
                BackgroundImage = Resources.ProcessSettings
            };

            //Event Stuff
            m_BatchCommandSettingsLabel.Click += Settings_Click;

            Controls.Add(m_BatchCommandSettingsLabel);

            #endregion
        }

        #endregion

        //################################################################################
        #region Event Handler Methods

        private void RunProcess_Click(object sender, EventArgs e)
        {
            BackgroundWorker.RunWorkerAsync();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            //todo: implement settings options of CommandPanel
        }

        #endregion
    }
}
