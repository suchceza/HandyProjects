using HandyTool.Components.BasePanels;
using HandyTool.Style;
using HandyTool.Style.Colors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HandyTool.Components.CustomPanels
{
    internal sealed class ProcessStarterPopup<T> : DraggablePanel where T : ColorBase, new()
    {
        //################################################################################
        #region Fields

        private readonly Label m_BranchLabel;
        private readonly Label m_ApplicationLabel;
        private readonly Label m_EditionLabel;
        private readonly ComboBox m_BranchSelector;
        private readonly ComboBox m_ApplicationSelector;
        private readonly ComboBox m_EditionSelector;

        #endregion

        //################################################################################
        #region Constructor

        public ProcessStarterPopup() : base(null)
        {
            m_BranchLabel = new Label();
            m_ApplicationLabel = new Label();
            m_EditionLabel = new Label();
            m_BranchSelector = new ComboBox();
            m_ApplicationSelector = new ComboBox();
            m_EditionSelector = new ComboBox();

            InitializeComponents();
        }

        #endregion

        internal PopupContainer PopupContainer { get; set; }

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
            Size = new Size(275, 73);

            #region Branch Label

            //Basic Stuff
            m_BranchLabel.Name = "Branch Label";

            //Text Stuff
            m_BranchLabel.Text = "Branch:";
            m_BranchLabel.Font = new Font(new FontFamily("Consolas"), 9, FontStyle.Bold);

            //Position/Size Stuff
            m_BranchLabel.Location = new Point(2, 2);
            m_BranchLabel.Size = new Size(120, 22);

            //Alignment Stuff
            m_BranchLabel.TextAlign = ContentAlignment.MiddleRight;

            //Style Stuff
            Painter<T>.Paint(m_BranchLabel, PaintMode.Normal);

            m_BranchLabel.MouseDown += DragAndDrop;

            Controls.Add(m_BranchLabel);

            #endregion

            #region Branch Selector ComboBox

            //Basic Stuff
            m_BranchSelector.Name = "Branch Selector Label";
            m_BranchSelector.FlatStyle = FlatStyle.Standard;
            m_BranchSelector.DropDownStyle = ComboBoxStyle.DropDownList;

            //Text Stuff
            //todo: add branch collector method here
            m_BranchSelector.Items.Add("TU5_OddFix");
            m_BranchSelector.Items.Add("TU41_OddFix");
            m_BranchSelector.Items.Add("TU51_OddFix");
            m_BranchSelector.Items.Add("WM5_WinCC_HW_Work");
            m_BranchSelector.Font = new Font(new FontFamily("Consolas"), 9, FontStyle.Bold);

            //Position/Size Stuff
            m_BranchSelector.Location = new Point(123, 2);
            m_BranchSelector.Size = new Size(150, 20);

            //Alignment Stuff
            m_BranchSelector.Padding = new Padding(3);

            //Style Stuff
            Painter<T>.Paint(m_BranchSelector, PaintMode.Normal);

            m_BranchSelector.SelectedValueChanged += SelectionChanged;

            Controls.Add(m_BranchSelector);

            #endregion

            #region Application Label

            //Basic Stuff
            m_ApplicationLabel.Name = "Application Label";

            //Text Stuff
            m_ApplicationLabel.Text = "Application:";
            m_ApplicationLabel.Font = new Font(new FontFamily("Consolas"), 9, FontStyle.Bold);

            //Position/Size Stuff
            m_ApplicationLabel.Location = new Point(2, 25);
            m_ApplicationLabel.Size = new Size(120, 22);

            //Alignment Stuff
            m_ApplicationLabel.TextAlign = ContentAlignment.MiddleRight;

            //Style Stuff
            Painter<T>.Paint(m_ApplicationLabel, PaintMode.Normal);

            m_ApplicationLabel.MouseDown += DragAndDrop;

            Controls.Add(m_ApplicationLabel);

            #endregion

            #region Application Selector ComboBox

            //Basic Stuff
            m_ApplicationSelector.Name = "Application Selector Label";
            m_ApplicationSelector.FlatStyle = FlatStyle.Standard;
            m_ApplicationSelector.DropDownStyle = ComboBoxStyle.DropDownList;

            //Text Stuff
            //todo: add application collector method here
            m_ApplicationSelector.Items.Add("Portal.exe");
            m_ApplicationSelector.Items.Add("File.Utility.exe");
            m_ApplicationSelector.Items.Add("File.Storage.Server.exe");
            m_ApplicationSelector.Items.Add("Tray.Monitor.exe");
            m_ApplicationSelector.Font = new Font(new FontFamily("Consolas"), 9, FontStyle.Bold);

            //Position/Size Stuff
            m_ApplicationSelector.Location = new Point(123, 25);
            m_ApplicationSelector.Size = new Size(150, 20);

            //Alignment Stuff
            m_ApplicationSelector.Padding = new Padding(3);

            //Style Stuff
            Painter<T>.Paint(m_ApplicationSelector, PaintMode.Normal);

            m_ApplicationSelector.SelectedValueChanged += SelectionChanged;

            Controls.Add(m_ApplicationSelector);

            #endregion

            #region Edition Label

            //Basic Stuff
            m_EditionLabel.Name = "Edition Label";

            //Text Stuff
            m_EditionLabel.Text = "Edition:";
            m_EditionLabel.Font = new Font(new FontFamily("Consolas"), 9, FontStyle.Bold);

            //Position/Size Stuff
            m_EditionLabel.Location = new Point(2, 48);
            m_EditionLabel.Size = new Size(120, 22);

            //Alignment Stuff
            m_EditionLabel.TextAlign = ContentAlignment.MiddleRight;

            //Style Stuff
            Painter<T>.Paint(m_EditionLabel, PaintMode.Normal);

            m_EditionLabel.MouseDown += DragAndDrop;

            Controls.Add(m_EditionLabel);

            #endregion

            #region Edition Selector ComboBox

            //todo: this combobox will be selectable only TIA Portal is selected as application

            //Basic Stuff
            m_EditionSelector.Name = "Edition Selector Label";
            m_EditionSelector.FlatStyle = FlatStyle.Standard;
            m_EditionSelector.DropDownStyle = ComboBoxStyle.DropDownList;

            //Text Stuff
            //todo: add edition collector method here
            m_EditionSelector.Items.Add("Professional");
            m_EditionSelector.Items.Add("S7 Safety");
            m_EditionSelector.Items.Add("SPL Basic");
            m_EditionSelector.Items.Add("WinCC Prof");
            m_EditionSelector.Font = new Font(new FontFamily("Consolas"), 9, FontStyle.Bold);

            //Position/Size Stuff
            m_EditionSelector.Location = new Point(123, 48);
            m_EditionSelector.Size = new Size(150, 20);

            //Alignment Stuff
            m_EditionSelector.Padding = new Padding(3);

            //Style Stuff
            Painter<T>.Paint(m_EditionSelector, PaintMode.Normal);

            m_EditionSelector.SelectedValueChanged += SelectionChanged;

            Controls.Add(m_EditionSelector);

            #endregion
        }

        #endregion

        private void SelectionChanged(object sender, EventArgs e)
        {
            PopupContainer.Show();
        }
    }
}
