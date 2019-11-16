using System.Collections.Generic;
using System.IO;

namespace HandyTool.TiaBranch
{
    internal class BranchCollector
    {
        //################################################################################
        #region Fields

        private readonly IList<BranchInfo> m_BranchList;

        #endregion

        //################################################################################
        #region Constructor

        public BranchCollector()
        {
            m_BranchList = new List<BranchInfo>();
        }

        #endregion

        //################################################################################
        #region Properties

        public IEnumerable<BranchInfo> Branches => m_BranchList;

        #endregion

        //################################################################################
        #region Internal Implementation

        internal void CollectBranches()
        {
            foreach (var directoryPath in Directory.EnumerateDirectories(Constants.RootFolder))
            {
                var branchInfo = new BranchInfo(new DirectoryInfo(directoryPath));

                if (IsValidBranch(branchInfo))
                {
                    CollectEditions(branchInfo);

                    CollectApplications(branchInfo);

                    m_BranchList.Add(branchInfo);
                }
            }
        }

        #endregion

        //################################################################################
        #region Private Implementation

        private void CollectEditions(BranchInfo branchInfo)
        {
            foreach (var edition in Constants.EditionList)
            {
                var editionFolder = $"{branchInfo.BranchDirectory}{Constants.DebugBinariesFolder}{Constants.EditionsFolder}{edition.Value}";
                if (!Directory.Exists(editionFolder)) continue;

                var editionBinDirectory = new DirectoryInfo($"{editionFolder}\\{Constants.BinFolder}");
                foreach (var fileInfo in editionBinDirectory.GetFiles("*.exe"))
                {
                    branchInfo.AddEdition(edition.Key, fileInfo.FullName);
                }
            }
        }

        private void CollectApplications(BranchInfo branchInfo)
        {
            foreach (var app in Constants.AppList)
            {
                var appPath = $"{branchInfo.BranchDirectory}{Constants.DebugBinariesFolder}{Constants.BinFolder}{app.Value}";
                if (!File.Exists(appPath)) continue;

                branchInfo.AddApplication(app.Key, appPath);
            }
        }

        private bool IsValidBranch(BranchInfo branchInfo)
        {
            if (!IsTiaPortalBranch(branchInfo)) return false;

            if (!IsBinaryExists(branchInfo)) return false;

            if (!IsBuildSuccess(branchInfo)) return false;

            return true;
        }

        private bool IsTiaPortalBranch(BranchInfo branchInfo)
        {
            //portal exe cannot be found
            return File.Exists($"{branchInfo.BranchDirectory}{Constants.DebugBinariesFolder}{Constants.BinFolder}{Constants.AppList["Portal"]}");
        }

        private bool IsBinaryExists(BranchInfo branchInfo)
        {
            //bin folder doesn't exist
            return Directory.Exists($"{branchInfo.BranchDirectory}{Constants.DebugBinariesFolder}{Constants.BinFolder}");
        }

        private bool IsBuildSuccess(BranchInfo branchInfo)
        {
            //mergesyslib.log file doesn't exist
            return File.Exists($"{branchInfo.BranchDirectory}{Constants.DebugBinariesFolder}{Constants.MergeSysLibLogFile}");
        }

        #endregion
    }
}
