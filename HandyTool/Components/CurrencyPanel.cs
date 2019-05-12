using HandyTool.Components.Custom;
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
    internal class CurrencyPanel : Panel
    {
        //################################################################################
        #region Fields

        private const int c_SpaceBetween = 1;
        private const int c_PaddingMargin = 2;

        private CurrencySummary m_PreviousValues;
        private bool m_IsUpdateCancelled;

        private readonly ICurrency m_Currency;

        private readonly Control m_Parent;

        private readonly Label m_CurrencyLabel;
        private readonly Label m_CurrencyValue;
        private Label m_CurrencySettings;
        private readonly Popup m_Popup;
        private readonly SummaryPopup<Blue> m_CurrencySummary;

        private readonly BackgroundWorker m_BackgroundWorker;

        #endregion

        //################################################################################
        #region Constructor

        public CurrencyPanel(ICurrency currency, Control parent) : this(currency, parent, 300)
        {

        }

        public CurrencyPanel(ICurrency currency, Control parent, int refreshRate)
        {
            m_Currency = currency;
            m_Parent = parent;

            SetPanelPosition();

            m_CurrencyLabel = new Label();
            m_CurrencyValue = new Label();

            m_BackgroundWorker = new BackgroundWorker();

            RefreshRate = refreshRate; //ms

            InitializeComponents();
            InitializeBackgroundWorker();

            m_CurrencySummary = new SummaryPopup<Blue>();
            m_Popup = new Popup(m_CurrencySummary);
            Paint += PaintBorder;

            m_BackgroundWorker.RunWorkerAsync();
        }

        #endregion

        //################################################################################
        #region Properties

        public int RefreshRate { get; set; }

        #endregion

        //################################################################################
        #region BackgroundWorker Implementation

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var yahooService = new Yahoo(m_Currency.Source);

            yahooService.CurrencyUpdated += UpdateCurrency;

            while (!m_IsUpdateCancelled)
            {
                yahooService.GetRateData();
                Thread.Sleep(500);
            }

            yahooService.CurrencyUpdated -= UpdateCurrency;
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            m_IsUpdateCancelled = true;
        }

        #endregion

        //################################################################################
        #region Private Implementation

        private void SetPanelPosition()
        {
            Name = $@"CurrencyPanel_{m_Currency.Name}";
            TabIndex = 0;
            TabStop = false;

            int posY = 1;

            for (int i = 0; i < m_Parent.Controls.Count; i++)
            {
                posY += 1 + m_Parent.Controls[i].Height;
            }

            Location = new Point(1, posY);
            Size = new Size(m_Parent.Width - 2, 22);
        }

        private void InitializeComponents()
        {
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
            Painter<Black>.Normal(m_CurrencyLabel);

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
            m_CurrencyValue.BackColor = Color.FromArgb(238, 227, 171);

            Controls.Add(m_CurrencyValue);

            #endregion

            #region Currency Settings

            m_CurrencySettings = new ImageLabel(this, 2)
            {
                BackgroundImage = Resources.Summary,
                Size = new Size(18, 18)
            };

            //Event Stuff
            m_CurrencySettings.Click += CurrencySettings_Click;

            Controls.Add(m_CurrencySettings);

            #endregion
        }

        private void InitializeBackgroundWorker()
        {
            m_BackgroundWorker.WorkerReportsProgress = false;
            m_BackgroundWorker.WorkerSupportsCancellation = false;

            m_BackgroundWorker.DoWork += BackgroundWorker_DoWork;
            m_BackgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
        }

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
                    Painter<Green>.Light(m_CurrencyValue);
                }
                else if (currencySummary.Actual < m_PreviousValues.Actual)
                {
                    Painter<Red>.Light(m_CurrencyValue);
                }

                m_CurrencyValue.Text = $@"{currencySummary.Actual:F4}";
                m_PreviousValues = currencySummary;
            }
        }

        private void PaintBorder(object sender, PaintEventArgs e)
        {
            Rectangle border = new Rectangle(new Point(0, 0), new Size(Width - 1, Height - 1));
            CreateGraphics().DrawRectangle(new Pen(Color.White, 1), border);
        }

        #endregion

        //################################################################################
        #region Event Implementation

        private void CurrencySettings_Click(object sender, EventArgs e)
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
                m_CurrencySummary.SetValues(m_PreviousValues);
            }
            else
            {
                m_Popup.Close();
            }
        }

        #endregion
    }
}
