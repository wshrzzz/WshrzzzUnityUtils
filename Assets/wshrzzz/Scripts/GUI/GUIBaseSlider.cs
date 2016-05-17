using UnityEngine;
using System;

namespace Wshrzzz.UnityUtils
{
    public abstract class GUIBaseSlider : GUIBase
    {
        protected static readonly Vector2 Default_Position = Vector2.zero;
        protected static readonly Vector2 Default_Size = new Vector2(100f, 100f);
        protected static readonly PivotType Default_Pivot = PivotType.LeftTop;

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
            : base(Default_Position, Default_Size, Default_Pivot)
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
