using UnityEngine;
using System;
using System.Collections.Generic;

namespace Wshrzzz.UnityUtils
{
    public abstract class GUIBase : IGUIDrawable
    {
        private static int s_AllocID = 0;

        public bool Enable { get; set; }
        public string Name { get; set; }
        public int ID { get; private set; }

        private GUIBase m_BaseControl;

        private Vector2 m_Position;
        public Vector2 Position
        {
            get
            {
                return m_BaseControl.m_Position;
            }
            set
            {
                m_BaseControl.m_Position = value;
                UpdateRect();
            }
        }
        private Vector2 m_Size;
        public Vector2 Size
        {
            get
            {
                return m_BaseControl.m_Size;
            }
            set
            {
                m_BaseControl.m_Size = value;
                UpdateRect();
                Resize();
            }
        }
        private PivotType m_Pivot;
        public PivotType Pivot
        {
            get
            {
                return m_BaseControl.m_Pivot;
            }
            set
            {
                m_BaseControl.m_Pivot = value;
                UpdateRect();
            }
        }
        private Rect m_DrawingRect;
        protected Rect DrawingRect
        {
            get
            {
                return m_BaseControl.m_DrawingRect;
            }
            set
            {
                m_BaseControl.m_DrawingRect = value;
                UpdatePosAndSize();
                Resize();
            }
        }

        public GUIContent Content { get; set; }
        public string Text
        {
            get
            {
                return m_BaseControl.Content.text;
            }
            set
            {
                m_BaseControl.Content.text = value;
            }
        }
        public Texture Image
        {
            get
            {
                return m_BaseControl.Content.image;
            }
            set
            {
                m_BaseControl.Content.image = value;
            }
        }

        public Color Color = new Color(1f, 1f, 1f, 1f);

        private GUIBase m_Parent;
        public GUIBase Parent
        {
            get
            {
                return m_BaseControl.m_Parent;
            }
            set
            {
                if (m_BaseControl.m_Parent != null)
                {
                    m_BaseControl.m_Parent.RemoveChild(this);
                }
                if (value != null)
                {
                    m_BaseControl.m_Parent = value;
                    m_BaseControl.m_Parent.AddChild(this);
                }
            }
        }
        private List<GUIBase> m_Children = new List<GUIBase>();

        public GUIBase()
        {
            Init();
        }

        private void Init()
        {
            this.Enable = true;
            this.Name = "";
            this.ID = GenID();

            this.m_BaseControl = this;

            Content = new GUIContent();
        }

        private static int GenID()
        {
            return s_AllocID++;
        }

        protected void SetBaseControl(GUIBase control)
        {
            m_BaseControl = control;
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

        protected virtual void Resize()
        {

        }

        public void Draw()
        {
            if (Enable)
            {
                GUIDraw();
            }
        }

        protected virtual void GUIDraw()
        {
            UniqueDraw(() => { });
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
                        child.Draw();
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
