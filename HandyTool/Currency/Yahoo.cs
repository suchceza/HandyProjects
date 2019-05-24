using HtmlAgilityPack;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;

namespace HandyTool.Currency
{
    internal class Yahoo
    {
        //################################################################################
        #region Fields

        private readonly string m_CurrencySourceUrl;
        private CurrencySummary m_PreviousSummary;

        #endregion

        //################################################################################
        #region Events

        internal event CurrencyUpdateCallback CurrencyUpdated;

        #endregion

        //################################################################################
        #region Constructor

        public Yahoo(string currencySourceUrl)
        {
            m_CurrencySourceUrl = currencySourceUrl;
            m_PreviousSummary = new CurrencySummary();
        }

        #endregion

        //################################################################################
        #region Internal Implementation

        public void GetRateData(object sender, DoWorkEventArgs e)
        {
            if (sender is BackgroundWorker worker && worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            try
            {
                //todo: implement your own business logic to read html data instead of using third party APIs
                var doc = new HtmlDocument();
                doc.LoadHtml(ReadHtmlData());

                var summaryData = ReadSummaryData(doc);

                var currencySummary = new CurrencySummary
                {
                    Actual = GetActualCurrency(doc),
                    PreviousClose = TryParseCurrencyText(summaryData["previous-close"], m_PreviousSummary.PreviousClose),
                    Open = TryParseCurrencyText(summaryData["open"], m_PreviousSummary.Open),
                    DayRangeLow = GetDayRangeLow(summaryData["day-range"]),
                    DayRangeHigh = GetDayRangeHigh(summaryData["day-range"]),
                    YearRangeLow = GetYearRangeLow(summaryData["year-range"]),
                    YearRangeHigh = GetYearRangeHigh(summaryData["year-range"])
                };

                m_PreviousSummary = currencySummary;
                OnCurrencyUpdated(currencySummary);
            }
            catch (Exception)
            {
                //todo: get this result when thread ended. and put wrong data into the result object
                e.Result = "Exception occured during currency data fetching.";
                throw;
            }
        }

        #endregion

        //################################################################################
        #region Event Callbacks

        internal virtual void OnCurrencyUpdated(CurrencySummary currencySummary)
        {
            if (CurrencyUpdated != null)
            {
                var args = new CurrencyUpdatedEventArgs { CurrencySummary = currencySummary };

                CurrencyUpdated(args);
            }
        }

        #endregion

        //################################################################################
        #region Private Implementation

        #region Html Data Readers

        private string ReadHtmlData()
        {
            string html = string.Empty;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(m_CurrencySourceUrl);

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                    if (stream != null)
                        using (StreamReader reader = new StreamReader(stream))
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

        private IDictionary<string, string> ReadSummaryData(HtmlDocument doc)
        {
            var summaryData = new Dictionary<string, string>
            {
                {"previous-close", string.Empty},
                {"open", string.Empty},
                {"day-range", string.Empty},
                {"year-range", string.Empty}
            };

            var divList = doc.DocumentNode.Descendants("div");

            foreach (var currentDiv in divList)
            {
                if (currentDiv.Attributes.Contains("id"))
                {
                    if (currentDiv.GetAttributeValue("id", string.Empty).Equals("quote-summary"))
                    {
                        HtmlNode[] tdList = currentDiv.Descendants("td").ToArray();
                        summaryData["previous-close"] = tdList[1].InnerText;
                        summaryData["open"] = tdList[3].InnerText;
                        summaryData["day-range"] = tdList[7].InnerText;
                        summaryData["year-range"] = tdList[9].InnerText;
                    }
                }
            }

            return summaryData;
        }

        #endregion

        #region Rate Summary Getters

        private double GetActualCurrency(HtmlDocument doc)
        {
            string currencyText = string.Empty;
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
                                currencyText = currentSpan.InnerText;
                                if (!string.IsNullOrEmpty(currencyText))
                                    break;
                            }
                        }
                    }
                }
            }

            return TryParseCurrencyText(currencyText, m_PreviousSummary.Actual);
        }

        private double GetDayRangeLow(string currencyText)
        {
            var dayRangeLow = currencyText.Split('-')[0].Trim();
            return TryParseCurrencyText(dayRangeLow, m_PreviousSummary.DayRangeLow);
        }

        private double GetDayRangeHigh(string currencyText)
        {
            var dayRangeHigh = currencyText.Split('-')[1].Trim();
            return TryParseCurrencyText(dayRangeHigh, m_PreviousSummary.DayRangeHigh);
        }

        private double GetYearRangeLow(string currencyText)
        {
            var yearRangeLow = currencyText.Split('-')[0].Trim();
            return TryParseCurrencyText(yearRangeLow, m_PreviousSummary.YearRangeLow);
        }

        private double GetYearRangeHigh(string currencyText)
        {
            var yearRangeHigh = currencyText.Split('-')[1].Trim();
            return TryParseCurrencyText(yearRangeHigh, m_PreviousSummary.YearRangeHigh);
        }

        #endregion

        private double TryParseCurrencyText(string currencyText, double previousValue)
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
