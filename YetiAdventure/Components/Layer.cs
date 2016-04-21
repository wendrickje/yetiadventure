using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YetiAdventure.Interfaces;

namespace YetiAdventure.Components
{
    public class Layer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="zindex"></param>
        public Layer(LayerIndex zindex)
        {
            LayerIndex = zindex;
        }

        /// <summary>
        /// where the layer is
        /// </summary>
        public LayerIndex LayerIndex { get; private set; }

        /// <summary>
        /// layer hue
        /// </summary>
        public Color Hue { get; set; }


        List<IDrawableObject> _children;

        /// <summary>
        /// DrawableObjects on this layer
        /// </summary>
        public List<IDrawableObject> Children { get { return _children ?? (_children = new List<IDrawableObject>()); } }

      

    }

    public enum LayerIndex
    {
        Foreground = 0,
        Middleground = 1,
        Background = 2,
        Backdrop = 4
    }
}
