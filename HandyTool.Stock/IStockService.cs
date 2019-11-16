using System;

namespace HandyTool.Stock
{
    public interface IStockService
    {
        event EventHandler<StockUpdateEventArgs> StockUpdated;

        void GetStockData(IStock stock);
    }
}
