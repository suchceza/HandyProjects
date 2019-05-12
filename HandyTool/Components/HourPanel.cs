using HandyTool.Components.Custom;
using HandyTool.Properties;
using HandyTool.Style;
using HandyTool.Style.Colors;

using System;
using System.ComponentModel;
using System.Drawing;
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

            //BackgroundWorker.RunWorkerAsync();
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
            throw new System.NotImplementedException();
        }

        protected override void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        //################################################################################
        #region Private Implementation

        private void InitializeComponents()
        {
            #region Hour Text

            //Text Stuff
            m_HourText.Text = @"00:02:45";
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

        private void PaintBorder(object sender, PaintEventArgs e)
        {
            Rectangle border = new Rectangle(new Point(0, 0), new Size(Width - 1, Height - 1));
            CreateGraphics().DrawRectangle(new Pen(Color.White, 1), border);
        }

        #endregion

        //################################################################################
        #region Event Implementation

        private void HourDetails_Click(object sender, EventArgs e)
        {

        }

        private void HourText_DoubleClick(object sender, EventArgs e)
        {
            m_HourText.ReadOnly = false;
        }

        private void HourText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                m_HourText.Text = DateTime.Now.ToLongTimeString();
                m_HourText.ReadOnly = true;
            }
        }

        #endregion
    }
}
