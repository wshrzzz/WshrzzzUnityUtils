using UnityEngine;
using System;
using System.Collections.Generic;

namespace Wshrzzz.UnityUtils
{
    public abstract class GUIBase
    {
        private static int s_AllocID = 0;

        public string Name { get; set; }
        public int ID { get; private set; }

        private Vector2 m_Position;
        public Vector2 Position
        {
            get
            {
                return m_Position;
            }
            set
            {
                m_Position = value;
                UpdateRect();
            }
        }
        private Vector2 m_Size;
        public Vector2 Size
        {
            get
            {
                return m_Size;
            }
            set
            {
                m_Size = value;
                UpdateRect();
            }
        }
        private PivotType m_Pivot;
        public PivotType Pivot
        {
            get
            {
                return m_Pivot;
            }
            set
            {
                m_Pivot = value;
                UpdateRect();
            }
        }
        private Rect m_DrawingRect;
        protected Rect DrawingRect
        {
            get
            {
                return m_DrawingRect;
            }
            set
            {
                m_DrawingRect = value;
                UpdatePosAndSize();
            }
        }

        public GUIContent Content { get; set; }
        public string Text
        {
            get
            {
                return Content.text;
            }
            set
            {
                Content.text = value;
            }
        }
        public Texture Image
        {
            get
            {
                return Content.image;
            }
            set
            {
                Content.image = value;
            }
        }

        public Color Color = new Color(1f, 1f, 1f, 1f);

        private GUIBase m_Parent;
        public GUIBase Parent
        {
            get
            {
                return m_Parent;
            }
            set
            {
                if (m_Parent != null)
                {
                    m_Parent.RemoveChild(this);
                }
                if (value != null)
                {
                    m_Parent = value;
                    m_Parent.AddChild(this);
                }
            }
        }
        private List<GUIBase> m_Children = new List<GUIBase>();

        public GUIBase(Vector2 position, Vector2 size, PivotType pivot)
        {
            Init(position, size, pivot);
        }

        private void Init(Vector2 position, Vector2 size, PivotType pivot)
        {
            this.Name = "";
            this.ID = GenID();

            this.m_Position = position;
            this.m_Size = size;
            this.m_Pivot = pivot;
            UpdateRect();

            Content = new GUIContent();
        }

        private static int GenID()
        {
            return s_AllocID++;
        }

        private void UpdateRect()
        {
            switch (Pivot)
            {
                case PivotType.LeftTop:
                    DrawingRect = new Rect(Position.x, Position.y, Size.x, Size.y);
                    break;
                case PivotType.LeftMiddle:
                    DrawingRect = new Rect(Position.x, Position.y - Size.y * 0.5f, Size.x, Size.y);
                    break;
                case PivotType.LeftBottom:
                    DrawingRect = new Rect(Position.x, Position.y - Size.y, Size.x, Size.y);
                    break;
                case PivotType.MiddleTop:
                    DrawingRect = new Rect(Position.x - Size.x * 0.5f, Position.y, Size.x, Size.y);
                    break;
                case PivotType.Center:
                    DrawingRect = new Rect(Position.x - Size.x * 0.5f, Position.y - Size.y * 0.5f, Size.x, Size.y);
                    break;
                case PivotType.MiddleBottom:
                    DrawingRect = new Rect(Position.x - Size.x * 0.5f, Position.y - Size.y, Size.x, Size.y);
                    break;
                case PivotType.RightTop:
                    DrawingRect = new Rect(Position.x - Size.x, Position.y, Size.x, Size.y);
                    break;
                case PivotType.RightMiddle:
                    DrawingRect = new Rect(Position.x - Size.x, Position.y - Size.y * 0.5f, Size.x, Size.y);
                    break;
                case PivotType.RightBottom:
                    DrawingRect = new Rect(Position.x - Size.x, Position.y - Size.y, Size.x, Size.y);
                    break;
                default:
                    break;
            }
        }

        private void UpdatePosAndSize()
        {
            switch (Pivot)
            {
                case PivotType.LeftTop:
                    m_Position = new Vector2(m_DrawingRect.xMin, m_DrawingRect.yMin);
                    break;
                case PivotType.LeftMiddle:
                    m_Position = new Vector2(m_DrawingRect.xMin, m_DrawingRect.center.y);
                    break;
                case PivotType.LeftBottom:
                    m_Position = new Vector2(m_DrawingRect.xMin, m_DrawingRect.yMax);
                    break;
                case PivotType.MiddleTop:
                    m_Position = new Vector2(m_DrawingRect.center.x, m_DrawingRect.yMin);
                    break;
                case PivotType.Center:
                    m_Position = m_DrawingRect.center;
                    break;
                case PivotType.MiddleBottom:
                    m_Position = new Vector2(m_DrawingRect.center.x, m_DrawingRect.yMax);
                    break;
                case PivotType.RightTop:
                    m_Position = new Vector2(m_DrawingRect.xMax, m_DrawingRect.yMin);
                    break;
                case PivotType.RightMiddle:
                    m_Position = new Vector2(m_DrawingRect.xMax, m_DrawingRect.center.y);
                    break;
                case PivotType.RightBottom:
                    m_Position = new Vector2(m_DrawingRect.xMax, m_DrawingRect.yMax);
                    break;
                default:
                    break;
            }
            m_Size = new Vector2(m_DrawingRect.width, m_DrawingRect.height);
        }

        protected void UniqueDraw(Action drawHandler)
        {
            if (drawHandler == null)
                return;

            Color guiColor = GUI.color;
            GUI.color = this.Color;

            drawHandler();
            DrawChildren(DrawingRect);

            GUI.color = guiColor;
        }

        protected void UniqueDraw(Action drawHandler, Rect childCoordinate)
        {
            if (drawHandler == null)
                return;

            Color guiColor = GUI.color;
            GUI.color = this.Color;

            drawHandler();
            DrawChildren(childCoordinate);

            GUI.color = guiColor;
        }

        private void DrawChildren(Rect childCoordinate)
        {
            if (m_Children.Count != 0)
            {
                GUI.BeginGroup(childCoordinate);

                try
                {
                    foreach (var child in m_Children)
                    {
                        if (child is IGUIDrawable)
                            (child as IGUIDrawable).Draw();
                    }
                }
                catch (InvalidOperationException)
                {

                }

                GUI.EndGroup();
            }
        }

        protected void AddChild(GUIBase child)
        {
            m_Children.Add(child);
        }

        protected void RemoveChild(GUIBase child)
        {
            m_Children.Remove(child);
        }
    }

    public enum PivotType
    {
        LeftTop,
        LeftMiddle,
        LeftBottom,
        MiddleTop,
        Center,
        MiddleBottom,
        RightTop,
        RightMiddle,
        RightBottom,
    }

    public class GUIEventArgs : EventArgs
    {
        public DateTime Time;
        public GUIBase Base;
    }
}
