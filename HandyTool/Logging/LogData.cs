using System.Collections.Generic;

namespace HandyTool.Logging
{
    internal class LogData
    {
        //################################################################################
        #region Fields

        private readonly IList<LogData> m_InnerItems;

        #endregion

        //################################################################################
        #region Constructor

        public LogData(string logMessage)
        {
            Level = 0;
            Message = logMessage;
            m_InnerItems = new List<LogData>();
        }

        #endregion

        //################################################################################
        #region Properties

        public IEnumerable<LogData> InnerItems => m_InnerItems;

        public string Message { get; }

        public int Level { get; set; }

        #endregion

        //################################################################################
        #region Internal Implementation

        internal void AddInnerLogItem(LogData innerLogItem)
        {
            innerLogItem.Level = Level + 1;
            m_InnerItems.Add(innerLogItem);
        }

        #endregion
    }
}
