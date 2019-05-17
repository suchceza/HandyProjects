﻿using HandyTool.Components.Custom;
using HandyTool.Currency;
using HandyTool.Properties;
using HandyTool.Style;
using HandyTool.Style.Colors;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace HandyTool.Components
{
    internal class CurrencyPanel : BackgroundWorkerPanel
    {
        //################################################################################
        #region Fields

        private const int c_SpaceBetween = 1;
        private const int c_PaddingMargin = 2;

        private CurrencySummary m_PreviousValues;
        private bool m_IsUpdateCancelled;

        private readonly ICurrency m_Currency;

        private readonly Label m_CurrencyLabel;
        private readonly Label m_CurrencyValue;
        private ImageLabel m_CurrencySummary;
        private ImageLabel m_CurrencyRefresh;

        private readonly Popup m_Popup;
        private readonly CurrencySummaryPopup<Blue> m_CurrencySummaryPopup;

        #endregion

        //################################################################################
        #region Constructor

        public CurrencyPanel(ICurrency currency, Control parentControl) : this(currency, parentControl, 1000)
        {

        }

        public CurrencyPanel(ICurrency currency, Control parentControl, int refreshRate) : base(parentControl)
        {
            m_Currency = currency;

            m_CurrencyLabel = new Label();
            m_CurrencyValue = new Label();

            RefreshRate = refreshRate; //ms

            InitializeComponents();

            m_CurrencySummaryPopup = new CurrencySummaryPopup<Blue>();
            m_Popup = new Popup(m_CurrencySummaryPopup);
            Paint += PaintBorder;

            BackgroundWorker.RunWorkerAsync();
        }

        #endregion

        //################################################################################
        #region Properties

        public int RefreshRate { get; set; }

        public string CurrencyName => m_Currency.Name;

        public string CurrentRateValue => $"{m_PreviousValues.Actual:F4}";

        #endregion

        //################################################################################
        #region Protected Implementation

        protected override void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var yahooService = new Yahoo(m_Currency.Source);

            yahooService.CurrencyUpdated += UpdateCurrency;

            try
            {
                while (!m_IsUpdateCancelled)
                {
                    yahooService.GetRateData();
                    Thread.Sleep(500);
                }
            }
            finally
            {
                yahooService.CurrencyUpdated -= UpdateCurrency;
            }
        }

        protected override void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if any error occurs and thread is killed
            m_IsUpdateCancelled = true;
            Painter<Black>.Paint(m_CurrencyValue, PaintMode.Normal);
            m_CurrencyValue.Text = @"N/A";
            m_CurrencyRefresh.Enabled = true;
            m_CurrencyRefresh.BackgroundImage = Resources.RefreshProcessEnabled;
            WriteLogsIfHappened(e.Error, e.Result, $"Currency[{m_Currency.Name}]", e.Cancelled);
        }

        protected sealed override void InitializeComponents()
        {
            Name = $@"CurrencyPanel_{m_Currency.Name}";
            TabIndex = 0;
            TabStop = false;
            Size = new Size(ParentControl.Width - 2, 22);
            Visible = Settings.Default.CurrencySwitch;

            #region Currency Label

            //Basic Stuff
            m_CurrencyLabel.Name = "Currency Label";

            //Text Stuff
            m_CurrencyLabel.Text = $@"{m_Currency.Name}:";
            m_CurrencyLabel.Font = new Font(new FontFamily("Consolas"), 9, FontStyle.Bold);

            //Position/Size Stuff
            m_CurrencyLabel.Location = new Point(c_PaddingMargin, c_PaddingMargin);
            m_CurrencyLabel.Size = new Size(70, 18);

            //Alignment Stuff
            m_CurrencyLabel.Padding = new Padding(c_PaddingMargin);
            m_CurrencyLabel.TextAlign = ContentAlignment.MiddleCenter;

            //Style Stuff
            Painter<Black>.Paint(m_CurrencyLabel, PaintMode.Normal);

            Controls.Add(m_CurrencyLabel);

            #endregion

            #region Currency Value

            //Basic Stuff
            m_CurrencyValue.Name = "Currency Value";

            //Text Stuff
            m_CurrencyValue.Text = @"N/A";
            m_CurrencyValue.Font = new Font(new FontFamily("Consolas"), 9, FontStyle.Bold);

            //Position/Size Stuff
            m_CurrencyValue.Location = new Point(c_PaddingMargin + Controls[0].Width + c_SpaceBetween, c_PaddingMargin);
            m_CurrencyValue.Size = new Size(70, 18);

            //Alignment Stuff
            m_CurrencyValue.Padding = new Padding(c_PaddingMargin);
            m_CurrencyValue.TextAlign = ContentAlignment.MiddleRight;

            //Style Stuff
            Painter<Black>.Paint(m_CurrencyValue, PaintMode.Normal);

            Controls.Add(m_CurrencyValue);

            #endregion

            #region Currency Summary

            m_CurrencySummary = new ImageLabel(this, 2, "Display summary of the currency rate")
            {
                BackgroundImage = Resources.SummaryCurrency
            };

            //Event Stuff
            m_CurrencySummary.Click += CurrencySummary_Click;

            Controls.Add(m_CurrencySummary);

            #endregion

            #region Currency Refresh

            m_CurrencyRefresh = new ImageLabel(this, 2, "Refresh fetching of the currency rate")
            {
                BackgroundImage = Resources.RefreshProcessDisabled,
                Enabled = false
            };

            //Event Stuff
            m_CurrencyRefresh.Click += CurrencyRefresh_Click;

            Controls.Add(m_CurrencyRefresh);

            #endregion
        }

        #endregion

        //################################################################################
        #region Private Implementation

        private void UpdateCurrency(CurrencyUpdatedEventArgs args)
        {
            if (m_CurrencyValue.InvokeRequired)
            {
                CurrencyUpdateCallback callback = UpdateCurrency;
                Invoke(callback, args);
            }
            else
            {
                CurrencySummary currencySummary = args.CurrencySummary;

                if (currencySummary.Actual > m_PreviousValues.Actual)
                {
                    Painter<Green>.Paint(m_CurrencyValue, PaintMode.Light);
                }
                else if (currencySummary.Actual < m_PreviousValues.Actual)
                {
                    Painter<Red>.Paint(m_CurrencyValue, PaintMode.Light);
                }

                m_CurrencyValue.Text = $@"{currencySummary.Actual:F4}";
                m_PreviousValues = currencySummary;
            }
        }

        #endregion

        //################################################################################
        #region Event Handler Methods

        private void CurrencySummary_Click(object sender, EventArgs e)
        {
            if (!m_Popup.Visible)
            {
                var top = Parent.Top;

                foreach (Control control in Parent.Controls)
                {
                    if (control.Name == Name)
                    {
                        top += control.Top;
                        top -= m_Popup.Height - control.Height;
                    }
                }

                m_Popup.Show(new Point(Parent.Left - 50, top));
                m_CurrencySummaryPopup.SetValues(m_PreviousValues);
            }
            else
            {
                m_Popup.Close();
            }
        }

        private void CurrencyRefresh_Click(object sender, EventArgs e)
        {
            m_IsUpdateCancelled = false;
            m_CurrencyRefresh.Enabled = false;
            m_CurrencyRefresh.BackgroundImage = Resources.RefreshProcessDisabled;
            BackgroundWorker.RunWorkerAsync();
        }

        #endregion
    }
}
