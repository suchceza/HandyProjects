using HandyTool.Stock.StockInfo;

using HtmlAgilityPack;

using System;

namespace HandyTool.Stock.StockServices
{
    internal class Yahoo : StockServiceBase, IStockService
    {
        //################################################################################
        #region Fields

        private StockData PreviousStockData { get; set; }

        #endregion

        //################################################################################
        #region IStockService Members

        event EventHandler<StockUpdateEventArgs> IStockService.StockUpdated
        {
            add => StockUpdate += value;
            remove => StockUpdate -= value;
        }

        void IStockService.GetStockData(IStock stock)
        {
            var htmlDoc = ReadHtmlDocument(stock.SourceUrl);
            var actualData = GetActualData(htmlDoc);
            var changeRate = GetChangeRate(htmlDoc);

            var stockData = new StockData
            {
                ActualData = actualData,
                ChangeRate = changeRate
            };

            PreviousStockData = stockData;
            OnStockUpdate(stockData);
        }

        #endregion

        //################################################################################
        #region Private Members

        private double GetActualData(HtmlDocument document)
        {
            string currencyText = string.Empty;
            var divList = document.DocumentNode.Descendants("div");

            foreach (var currentDiv in divList)
            {
                if (currentDiv.Attributes.Contains("id"))
                {
                    if (currentDiv.GetAttributeValue("id", string.Empty).Equals("quote-header-info"))
                    {
                        var spanList = currentDiv.Descendants("span");
                        foreach (var currentSpan in spanList)
                        {
                            if (currentSpan.GetAttributeValue("class", string.Empty).Contains("Trsdu(0.3s)"))
                            {
                                currencyText = currentSpan.InnerText;
                                if (!string.IsNullOrEmpty(currencyText))
                                    break;
                            }
                        }
                    }
                }
            }

            return TryParseCurrencyText(currencyText, PreviousStockData.ActualData);
        }

        private static double GetChangeRate(HtmlDocument document)
        {
            return 0;
        }

        private static double TryParseCurrencyText(string currencyText, double previousValue)
        {
            double actualCurrency;

            try
            {
                actualCurrency = double.Parse(currencyText);
            }
            catch (Exception)
            {
                actualCurrency = previousValue;
            }

            return actualCurrency;
        }

        #endregion
    }
}
