using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;

namespace YetiAdventure.Engine.Physics
{
    public class PhysicsEngine
    {
        /// <summary>
        /// Singleton engine instance.
        /// </summary>
        private static PhysicsEngine sPhysicsInstance;

        /// <summary>
        /// The physical world where simulation occurs.
        /// </summary>
        private World mPhysicsWorld;

        private PhysicsWorldDebugView mDebugView = null;

        private PhysicsEngine()
        {
            // Earth gravity of 9.8 m/s^2
            mPhysicsWorld = new World(Vector2.UnitY * 50.8f);
        }

        public static PhysicsEngine GetSingleton()
        {
            if (sPhysicsInstance == null)
                sPhysicsInstance = new PhysicsEngine();
            return sPhysicsInstance;
        }

        public World PhysicsWorld
        {
            get { return mPhysicsWorld; }
        }

        public void Update(float inElapsed)
        {
            mPhysicsWorld.Step(inElapsed);
        }

        public void Draw(SpriteBatch inBatch)
        {
            if (mDebugView == null)
            {
                mDebugView = new PhysicsWorldDebugView(mPhysicsWorld, inBatch);
                mPhysicsWorld.SetDebugDrawer(mDebugView);
            }

            mPhysicsWorld.DrawDebugData();
        }
    }
}