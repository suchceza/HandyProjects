using System.Windows.Forms;

namespace HandyTool.Components.Custom
{
    internal sealed class Popup : ToolStripDropDown
    {
        public Control Content { get; set; }

        public Popup(Control content)
        {
            //Basic setup
            AutoSize = false;
            DoubleBuffered = true;
            ResizeRedraw = true;
            Padding = new Padding(0);

            Content = content;
            var host = new ToolStripControlHost(content);

            //Positioning and sizing
            Size = content.Size;
            MaximumSize = content.Size;
            MinimumSize = content.MinimumSize;
            BackColor = content.BackColor;

            //Add the host to the list
            Items.Add(host);
        }
    }
}
