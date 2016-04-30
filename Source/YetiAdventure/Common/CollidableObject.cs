using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YetiAdventure.Interfaces;

namespace YetiAdventure.Common
{
    public class CollidableObject : DrawableObject, ICollidable
    {
        public CollidableObject(DrawableObjectType ot)
            : base(ot)
        {

        }

        public Vector2 Velocity { get; set; }

        private bool _isOnGround = false;

        public bool IsOnGround
        {
            get { return _isOnGround; }
            set { _isOnGround = value; }
        }

        public bool IsJumping { get; set; }
    }
}
