
namespace HandyTool.Currency
{
    internal interface ICurrency
    {
        string Name { get; }

        string Source { get; }
    }

    //todo: convert them to inner classes in each currency service class. eg. move those into Yahoo.cs

    internal class EurTryCurrency : ICurrency
    {
        string ICurrency.Name => "EUR/TRY";

        string ICurrency.Source => "https://finance.yahoo.com/quote/EURTRY%3DX?p=EURTRY%3DX";
    }

    internal class EurUsdCurrency : ICurrency
    {
        string ICurrency.Name => "EUR/USD";

        string ICurrency.Source => "https://finance.yahoo.com/quote/EURUSD%3DX?p=EURUSD%3DX";
    }

    internal class UsdTryCurrency : ICurrency
    {
        string ICurrency.Name => "USD/TRY";

        string ICurrency.Source => "https://finance.yahoo.com/quote/USDTRY%3DX?p=USDTRY%3DX";
    }
}
