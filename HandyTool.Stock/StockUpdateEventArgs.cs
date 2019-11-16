using HandyTool.Stock.StockInfo;

namespace HandyTool.Stock
{
    public class StockUpdateEventArgs
    {
        public StockUpdateEventArgs(StockData stockData)
        {
            StockData = stockData;
        }

        public StockData StockData { get; }
    }
}