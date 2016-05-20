using UnityEngine;
using System;

namespace Wshrzzz.UnityUtils
{
    public class GUIWindow : GUIBase
    {
        private bool m_Draggable = true;
        public bool Draggable
        {
            get
            {
                return m_Draggable;
            }
            set
            {
                m_Draggable = value;
            }
        }

        private Rect m_ContentRect;
        public Vector2 Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                base.Position = value;
                UpdateContentRect();
            }
        }
        public Vector2 Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                base.Size = value;
                UpdateContentRect();
            }
        }
        protected Rect DrawingRect
        {
            get
            {
                return base.DrawingRect;
            }
            set
            {
                base.DrawingRect = value;
                UpdateContentRect();
            }
        }

        public GUIWindow(string title)
        {
            this.Text = title;
            UpdateContentRect();
        }

        protected override void GUIDraw()
        {
            DrawingRect = GUI.Window(ID, DrawingRect, (id) =>
            {
                UniqueDraw(() =>
                    {

                    }, m_ContentRect);
                if (m_Draggable) GUI.DragWindow();
            }, Content);
        }

        private void UpdateContentRect()
        {
            m_ContentRect = new Rect(0f, 0f, base.Size.x, base.Size.y);
        }
    } 
}
