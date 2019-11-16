using System.Collections.Generic;

namespace HandyTool.TiaBranch
{
    internal static class Constants
    {
        //Folders
        internal static string RootFolder => @"D:\WS\";
        internal static string DebugBinariesFolder => @"\binaries\Debug\x64\";
        internal static string BinFolder => @"bin\";
        internal static string EditionsFolder => @"Editions\";
        internal static string MergeSysLibLogFile => @"_Buildlogs\MergeSysLib.log"; //one of the latest log files when build finishes successfully

        //Apps
        internal static IDictionary<string, string> AppList { get; } = new Dictionary<string, string>
        {
            { "Portal", "Siemens.Automation.Portal.exe"},
            { "FileUtility", "Siemens.Automation.ObjectFrame.Tools.FileStorage.FileUtility.exe"},
            { "TrayMonitor", "Siemens.Automation.ObjectFrame.Tools.FileStorageTrayMonitor.exe"},
            { "ProjectConsole", "Siemens.Automation.ObjectFrame.Tools.Project.Console.exe"},
            { "FileStorageServer", "Siemens.Automation.ObjectFrame.FileStorage.Server.exe"}
        };

        //Editions
        internal static IDictionary<string, string> EditionList { get; } = new Dictionary<string, string>
        {
            { "Step7 Prof", "STEP7.Prof"},
            { "WinCCUA Start", "TIA.WinCCUA.Start"},
            { "S7 Safety WinCC", "S7Failsafe.Safety.WinCC.Start"},
            { "TIA Professional", "TIA.Professional.Start"},
            { "WinCC Professional", "WinCC.Professional.Start"},
            { "WinCC Comfort Advanced", "WinCC.ComfortAdvanced.Start"}
        };
    }
}
