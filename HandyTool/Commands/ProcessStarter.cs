using HandyTool.Components.Popups;
using HandyTool.Style.Colors;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HandyTool.Commands
{
    internal class ProcessStarter : ICommand
    {
        //################################################################################
        #region Fields

        private StringBuilder m_Output = new StringBuilder();
        private readonly PopupContainer m_Popup;
        private readonly ProcessStarterPopup<Black> m_SummaryPopup;

        #endregion

        //################################################################################
        #region Constructor

        public ProcessStarter()
        {
            m_SummaryPopup = new ProcessStarterPopup<Black>();
            m_Popup = new PopupContainer(m_SummaryPopup);
            m_SummaryPopup.PopupContainer = m_Popup;
        }

        #endregion

        //################################################################################
        #region ICommand Implementation

        string ICommand.Output => m_Output.ToString();

        void ICommand.Execute()
        {
            if (!m_Popup.Visible)
            {
                m_Popup.Show(GetScreenCenterPosition());
            }
        }

        #endregion

        //################################################################################
        #region Private Implementation

        private Point GetScreenCenterPosition()
        {
            decimal screenWidth = Screen.PrimaryScreen.Bounds.Width;
            decimal screenHeight = Screen.PrimaryScreen.Bounds.Height;

            decimal panelWidth = m_SummaryPopup.Width;
            decimal panelHeight = m_SummaryPopup.Height;

            var x = Math.Floor((screenWidth / 2) - (panelWidth / 2));
            var y = Math.Floor((screenHeight / 2) - (panelHeight / 2));

            return new Point((int)x, (int)y);
        }

        #endregion
    }
}
