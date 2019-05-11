
namespace HandyTool.Currency
{
    internal delegate void CurrencyUpdateCallback(CurrencyUpdatedEventArgs args);

    internal class CurrencyUpdatedEventArgs
    {
        internal CurrencySummary CurrencySummary { get; set; }
    }
}
