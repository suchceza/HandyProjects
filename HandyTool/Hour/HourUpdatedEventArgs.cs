namespace HandyTool.Hour
{
    internal delegate void HourUpdateCallback(HourUpdatedEventArgs args);

    internal class HourUpdatedEventArgs
    {
        internal string Hour { get; set; }
    }
}
