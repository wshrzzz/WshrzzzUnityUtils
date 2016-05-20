using UnityEngine;
using System;

namespace Wshrzzz.UnityUtils
{
    public class GUIPasswordField : GUIBaseText
    {
        private static readonly char Default_Mask_Char = '*';

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
        public char MaskChar { get; set; }

        public event EventHandler<TextChangedEventArgs> TextChangedEvent;

        public GUIPasswordField()
            : base()
        {
            m_LastText = base.Text;
            MaskChar = Default_Mask_Char;
        }

        public GUIPasswordField(string text)
            : base(text)
        {
            base.Text = text;
            m_LastText = base.Text;
            MaskChar = Default_Mask_Char;
        }

        protected override void GUIDraw()
        {
            UniqueDraw(() =>
            {
                if (m_MaxLength >= 0)
                {
                    base.Text = GUI.PasswordField(DrawingRect, base.Text, MaskChar, m_MaxLength);
                }
                else
                {
                    base.Text = GUI.PasswordField(DrawingRect, base.Text, MaskChar);
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
