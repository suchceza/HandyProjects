using System.ComponentModel;
using System.Windows.Forms;

namespace HandyTool.Components.Custom
{
    internal abstract class BackgroundWorkerPanel : DraggablePanel
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
        #region Private Implementation

        private void InitializeBackgroundWorker()
        {
            BackgroundWorker.WorkerReportsProgress = false;
            BackgroundWorker.WorkerSupportsCancellation = false;

            BackgroundWorker.DoWork += BackgroundWorker_DoWork;
            BackgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
        }

        #endregion
    }
}
