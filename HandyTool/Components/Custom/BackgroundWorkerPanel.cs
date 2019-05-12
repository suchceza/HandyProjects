using System.ComponentModel;

namespace HandyTool.Components.Custom
{
    internal abstract class BackgroundWorkerPanel : DraggablePanel
    {
        protected BackgroundWorkerPanel()
        {
            BackgroundWorker = new BackgroundWorker();
            InitializeBackgroundWorker();
        }

        protected BackgroundWorker BackgroundWorker { get; }

        protected abstract void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e);

        protected abstract void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e);

        private void InitializeBackgroundWorker()
        {
            BackgroundWorker.WorkerReportsProgress = false;
            BackgroundWorker.WorkerSupportsCancellation = false;

            BackgroundWorker.DoWork += BackgroundWorker_DoWork;
            BackgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
        }
    }
}
