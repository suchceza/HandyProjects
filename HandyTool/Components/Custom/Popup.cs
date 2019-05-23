using HandyTool.Style;

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HandyTool.Components.Custom
{
    internal struct PopupItem
    {
        public string Title { get; set; }

        public string Value { get; set; }

        public PaintMode Style { get; set; }
    }

    internal sealed class Popup<T> : DraggablePanel where T : ColorBase, new()
    {
        //################################################################################
        #region Fields

        private readonly int m_DefaultTitleWidth = 120;
        private readonly int m_DefaultValueWidth = 70;
        private readonly int m_DefaultHeight = 16;

        private readonly IList<PopupItem> m_PopupItems;
        private readonly IDictionary<Label, Label> m_LabelItems = new Dictionary<Label, Label>();

        #endregion

        //################################################################################
        #region Constructor

        public Popup(List<PopupItem> items) : base(null)
        {
            m_PopupItems = items;
            InitializeComponents();
        }

        #endregion

        //################################################################################
        #region Internal Implementation

        internal void SetValues(IList<string> values)
        {
            int index = 0;
            foreach (var label in m_LabelItems)
            {
                label.Value.Text = values[index];
                index++;
            }
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
            //Adjust container panel
            Size = new Size(193, m_PopupItems.Count * 17 + 3);
            MouseDown += DragAndDrop;

            //Adjust content items
            var yCoordinate = 1;
            foreach (var item in m_PopupItems)
            {
                AdjustControl(item, yCoordinate);
                yCoordinate += 17;
            }

            foreach (var item in m_LabelItems)
            {
                Controls.Add(item.Key);
                Controls.Add(item.Value);
            }
        }

        #endregion

        //################################################################################
        #region Private Implementation

        private void AdjustControl(PopupItem item, int yCoordinate)
        {
            var titleLabel = CreateLabel($"{item.Title}:", item.Style, new Size(m_DefaultTitleWidth, m_DefaultHeight), new Point(1, yCoordinate));
            var valueLabel = CreateLabel(item.Value, item.Style, new Size(m_DefaultValueWidth, m_DefaultHeight), new Point(122, yCoordinate));

            m_LabelItems.Add(titleLabel, valueLabel);
        }

        private Label CreateLabel(string text, PaintMode paintMode, Size size, Point location)
        {
            var label = new Label
            {
                //Adjust text
                Text = text,
                Font = new Font(new FontFamily("Consolas"), 9, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleRight,

                //Adjust size and location
                Size = size,
                Location = location
            };

            //Adjust event
            label.MouseDown += DragAndDrop;

            //Adjust color
            Painter<T>.Paint(label, paintMode);

            return label;
        }

        #endregion
    }
}
