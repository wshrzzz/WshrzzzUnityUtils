using UnityEngine;
using System;

namespace Wshrzzz.UnityUtils
{
    public class GUIImage : GUIBase
    {
        public GUIImage()
        {

        }

        public GUIImage(Texture image)
        {
            this.Image = image;
        }

        protected override void GUIDraw()
        {
            UniqueDraw(() =>
            {
                GUI.DrawTexture(DrawingRect, Image);
            });
        }
    } 
}
