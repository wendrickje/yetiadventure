using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YetiAdventure.Shared;
using YetiAdventure.Shared.Common;
using YetiAdventure.Shared.Models;

namespace YetiAdventure.Shared.Interfaces
{
    /// <summary>
    /// level builder service
    /// </summary>
    public interface ILevelBuilderService
    {

        /// <summary>
        /// Gets the mouse position.
        /// </summary>
        /// <returns></returns>
        Point GetMousePosition();

        /// <summary>
        /// Draws the string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        void DrawString(string value, Point position, Color color);

        /// <summary>
        /// Sets the active level builder tool.
        /// </summary>
        /// <param name="tool">The tool.</param>
        void SetActiveLevelBuilderTool(LevelBuilderTool tool);
    }
}
