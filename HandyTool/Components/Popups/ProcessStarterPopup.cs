using HandyTool.Components.BasePanels;
using HandyTool.Style;
using HandyTool.TiaBranch;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace HandyTool.Components.Popups
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
        private readonly Button m_StartProcessButton;

        private readonly BranchCollector m_BranchCollector;

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
            m_StartProcessButton = new Button();

            m_BranchCollector = new BranchCollector();
            m_BranchCollector.CollectBranches();

            InitializeComponents();
        }

        #endregion

        //################################################################################
        #region Properties

        internal PopupContainer PopupContainer { get; set; }

        internal string Output { get; private set; }

        #endregion

        //################################################################################
        #region Protected Implementation

        protected override void InitializeComponents()
        {
            Size = new Size(300, 97);

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
            GetBranchNames();
            m_BranchSelector.Font = new Font(new FontFamily("Consolas"), 9, FontStyle.Bold);

            //Position/Size Stuff
            m_BranchSelector.Location = new Point(123, 2);
            m_BranchSelector.Size = new Size(175, 20);

            //Alignment Stuff
            m_BranchSelector.Padding = new Padding(3);

            //Style Stuff
            Painter<T>.Paint(m_BranchSelector, PaintMode.Normal);

            m_BranchSelector.SelectedValueChanged += BranchSelectionChanged;

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
            m_ApplicationSelector.Enabled = false;

            //Text Stuff
            m_ApplicationSelector.Font = new Font(new FontFamily("Consolas"), 9, FontStyle.Bold);

            //Position/Size Stuff
            m_ApplicationSelector.Location = new Point(123, 25);
            m_ApplicationSelector.Size = new Size(175, 20);

            //Alignment Stuff
            m_ApplicationSelector.Padding = new Padding(3);

            //Style Stuff
            Painter<T>.Paint(m_ApplicationSelector, PaintMode.Normal);

            m_ApplicationSelector.SelectedValueChanged += ApplicationSelectionChanged;

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
            m_EditionSelector.Enabled = false;

            //Text Stuff
            m_EditionSelector.Font = new Font(new FontFamily("Consolas"), 9, FontStyle.Bold);

            //Position/Size Stuff
            m_EditionSelector.Location = new Point(123, 48);
            m_EditionSelector.Size = new Size(175, 20);

            //Alignment Stuff
            m_EditionSelector.Padding = new Padding(3);

            //Style Stuff
            Painter<T>.Paint(m_EditionSelector, PaintMode.Normal);

            m_EditionSelector.SelectedValueChanged += EditionSelectionChanged;

            Controls.Add(m_EditionSelector);

            #endregion

            #region Start Process Button

            //Basic Stuff
            m_StartProcessButton.Name = "Process Start Button";
            m_StartProcessButton.Text = "Start Process";
            m_StartProcessButton.FlatStyle = FlatStyle.Flat;
            m_StartProcessButton.Enabled = false;

            //Text Stuff
            m_StartProcessButton.Font = new Font(new FontFamily("Consolas"), 9, FontStyle.Bold);

            //Position/Size Stuff
            m_StartProcessButton.Location = new Point(2, 71);
            m_StartProcessButton.Size = new Size(296, 23);

            //Alignment Stuff

            //Style Stuff
            Painter<T>.Paint(m_StartProcessButton, PaintMode.Normal);

            m_StartProcessButton.Click += StartButtonClicked;

            Controls.Add(m_StartProcessButton);

            #endregion
        }

        #endregion

        //################################################################################
        #region Event Handler Methods

        private void BranchSelectionChanged(object sender, EventArgs e)
        {
            PopupContainer.Show();

            m_ApplicationSelector.Items.Clear();

            var applications = ((BranchInfo)m_BranchSelector.SelectedItem).Applications;
            foreach (var application in applications)
            {
                m_ApplicationSelector.Items.Add(application);
            }

            m_ApplicationSelector.Enabled = true;
        }

        private void ApplicationSelectionChanged(object sender, EventArgs e)
        {
            PopupContainer.Show();
            m_StartProcessButton.Enabled = true;

            if (((ApplicationInfo)m_ApplicationSelector.SelectedItem).Name == "Portal")
            {
                m_EditionSelector.Items.Clear();

                var editions = ((BranchInfo)m_BranchSelector.SelectedItem).Editions;
                foreach (var edition in editions)
                {
                    m_EditionSelector.Items.Add(edition);
                }

                m_EditionSelector.Enabled = true;
            }
            else
            {
                m_EditionSelector.Items.Clear();
                m_EditionSelector.Enabled = false;
            }
        }

        private void EditionSelectionChanged(object sender, EventArgs e)
        {
            PopupContainer.Show();
        }

        private void StartButtonClicked(object sender, EventArgs e)
        {
            string processPath;

            if (m_EditionSelector.Enabled && m_EditionSelector.SelectedItem != null)
            {
                processPath = ((EditionInfo)m_EditionSelector.SelectedItem).Path;
            }
            else
            {
                processPath = ((ApplicationInfo)m_ApplicationSelector.SelectedItem).Path;
            }

            bool isExceptionThrown = false;
            try
            {
                Process.Start(processPath);
            }
            catch (Exception)
            {
                isExceptionThrown = true;
                //todo: command output in error case
            }
            finally
            {
                if (!isExceptionThrown)
                {
                    //todo: command output in success case
                }
            }
        }

        #endregion

        //################################################################################
        #region Private Implementation

        private void GetBranchNames()
        {
            foreach (var branch in m_BranchCollector.Branches)
            {
                m_BranchSelector.Items.Add(branch);
            }
        }

        #endregion
    }
}
