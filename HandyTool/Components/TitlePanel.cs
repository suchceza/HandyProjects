using HandyTool.Components.Custom;
using HandyTool.Properties;
using HandyTool.Style;
using HandyTool.Style.Colors;

using System;
using System.Drawing;
using System.Windows.Forms;

namespace HandyTool.Components
{
    internal sealed class TitlePanel : DraggablePanel
    {
        //################################################################################
        #region Fields

        private const int c_Padding = 2;

        private Label m_ImageLabel;
        private Label m_CloseLabel;
        private readonly Label m_TitleLabel = new Label();

        #endregion

        //################################################################################
        #region Constructor

        public TitlePanel(Control parentControl) : base(parentControl)
        {
            InitializeComponents();
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
            Name = "TitlePanel";
            Location = new Point(1, 1);
            Size = new Size(ParentControl.Width - 2, 18);
            BackColor = Color.White;
            TabIndex = 0;
            TabStop = false;

            #region Image Label

            m_ImageLabel = new ImageLabel(this, 0, "Handy Box v1.0")
            {
                BackgroundImage = Resources.Logo,
                Cursor = Cursors.Default
            };

            //Event Stuff
            m_ImageLabel.DoubleClick += ImageLabel_DoubleClick;

            Controls.Add(m_ImageLabel);

            #endregion

            #region Title Label

            //Basic Stuff
            m_TitleLabel.Name = "Title";

            //Text Stuff
            m_TitleLabel.Text = @"Handy Box";
            m_TitleLabel.Font = new Font(new FontFamily("Consolas"), 8, FontStyle.Bold);

            //Position/Size Stuff
            m_TitleLabel.Location = new Point(m_ImageLabel.Width, 0);
            m_TitleLabel.Size = new Size(Width - 38, 18);

            //Alignment Stuff
            m_TitleLabel.Padding = new Padding(c_Padding);
            m_TitleLabel.TextAlign = ContentAlignment.MiddleLeft;

            //Style Stuff
            Painter<Black>.Paint(m_TitleLabel, PaintMode.Light);

            //Event Stuff
            m_TitleLabel.MouseDown += DragAndDrop;

            Controls.Add(m_TitleLabel);

            #endregion

            #region Close Label

            m_CloseLabel = new ImageLabel(this, 0, "Close")
            {
                BackgroundImage = Resources.Close
            };

            //Event Stuff
            m_CloseLabel.Click += CloseLabel_Click;

            Controls.Add(m_CloseLabel);

            #endregion
        }

        #endregion

        //################################################################################
        #region Event Implementations

        private void ImageLabel_DoubleClick(object sender, EventArgs e)
        {
            ParentControl.Visible = false;
        }

        private void CloseLabel_Click(object sender, EventArgs e)
        {
            CustomApplicationContext.Exit();
        }

        #endregion
    }
}
