using HandyTool.Components.Custom;
using HandyTool.Properties;
using HandyTool.Style;
using HandyTool.Style.Colors;

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

        private Label m_ImageLabel;
        private Label m_CloseLabel;
        private readonly Label m_TitleLabel = new Label();

        #endregion

        //################################################################################
        #region Constructor

        public TitlePanel(Control parent)
        {
            InitializeComponents(parent);
        }

        #endregion

        //################################################################################
        #region Private Implementation

        private void InitializeComponents(Control parent)
        {
            Name = "TitlePanel";
            Location = new Point(1, 1);
            Size = new Size(parent.Width - 2, 16);
            BackColor = Color.White;
            TabIndex = 0;
            TabStop = false;

            #region Image Label

            m_ImageLabel = new ImageLabel(this, 0)
            {
                BackgroundImage = Resources.Logo,
                Size = new Size(18, 16),
                Cursor = Cursors.Default
            };

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
            m_TitleLabel.Size = new Size(Width - 36, 16);

            //Alignment Stuff
            m_TitleLabel.Padding = new Padding(c_Padding);
            m_TitleLabel.TextAlign = ContentAlignment.MiddleLeft;


            //Style Stuff
            Painter<Black>.Light(m_TitleLabel);

            //Event Stuff
            m_TitleLabel.MouseDown += DragAndDrop;

            Controls.Add(m_TitleLabel);

            #endregion

            #region Close Label

            m_CloseLabel = new ImageLabel(this, 0)
            {
                BackgroundImage = Resources.Close,
                Size = new Size(16, 16)
            };

            //Event Stuff
            m_CloseLabel.Click += CloseLabel_Click;

            Controls.Add(m_CloseLabel);

            #endregion
        }

        #endregion

        //################################################################################
        #region Event Implementations

        private void CloseLabel_Click(object sender, EventArgs e)
        {
            CustomApplicationContext.Exit();
        }

        #endregion
    }
}
