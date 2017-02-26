using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Common;
using FarseerPhysics.Common.Decomposition;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpenTK.Input;
using Prism.Events;
using YetiAdventure.Engine.Common;
using YetiAdventure.Engine.Interfaces;
using YetiAdventure.Engine.Physics;
using YetiAdventure.Shared.Events;
using YetiAdventure.Shared.Models;

namespace YetiAdventure.Engine.Components.BuilderOperations
{
    /// <summary>
    /// draw polygon operation
    /// </summary>
    public class PolygonOperation : ToolOperationBase
    {
        private List<Vector2> _polygonVertices;
        private Dictionary<int, Primitive> _primitives;
        private IEventAggregator _eventAggregator;
        private IPrimitiveManager _primitiveManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonOperation"/> class.
        /// </summary>
        public PolygonOperation(IEventAggregator eventAggregator, IPrimitiveManager primitiveManager)
        {
            _polygonVertices = new List<Vector2>();
            _primitives = new Dictionary<int, Primitive>();
            _eventAggregator = eventAggregator;
            _primitiveManager = primitiveManager;
        }
        

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="args"></param>
        public override void Update(ToolOperationArgs args)
        {
            var position = args.MousePoint;
            var mouseState = args.MouseState;
            var prevousMouseState = args.PreviousMouseState;
            if (args.IsLeftMouseButtonClicked())
            {
                _polygonVertices.Add(position);
            }
            if (mouseState.RightButton == ButtonState.Pressed && prevousMouseState.RightButton == ButtonState.Released)
            {
                if (_polygonVertices.Count >= 3)
                {
                    var verts = CreateLevelPolygon(_polygonVertices.ToArray());
                    var primitive = CreatePrimitive(_polygonVertices);
                    AddPrimitive(_primitives.Count, primitive);

                    _polygonVertices.Clear();
                }
            }
        }

        /// <summary>
        /// Adds the primitive.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="primitive">The primitive.</param>
        internal void AddPrimitive(int id, Primitive primitive)
        {
            _primitives.Add(id, primitive);
            var ev = _eventAggregator.GetEvent<PrimitiveCreatedEvent>();
            ev.Publish(new PrimitiveCreatedEventArgs() { NewItem = primitive });
        }

        /// <summary>
        /// Draws the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public override void Draw(ToolOperationArgs args)
        {
        }

        /// <summary>
        /// Creates the level polygon.
        /// </summary>
        /// <param name="inVertices">The in vertices.</param>
        /// <returns></returns>
        private Vertices CreateLevelPolygon(Vector2[] inVertices)
        {
            Vertices bodyVertices = new Vertices(inVertices);

            List<Vertices> decomposedVertices = Triangulate.ConvexPartition(bodyVertices, TriangulationAlgorithm.Bayazit);
            for (int bodyIndex = 0; bodyIndex < decomposedVertices.Count; ++bodyIndex)
            {
                var levelBody = BodyFactory.CreatePolygon(PhysicsEngine.GetSingleton().PhysicsWorld, decomposedVertices[bodyIndex], 1.0f);
                levelBody.IsStatic = true;
            }
            return bodyVertices;
        }


        /// <summary>
        /// Creates the primitive.
        /// </summary>
        /// <param name="verts">The verts.</param>
        /// <returns></returns>
        public Primitive CreatePrimitive(List<Vector2> verts)
        {

            var bodyVertices = new Vertices(verts);
            Body levelBody;
            List<Vertices> decomposedVertices = Triangulate.ConvexPartition(bodyVertices, TriangulationAlgorithm.Bayazit);
            for (int bodyIndex = 0; bodyIndex < decomposedVertices.Count; ++bodyIndex)
            {
                levelBody = BodyFactory.CreatePolygon(PhysicsEngine.GetSingleton().PhysicsWorld, decomposedVertices[bodyIndex], 1.0f);
                levelBody.IsStatic = true;
            }
            var sortedX = verts.OrderBy(v => v.X);
            var sortedY = verts.OrderBy(v => v.Y);
            var leftMost = sortedX.First();
            var rightMost = sortedX.Last();
            var topMost = sortedY.First();
            var bottomMost = sortedY.Last();
            var width = rightMost.X - leftMost.X;
            var height = bottomMost.Y - topMost.Y;
            var rect = new Microsoft.Xna.Framework.Rectangle((int)leftMost.X, (int)topMost.Y, (int)width, (int)height);
            var center = rect.Center;
            var primitive = new Primitive(rect.ConvertToSharedRectangle());

            primitive.Verticies.AddRange(verts.Select(v => new Shared.Common.Point(v.X, v.Y)));
            return primitive;

        }

    }
    

}
