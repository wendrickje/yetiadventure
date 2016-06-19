using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using YetiAdventure.Engine.Physics;

namespace YetiAdventure.Engine.Levels
{
    public class Level
    {
        private World mPhysicsWorld;
        private PhysicalAssembly mLevelAssembly;
        private NavigationGraph mNavigationGraph;

        public Level()
        {
            mPhysicsWorld = null;
            mLevelAssembly = null;
            mNavigationGraph = new NavigationGraph(this);
        }

        public void Initialize()
        {

        }

        public void Serialize(string inFilename)
        {

        }

        public void Update(GameTime inTime)
        {
            float elapsedSeconds = (float)inTime.ElapsedGameTime.TotalSeconds;
            mPhysicsWorld.Step(elapsedSeconds);
        }

        public void Draw(GameTime inTime)
        {

        }
    }
}