using System;
using System.Collections.Generic;
using System.Xml.Linq;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;


namespace YetiAdventure.Engine.Physics
{
    public class PhysicalObject
    {
        private Fixture mFixture = null;
        /*
        // Keep track of the physics engine body this object corresponds to.
        private Body mPhysicsBody;
        private uint mID;
        //private String particleID;
        private EngineTexture mTexture;
        private static uint sIDCounter = 0;

        // @TODO: Add particle system support
        //List<ParticleInstance> ParticleInstance = new List<ParticleInstance>();
        public PhysicalObject(XElement inElement) {
            mID = GetNewID();
            Load(inElement);
        }

        public PhysicalObject(Body inPhysicsBody, EngineTexture inTexture)
        {
            inPhysicsBody.UserData = this;
            mPhysicsBody = inPhysicsBody;
            mTexture = inTexture;
            mID = GetNewID(); 
            
        }

        private uint GetNewID()
        {
            return sIDCounter++;
        }

        public uint ID
        {
            get { return mID; }
        }

        // @TODO: Add support for particle system
        /*
        public String ParticleID
        {
          get { return particleID; } 
        }

        public List<ParticleInstance> ParticleInstances
        {   get { return ParticleInstance; }
            set { value = ParticleInstance; }
        }

        public Body PhysicsBody {
            get { return mPhysicsBody; }
        }

        public EngineTexture Texture
        {
            get { return mTexture; }
        }
        */
    }
}

