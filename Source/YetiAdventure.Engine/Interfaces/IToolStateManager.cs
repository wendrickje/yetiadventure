using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpenTK.Input;
using YetiAdventure.Shared.Common;

namespace YetiAdventure.Engine.Interfaces
{
    /// <summary>
    /// tool state manager
    /// </summary>
    public interface IToolStateManager
    {
        /// <summary>
        /// Gets the active tool.
        /// </summary>
        /// <value>
        /// The active tool.
        /// </value>
        LevelBuilderTool ActiveTool { get; }

        /// <summary>
        /// Sets the active tool.
        /// </summary>
        /// <param name="tool">The tool.</param>
        void SetActiveTool(LevelBuilderTool tool);

        /// <summary>
        /// Updates the specified mouse point.
        /// </summary>
        /// <param name="mousePoint">The mouse point.</param>
        /// <param name="mouseState">State of the mouse.</param>
        void Update(Vector2 mousePoint, MouseState mouseState);

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="_spriteBatch">The sprite batch.</param>
        /// <param name="gameTime">The game time.</param>
        void Draw(SpriteBatch _spriteBatch, GameTime gameTime);
    }
}
