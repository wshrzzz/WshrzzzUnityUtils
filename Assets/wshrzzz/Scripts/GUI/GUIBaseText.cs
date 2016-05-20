using UnityEngine;
using System;

namespace Wshrzzz.UnityUtils
{
    public abstract class GUIBaseText : GUIBase
    {
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
        {
            m_LastText = base.Text;
        }

        public GUIBaseText(string text)
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
