using System;
using System.Text;

namespace HandyTool.Commands
{
    internal class AutoDebugFileCreator : ICommand
    {
        //################################################################################
        #region Fields

        private StringBuilder m_Output = new StringBuilder();

        #endregion

        //################################################################################
        #region ICommand Implementation

        string ICommand.Output => m_Output.ToString();

        void ICommand.Execute()
        {
            //todo: implement auto debug creator
            throw new NotImplementedException();
        }

        #endregion

        //################################################################################
        #region Private Implementation

        #endregion
    }
}
