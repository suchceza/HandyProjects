using System;

namespace HandyTool.Hour
{
    internal class WorkingHours
    {
        //################################################################################
        #region Fields

        private readonly TimeSpan m_LunchBreak = new TimeSpan(0, 45, 0);
        private readonly TimeSpan m_BaseWorkhour = new TimeSpan(8, 0, 0);
        private readonly TimeSpan m_AllowedOverwork = new TimeSpan(2, 0, 0);

        #endregion

        //################################################################################
        #region Constructor

        public WorkingHours()
        {
            EmptyInitializer();
        }

        public WorkingHours(DateTime startTime)
        {
            StartTime = startTime;
            StartTimeLunchBreakIncluded = StartTime.Add(m_LunchBreak);
            FinishTime = StartTime.Add(m_BaseWorkhour).Add(m_LunchBreak);
            DeadlineTime = FinishTime.Add(m_AllowedOverwork);
        }

        #endregion

        //################################################################################
        #region Properties

        internal DateTime StartTime { get; private set; }

        internal DateTime StartTimeLunchBreakIncluded { get; }

        internal DateTime FinishTime { get; private set; }

        internal DateTime DeadlineTime { get; private set; }

        #endregion

        //################################################################################
        #region Internal Implementation

        internal string StartTimeString()
        {
            return TimeToString(StartTime);
        }

        internal string FinishTimeString()
        {
            return TimeToString(FinishTime);
        }

        internal string DeadlineTimeString()
        {
            return TimeToString(DeadlineTime);
        }

        #endregion

        //################################################################################
        #region Private Implementation

        private void EmptyInitializer()
        {
            var emptyDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            StartTime = emptyDateTime;
            FinishTime = emptyDateTime;
            DeadlineTime = emptyDateTime;
        }

        private string TimeToString(DateTime dateTime)
        {
            return $@"{dateTime.Hour:00}:{dateTime.Minute:00}:{dateTime.Second:00}";
        }

        #endregion
    }
}
