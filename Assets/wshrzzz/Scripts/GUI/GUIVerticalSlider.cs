using UnityEngine;
using System;

namespace Wshrzzz.UnityUtils
{
    public class GUIVerticalSlider : GUIBaseSlider
    {
        public float TopValue { get; set; }
        public float BottomValue { get; set; }

        public event EventHandler<ScrolledEventArgs> ScrolledEvent;

        public GUIVerticalSlider()
        {
        }

        protected override void GUIDraw()
        {
            UniqueDraw(() =>
            {
                m_Value = GUI.VerticalSlider(DrawingRect, m_Value, TopValue, BottomValue);
                if (ScrolledEvent != null && m_LastValue != m_Value)
                {
                    ScrolledEventArgs args = new ScrolledEventArgs();
                    args.Time = DateTime.Now;
                    args.Base = this;
                    args.Slider = this;
                    args.LastValue = m_LastValue;
                    args.CurrentValue = m_Value;
                    ScrolledEvent(this, args);

                    m_LastValue = m_Value;
                }
            });
        }
    }
}
