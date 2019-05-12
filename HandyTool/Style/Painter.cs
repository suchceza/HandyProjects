using System;
using System.Windows.Forms;

namespace HandyTool.Style
{
    internal enum PaintMode
    {
        Light,
        Normal,
        Dark
    }

    internal static class Painter<T> where T : ColorBase, new()
    {
        //################################################################################
        #region Fields

        private static readonly ColorPalette<T> ColorPalette = new ColorPalette<T>();

        #endregion

        //################################################################################
        #region Internal Implementation

        internal static void Paint(Control control, PaintMode mode)
        {
            switch (mode)
            {
                case PaintMode.Light:
                    Light(control);
                    break;

                case PaintMode.Normal:
                    Normal(control);
                    break;

                case PaintMode.Dark:
                    Dark(control);
                    break;

                default:
                    throw new InvalidOperationException("Unexpected error occured on Paint");
            }
        }

        #endregion

        //################################################################################
        #region Private Implementation

        private static void Light(Control control)
        {
            control.BackColor = ColorPalette.Light.BackColor;
            control.ForeColor = ColorPalette.Light.ForeColor;
        }

        private static void Normal(Control control)
        {
            control.BackColor = ColorPalette.Normal.BackColor;
            control.ForeColor = ColorPalette.Normal.ForeColor;
        }

        private static void Dark(Control control)
        {
            control.BackColor = ColorPalette.Dark.BackColor;
            control.ForeColor = ColorPalette.Dark.ForeColor;
        }

        #endregion
    }
}
