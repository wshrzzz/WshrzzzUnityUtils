using UnityEngine;
using System;

namespace Wshrzzz.UnityUtils
{
    public class GUILabel : GUIBase
    {
        public GUILabel()
        {

        }

        public GUILabel(string text)
        {
            this.Text = text;
        }

        public GUILabel(Texture image)
        {
            this.Image = image;
        }

        public GUILabel(string text, Texture image)
        {
            this.Text = text;
            this.Image = image;
        }

        protected override void GUIDraw()
        {
            UniqueDraw(() =>
            {
                GUI.Label(DrawingRect, Content);
            });
        }
    } 
}
