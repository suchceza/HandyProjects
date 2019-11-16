using HandyTool.Components.BasePanels;
using HandyTool.Components.CustomPanels;
using HandyTool.Components.Popups;
using HandyTool.Hour;
using HandyTool.Properties;
using HandyTool.Style;
using HandyTool.Style.Colors;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace HandyTool.Components.WorkerPanels
{
    internal class HourPanel : BackgroundWorkerPanel
    {
        //################################################################################
        #region Fields

        private readonly TimeSpan m_ReminderStartLimit = new TimeSpan(0, 30, 0);

        private readonly TextBox m_HourText;
        private ImageButton m_HourDetails;
        private ImageButton m_HourStartStop;

        private WorkingHours m_WorkingHours;
        private bool m_IsCancelled;

        private readonly PopupContainer m_Popup;
        private readonly InfoPopup<Blue> m_SummaryPopup;

        #endregion

        //################################################################################
        #region Constructor

        public HourPanel(Control parentControl) : base(parentControl)
        {
            m_HourText = new TextBox();
            m_WorkingHours = new WorkingHours();
            m_IsCancelled = Settings.Default.WorkHourStopped;

            InitializeComponents();

            m_SummaryPopup = new InfoPopup<Blue>
            (
                new List<PopupItem>
                {
                    new PopupItem{ Title = "Start", Value = "00:00:00", Style = PaintMode.Light },
                    new PopupItem{ Title = "Finish", Value = "00:00:00", Style = PaintMode.Light },
                    new PopupItem{ Title = "Deadline", Value = "00:00:00", Style = PaintMode.Dark }
                }
            );

            m_Popup = new PopupContainer(m_SummaryPopup);
            Paint += PaintBorder;

            SwitchHourStartStop(isInitialization: true);
        }

        #endregion

        //################################################################################
        #region Properties

        public string ElapsedTime => m_HourText.Text;

        private DateTime SavedDateTime => new DateTime(Settings.Default.Year,
                                                       Settings.Default.Month,
                                                       Settings.Default.Day,
                                                       Settings.Default.Hour,
                                                       Settings.Default.Minute,
                                                       Settings.Default.Second);

        #endregion

        //################################################################################
        #region Events

        internal event HourUpdateCallback HourUpdated;

        #endregion

        //################################################################################
        #region Protected Implementation

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
            StopHour();
            WriteLogsIfHappened(e.Error, "WorkHour", e.Cancelled);
        }

        protected sealed override void InitializeComponents()
        {
            Name = @"WorkHourPanel";
            TabIndex = 0;
            TabStop = false;
            Size = new Size(ParentControl.Width - 2, 22);
            Visible = Settings.Default.WorkHourSwitch;

            #region Hour Text

            //Text Stuff
            m_HourText.Text = @"N/A";
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

            m_HourDetails = new ImageButton(this, "Display work hour details")
            {
                BackgroundImage = Resources.SummaryWorkHour
            };

            //Event Stuff
            m_HourDetails.Click += HourDetails_Click;

            Controls.Add(m_HourDetails);

            #endregion

            #region Hour Start/Stop

            m_HourStartStop = new ImageButton(this, "Start/Stop hour counting")
            {
                BackgroundImage = Resources.RunProcess
            };

            //Event Stuff
            m_HourStartStop.Click += HourStartStop_Click;

            Controls.Add(m_HourStartStop);

            #endregion
        }

        #endregion

        //################################################################################
        #region Event Handler Methods

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
                m_SummaryPopup.SetValues(new List<string>
                {
                    m_WorkingHours.StartTimeString(),
                    m_WorkingHours.FinishTimeString(),
                    m_WorkingHours.DeadlineTimeString()
                });
            }
            else
            {
                m_Popup.Close();
            }
        }

        private void HourStartStop_Click(object sender, EventArgs e)
        {
            Painter<Black>.Paint(m_HourText, PaintMode.Normal);
            SwitchHourStartStop();
        }

        private void HourText_DoubleClick(object sender, EventArgs e)
        {
            //todo: hour text cannot be double clicked unless it is stopped, do isStopped check here
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

                StartHour();
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

        private void SwitchHourStartStop(bool isInitialization = false)
        {
            if (isInitialization && !IsPresentDay())
            {
                return;
            }

            var isStopped = Settings.Default.WorkHourStopped;

            if (isInitialization && IsPresentDay() && isStopped)
            {
                return;
            }

            if (!IsPresentDay() && isStopped)
            {
                ShowBalloonTip("Hour isn't set", "Please set the hour before start.", ToolTipIcon.Warning);
                return;
            }

            if (isInitialization ^ isStopped)
            {
#if DEBUG
                StartTestHour();
#else
                StartHour();
#endif
            }
            else
            {
                StopHour();
            }
        }

        private void StartTestHour()
        {
            var dateTime = DateTime.Now - TimeSpan.FromSeconds(614 * 60 + 58);
            m_WorkingHours = new WorkingHours(dateTime);

            m_HourStartStop.BackgroundImage = Resources.StopProcess;
            m_IsCancelled = false;

            Settings.Default.WorkHourStopped = m_IsCancelled;
            Settings.Default.Save();

            BackgroundWorker.RunWorkerAsync();
        }

        private void StartHour()
        {
            m_HourStartStop.BackgroundImage = Resources.StopProcess;
            m_IsCancelled = false;

            Settings.Default.WorkHourStopped = m_IsCancelled;
            Settings.Default.Save();

            ReadSavedDateTime();
            BackgroundWorker.RunWorkerAsync();
        }

        private void StopHour()
        {
            m_HourStartStop.BackgroundImage = Resources.RunProcess;
            m_IsCancelled = true;

            m_HourText.Text = @"N/A";

            Settings.Default.WorkHourStopped = m_IsCancelled;
            Settings.Default.Save();
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
                DeadlineCheck();
                m_HourText.Text = $@"{args.ElapsedTime.Hours:00}:" +
                                  $@"{args.ElapsedTime.Minutes:00}." +
                                  $@"{args.ElapsedTime.Seconds:00}";
            }
        }

        private void CalculateElapsedTime()
        {
            var elapsed = DateTime.Now.Subtract(m_WorkingHours.StartTimeLunchBreakIncluded);
            OnHourUpdated(elapsed);
        }

        private void ReadSavedDateTime()
        {
            if (IsPresentDay() && !m_IsCancelled)
            {
                m_WorkingHours = new WorkingHours(SavedDateTime);
            }
        }

        private bool IsPresentDay()
        {
            return DateTime.Now.Year == SavedDateTime.Year &&
                   DateTime.Now.Month == SavedDateTime.Month &&
                   DateTime.Now.Day == SavedDateTime.Day;
        }

        private void DeadlineCheck()
        {
            var remainingTime = m_WorkingHours.DeadlineTime.Subtract(DateTime.Now);

            if (remainingTime > m_ReminderStartLimit)
                return;

            Painter<Red>.Paint(m_HourText, PaintMode.Normal);

            var ceilRemainingMinutes = Math.Ceiling(remainingTime.TotalMinutes);
            if (ceilRemainingMinutes <= m_ReminderStartLimit.Minutes && // remaining time less than 30 minutes
                ceilRemainingMinutes % 5 == 0 && // remain every 5 minutes
                remainingTime.Seconds == 59) // show balloon tooltip only once
            {
                var tooltipIcon = ceilRemainingMinutes > 15 ? ToolTipIcon.Info : ToolTipIcon.Warning;
                ShowBalloonTip("Deadline Approaching", $"{ceilRemainingMinutes} minutes remain.", tooltipIcon);
            }
        }

        #endregion
    }
}
