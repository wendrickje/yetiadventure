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
        /// Moves the primitive to <paramref name="destination"/>; where <paramref name="destination"/> will become the top left of the primitive.
        /// </summary>
        /// <param name="primitive">The primitive.</param>
        /// <param name="destination">The top left destination point.</param>
        void MovePrimitive(Primitive primitive, Shared.Common.Point destination);

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="_spriteBatch">The sprite batch.</param>
        /// <param name="gameTime">The game time.</param>
        void Draw(SpriteBatch _spriteBatch, GameTime gameTime, SpriteFont font);

        /// <summary>
        /// Gets the primitive under point.
        /// </summary>
        /// <param name="point">The point.</param>
        Primitive GetPrimitiveUnderPoint(Shared.Common.Point point);

        /// <summary>
        /// Gets the primitive identifier under point.
        /// </summary>
        /// <param name="mousePoint">The mouse point.</param>
        /// <returns></returns>
        Guid GetPrimitiveIdUnderPoint(Shared.Common.Point mousePoint);

        /// <summary>
        /// Moves the primitive by identifier.
        /// </summary>
        /// <param name="primitiveId">The primitive identifier.</param>
        /// <param name="point">The point.</param>
        void MovePrimitiveById(Guid primitiveId, Shared.Common.Point point);
    }
}
