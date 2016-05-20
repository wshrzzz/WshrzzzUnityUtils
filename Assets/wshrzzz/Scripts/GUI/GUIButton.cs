using UnityEngine;
using System;

namespace Wshrzzz.UnityUtils
{
    public class GUIButton : GUIBase
    {
        public event EventHandler<ButtonClickEventArgs> ButtonClickEvent;

        public GUIButton()
        {

        }

        public GUIButton(string text)
        {
            this.Text = text;
        }

        public GUIButton(Texture image)
        {
            this.Image = image;
        }

        public GUIButton(string text, Texture image)
        {
            this.Text = text;
            this.Image = image;
        }

        protected override void GUIDraw()
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
