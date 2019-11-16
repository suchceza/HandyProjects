using HandyTool.Stock.StockInfo;

namespace HandyTool.Stock
{
    public class StockUpdateEventArgs
    {
        //################################################################################
        #region Constructor

        public StockUpdateEventArgs(StockData stockData)
        {
            StockData = stockData;
        }

        #endregion

        //################################################################################
        #region Properties

        public StockData StockData { get; }

        #endregion
    }
}