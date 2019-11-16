using System;
using System.Collections.Generic;
using System.Threading;

namespace HandyTool.Stock.Tests.Dummies
{
    class DummyStockService : IStockService
    {
        private event EventHandler<StockUpdateEventArgs> StockUpdated;

        event EventHandler<StockUpdateEventArgs> IStockService.StockUpdated
        {
            add => StockUpdated += value;
            remove => StockUpdated -= value;
        }

        void IStockService.GetStockData(IStock stock)
        {
            //create dummy stock data
            var stockData = new StockData
            {
                ActualData = 0.1,
                ChangeRate = 0.2
            };

            var args = new StockUpdateEventArgs(stockData);
            Volatile.Read(ref StockUpdated)?.Invoke(this, args);
        }
    }
}
