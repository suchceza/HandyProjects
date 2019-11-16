using HtmlAgilityPack;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;

namespace HandyTool.Currency.Services
{
    internal class Yahoo
    {
        //################################################################################
        #region Fields

        private readonly string m_CurrencySourceUrl;
        private CurrencySummaryData m_PreviousSummary;

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
            m_PreviousSummary = new CurrencySummaryData();
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

                var currencySummary = new CurrencySummaryData
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

        internal virtual void OnCurrencyUpdated(CurrencySummaryData currencySummary)
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
            return TryParseCurrencyText(TrySplitCurrencyText(currencyText, isHigh: false), m_PreviousSummary.DayRangeLow);
        }

        private double GetDayRangeHigh(string currencyText)
        {
            return TryParseCurrencyText(TrySplitCurrencyText(currencyText, isHigh: true), m_PreviousSummary.DayRangeHigh);
        }

        private double GetYearRangeLow(string currencyText)
        {
            return TryParseCurrencyText(TrySplitCurrencyText(currencyText, isHigh: false), m_PreviousSummary.YearRangeLow);
        }

        private double GetYearRangeHigh(string currencyText)
        {
            return TryParseCurrencyText(TrySplitCurrencyText(currencyText, isHigh: true), m_PreviousSummary.YearRangeHigh);
        }

        #endregion

        private string TrySplitCurrencyText(string currencyText, bool isHigh)
        {
            try
            {
                var index = isHigh ? 1 : 0;
                return currencyText.Split('-')[index].Trim();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

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

        //################################################################################
        #region Currency Service Types

        private struct EurTryCurrency : ICurrency
        {
            string ICurrency.Name => "EUR/TRY";

            string ICurrency.Tag => "EURTRY";

            string ICurrency.Source => "https://finance.yahoo.com/quote/EURTRY%3DX?p=EURTRY%3DX";
        }

        private struct EurUsdCurrency : ICurrency
        {
            string ICurrency.Name => "EUR/USD";

            string ICurrency.Tag => "EURUSD";

            string ICurrency.Source => "https://finance.yahoo.com/quote/EURUSD%3DX?p=EURUSD%3DX";
        }

        private struct UsdTryCurrency : ICurrency
        {
            string ICurrency.Name => "USD/TRY";

            string ICurrency.Tag => "USDTRY";

            string ICurrency.Source => "https://finance.yahoo.com/quote/USDTRY%3DX?p=USDTRY%3DX";
        }

        #endregion

        //################################################################################
        #region Currency Service Properties

        internal static ICurrency EurTry => new EurTryCurrency();

        internal static ICurrency EurUsd => new EurUsdCurrency();

        internal static ICurrency UsdTry => new UsdTryCurrency();

        #endregion
    }
}
