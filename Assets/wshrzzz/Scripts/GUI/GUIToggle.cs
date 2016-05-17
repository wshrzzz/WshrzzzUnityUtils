using UnityEngine;
using System;

namespace Wshrzzz.UnityUtils
{
    public class GUIToggle : GUIBase, IGUIDrawable
    {
        private static readonly Vector2 Default_Position = Vector2.zero;
        private static readonly Vector2 Default_Size = new Vector2(100f, 100f);
        private static readonly PivotType Default_Pivot = PivotType.LeftTop;

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
            : base(Default_Position, Default_Size, Default_Pivot)
        {
            m_LastToggle = m_ToggleValue;
        }

        public GUIToggle(string text)
            : base(Default_Position, Default_Size, Default_Pivot)
        {
            this.Text = text;
            m_LastToggle = m_ToggleValue;
        }

        public GUIToggle(Texture image)
            : base(Default_Position, Default_Size, Default_Pivot)
        {
            this.Image = image;
            m_LastToggle = m_ToggleValue;
        }

        public GUIToggle(string text, Texture image)
            : base(Default_Position, Default_Size, Default_Pivot)
        {
            this.Text = text;
            this.Image = image;
            m_LastToggle = m_ToggleValue;
        }

        public void Draw()
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
