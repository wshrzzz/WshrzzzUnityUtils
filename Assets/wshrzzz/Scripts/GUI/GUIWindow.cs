using UnityEngine;
using System;

namespace Wshrzzz.UnityUtils
{
    public class GUIWindow : GUIBase
    {
        public bool Draggable { get; set; }

        private Rect m_ContentRect;

        public GUIWindow(string title)
        {
            this.Text = title;
            this.Draggable = true;
            Resize();
        }

        protected override void GUIDraw()
        {
            DrawingRect = GUI.Window(ID, DrawingRect, (id) =>
            {
                UniqueDraw(() =>
                    {

                    }, m_ContentRect);
                if (Draggable) GUI.DragWindow();
            }, Content);
        }

        protected override void Resize()
        {
            base.Resize();
            float left = PaddingLeft;
            float right = DrawingRect.size.x - PaddingRight;
            float top = PaddingTop;
            float bottom = DrawingRect.size.y - PaddingBottom;
            m_ContentRect.Set(left, top, right - left, bottom - top);
        }
    } 
}
