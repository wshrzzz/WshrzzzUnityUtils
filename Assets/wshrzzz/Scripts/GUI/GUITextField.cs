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

        public event EventHandler TextChangedEvent;

        public GUITextField()
            : base(Default_Position, Default_Size, Default_Pivot)
        {
            m_LastText = this.Text;
        }

        public GUITextField(string text)
            : base(Default_Position, Default_Size, Default_Pivot)
        {
            this.Text = text;
            m_LastText = this.Text;
        }

        public void Draw()
        {
            UniqueDraw(() =>
            {
                if (m_MaxLength >= 0)
                {
                    Text = GUI.TextField(DrawingRect, Text, m_MaxLength);
                }
                else
                {
                    Text = GUI.TextField(DrawingRect, Text);
                }
                if (TextChangedEvent != null && Text != m_LastText)
                {
                    TextChangedEvent(this, null);
                }
                m_LastText = Text;
            });
        }
    } 
}
