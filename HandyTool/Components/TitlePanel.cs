using HandyTool.Properties;

using System;
using System.Drawing;
using System.Windows.Forms;

namespace HandyTool.Components
{
    class TitlePanel : DraggablePanel
    {
        //################################################################################
        #region Fields

        private const int c_Padding = 1;

        private readonly Label m_ImageLabel = new Label();
        private readonly Label m_TitleLabel = new Label();
        private readonly Label m_CloseLabel = new Label();

        #endregion

        //################################################################################
        #region Constructor

        public TitlePanel(Control parent)
        {
            Name = "TitlePanel";
            Location = new Point(1, 1);
            Size = new Size(parent.Width - 2, 16);
            TabIndex = 0;
            TabStop = false;

            InitializeComponents();
        }

        #endregion

        //################################################################################
        #region Event Implementations

        private void CloseLabel_Click(object sender, EventArgs e)
        {
            CustomApplicationContext.Exit();
        }

        #endregion

        //################################################################################
        #region Private Implementation

        private void InitializeComponents()
        {
            #region Image Label

            //Position/Size Stuff
            m_ImageLabel.Location = new Point(0, 0);
            m_ImageLabel.Size = new Size(18, 16);

            //Alignment Stuff
            m_ImageLabel.Padding = new Padding(c_Padding);
            m_ImageLabel.TextAlign = ContentAlignment.MiddleCenter;

            //Style Stuff
            m_ImageLabel.BackgroundImage = Resources.Logo;
            m_ImageLabel.BackgroundImageLayout = ImageLayout.Center;
            m_ImageLabel.BackColor = Color.FromArgb(255, 255, 255);

            //Event Stuff
            m_ImageLabel.MouseDown += DragAndDrop;

            Controls.Add(m_ImageLabel);

            #endregion

            #region Title Label

            //Basic Stuff
            m_TitleLabel.Name = "Title";

            //Text Stuff
            m_TitleLabel.Text = @"Handy Tool Box v1.0";
            m_TitleLabel.Font = new Font(new FontFamily("Consolas"), 8, FontStyle.Bold);

            //Position/Size Stuff
            m_TitleLabel.Location = new Point(m_ImageLabel.Width, 0);
            m_TitleLabel.Size = new Size(Width - 35, 16);

            //Alignment Stuff
            m_TitleLabel.Padding = new Padding(c_Padding);
            m_TitleLabel.TextAlign = ContentAlignment.MiddleLeft;

            //Style Stuff
            m_TitleLabel.ForeColor = Color.FromArgb(61, 61, 61);
            m_TitleLabel.BackColor = Color.FromArgb(255, 255, 255);

            //Event Stuff
            m_TitleLabel.MouseDown += DragAndDrop;

            Controls.Add(m_TitleLabel);

            #endregion

            #region Close Label

            //Position/Size Stuff
            m_CloseLabel.Location = new Point(m_ImageLabel.Width + 1 + m_TitleLabel.Width, 0);
            m_CloseLabel.Size = new Size(16, 16);

            //Alignment Stuff
            m_CloseLabel.Padding = new Padding(c_Padding);
            m_CloseLabel.TextAlign = ContentAlignment.MiddleCenter;

            //Style Stuff
            m_CloseLabel.BackgroundImage = Resources.Close;
            m_CloseLabel.BackColor = Color.FromArgb(255, 255, 255);
            m_CloseLabel.Cursor = Cursors.Hand;

            //Event Stuff
            m_CloseLabel.Click += CloseLabel_Click;

            Controls.Add(m_CloseLabel);

            #endregion
        }

        #endregion
    }
}
