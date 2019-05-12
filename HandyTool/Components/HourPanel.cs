using HandyTool.Components.Custom;
using HandyTool.Hour;
using HandyTool.Properties;
using HandyTool.Style;
using HandyTool.Style.Colors;

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace HandyTool.Components
{
    internal sealed class HourPanel : BackgroundWorkerPanel
    {
        //################################################################################
        #region Fields

        private readonly Control m_Parent;

        private Label m_HourDetails;
        private readonly TextBox m_HourText;

        private HourCounter m_HourCounter;
        private bool m_IsCancelled = true;

        #endregion

        //################################################################################
        #region Constructor

        public HourPanel(Control parent)
        {
            m_Parent = parent;
            m_HourText = new TextBox();

            SetPanelPosition();
            InitializeComponents();

            Paint += PaintBorder;

            BackgroundWorker.RunWorkerAsync();
        }

        #endregion

        //################################################################################
        #region Protected Implementation

        protected override void DragAndDrop(object sender, MouseEventArgs e)
        {
            //this class doesn't support drag and drop functionality
        }

        protected override void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var stopwatch = new Stopwatch();

            while (!m_IsCancelled)
            {
                UpdateHour(null);
                Thread.Sleep(1000);
            }
        }

        protected override void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            m_IsCancelled = true;
        }

        #endregion

        //################################################################################
        #region Private Implementation

        private void InitializeComponents()
        {
            #region Hour Text

            //Text Stuff
            m_HourText.Text = @"not set";
            m_HourText.Font = new Font(new FontFamily("Consolas"), 11, FontStyle.Bold);
            m_HourText.BorderStyle = BorderStyle.None;
            m_HourText.TextAlign = HorizontalAlignment.Center;
            m_HourText.TabStop = false;
            m_HourText.ReadOnly = true;

            //Position/Size Stuff
            m_HourText.Location = new Point(2, 2);
            m_HourText.AutoSize = false;
            m_HourText.Size = new Size(141, 18);

            //Alignment Stuff
            m_HourText.Padding = new Padding(2);

            //Style Stuff
            Painter<Black>.Paint(m_HourText, PaintMode.Normal);

            m_HourText.DoubleClick += HourText_DoubleClick;
            m_HourText.KeyPress += HourText_KeyPress;

            Controls.Add(m_HourText);

            #endregion

            #region Hour Details

            m_HourDetails = new ImageLabel(this, 2)
            {
                BackgroundImage = Resources.Table,
                Size = new Size(18, 18)
            };

            //Event Stuff
            m_HourDetails.Click += HourDetails_Click;

            Controls.Add(m_HourDetails);

            #endregion
        }

        private void SetPanelPosition()
        {
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

        private DateTime TryParseClock(string value)
        {
            try
            {
                var year = DateTime.Now.Year;
                var month = DateTime.Now.Month;
                var day = DateTime.Now.Day;
                var hour = int.Parse(value.Substring(0, 2));
                var minute = int.Parse(value.Substring(2, 2));
                var second = int.Parse(value.Substring(4, 2));

                return new DateTime(year, month, day, hour, minute, second);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        private void UpdateHour(HourUpdatedEventArgs args)
        {
            if (m_HourText.InvokeRequired)
            {
                HourUpdateCallback callback = UpdateHour;
                Invoke(callback, args);
            }
            else
            {
                var difference = DateTime.Now - m_HourCounter.ShiftEndTime + m_HourCounter.WorkingHour;
                m_HourText.Text = $@"{Math.Abs(difference.Hours):00}:{Math.Abs(difference.Minutes):00}:{Math.Abs(difference.Seconds):00}";
            }
        }

        #endregion

        //################################################################################
        #region Event Implementation

        private void PaintBorder(object sender, PaintEventArgs e)
        {
            Rectangle border = new Rectangle(new Point(0, 0), new Size(Width - 1, Height - 1));
            CreateGraphics().DrawRectangle(new Pen(Color.White, 1), border);
        }

        private void HourDetails_Click(object sender, EventArgs e)
        {

        }

        private void HourText_DoubleClick(object sender, EventArgs e)
        {
            m_HourText.ReadOnly = false;
            m_HourText.SelectAll();
            m_IsCancelled = true;
        }

        private void HourText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) //enter key pressed
            {
                var parsedTime = TryParseClock(m_HourText.Text);
                m_HourCounter = new HourCounter(parsedTime);

                m_HourText.Text = $@"{parsedTime.Hour:00}:{parsedTime.Minute:00}:{parsedTime.Second:00}";
                m_HourText.ReadOnly = true;
                m_IsCancelled = false;
                BackgroundWorker.RunWorkerAsync();
            }
        }

        #endregion
    }
}
