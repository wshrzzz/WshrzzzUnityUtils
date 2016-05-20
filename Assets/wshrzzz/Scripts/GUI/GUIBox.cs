using UnityEngine;
using System;

namespace Wshrzzz.UnityUtils
{
    public class GUIBox : GUIBase
    {
        public GUIBox()
        {

        }

        public GUIBox(string text)
        {
            this.Text = text;
        }

        public GUIBox(Texture image)
        {
            this.Image = image;
        }

        public GUIBox(string text, Texture image)
        {
            this.Text = text;
            this.Image = image;
        }

        protected override void GUIDraw()
        {
            UniqueDraw(() =>
            {
                GUI.Box(DrawingRect, Content);
            });
        }
    } 
}
