using HandyTool.Currency;
using HandyTool.Style;

using System.Drawing;
using System.Windows.Forms;

namespace HandyTool.Components
{
    internal class SummaryPopup<T> : DraggablePanel where T : ColorBase, new()
    {
        //################################################################################
        #region Fields

        private readonly Label m_PreviousCloseLabel = new Label();
        private readonly Label m_PreviousCloseValue = new Label();

        private readonly Label m_OpenLabel = new Label();
        private readonly Label m_OpenValue = new Label();

        private readonly Label m_DailyLowLabel = new Label();
        private readonly Label m_DailyLowValue = new Label();

        private readonly Label m_DailyHighLabel = new Label();
        private readonly Label m_DailyHighValue = new Label();

        private readonly Label m_YearlyLowLabel = new Label();
        private readonly Label m_YearlyLowValue = new Label();

        private readonly Label m_YearlyHighLabel = new Label();
        private readonly Label m_YearlyHighValue = new Label();

        #endregion

        //################################################################################
        #region Constructor

        public SummaryPopup()
        {
            InitializeComponents();
        }

        #endregion

        //################################################################################
        #region Internal Implementation

        internal void SetValues(CurrencySummary summary)
        {
            m_PreviousCloseValue.Text = $@"{summary.PreviousClose:F4}";
            m_OpenValue.Text = $@"{summary.Open:F4}";
            m_DailyLowValue.Text = $@"{summary.DayRangeLow:F4}";
            m_DailyHighValue.Text = $@"{summary.DayRangeHigh:F4}";
            m_YearlyLowValue.Text = $@"{summary.YearRangeLow:F4}";
            m_YearlyHighValue.Text = $@"{summary.YearRangeHigh:F4}";
        }

        #endregion

        //################################################################################
        #region Private Implementation

