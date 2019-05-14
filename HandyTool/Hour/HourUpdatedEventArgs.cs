using System;

namespace HandyTool.Hour
{
    internal delegate void HourUpdateCallback(HourUpdatedEventArgs args);

    internal class HourUpdatedEventArgs
    {
        internal TimeSpan ElapsedTime { get; set; }
    }
}
