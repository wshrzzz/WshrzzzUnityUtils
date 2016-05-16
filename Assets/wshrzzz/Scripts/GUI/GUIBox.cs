using UnityEngine;
using System;

namespace Wshrzzz.UnityUtils
{
    public class GUIBox : GUIBase, IGUIDrawable
    {
        private static readonly Vector2 Default_Position = Vector2.zero;
        private static readonly Vector2 Default_Size = new Vector2(100f, 100f);
        private static readonly PivotType Default_Pivot = PivotType.LeftTop;

        public GUIBox()
            : base(Default_Position, Default_Size, Default_Pivot)
        {

        }

        public GUIBox(string text)
            : base(Default_Position, Default_Size, Default_Pivot)
        {
            this.Text = text;
        }

        public GUIBox(Texture image)
            : base(Default_Position, Default_Size, Default_Pivot)
        {
            this.Image = image;
        }

        public GUIBox(string text, Texture image)
            : base(Default_Position, Default_Size, Default_Pivot)
        {
            this.Text = text;
            this.Image = image;
        }

        public void Draw()
        {
            UniqueDraw(() =>
            {
                GUI.Box(DrawingRect, Content);
            });
        }
    } 
}
