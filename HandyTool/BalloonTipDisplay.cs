using System.Windows.Forms;

namespace HandyTool
{
    internal class BalloonTipDisplay
    {
        public BalloonTipDisplay(NotifyIcon notifyIcon)
        {
            NotifyIcon = notifyIcon;
        }

        private static NotifyIcon NotifyIcon { get; set; }

        internal static void Show(string title, string text, ToolTipIcon icon, int timeout)
        {
            NotifyIcon.BalloonTipIcon = icon;
            NotifyIcon.BalloonTipTitle = title;
            NotifyIcon.BalloonTipText = text;

            NotifyIcon.ShowBalloonTip(timeout);
        }
    }
}
