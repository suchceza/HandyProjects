
namespace HandyTool.Currency
{
    internal interface ICurrency
    {
        string Name { get; }

        string Source { get; }
    }

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

    internal class EurBgnCurrency : ICurrency
    {
        string ICurrency.Name => "EUR/BGN";

        string ICurrency.Source => "https://finance.yahoo.com/quote/EURBGN%3DX?p=EURBGN%3DX";
    }

    internal class UsdTryCurrency : ICurrency
    {
        string ICurrency.Name => "USD/TRY";

        string ICurrency.Source => "https://finance.yahoo.com/quote/USDTRY%3DX?p=USDTRY%3DX";
    }

    internal class BgnTryCurrency : ICurrency
    {
        string ICurrency.Name => "BGN/TRY";

        string ICurrency.Source => "https://finance.yahoo.com/quote/BGNTRY%3DX?p=BGNTRY%3DX";
    }
}
