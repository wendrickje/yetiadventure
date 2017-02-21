using System;

using YetiAdventure.Shared;
using YetiAdventure.Shared.Common;
using YetiAdventure.Shared.Models;

namespace YetiAdventure.Shared.Interfaces
{
    /// <summary>
    /// engine provider
    /// </summary>
    public interface IEngineProvider
    {
        /// <summary>
        /// Gets the primitive.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Primitive GetPrimitive(Guid id);

        /// <summary>
        /// Gets the mouse position.
        /// </summary>
        /// <returns></returns>
        Point GetMousePosition();

        /// <summary>
        /// Draws the string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="rect">The position.</param>
        /// <param name="color">The color.</param>
        void DrawString(string value, Point position, Color color);
    }
}