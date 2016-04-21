using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YetiAdventure.Interfaces
{
    public interface IDestructable
    {
        /// <summary>
        /// number of hits to destruct
        /// </summary>
        int HitPoints { get; set; }

        /// <summary>
        /// 
        /// </summary>
        Texture DestructionTexture { get; set; }

    }
}
