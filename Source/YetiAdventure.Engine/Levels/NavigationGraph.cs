using FarseerPhysics.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetiAdventure.Engine.Levels
{
    public class NavigationGraph
    {
        private Level mParentLevel = null;

        public NavigationGraph(Level inParentLevel)
        {
            mParentLevel = inParentLevel;
        }

        public void BuildGraph()
        {

        }
    }
}
