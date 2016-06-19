using FarseerPhysics.Collision;
using FarseerPhysics.Common.PhysicsLogic;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetiAdventure.Engine.Physics
{
    public class PhysicalVortex : PhysicsLogic
    {
        public PhysicalVortex(World world)
            : base(world, PhysicsLogicType.Vortex)
        {

        }

        public float MaxForce { get; set; }

        public void Activate(Vector2 inPosition, float inRadius, float inForce)
        {
            HashSet<Body> affectedBodies = new HashSet<Body>();

            AABB aabb;
            aabb.LowerBound = inPosition - new Vector2(inRadius);
            aabb.UpperBound = inPosition + new Vector2(inRadius);

            // Query the world for bodies within the inRadius.
            World.QueryAABB(fixture =>
            {
                if (Vector2.Distance(fixture.Body.Position, inPosition) <= inRadius)
                {
                    if (!affectedBodies.Contains(fixture.Body))
                        affectedBodies.Add(fixture.Body);
                }

                return true;
            }, ref aabb);

            ApplyVortexForces(inPosition, inRadius, inForce, affectedBodies);
        }

        private float GetPercent(float distance, float radius)
        {
            //(1-(distance/radius))^power-1
            float percent = (float)Math.Pow(1 - ((distance - radius) / radius), 2.0f) - 1;

            if (float.IsNaN(percent))
                return 0f;

            return MathHelper.Clamp(percent, 0f, 1f);
        }

        private void ApplyVortexForces(Vector2 inPosition, float inRadius, float inForce, HashSet<Body> affectedBodies)
        {
            Dictionary<Body, Vector2> forces = new Dictionary<Body, Vector2>(affectedBodies.Count);

            foreach (Body overlappingBody in affectedBodies)
            {
                if (IsActiveOn(overlappingBody))
                {
                    float distance = Vector2.Distance(inPosition, overlappingBody.Position);
                    float forcePercent = GetPercent(distance, inRadius);

                    Vector2 forceVector = (inPosition - overlappingBody.Position);
                    Vector2 inwardForce = forceVector;

                    float tempX = forceVector.X;
                    forceVector.X = forceVector.Y;
                    forceVector.Y = -tempX;

                    float scalar = 1f / (float)Math.Sqrt(forceVector.X * forceVector.X + forceVector.Y * forceVector.Y);
                    scalar *= MathHelper.Min(inForce * forcePercent, MaxForce);
                    forceVector *= scalar;
                    forceVector += (-inwardForce * scalar);
                    //forceVector = forceVector - (inwardForce * scalar);

                    overlappingBody.ApplyLinearImpulse(forceVector);
                    forces.Add(overlappingBody, forceVector);
                }
            }
        }
    }
}
