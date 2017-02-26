using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using YetiAdventure.Shared.Common;
using YetiAdventure.Shared.Models;

namespace YetiAdventure.Engine.Interfaces
{
    /// <summary>
    /// manages and monitors Primitives
    /// </summary>
    public interface IPrimitiveManager
    {
        /// <summary>
        /// Gets the primitive.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Primitive GetPrimitive(Guid id);

        /// <summary>
        /// Moves the primitive.
        /// </summary>
        /// <param name="primitive">The primitive.</param>
        /// <param name="point">The point.</param>
        void MovePrimitive(Primitive primitive, Shared.Common.Point point);

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="_spriteBatch">The sprite batch.</param>
        /// <param name="gameTime">The game time.</param>
        void Draw(SpriteBatch _spriteBatch, GameTime gameTime);
    }
}
