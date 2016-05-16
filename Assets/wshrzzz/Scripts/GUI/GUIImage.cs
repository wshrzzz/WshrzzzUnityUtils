using UnityEngine;
using System;

namespace Wshrzzz.UnityUtils
{
    public class GUIImage : GUIBase, IGUIDrawable
    {
        private static readonly Vector2 Default_Position = Vector2.zero;
        private static readonly Vector2 Default_Size = new Vector2(100f, 100f);
        private static readonly PivotType Default_Pivot = PivotType.LeftTop;

        private int m_MaxLength = -1;
        public int MaxLength
        {
            get
            {
                return m_MaxLength;
            }
            set
            {
                m_MaxLength = value;
            }
        }

        public GUIImage()
            : base(Default_Position, Default_Size, Default_Pivot)
        {

        }

        public GUIImage(Texture image)
            : base(Default_Position, Default_Size, Default_Pivot)
        {
            this.Image = image;
        }

        public void Draw()
        {
            UniqueDraw(() =>
            {
                GUI.DrawTexture(DrawingRect, Image);
            });
        }
    } 
}
