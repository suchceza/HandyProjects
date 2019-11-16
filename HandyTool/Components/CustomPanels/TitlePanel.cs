using HandyTool.Components.BasePanels;
using HandyTool.Properties;
using HandyTool.Style;
using HandyTool.Style.Colors;

using System;
using System.Drawing;
using System.Windows.Forms;

namespace HandyTool.Components.CustomPanels
{
    internal sealed class TitlePanel : DraggablePanel
    {
        //################################################################################
        #region Fields

        private readonly Label m_ImageLabel;
        private readonly Label m_CloseLabel;
        private readonly Label m_TitleLabel;

        #endregion

        //################################################################################
        #region Constructor

        public TitlePanel(Control parentControl) : base(parentControl)
        {
            m_ImageLabel = new Label();
            m_CloseLabel = new Label();
            m_TitleLabel = new Label();

            InitializeComponents();
        }

        #endregion

        //################################################################################
        #region Protected Implementation

        protected override void InitializeComponents()
        {
            Name = "TitlePanel";
            Location = new Point(1, 1);
            Size = new Size(185 - 2, 18);
            Painter<Black>.Paint(this, PaintMode.Light);
            TabIndex = 0;
            TabStop = false;

            #region Image Label

            //Basic properties
            m_ImageLabel.Name = "Logo Label";

            //Style properties
            m_ImageLabel.BackgroundImage = Resources.Logo;
            m_ImageLabel.Cursor = Cursors.Default;

            //Size/Alignment properties
            m_ImageLabel.Size = new Size(16, 16);

            //Event properties
            m_ImageLabel.DoubleClick += ImageLabel_DoubleClick;

            #endregion

            #region Title Label

            //Basic properties
            m_TitleLabel.Name = "Title Label";
            m_TitleLabel.Text = "Handy Box";

            //Style properties
            Painter<Black>.Paint(m_TitleLabel, PaintMode.Light);
            m_TitleLabel.Font = new Font(new FontFamily("Consolas"), 8, FontStyle.Bold);

            //Size/Alignment properties
            m_TitleLabel.Size = new Size(Width - 36, 16);
            m_TitleLabel.TextAlign = ContentAlignment.MiddleLeft;

            //Event properties
            m_TitleLabel.MouseDown += DragAndDrop;

            #endregion

            #region Close Label

            //Basic properties
            m_CloseLabel.Name = "Close Label";

            //Style properties
            m_CloseLabel.BackgroundImage = Resources.Close;

            //Size/Alignment properties
            m_CloseLabel.Size = new Size(16, 16);

            //Event properties
            m_CloseLabel.Click += CloseLabel_Click;

            #endregion

            AddControl(m_ImageLabel, "Handy Box v1.0");
            AddControl(m_TitleLabel);
            AddControl(m_CloseLabel, "Close");
        }

        #endregion

        //################################################################################
        #region Event Handler Implementations

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
