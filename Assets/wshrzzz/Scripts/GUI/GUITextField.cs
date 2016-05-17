using UnityEngine;
using System;

namespace Wshrzzz.UnityUtils
{
    public class GUITextField : GUIBase, IGUIDrawable
    {
        private static readonly Vector2 Default_Position = Vector2.zero;
        private static readonly Vector2 Default_Size = new Vector2(100f, 100f);
        private static readonly PivotType Default_Pivot = PivotType.LeftTop;

        private int m_MaxLength = -1;
        public int MaxLength
        {
            get
            {
                return m_MaxLength;
            }
            set
            {
                m_MaxLength = value;
            }
        }

        private string m_LastText;
        public string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                m_LastText = value;
            }
        }

        public event EventHandler<TextChangedEventArgs> TextChangedEvent;

        public GUITextField()
            : base(Default_Position, Default_Size, Default_Pivot)
        {
            m_LastText = base.Text;
        }

        public GUITextField(string text)
            : base(Default_Position, Default_Size, Default_Pivot)
        {
            base.Text = text;
            m_LastText = base.Text;
        }

        public void Draw()
        {
            UniqueDraw(() =>
            {
                if (m_MaxLength >= 0)
                {
                    base.Text = GUI.TextField(DrawingRect, base.Text, m_MaxLength);
                }
                else
                {
                    base.Text = GUI.TextField(DrawingRect, base.Text);
                }
                if (TextChangedEvent != null && base.Text != m_LastText)
                {
                    TextChangedEventArgs args = new TextChangedEventArgs();
                    args.Time = DateTime.Now;
                    args.Base = this;
                    args.TextField = this;
                    args.LastText = m_LastText;
                    args.CurrentText = base.Text;
                    TextChangedEvent(this, args);

                    m_LastText = Text;
                }
            });
        }
    }

    public class TextChangedEventArgs : GUIEventArgs
    {
        public GUITextField TextField;
        public string LastText;
        public string CurrentText;
    }
}
