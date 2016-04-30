using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YetiAdventure.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDrawableObject
    {
        /// <summary>
        /// position vector of the object
        /// </summary>
        Vector2 Position { get; set; }

        /// <summary>
        /// texture of the object
        /// </summary>
        Texture2D Texture { get; set; }

        /// <summary>
        /// 
        /// </summary>
        Rectangle Container { get; set; }


        /// <summary>
        /// what the object should do during game play
        /// </summary>
        /// <param name="gameTime"></param>
        void Update(GameTime gameTime);

        /// <summary>
        /// what the object should look like during game play
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);

    }
}
