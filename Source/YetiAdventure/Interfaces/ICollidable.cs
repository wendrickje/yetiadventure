using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YetiAdventure.Interfaces
{
    public interface ICollidable
    {
        /// <summary>
        /// 
        /// </summary>
        Vector2 Velocity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsOnGround { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsJumping { get; set; }
    }
}
