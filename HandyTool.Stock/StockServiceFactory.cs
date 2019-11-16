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

        public static IStockService CreateService(StockServiceType serviceType)
        {
            switch (serviceType)
            {
                case StockServiceType.Yahoo:
                    return new Yahoo();

                case StockServiceType.Garanti:
                    throw new NotImplementedException();

                default:
                    throw new ArgumentException("Invalid stock serviceType argument.");
            }
        }

        #endregion
    }
}
