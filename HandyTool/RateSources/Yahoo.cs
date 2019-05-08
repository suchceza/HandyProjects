using HandyTool.Interfaces;
using HtmlAgilityPack;
using System.IO;
using System.Net;

namespace HandyTool.RateSources
{
    class Yahoo : IRateFetchService
    {
        public double GetLatestRate()
        {
            string html = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://finance.yahoo.com/quote/EURTRY%3DX?p=EURTRY%3DX");

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
                if (stream != null)
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        html = reader.ReadToEnd();
                    }

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            string actualRate = string.Empty;
            var divList = doc.DocumentNode.Descendants("div");
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
                                actualRate = currentSpan.InnerText;
                                if (!string.IsNullOrEmpty(actualRate))
                                    break;
                            }
                        }
                    }
                }
            }

            return double.Parse(actualRate);
        }
    }
}
