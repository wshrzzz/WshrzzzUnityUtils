using UnityEngine;
using System;

namespace Wshrzzz.UnityUtils
{
    public abstract class GUIBaseText : GUIBase
    {
        private static readonly Vector2 Default_Position = Vector2.zero;
        private static readonly Vector2 Default_Size = new Vector2(100f, 100f);
        private static readonly PivotType Default_Pivot = PivotType.LeftTop;

        protected int m_MaxLength = -1;
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

        protected string m_LastText;
        
        public GUIBaseText()
            : base(Default_Position, Default_Size, Default_Pivot)
        {
            m_LastText = base.Text;
        }

        public GUIBaseText(string text)
            : base(Default_Position, Default_Size, Default_Pivot)
        {
            base.Text = text;
            m_LastText = base.Text;
        }
    }

    public class TextChangedEventArgs : GUIEventArgs
    {
        public GUIBaseText Text;
        public string LastText;
        public string CurrentText;
    }
}
