using HandyTool.Components.BasePanels;
using HandyTool.Style;
using HandyTool.Style.Colors;
using System.Drawing;
using System.Windows.Forms;

namespace HandyTool.Components.CustomPanels
{
    internal class AboutPanel : CustomPanelBase
    {
        //################################################################################
        #region Fields

        private readonly Label m_AboutContent;

        #endregion

        //################################################################################
        #region Constructor

        public AboutPanel(Control parentControl) : base(parentControl)
        {
            m_AboutContent = new Label();

            InitializeComponents();
        }

        #endregion

        //################################################################################
        #region Protected Implementation

        protected sealed override void InitializeComponents()
        {
            Name = "AboutPanel";
            TabIndex = 0;
            TabStop = false;
            Location = new Point(1, 1);
            Visible = false;
            VisibleChanged += AboutPanel_VisibleChanged;
            Click += CloseOnClick;

            #region Content Label

            //Text Stuff
            m_AboutContent.Font = new Font(new FontFamily("Consolas"), 8, FontStyle.Bold);
            m_AboutContent.Text = "HandyBox v1.2\n" +
                                  "Coded by Halid Ali\n" +
                                  "Copyright © 2019";

            //Position/Size Stuff
            m_AboutContent.Dock = DockStyle.Fill;

            //Alignment Stuff
            m_AboutContent.TextAlign = ContentAlignment.MiddleCenter;

            //Style Stuff
            Painter<Blue>.Paint(m_AboutContent, PaintMode.Light);

            //Event Stuff
            m_AboutContent.Click += CloseOnClick;
            m_AboutContent.Paint += PaintBorder;

            Controls.Add(m_AboutContent);

            #endregion
        }

        private void CloseOnClick(object sender, System.EventArgs e)
        {
            Visible = false;
        }

        private void AboutPanel_VisibleChanged(object sender, System.EventArgs e)
        {
            Size = new Size(ParentControl.Width - 2, ParentControl.Height - 2);
        }

        #endregion
    }
}
