using UnityEngine;
using System;

namespace Wshrzzz.UnityUtils
{
    public class GUIToggle : GUIBase
    {
        private bool m_LastToggle;
        private bool m_ToggleValue;
        public bool ToggleValue
        {
            get
            {
                return m_ToggleValue;
            }
            set
            {
                m_ToggleValue = value;
                m_LastToggle = value;
            }
        }

        public event EventHandler<ToggledEventArgs> ToggledEvent;

        public GUIToggle()
        {
            m_LastToggle = m_ToggleValue;
        }

        public GUIToggle(string text)
        {
            this.Text = text;
            m_LastToggle = m_ToggleValue;
        }

        public GUIToggle(Texture image)
        {
            this.Image = image;
            m_LastToggle = m_ToggleValue;
        }

        public GUIToggle(string text, Texture image)
        {
            this.Text = text;
            this.Image = image;
            m_LastToggle = m_ToggleValue;
        }

        protected override void GUIDraw()
        {
            UniqueDraw(() =>
            {
                m_ToggleValue = GUI.Toggle(DrawingRect, m_ToggleValue, Content);
                if (ToggledEvent != null && m_LastToggle != m_ToggleValue)
                {
                    ToggledEventArgs args = new ToggledEventArgs();
                    args.Time = DateTime.Now;
                    args.Base = this;
                    args.Toggle = this;
                    args.ToggleValue = m_ToggleValue;
                    ToggledEvent(this, args);

                    m_LastToggle = m_ToggleValue;
                }
            });
        }
    }

    public class ToggledEventArgs : GUIEventArgs
    {
        public GUIToggle Toggle;
        public bool ToggleValue;
    }
}
