using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetiAdventure.Common
{
    public abstract class GameAimer
    {
        public GameAimer(Texture2D texture)
        {
            Texture = texture;
        }

        public abstract bool IsShooting();

        public abstract Vector2 Position { get; }

        public Texture2D Texture { get; private set; }
    }
}
