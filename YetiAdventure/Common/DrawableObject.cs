using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YetiAdventure.Interfaces;

namespace YetiAdventure.Common
{
    public abstract class DrawableObject : IDrawableObject
    {
        public DrawableObject(DrawableObjectType ot)
        {
            DrawableObjectType = ot;
        }


        public DrawableObjectType DrawableObjectType { get; private set; }

        public Vector2 Position { get; set; }

        public Texture2D Texture { get; set; }

        public Rectangle Container { get; set; }

        public virtual void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }

        public virtual void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Container, Color.White);
        }



    }
}
