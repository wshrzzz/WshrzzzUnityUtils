using UnityEngine;
using System;

namespace Wshrzzz.UnityUtils
{
    public abstract class GUIBaseSlider : GUIBase
    {
        protected float m_LastValue;
        protected float m_Value;
        public float Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                m_Value = value;
                m_LastValue = value;
            }
        }
        
        public GUIBaseSlider()
        {
            m_LastValue = m_Value;
        }
    }

    public class ScrolledEventArgs : GUIEventArgs
    {
        public GUIBaseSlider Slider;
        public float LastValue;
        public float CurrentValue;
    }
}
