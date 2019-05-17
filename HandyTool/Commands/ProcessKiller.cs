using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;

namespace HandyTool.Commands
{
    internal class ProcessKiller : ICommand
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
            m_Output.Clear();

            var isProcessFound = false;
            var processes = Process.GetProcesses();

            foreach (var process in processes)
            {
                if (process.ProcessName.StartsWith(@"Siemens.Automation"))
                {
                    isProcessFound = true;
                    var commandLineArguments = GetCommandLineArguments(process);
                    process.Kill();

                    m_Output.AppendLine($@"Process Killed: {process.ProcessName}");

                    foreach (var argument in commandLineArguments.Split(' '))
                    {
                        if (!string.IsNullOrEmpty(argument.Trim()))
                        {
                            m_Output.AppendLine($"\t{argument}");
                        }
                    }
                }
            }

            if (!isProcessFound)
            {
                m_Output.Append(@"No process found.");
            }
        }

        #endregion

        //################################################################################
        #region Private Implementation

        private string GetCommandLineArguments(Process process)
        {
            var queryString = $"SELECT CommandLine FROM Win32_Process WHERE ProcessId = '{process.Id}'";

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(queryString))
            using (ManagementObjectCollection objects = searcher.Get())
            {
                return objects.Cast<ManagementObject>().SingleOrDefault()?["CommandLine"]?.ToString();
            }
        }

        #endregion
    }
}
