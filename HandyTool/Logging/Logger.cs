using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace HandyTool.Logging
{
    internal class Logger
    {
        //################################################################################
        #region Fields

        private readonly string m_LogFileName = "handy-box-logs.log";
        private string m_NotepadPath = @"C:\Windows\notepad.exe";

        #endregion

        //################################################################################
        #region Properties

        private string LogFolder => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        private string LogFile => Path.Combine(LogFolder, m_LogFileName);

        #endregion

        //################################################################################
        #region Internal Implementation

        internal void WriteLog(LogData logData)
        {
            //todo: improve writing of logs to the log file

            try
            {
                CreateLogFolderIfNotExist();
                CreateLogFileIfNotExist();
                WriteLogsToFileRecursively(logData);
            }
            catch (Exception e)
            {
                var x = e.Message;
            }
        }

        internal void ShowLogs()
        {
            Process.Start(m_NotepadPath, LogFile);
        }

        internal void ClearLogs()
        {
            File.Delete(LogFile);
            CreateLogFileIfNotExist();
        }

        #endregion

        //################################################################################
        #region Private Implementation

        private void CreateLogFolderIfNotExist()
        {
            if (!Directory.Exists(LogFolder))
            {
                Directory.CreateDirectory(LogFolder);
            }
        }

        private void CreateLogFileIfNotExist()
        {
            if (!File.Exists(LogFile))
            {
                var fileStream = File.Create(LogFile);
                fileStream.Close();
            }
        }

        private void WriteLogsToFileRecursively(LogData logData)
        {
            using (StreamWriter writer = new StreamWriter(LogFile, true))
            {
                //todo: 4 tabs are too much for inner exception. redesign is needed.
                var indent = string.Concat(Enumerable.Repeat("\t", logData.Level * 4));

                foreach (var line in logData.Message.Split(new[] { "\n" }, StringSplitOptions.None))
                {
                    writer.WriteLine($"{indent}{line}");
                }

                writer.Close();

                foreach (var innerLogData in logData.InnerItems)
                {
                    WriteLogsToFileRecursively(innerLogData);
                }
            }
        }

        #endregion
    }
}
