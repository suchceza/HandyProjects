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
            //todo: implement proxy updater
            throw new NotImplementedException();
        }

        #endregion

        //################################################################################
        #region Private Implementation

        #endregion
    }
}
