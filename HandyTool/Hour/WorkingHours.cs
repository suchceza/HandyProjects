using System;

namespace HandyTool.Hour
{
    internal class WorkingHours
    {
        //################################################################################
        #region Constructor

        public WorkingHours(DateTime startTime)
        {
            StartTime = startTime;
            FinishTime = StartTime.Add(RegularWorkHour);
            DeadlineTime = StartTime.Add(OverWorkHour);
        }

        #endregion

        //################################################################################
        #region Properties

        public DateTime StartTime { get; }

        public DateTime FinishTime { get; }

        public DateTime DeadlineTime { get; }

        public TimeSpan LunchBreakHour { get; } = new TimeSpan(0, 45, 0);

        public TimeSpan RegularWorkHour { get; } = new TimeSpan(8, 45, 0);

        public TimeSpan OverWorkHour { get; } = new TimeSpan(10, 45, 0);

        #endregion
    }
}
