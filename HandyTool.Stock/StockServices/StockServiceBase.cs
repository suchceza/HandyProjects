using HtmlAgilityPack;

using System;
using System.IO;
using System.Net;
using System.Threading;

namespace HandyTool.Stock.StockServices
{
    internal abstract class StockServiceBase
    {
        protected event EventHandler<StockUpdateEventArgs> StockUpdate;

        protected void OnStockUpdate(StockData stockData)
        {
            var args = new StockUpdateEventArgs(stockData);
            Volatile.Read(ref StockUpdate)?.Invoke(this, args);
        }

        protected HtmlDocument ReadHtmlDocument(string url)
        {
            //todo: implement your own business logic to read html data instead of using third party APIs
            var doc = new HtmlDocument();
            doc.LoadHtml(ReadHtmlData(url));

            return doc;
        }

        protected string ReadHtmlData(string url)
        {
            var html = string.Empty;

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);

                using (var response = (HttpWebResponse)request.GetResponse())
                using (var stream = response.GetResponseStream())
                    if (stream != null)
                        using (var reader = new StreamReader(stream))
                        {
                            html = reader.ReadToEnd();
                        }
            }
            catch (Exception)
            {
                return string.Empty;
            }

            return html;
        }
    }
}
