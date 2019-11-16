using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HandyTool.TiaBranch
{
    internal class BranchInfo
    {
        //################################################################################
        #region Constructor

        public BranchInfo(DirectoryInfo directoryInfo)
        {
            BranchDirectory = directoryInfo;
            BranchName = directoryInfo.FullName.Split('\\').Last();

            Editions = new List<EditionInfo>();
            Applications = new List<ApplicationInfo>();
        }

        #endregion

        //################################################################################
        #region Properties

        internal string BranchName { get; }

        internal DirectoryInfo BranchDirectory { get; }

        internal IList<EditionInfo> Editions { get; }

        internal IList<ApplicationInfo> Applications { get; }

        #endregion

        //################################################################################
        #region Internal Implementation

        internal void AddEdition(string editionName, string editionPath)
        {
            Editions.Add(new EditionInfo(editionName, editionPath));
        }

        internal void AddApplication(string appName, string appPath)
        {
            Applications.Add(new ApplicationInfo(appName, appPath));
        }

        #endregion

        //################################################################################
        #region Overrides

        public override string ToString()
        {
            return BranchName;
        }

        #endregion
    }
}
