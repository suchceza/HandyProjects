using System.Windows.Forms;

namespace HandyTool.Style
{
    internal static class Painter<T> where T : ColorBase, new()
    {
        private static readonly ColorPalette<T> ColorPalette = new ColorPalette<T>();

        internal static void Light(Control control)
        {
            control.BackColor = ColorPalette.Light.BackColor;
            control.ForeColor = ColorPalette.Light.ForeColor;
        }

        internal static void Normal(Control control)
        {
            control.BackColor = ColorPalette.Normal.BackColor;
            control.ForeColor = ColorPalette.Normal.ForeColor;
        }

        internal static void Dark(Control control)
        {
            control.BackColor = ColorPalette.Dark.BackColor;
            control.ForeColor = ColorPalette.Dark.ForeColor;
        }
    }
}
