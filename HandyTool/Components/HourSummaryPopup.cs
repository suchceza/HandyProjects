using HandyTool.Components.Custom;
using HandyTool.Hour;
using HandyTool.Style;

using System.Drawing;
using System.Windows.Forms;

namespace HandyTool.Components
{
    internal sealed class HourSummaryPopup<T> : DraggablePanel where T : ColorBase, new()
    {
        //################################################################################
        #region Fields

        private readonly Label m_StartTimeLabel = new Label();
        private readonly Label m_StartTimeValue = new Label();

        private readonly Label m_FinishTimeLabel = new Label();
        private readonly Label m_FinishTimeValue = new Label();

        private readonly Label m_DeadlineTimeLabel = new Label();
        private readonly Label m_DeadlineTimeValue = new Label();

        #endregion

        //################################################################################
        #region Constructor

        public HourSummaryPopup() : base(null)
        {
            InitializeComponents();
        }

        #endregion

        //################################################################################
        #region Internal Implementation

        internal void SetValues(WorkingHours summary)
        {
            if (summary != null)
            {
                m_StartTimeValue.Text = $@"{summary.StartTime.Hour:00}:{summary.StartTime.Minute:00}:{summary.StartTime.Second:00}";
                m_FinishTimeValue.Text = $@"{summary.FinishTime.Hour:00}:{summary.FinishTime.Minute:00}:{summary.FinishTime.Second:00}";
                m_DeadlineTimeValue.Text = $@"{summary.DeadlineTime.Hour:00}:{summary.DeadlineTime.Minute:00}:{summary.DeadlineTime.Second:00}";
            }
            else
            {
                m_StartTimeValue.Text = @"00:00:00";
                m_FinishTimeValue.Text = @"00:00:00";
                m_DeadlineTimeValue.Text = @"00:00:00";
            }
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

        protected override void InitializeComponents()
        {
            //Adjust container panel
            Size = new Size(193, 54);
            MouseDown += DragAndDrop;

            //Adjust content items
            AdjustControl(m_StartTimeLabel, PaintMode.Light, new Size(120, 16), new Point(1, 1), "Start:");
            AdjustControl(m_StartTimeValue, PaintMode.Light, new Size(70, 16), new Point(122, 1));
            AdjustControl(m_FinishTimeLabel, PaintMode.Light, new Size(120, 16), new Point(1, 18), "Finish:");
            AdjustControl(m_FinishTimeValue, PaintMode.Light, new Size(70, 16), new Point(122, 18));
            AdjustControl(m_DeadlineTimeLabel, PaintMode.Dark, new Size(120, 16), new Point(1, 35), "Deadline:");
            AdjustControl(m_DeadlineTimeValue, PaintMode.Dark, new Size(70, 16), new Point(122, 35));

            Controls.Add(m_StartTimeLabel);
            Controls.Add(m_StartTimeValue);
            Controls.Add(m_FinishTimeLabel);
            Controls.Add(m_FinishTimeValue);
            Controls.Add(m_DeadlineTimeLabel);
            Controls.Add(m_DeadlineTimeValue);
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
