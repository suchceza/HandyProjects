using HtmlAgilityPack;

using System;

namespace HandyTool.Stock.StockServices
{
    internal class Garanti : StockServiceBase, IStockService
    {
        event EventHandler<StockUpdateEventArgs> IStockService.StockUpdated
        {
            add => StockUpdate += value;
            remove => StockUpdate -= value;
        }

        void IStockService.GetStockData(IStock stock)
        {
            var htmlDoc = ReadHtmlDocument(stock.SourceUrl);
            /*var parsedHtml = */
            ParseHtmlDocument(htmlDoc);
        }

        private void ParseHtmlDocument(HtmlDocument document)
        {

        }
    }
}
