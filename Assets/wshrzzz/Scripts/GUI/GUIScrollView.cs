using UnityEngine;
using System;

namespace Wshrzzz.UnityUtils
{
    public class GUIScrollView : GUIBase, IGUIDrawable
    {
        private static readonly Vector2 Default_Position = Vector2.zero;
        private static readonly Vector2 Default_Size = new Vector2(100f, 100f);
        private static readonly PivotType Default_Pivot = PivotType.LeftTop;
        private static readonly Vector2 Default_Content_Size = new Vector2(100f, 100f);

        private Vector2 m_LastPosition;
        private Vector2 m_ScrollPosition;
        public Vector2 ScrollPosition
        {
            get
            {
                return m_ScrollPosition;
            }
            set
            {
                m_ScrollPosition = value;
                m_LastPosition = value;
            }
        }
        private Rect m_ContentRect;
        public Vector2 ContentSize
        {
            get
            {
                return new Vector2(m_ContentRect.width, m_ContentRect.height);
            }
            set
            {
                m_ContentRect = new Rect(0f, 0f, value.x, value.y);
            }
        }

        public GUIScrollView()
            : base(Default_Position, Default_Size, Default_Pivot)
        {
            m_LastPosition = m_ScrollPosition;
            ContentSize = Default_Content_Size;
        }

        public void Draw()
        {
            m_ScrollPosition = GUI.BeginScrollView(DrawingRect, m_ScrollPosition, m_ContentRect);
            UniqueDraw(() =>
            {

            });
            GUI.EndScrollView();
        }
    } 
}
