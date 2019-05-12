using System;

namespace HandyTool.Hour
{
    internal class HourCounter
    {
        //################################################################################
        #region Fields

        private readonly TimeSpan m_WorkingHour = new TimeSpan(8, 45, 0);

        private readonly DateTime m_Beginning;

        #endregion

        //################################################################################
        #region Constructor

        public HourCounter(DateTime beginning)
        {
            m_Beginning = beginning;

            CalculateWorkingTimes();
        }

        #endregion

        //################################################################################
        #region Properties

        public DateTime ShiftEndTime { get; private set; }

        public DateTime OverWorkEndTime { get; private set; }

        public TimeSpan WorkingHour => m_WorkingHour;

        #endregion

        //################################################################################
        #region Private Implementation

        private void CalculateWorkingTimes()
        {
            ShiftEndTime = m_Beginning.Add(m_WorkingHour);
            OverWorkEndTime = ShiftEndTime.AddHours(2);
        }

        #endregion
    }
}