        private void InitializeComponents()
        {
            Size = new Size(193, 105);
            MouseDown += DragAndDrop;

            var palette = new ColorPalette<T>();

            #region Previous Close

            m_PreviousCloseLabel.Text = @"Previous Close:";
            m_PreviousCloseLabel.Font = new Font(new FontFamily("Consolas"), 9, FontStyle.Bold);
            m_PreviousCloseLabel.Size = new Size(120, 16);
            m_PreviousCloseLabel.Location = new Point(1, 1);
            m_PreviousCloseLabel.TextAlign = ContentAlignment.MiddleRight;
            m_PreviousCloseLabel.BackColor = palette.Light.BackColor;
            m_PreviousCloseLabel.ForeColor = palette.Light.ForeColor;
            m_PreviousCloseLabel.MouseDown += DragAndDrop;

            m_PreviousCloseValue.Font = new Font(new FontFamily("Consolas"), 9, FontStyle.Bold);
            m_PreviousCloseValue.Size = new Size(70, 16);
            m_PreviousCloseValue.Location = new Point(122, 1);
            m_PreviousCloseValue.TextAlign = ContentAlignment.MiddleRight;
            m_PreviousCloseValue.BackColor = palette.Light.BackColor;
            m_PreviousCloseValue.ForeColor = palette.Light.ForeColor;
            m_PreviousCloseValue.MouseDown += DragAndDrop;

            Controls.Add(m_PreviousCloseLabel);
            Controls.Add(m_PreviousCloseValue);

            #endregion

            #region Open

            m_OpenLabel.Text = @"Open:";
            m_OpenLabel.Font = new Font(new FontFamily("Consolas"), 9, FontStyle.Bold);
            m_OpenLabel.Size = new Size(120, 16);
            m_OpenLabel.Location = new Point(1, 18);
            m_OpenLabel.TextAlign = ContentAlignment.MiddleRight;
            m_OpenLabel.BackColor = palette.Light.BackColor;
            m_OpenLabel.ForeColor = palette.Light.ForeColor;
            m_OpenLabel.BorderStyle = BorderStyle.None;
            m_OpenLabel.MouseDown += DragAndDrop;

            m_OpenValue.Font = new Font(new FontFamily("Consolas"), 9, FontStyle.Bold);
            m_OpenValue.Size = new Size(70, 16);
            m_OpenValue.Location = new Point(122, 18);
            m_OpenValue.TextAlign = ContentAlignment.MiddleRight;
            m_OpenValue.BackColor = palette.Light.BackColor;
            m_OpenValue.ForeColor = palette.Light.ForeColor;
            m_OpenValue.BorderStyle = BorderStyle.None;
            m_OpenValue.MouseDown += DragAndDrop;

            Controls.Add(m_OpenLabel);
            Controls.Add(m_OpenValue);

            #endregion

            #region Daily Low

            m_DailyLowLabel.Text = @"Daily Low:";
            m_DailyLowLabel.Font = new Font(new FontFamily("Consolas"), 9, FontStyle.Bold);
            m_DailyLowLabel.Size = new Size(120, 16);
            m_DailyLowLabel.Location = new Point(1, 35);
            m_DailyLowLabel.TextAlign = ContentAlignment.MiddleRight;
            m_DailyLowLabel.BackColor = palette.Normal.BackColor;
            m_DailyLowLabel.ForeColor = palette.Normal.ForeColor;
            m_DailyLowLabel.MouseDown += DragAndDrop;

            m_DailyLowValue.Font = new Font(new FontFamily("Consolas"), 9, FontStyle.Bold);
            m_DailyLowValue.Size = new Size(70, 16);
            m_DailyLowValue.Location = new Point(122, 35);
            m_DailyLowValue.TextAlign = ContentAlignment.MiddleRight;
            m_DailyLowValue.BackColor = palette.Normal.BackColor;
            m_DailyLowValue.ForeColor = palette.Normal.ForeColor;
            m_DailyLowValue.MouseDown += DragAndDrop;

            Controls.Add(m_DailyLowLabel);
            Controls.Add(m_DailyLowValue);

            #endregion

            #region Daily High

            m_DailyHighLabel.Text = @"Daily High:";
            m_DailyHighLabel.Font = new Font(new FontFamily("Consolas"), 9, FontStyle.Bold);
            m_DailyHighLabel.Size = new Size(120, 16);
            m_DailyHighLabel.Location = new Point(1, 52);
            m_DailyHighLabel.TextAlign = ContentAlignment.MiddleRight;
            m_DailyHighLabel.BackColor = palette.Normal.BackColor;
            m_DailyHighLabel.ForeColor = palette.Normal.ForeColor;
            m_DailyHighLabel.MouseDown += DragAndDrop;

            m_DailyHighValue.Font = new Font(new FontFamily("Consolas"), 9, FontStyle.Bold);
            m_DailyHighValue.Size = new Size(70, 16);
            m_DailyHighValue.Location = new Point(122, 52);
            m_DailyHighValue.TextAlign = ContentAlignment.MiddleRight;
            m_DailyHighValue.BackColor = palette.Normal.BackColor;
            m_DailyHighValue.ForeColor = palette.Normal.ForeColor;
            m_DailyHighValue.MouseDown += DragAndDrop;

            Controls.Add(m_DailyHighLabel);
            Controls.Add(m_DailyHighValue);

            #endregion

            #region Yearly Low

            m_YearlyLowLabel.Text = @"Yearly Low:";
            m_YearlyLowLabel.Font = new Font(new FontFamily("Consolas"), 9, FontStyle.Bold);
            m_YearlyLowLabel.Size = new Size(120, 16);
            m_YearlyLowLabel.Location = new Point(1, 69);
            m_YearlyLowLabel.TextAlign = ContentAlignment.MiddleRight;
            m_YearlyLowLabel.BackColor = palette.Dark.BackColor;
            m_YearlyLowLabel.ForeColor = palette.Dark.ForeColor;
            m_YearlyLowLabel.BorderStyle = BorderStyle.None;
            m_YearlyLowLabel.MouseDown += DragAndDrop;

            m_YearlyLowValue.Font = new Font(new FontFamily("Consolas"), 9, FontStyle.Bold);
            m_YearlyLowValue.Size = new Size(70, 16);
            m_YearlyLowValue.Location = new Point(122, 69);
            m_YearlyLowValue.TextAlign = ContentAlignment.MiddleRight;
            m_YearlyLowValue.BackColor = palette.Dark.BackColor;
            m_YearlyLowValue.ForeColor = palette.Dark.ForeColor;
            m_YearlyLowValue.BorderStyle = BorderStyle.None;
            m_YearlyLowValue.MouseDown += DragAndDrop;

            Controls.Add(m_YearlyLowLabel);
            Controls.Add(m_YearlyLowValue);

            #endregion

            #region Yearly High

            m_YearlyHighLabel.Text = @"Yearly High:";
            m_YearlyHighLabel.Font = new Font(new FontFamily("Consolas"), 9, FontStyle.Bold);
            m_YearlyHighLabel.Size = new Size(120, 16);
            m_YearlyHighLabel.Location = new Point(1, 86);
            m_YearlyHighLabel.TextAlign = ContentAlignment.MiddleRight;
            m_YearlyHighLabel.BackColor = palette.Dark.BackColor;
            m_YearlyHighLabel.ForeColor = palette.Dark.ForeColor;
            m_YearlyHighLabel.BorderStyle = BorderStyle.None;
            m_YearlyHighLabel.MouseDown += DragAndDrop;

            m_YearlyHighValue.Font = new Font(new FontFamily("Consolas"), 9, FontStyle.Bold);
            m_YearlyHighValue.Size = new Size(70, 16);
            m_YearlyHighValue.Location = new Point(122, 86);
            m_YearlyHighValue.TextAlign = ContentAlignment.MiddleRight;
            m_YearlyHighValue.BackColor = palette.Dark.BackColor;
            m_YearlyHighValue.ForeColor = palette.Dark.ForeColor;
            m_YearlyHighValue.BorderStyle = BorderStyle.None;
            m_YearlyHighValue.MouseDown += DragAndDrop;

            Controls.Add(m_YearlyHighLabel);
            Controls.Add(m_YearlyHighValue);

            #endregion
        }

        #endregion
    }
}
