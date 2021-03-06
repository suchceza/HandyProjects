﻿using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HandyTool.Components.BasePanels
{
    internal abstract class DraggablePanel : CustomPanelBase
    {
        //################################################################################
        #region Constants

        private const int c_WmNclButtonDown = 0xA1;
        private const int c_HtCaption = 0x2;

        #endregion

        //################################################################################
        #region DLL Imports

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        #endregion

        //################################################################################
        #region Constructor

        protected DraggablePanel(Control parentControl) : base(parentControl)
        {

        }

        #endregion

        //################################################################################
        #region Properties

        internal static int WmNclButtonDown => c_WmNclButtonDown;

        internal static int HtCaption => c_HtCaption;

        #endregion

        //################################################################################
        #region Abstract Methods

        protected abstract void DragAndDrop(object sender, MouseEventArgs e);

        #endregion
    }
}
