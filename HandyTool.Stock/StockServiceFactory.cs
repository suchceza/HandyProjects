using HandyTool.Stock.StockServices;

using System;

namespace HandyTool.Stock
{
    public class StockServiceFactory
    {
        private StockServiceFactory() { }

        public static IStockService CreateService(StockService service)
        {
            switch (service)
            {
                case StockService.Yahoo:
                    return new Yahoo();

                case StockService.Garanti:
                    throw new NotImplementedException();

                default:
                    throw new ArgumentException("Invalid stock service argument.");
            }
        }
    }

    public enum StockService
    {
        Yahoo,
        Garanti
    }
}
