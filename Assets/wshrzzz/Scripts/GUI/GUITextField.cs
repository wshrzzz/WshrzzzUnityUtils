using UnityEngine;
using System;

namespace Wshrzzz.UnityUtils
{
    public class GUITextField : GUIBaseText
    {
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
            : base()
        {
            m_LastText = base.Text;
        }

        public GUITextField(string text)
            : base(text)
        {
            base.Text = text;
            m_LastText = base.Text;
        }

        protected override void GUIDraw()
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
                    args.Text = this;
                    args.LastText = m_LastText;
                    args.CurrentText = base.Text;
                    TextChangedEvent(this, args);

                    m_LastText = Text;
                }
            });
        }
    }
}
