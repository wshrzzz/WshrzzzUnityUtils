using UnityEngine;
using System;

namespace Wshrzzz.UnityUtils
{
    public class GUIScrollView : GUIBase
    {
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

        public GUIScrollView()
        {
            m_LastPosition = m_ScrollPosition;
        }

        protected override void GUIDraw()
        {
            m_ScrollPosition = GUI.BeginScrollView(DrawingRect, m_ScrollPosition, ContentDrawingRect);
            UniqueDraw(() =>
            {

            }, ContentDrawingRect);
            GUI.EndScrollView();
        }
    } 
}
