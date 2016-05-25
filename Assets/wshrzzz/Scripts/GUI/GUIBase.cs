using UnityEngine;
using System;
using System.Collections.Generic;

namespace Wshrzzz.UnityUtils
{
    public abstract class GUIBase : IGUIDrawable
    {
        private static int s_AllocID = 0;
        private static readonly Vector2 Screen_Size = new Vector2(Screen.width, Screen.height);

        public delegate void HandleResizeDelegate();
        public HandleResizeDelegate ResizeHandler;

        public bool Enable { get; set; }
        public string Name { get; set; }
        public int ID { get; private set; }

        // position and size
        private LocationType m_Location;
        public LocationType Location
        {
            set
            {
                m_Location = value;
                UpdateRect();
            }
        }
        private MarginType m_Margin;
        public MarginType Margin
        {
            set
            {
                m_Margin = value;
                UpdateRect();
            }
        }
        private Vector2 m_Position;
        public Vector2 Position
        {
            set
            {
                m_Position = value;
                UpdateRect();
            }
        }
        private Vector2 m_Size;
        public Vector2 Size
        {
            set
            {
                m_Size = value;
                UpdateRect();
            }
        }
        private PivotType m_Pivot;
        public PivotType Pivot
        {
            set
            {
                m_Pivot = value;
                UpdateRect();
            }
        }
        #region padding
        private float m_PaddingLeft;
        public float PaddingLeft
        {
            get
            {
                return m_PaddingLeft;
            }
            set
            {
                m_PaddingLeft = value;
                UpdateRect();
            }
        }
        private float m_PaddingRight;
        public float PaddingRight
        {
            get
            {
                return m_PaddingRight;
            }
            set
            {
                m_PaddingRight = value;
                UpdateRect();
            }
        }
        private float m_PaddingTop;
        public float PaddingTop
        {
            get
            {
                return m_PaddingTop;
            }
            set
            {
                m_PaddingTop = value;
                UpdateRect();
            }
        }
        private float m_PaddingBottom;
        public float PaddingBottom
        {
            get
            {
                return m_PaddingBottom;
            }
            set
            {
                m_PaddingBottom = value;
                UpdateRect();
            }
        }
        public float PaddingLeftRight
        {
            set
            {
                m_PaddingLeft = value;
                m_PaddingRight = value;
                UpdateRect();
            }
        }
        public float PaddingTopBottom
        {
            set
            {
                m_PaddingTop = value;
                m_PaddingBottom = value;
                UpdateRect();
            }
        }
        public float PaddingAll
        {
            set
            {
                m_PaddingLeft = value;
                m_PaddingRight = value;
                m_PaddingTop = value;
                m_PaddingBottom = value;
                UpdateRect();
            }
        }
        #endregion
        #region margin
        private float m_MarginLeft;
        public float MarginLeft
        {
            get
            {
                return m_MarginLeft;
            }
            set
            {
                m_MarginLeft = value;
                UpdateRect();
            }
        }
        private float m_MarginRight;
        public float MarginRight
        {
            get
            {
                return m_MarginRight;
            }
            set
            {
                m_MarginRight = value;
                UpdateRect();
            }
        }
        private float m_MarginTop;
        public float MarginTop
        {
            get
            {
                return m_MarginTop;
            }
            set
            {
                m_MarginTop = value;
                UpdateRect();
            }
        }
        private float m_MarginBottom;
        public float MarginBottom
        {
            get
            {
                return m_MarginBottom;
            }
            set
            {
                m_MarginBottom = value;
                UpdateRect();
            }
        }
        public float MarginLeftRight
        {
            set
            {
                m_MarginLeft = value;
                m_MarginRight = value;
                UpdateRect();
            }
        }
        public float MarginTopBottom
        {
            set
            {
                m_MarginTop = value;
                m_MarginBottom = value;
                UpdateRect();
            }
        }
        public float MarginAll
        {
            set
            {
                m_MarginLeft = value;
                m_MarginRight = value;
                m_MarginTop = value;
                m_PaddingBottom = value;
                UpdateRect();
            }
        }
        #endregion
        private Rect m_DrawingRect;
        public Rect DrawingRect
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
        private Rect m_ContentDrawingRect;
        public Rect ContentDrawingRect
        {
            get
            {
                return m_ContentDrawingRect;
            }
            set
            {
                m_ContentDrawingRect = value;
            }
        }

