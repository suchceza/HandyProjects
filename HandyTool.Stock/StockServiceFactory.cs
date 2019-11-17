using HandyTool.Stock.StockServices;

using System;

namespace HandyTool.Stock
{
    public class StockServiceFactory
    {
        //################################################################################
        #region Constructor

        private StockServiceFactory() { }

        #endregion

        //################################################################################
        #region Public Static Members

        public static IStockService CreateService(string serviceName)
        {
            switch (serviceName)
            {
                case "Yahoo":
                    return new Yahoo();

                case "Garanti":
                    return new Garanti();

                default:
                    throw new ArgumentException("Invalid stock serviceType argument.");
            }
        }

        #endregion
    }
}
