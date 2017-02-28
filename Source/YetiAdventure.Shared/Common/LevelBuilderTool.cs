using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetiAdventure.Shared.Common
{
    /// <summary>
    /// level builder tools
    /// </summary>
    public enum LevelBuilderTool
    {
        /// <summary>
        /// The selector tool (selects objects)
        /// </summary>
        Selector,

        /// <summary>
        /// The transform tool (resize, move, rotate polygons)
        /// </summary>
        Transform,

        /// <summary>
        /// The polygon draw tool (draws polygons)
        /// </summary>
        DrawPolygon,

        /// <summary>
        /// The panner/hand tool (moves viewport around)
        /// </summary>
        Panner,

        /// <summary>
        /// A ruler tool to measure distances in the level editor.
        /// </summary>
        Ruler,

    }
}
