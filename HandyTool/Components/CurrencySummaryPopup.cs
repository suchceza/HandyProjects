using HandyTool.Components.Custom;
using HandyTool.Currency;
using HandyTool.Style;

using System.Drawing;
using System.Windows.Forms;

namespace HandyTool.Components
{
    internal class CurrencySummaryPopup<T> : DraggablePanel where T : ColorBase, new()
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

        public CurrencySummaryPopup() : base(null)
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
        #region Protected Implementation

        protected override void DragAndDrop(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Parent.Handle, WmNclButtonDown, HtCaption, 0);
            }
        }

        protected sealed override void InitializeComponents()
        {
            //Adjust container panel
            Size = new Size(193, 105);
            MouseDown += DragAndDrop;

            //Adjust content items
            AdjustControl(m_PreviousCloseLabel, PaintMode.Light, new Size(120, 16), new Point(1, 1), "Previous Close:");
            AdjustControl(m_PreviousCloseValue, PaintMode.Light, new Size(70, 16), new Point(122, 1));
            AdjustControl(m_OpenLabel, PaintMode.Light, new Size(120, 16), new Point(1, 18), "Open:");
            AdjustControl(m_OpenValue, PaintMode.Light, new Size(70, 16), new Point(122, 18));
            AdjustControl(m_DailyLowLabel, PaintMode.Normal, new Size(120, 16), new Point(1, 35), "Daily Low:");
            AdjustControl(m_DailyLowValue, PaintMode.Normal, new Size(70, 16), new Point(122, 35));
            AdjustControl(m_DailyHighLabel, PaintMode.Normal, new Size(120, 16), new Point(1, 52), "Daily High:");
            AdjustControl(m_DailyHighValue, PaintMode.Normal, new Size(70, 16), new Point(122, 52));
            AdjustControl(m_YearlyLowLabel, PaintMode.Dark, new Size(120, 16), new Point(1, 69), "Yearly Low:");
            AdjustControl(m_YearlyLowValue, PaintMode.Dark, new Size(70, 16), new Point(122, 69));
            AdjustControl(m_YearlyHighLabel, PaintMode.Dark, new Size(120, 16), new Point(1, 86), "Yearly High:");
            AdjustControl(m_YearlyHighValue, PaintMode.Dark, new Size(70, 16), new Point(122, 86));

            Controls.Add(m_PreviousCloseLabel);
            Controls.Add(m_PreviousCloseValue);
            Controls.Add(m_OpenLabel);
            Controls.Add(m_OpenValue);
            Controls.Add(m_DailyLowLabel);
            Controls.Add(m_DailyLowValue);
            Controls.Add(m_DailyHighLabel);
            Controls.Add(m_DailyHighValue);
            Controls.Add(m_YearlyLowLabel);
            Controls.Add(m_YearlyLowValue);
            Controls.Add(m_YearlyHighLabel);
            Controls.Add(m_YearlyHighValue);
        }

        #endregion

        //################################################################################
        #region Private Implementation

        private void AdjustControl(Label label, PaintMode paintMode, Size size, Point location, string text = "N/A")
        {
            //Adjust text
            label.Text = text;
            label.Font = new Font(new FontFamily("Consolas"), 9, FontStyle.Bold);
            label.TextAlign = ContentAlignment.MiddleRight;

            //Adjust color
            Painter<T>.Paint(label, paintMode);

            //Adjust size and location
            label.Size = size;
            label.Location = location;

            //Adjust event
            label.MouseDown += DragAndDrop;
        }

        #endregion
    }
}
