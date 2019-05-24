using System;

namespace HandyTool.Commands
{
    class TempFolderCleaner : ICommand
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
            //todo: implement temp folder cleaner
            throw new NotImplementedException();
        }

        #endregion

        //################################################################################
        #region Private Implementation

        #endregion
    }
}
