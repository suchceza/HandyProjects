using HandyTool.Logging;

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace HandyTool.Components.BasePanels
{
    internal abstract class BackgroundWorkerPanel : CustomPanelBase
    {
        //################################################################################
        #region Constructor

        protected BackgroundWorkerPanel(Control parentControl) : base(parentControl)
        {
            BackgroundWorker = new BackgroundWorker();
            InitializeBackgroundWorker();
        }

        #endregion

        //################################################################################
        #region Properties

        protected BackgroundWorker BackgroundWorker { get; }

        #endregion

        //################################################################################
        #region Abstract Methods

        protected abstract void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e);

        protected abstract void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e);

        #endregion

        //################################################################################
        #region Protected Implementation

        protected void WriteLogsIfHappened(Exception exception, string actionName, bool isCancelled, string processData = null)
        {
            var logDateTime = $"{DateTime.Now.ToString("yyyy.MM.dd - HH:mm:ss.fffff")}";
            var logMessage = $"{logDateTime}\tExecuted Action: {actionName}";
            var mainLogItem = new LogData(logMessage);

            //-- log process data -------------------------------------------------------------------
            if (processData != null)
            {
                var subLogItemProcessData = new LogData(processData);
                mainLogItem.AddInnerLogItem(subLogItemProcessData);
            }

            //-- log exception ----------------------------------------------------------------------
            if (exception != null)
            {
                LogException(exception, mainLogItem);
            }

            //-- log cancellation --------------------------------------------------------------------
            if (isCancelled)
            {
                var subLogItemCancelled = new LogData("Action cancelled.");
                mainLogItem.AddInnerLogItem(subLogItemCancelled);
            }

            //todo: change null with log collection
            Logger.WriteLog(mainLogItem);
        }

        #endregion

        //################################################################################
        #region Private Implementation

        private void InitializeBackgroundWorker()
        {
            BackgroundWorker.WorkerReportsProgress = false;
            BackgroundWorker.WorkerSupportsCancellation = false;

            BackgroundWorker.DoWork += BackgroundWorker_DoWork;
            BackgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
        }

        private void LogException(Exception exception, LogData parentLogItem, bool isInnerException = false)
        {
            var logMessage = string.Empty;
            var messageHeader = isInnerException ? "Inner " : "";
            logMessage += $"{messageHeader}Exception Message: {exception.Message}\n";
            logMessage += $"{exception.StackTrace}";

            var childLogItem = new LogData(logMessage);
            parentLogItem.AddInnerLogItem(childLogItem);

            if (exception.InnerException != null)
            {
                LogException(exception.InnerException, childLogItem, true);
            }
        }

        #endregion
    }
}
