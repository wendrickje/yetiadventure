using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YetiAdventure.Shared;
using YetiAdventure.Shared.Interfaces;

namespace YetiAdventure.Engine.Levels
{
    /// <summary>
    /// API context for interaction with the game and the level builder
    /// </summary>
    public class LevelBuilderContext : ILevelBuilderContext
    {
        public void MovePrimitive(Primitive primitive, double x, double y)
        {
            Debug.WriteLine("primitive {0} moving {1},{2}", primitive.Guid.ToString(), x, y);
        }
    }
}
