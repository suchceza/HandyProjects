using System;

namespace HandyTool.Commands
{
    class ProxyUpdater : ICommand
    {
        //################################################################################
        #region Fields

        #endregion

        //################################################################################
        #region Constructor

        #endregion

        //################################################################################
        #region ICommand Implementation

        string ICommand.Output { get; }

        void ICommand.Execute()
        {
            throw new NotImplementedException();
        }

        #endregion

        //################################################################################
        #region Private Implementation

        #endregion
    }
}
