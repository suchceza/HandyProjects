﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace HandyTool.Components.Custom
{
    internal sealed class ImageLabel : Label
    {
        //################################################################################
        #region Fields

        private readonly Control m_Parent;
        private readonly int m_Origin;
        private readonly bool m_IsSwitchable;

        private bool m_IsSwitchOn;

        #endregion

        //################################################################################
        #region Constructor

        public ImageLabel(Control parent, int origin, bool isSwitchable = false, bool isSwitchedOn = false)
        {
            m_Parent = parent;
            m_Origin = origin;
            m_IsSwitchable = isSwitchable;
            m_IsSwitchOn = isSwitchedOn;

            InitializeComponent();
        }

        #endregion

        //################################################################################
        #region Private Implementation

        private void InitializeComponent()
        {
            //Adjust style
            BackgroundImageLayout = ImageLayout.Center;
            BackColor = !m_IsSwitchOn && m_IsSwitchable ? Color.Black : Color.White;
            Cursor = Cursors.Hand;

            //Adjust position
            Location = SetLocation();

            //Adjust events
            Click += OnClick;
        }

        private Point SetLocation()
        {
            int x = m_Origin;

            foreach (Control control in m_Parent.Controls)
            {
                x += 1 + control.Width;
            }

            return new Point(x, m_Origin);
        }

        private void OnClick(object sender, EventArgs e)
        {
            if (m_IsSwitchable)
            {
                m_IsSwitchOn = !m_IsSwitchOn;
                BackColor = m_IsSwitchOn ? Color.White : Color.Black;
            }
        }

        #endregion
    }
}
