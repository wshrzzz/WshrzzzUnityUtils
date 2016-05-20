using UnityEngine;
using System;

namespace Wshrzzz.UnityUtils
{
    public class GUIHorizontalSlider : GUIBaseSlider
    {
        public float LeftValue { get; set; }
        public float RightValue { get; set; }

        public event EventHandler<ScrolledEventArgs> ScrolledEvent;

        public GUIHorizontalSlider()
            : base()
        {
        }

        protected override void GUIDraw()
        {
            UniqueDraw(() =>
            {
                m_Value = GUI.HorizontalSlider(DrawingRect, m_Value, LeftValue, RightValue);
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