        // content
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

        // style
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
                UpdateRect();
            }
        }
        private List<GUIBase> m_Children = new List<GUIBase>();
        protected List<GUIBase> Children
        {
            get
            {
                return m_Children;
            }
        }

        public GUIBase()
        {
            Init();
        }

        private void Init()
        {
            this.Enable = true;
            this.Name = "";
            this.ID = GenID();

            Content = new GUIContent();
        }

        private static int GenID()
        {
            return s_AllocID++;
        }

        protected void UpdateRect()
        {
            switch (m_Location)
            {
                case LocationType.Fixed:
                    UpdateRectWithFixed();
                    break;
                case LocationType.Relative:
                    UpdateRectWithRelative();
                    break;
                default:
                    break;
            }

            UpdateContentRect();

            Resize();

            foreach (var child in Children)
            {
                child.UpdateRect();
            }
        }

        private void UpdateRectWithFixed()
        {
            switch (m_Pivot)
            {
                case PivotType.LeftTop:
                    m_DrawingRect.Set(m_Position.x, m_Position.y, m_Size.x, m_Size.y);
                    break;
                case PivotType.LeftMiddle:
                    m_DrawingRect.Set(m_Position.x, m_Position.y - m_Size.y * 0.5f, m_Size.x, m_Size.y);
                    break;
                case PivotType.LeftBottom:
                    m_DrawingRect.Set(m_Position.x, m_Position.y - m_Size.y, m_Size.x, m_Size.y);
                    break;
                case PivotType.MiddleTop:
                    m_DrawingRect.Set(m_Position.x - m_Size.x * 0.5f, m_Position.y, m_Size.x, m_Size.y);
                    break;
                case PivotType.Center:
                    m_DrawingRect.Set(m_Position.x - m_Size.x * 0.5f, m_Position.y - m_Size.y * 0.5f, m_Size.x, m_Size.y);
                    break;
                case PivotType.MiddleBottom:
                    m_DrawingRect.Set(m_Position.x - m_Size.x * 0.5f, m_Position.y - m_Size.y, m_Size.x, m_Size.y);
                    break;
                case PivotType.RightTop:
                    m_DrawingRect.Set(m_Position.x - m_Size.x, m_Position.y, m_Size.x, m_Size.y);
                    break;
                case PivotType.RightMiddle:
                    m_DrawingRect.Set(m_Position.x - m_Size.x, m_Position.y - m_Size.y * 0.5f, m_Size.x, m_Size.y);
                    break;
                case PivotType.RightBottom:
                    m_DrawingRect.Set(m_Position.x - m_Size.x, m_Position.y - m_Size.y, m_Size.x, m_Size.y);
                    break;
                default:
                    break;
            }
        }

        private void UpdateRectWithRelative()
        {
            bool fixedLeft = CheckMarginType(MarginType.Left);
            float left = fixedLeft ? m_MarginLeft : 0f;
            bool fixedRight = CheckMarginType(MarginType.Right);
            float right = fixedRight ? GetParentSize().x - m_MarginRight : GetParentSize().x;
            if (CheckMarginType(MarginType.FixedWidth))
            {
                if (fixedLeft && !fixedRight)
                {
                    right = left + m_Size.x;
                }
                else if (!fixedLeft && fixedRight)
                {
                    left = right - m_Size.x;
                }
                else if (!fixedLeft && !fixedRight)
                {
                    left = (GetParentSize().x - m_Size.x) * 0.5f;
                    right = GetParentSize().x - left;
                }
            }

            bool fixedTop = CheckMarginType(MarginType.Top);
            float top = fixedTop ? m_MarginTop : 0f;
            bool fixedBottom = CheckMarginType(MarginType.Bottom);
            float bottom = fixedBottom ? GetParentSize().y - m_MarginBottom : GetParentSize().y;
            if (CheckMarginType(MarginType.FixedHeight))
            {
                if (fixedTop && !fixedBottom)
                {
                    bottom = top + m_Size.y;
                }
                else if (!fixedTop && fixedBottom)
                {
                    top = bottom - m_Size.y;
                }
                else if (!fixedTop && !fixedBottom)
                {
                    top = (GetParentSize().y - m_Size.y) * 0.5f;
                    bottom = GetParentSize().y - top;
                }
            }

            m_DrawingRect.Set(left, top, right - left, bottom - top);
            m_Size = m_DrawingRect.size;
        }

        private void UpdatePosAndSize()
        {
            m_Size = m_DrawingRect.size;
            switch (m_Pivot)
            {
                case PivotType.LeftTop:
                    m_Position.Set(m_DrawingRect.xMin, m_DrawingRect.yMin);
                    break;
                case PivotType.LeftMiddle:
                    m_Position.Set(m_DrawingRect.xMin, m_DrawingRect.y);
                    break;
                case PivotType.LeftBottom:
                    m_Position.Set(m_DrawingRect.xMin, m_DrawingRect.yMax);
                    break;
                case PivotType.MiddleTop:
                    m_Position.Set(m_DrawingRect.x, m_DrawingRect.yMin);
                    break;
                case PivotType.Center:
                    m_Position.Set(m_DrawingRect.x, m_DrawingRect.y);
                    break;
                case PivotType.MiddleBottom:
                    m_Position.Set(m_DrawingRect.x, m_DrawingRect.yMax);
                    break;
                case PivotType.RightTop:
                    m_Position.Set(m_DrawingRect.xMax, m_DrawingRect.yMin);
                    break;
                case PivotType.RightMiddle:
                    m_Position.Set(m_DrawingRect.xMax, m_DrawingRect.y);
                    break;
                case PivotType.RightBottom:
                    m_Position.Set(m_DrawingRect.xMax, m_DrawingRect.yMax);
                    break;
                default:
                    break;
            }
        }

        private Vector2 GetParentSize()
        {
            if (m_Parent == null)
            {
                return Screen_Size;
            }
            else
            {
                return m_Parent.m_ContentDrawingRect.size;
            }
        }

        private void UpdateContentRect()
        {
            float left = m_DrawingRect.xMin + m_PaddingLeft;
            float right = m_DrawingRect.xMax - m_PaddingRight;
            float top = m_DrawingRect.yMin + m_PaddingTop;
            float bottom = m_DrawingRect.yMax - m_PaddingBottom;
            m_ContentDrawingRect.Set(left, top, right - left, bottom - top);
        }

        private bool CheckMarginType(MarginType type)
        {
            return ((int)m_Margin & (int)type) != 0;
        }

        protected virtual void Resize()
        {
            if (ResizeHandler != null)
            {
                ResizeHandler();
            }
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
            DrawChildren(m_ContentDrawingRect);

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
            if (Children.Count != 0)
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

        private void AddChild(GUIBase child)
        {
            if (!m_Children.Contains(child))
            {
                m_Children.Add(child);
            }
        }

        private void RemoveChild(GUIBase child)
        {
            if (m_Children.Contains(child))
            {
                m_Children.Remove(child);
            }
        }

        public void DetachAllChildren()
        {
            while (m_Children.Count != 0)
            {
                m_Children[0].Parent = null;
            }
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

    public enum MarginType
    {
        Fit = 0,
        Left = 1,
        Right = 2,
        Top = 4,
        Bottom  = 8,
        FixedWidth = 16,
        FixedHeight = 32,
    }

    public enum LocationType
    {
        Fixed,
        Relative,
    }

    public class GUIEventArgs : EventArgs
    {
        public DateTime Time;
        public GUIBase Base;
    }
}
