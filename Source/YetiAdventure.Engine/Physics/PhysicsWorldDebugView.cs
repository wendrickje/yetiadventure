using System;
using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using YetiAdventure.Engine.Common;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Collision;
using System.Diagnostics;

namespace YetiAdventure.Engine.Physics
{
    class PhysicsWorldDebugView : DebugViewBase
    {
        /// <summary>
        /// The spritebatch used to render the physical world.
        /// </summary>
        private SpriteBatch _spritebatch = null;

        /// <summary>
        /// The scale used to bump up the rendering length of the transform axes.
        /// </summary>
        private float _transformScale = 0.25f;

        /// <summary>
        /// Initialize the PhysicsWorldDebugView instance with the physical world to visualize.
        /// </summary>
        /// <param name="world">The physics world instance to draw.</param>
        public PhysicsWorldDebugView(World world, SpriteBatch inSpritebatch)
            : base(world)
        {
            _spritebatch = inSpritebatch;
            AppendFlags(DebugViewFlags.CenterOfMass | DebugViewFlags.Shape | DebugViewFlags.ContactPoints);
        }

        public override void DrawCircle(Vector2 center, float radius, Color inColor)
        {
            _spritebatch.DrawCircle(center, radius, 10, inColor);
        }


        public override void DrawPolygon(Vector2[] vertices, int count, Color inColor, bool closed = true)
        {
            // Step through pairs of vertices to draw a polygon in line segments.
            for (int segmentIndex = 1; segmentIndex < vertices.Length; ++segmentIndex)
            {
                Vector2 prev = vertices[segmentIndex - 1];
                Vector2 current = vertices[segmentIndex];

                _spritebatch.DrawLine(prev, current, inColor);
            }
            
            if (closed)
            {
                // Close the polygon by drawing a line from the first to the last vertex.
                _spritebatch.DrawLine(vertices[0], vertices[vertices.Length - 1], inColor);
            }
        }

        public override void DrawSegment(Vector2 start, Vector2 end, Color inColor)
        {
            _spritebatch.DrawLine(start, end, inColor);
        }

        public override void DrawPoint(Vector2 point, float radius, Color inColor)
        {
            DrawCircle(point, 0.5f, inColor);
        }

        public override void DrawSolidCircle(Vector2 center, float radius, Vector2 axis, Color inColor)
        {
            DrawCircle(center, radius, inColor);
        }

        public override void DrawSolidPolygon(Vector2[] vertices, int count, Color inColor)
        {
            DrawPolygon(vertices, count, inColor, true);
        }

        public override void DrawTransform(ref Transform transform)
        {
            // Draw the X Axis in Red.
            Vector2 xAxis = transform.q.GetXAxis();
            _spritebatch.DrawLine(transform.p, transform.p + (xAxis * _transformScale), Color.Red);

            // Draw the Y Axis in Green.
            Vector2 yAxis = transform.q.GetYAxis();
            _spritebatch.DrawLine(transform.p, transform.p + (yAxis * _transformScale), Color.Green);
        }
    }
}