using UnityEngine;
using System;

namespace Wshrzzz.UnityUtils
{
    public class GUIButton : GUIBase, IGUIDrawable
    {
        private static readonly Vector2 Default_Position = Vector2.zero;
        private static readonly Vector2 Default_Size = new Vector2(100f, 100f);
        private static readonly PivotType Default_Pivot = PivotType.LeftTop;

        public event EventHandler<ButtonClickEventArgs> ButtonClickEvent;

        public GUIButton()
            : base(Default_Position, Default_Size, Default_Pivot)
        {

        }

        public GUIButton(string text)
            : base(Default_Position, Default_Size, Default_Pivot)
        {
            this.Text = text;
        }

        public GUIButton(Texture image)
            : base(Default_Position, Default_Size, Default_Pivot)
        {
            this.Image = image;
        }

        public GUIButton(string text, Texture image)
            : base(Default_Position, Default_Size, Default_Pivot)
        {
            this.Text = text;
            this.Image = image;
        }

        public void GUIDraw()
        {
            UniqueDraw(() =>
            {
                if (GUI.Button(DrawingRect, Content))
                {
                    if (ButtonClickEvent != null)
                    {
                        ButtonClickEventArgs args = new ButtonClickEventArgs();
                        args.Time = DateTime.Now;
                        args.Base = this;
                        args.Button = this;
                        ButtonClickEvent(this, args);
                    }
                }
            });
        }
    }

    public class ButtonClickEventArgs : GUIEventArgs
    {
        public GUIButton Button;
    }
}
