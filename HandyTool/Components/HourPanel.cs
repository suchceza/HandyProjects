using HandyTool.Components.Custom;
using HandyTool.Hour;
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
    internal class HourPanel : BackgroundWorkerPanel
    {
        //################################################################################
        #region Fields

        private Label m_HourDetails;
        private readonly TextBox m_HourText;

        private WorkingHours m_WorkingHours;
        private bool m_IsCancelled = true;

        private readonly Popup m_Popup;
        private readonly HourSummaryPopup<Blue> m_HourSummaryPopup;

        #endregion

        //################################################################################
        #region Constructor

        public HourPanel(Control parentControl) : base(parentControl)
        {
            m_HourText = new TextBox();

            InitializeComponents();

            m_HourSummaryPopup = new HourSummaryPopup<Blue>();
            m_Popup = new Popup(m_HourSummaryPopup);
            Paint += PaintBorder;

            ReadSavedDateTime();
            BackgroundWorker.RunWorkerAsync();
        }

        #endregion

        //################################################################################
        #region Events

        internal event HourUpdateCallback HourUpdated;

        #endregion

        //################################################################################
        #region Protected Implementation

        protected override void DragAndDrop(object sender, MouseEventArgs e)
        {
            //this class doesn't support drag and drop functionality
        }

        protected override void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            HourUpdated += UpdateHour;

            while (!m_IsCancelled)
            {
                CalculateElapsedTime();
                Thread.Sleep(1000);
            }

            HourUpdated -= UpdateHour;
        }

        protected override void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            m_IsCancelled = true;
        }

        protected sealed override void InitializeComponents()
        {
            TabIndex = 0;
            TabStop = false;
            Size = new Size(ParentControl.Width - 2, 22);
            Visible = Settings.Default.WorkHourSwitch;

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

        #endregion

        //################################################################################
        #region Event Implementation

        private void HourDetails_Click(object sender, EventArgs e)
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
                m_HourSummaryPopup.SetValues(m_WorkingHours);
            }
            else
            {
                m_Popup.Close();
            }
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
                m_WorkingHours = new WorkingHours(parsedTime);

                m_HourText.Text = $@"{parsedTime.Hour:00}:{parsedTime.Minute:00}:{parsedTime.Second:00}";
                m_HourText.ReadOnly = true;

                //save latest hour data
                Settings.Default.Year = parsedTime.Year;
                Settings.Default.Month = parsedTime.Month;
                Settings.Default.Day = parsedTime.Day;
                Settings.Default.Hour = parsedTime.Hour;
                Settings.Default.Minute = parsedTime.Minute;
                Settings.Default.Second = parsedTime.Second;
                Settings.Default.Save();

                m_IsCancelled = false;
                BackgroundWorker.RunWorkerAsync();
            }
        }

        #endregion

        //################################################################################
        #region Event Callbacks

        internal virtual void OnHourUpdated(TimeSpan timeSpan)
        {
            if (HourUpdated != null)
            {
                var args = new HourUpdatedEventArgs { ElapsedTime = timeSpan };

                HourUpdated(args);
            }
        }

        #endregion

        //################################################################################
        #region Private Implementation

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
                m_HourText.Text = $@"{args.ElapsedTime.Hours:00}:{args.ElapsedTime.Minutes:00}:{args.ElapsedTime.Seconds:00}";
            }
        }

        private void CalculateElapsedTime()
        {
            var elapsed = DateTime.Now.Subtract(m_WorkingHours.StartTime).Subtract(m_WorkingHours.LunchBreakHour);
            OnHourUpdated(elapsed);
        }

        private void ReadSavedDateTime()
        {
            var savedDateTime = new DateTime
            (
                Settings.Default.Year,
                Settings.Default.Month,
                Settings.Default.Day,
                Settings.Default.Hour,
                Settings.Default.Minute,
                Settings.Default.Second
            );

            if (DateTime.Now.Year == savedDateTime.Year &&
                DateTime.Now.Month == savedDateTime.Month &&
                DateTime.Now.Day == savedDateTime.Day)
            {
                m_WorkingHours = new WorkingHours(savedDateTime);
                m_IsCancelled = false;
            }
        }

        #endregion
    }
}
