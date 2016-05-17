using UnityEngine;
using System;

namespace Wshrzzz.UnityUtils
{
    public class GUILabel : GUIBase, IGUIDrawable
    {
        private static readonly Vector2 Default_Position = Vector2.zero;
        private static readonly Vector2 Default_Size = new Vector2(100f, 100f);
        private static readonly PivotType Default_Pivot = PivotType.LeftTop;

        public GUILabel()
            : base(Default_Position, Default_Size, Default_Pivot)
        {

        }

        public GUILabel(string text)
            : base(Default_Position, Default_Size, Default_Pivot)
        {
            this.Text = text;
        }

        public GUILabel(Texture image)
            : base(Default_Position, Default_Size, Default_Pivot)
        {
            this.Image = image;
        }

        public GUILabel(string text, Texture image)
            : base(Default_Position, Default_Size, Default_Pivot)
        {
            this.Text = text;
            this.Image = image;
        }

        public void Draw()
        {
            UniqueDraw(() =>
            {
                GUI.Label(DrawingRect, Content);
            });
        }
    } 
}
