using HandyTool.Components.BasePanels;
using HandyTool.Components.CustomPanels;
using HandyTool.Style.Colors;
using System;
using System.Drawing;
using System.Text;
using System.Threading;

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
                //todo: show popup at screen center
                m_Popup.Show(new Point(200, 200));
            }
        }

        #endregion

        //################################################################################
        #region Private Implementation

        #endregion
    }
}
