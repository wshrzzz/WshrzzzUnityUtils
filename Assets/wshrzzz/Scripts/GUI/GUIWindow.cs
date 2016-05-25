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
            UpdateContentRect();
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

        private void UpdateContentRect()
        {
            float left = PaddingLeft;
            float right = base.Size.x - PaddingRight;
            float top = PaddingTop;
            float bottom = base.Size.y - PaddingBottom;
            m_ContentRect = new Rect(left, top, right - left, bottom - top);
        }

        protected override void Resize()
        {
            base.Resize();
            float left = PaddingLeft;
            float right = base.Size.x - PaddingRight;
            float top = PaddingTop;
            float bottom = base.Size.y - PaddingBottom;
            m_ContentRect = new Rect(left, top, right - left, bottom - top);
        }
    } 
}
