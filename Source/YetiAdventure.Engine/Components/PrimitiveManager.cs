using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Prism.Events;
using YetiAdventure.Engine.Common;
using YetiAdventure.Engine.Interfaces;
using YetiAdventure.Shared.Common;
using YetiAdventure.Shared.Events;
using YetiAdventure.Shared.Models;

namespace YetiAdventure.Engine.Components
{
    /// <summary>
    /// manages and monitors Primitives
    /// </summary>
    public class PrimitiveManager : IPrimitiveManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrimitiveManager"/> class.
        /// </summary>
        public PrimitiveManager(IEventAggregator eventagg)
        {
            eventagg.GetEvent<PrimitiveCreatedEvent>().Subscribe(HandleOnPolygonCreatedEvent);
        }


        /// <summary>
        /// Handles the on polygon created event.
        /// </summary>
        /// <param name="obj">The <see cref="PrimitiveCreatedEventArgs"/> instance containing the event data.</param>
        private void HandleOnPolygonCreatedEvent(PrimitiveCreatedEventArgs obj)
        {
            Primitives.Add(Guid.NewGuid(), obj.NewItem);
        }

        private Dictionary<Guid, Primitive> _primitives = new Dictionary<Guid, Primitive>();

        /// <summary>
        /// Gets the primitives.
        /// </summary>
        /// <value>
        /// The primitives.
        /// </value>
        public Dictionary<Guid, Primitive> Primitives
        {
            get
            {
                return _primitives;
            }
        }



        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="gameTime">The game time.</param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, SpriteFont font)
        {

            //foreach (var prim in Primitives)
            //{
            //    var verts = prim.Value.Verticies.Select(v => v.ConvertToVector2()).ToArray();
            //    var pos = new Vector2(prim.Value.Bounds.X, prim.Value.Bounds.Y);
            //    foreach (var vert in verts)
            //    {

            //        var str = String.Format("x{0}, y{0}", vert.X, vert.Y);
            //        spriteBatch.DrawString(font, str, new Vector2(pos.X, pos.Y), Microsoft.Xna.Framework.Color.White);
            //        pos.Y = pos.Y + 25f;
            //    }
            //    spriteBatch.DrawPolygon(verts, Microsoft.Xna.Framework.Color.Red);
            //}
        }

        /// <summary>
        /// Gets the primitive.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the primitive or <c>Null</c> if not found</returns>
        public Primitive GetPrimitive(Guid id)
        {
            return Primitives.ContainsKey(id) ? Primitives[id] : null;
        }

        /// <summary>
        /// Moves the primitive to <paramref name="destination" />; where <paramref name="destination" /> will become the top left of the primitive.
        /// </summary>
        /// <param name="primitive">The primitive.</param>
        /// <param name="destination">The top left destination point.</param>
        public void MovePrimitive(Primitive primitive, Shared.Common.Point destination)
        {
            //need to get top-left use point as reference
            var previousBounds = primitive.Bounds;
            var previousPoint = new Shared.Common.Point(previousBounds.X, previousBounds.Y);
            primitive.Bounds = new Shared.Common.Rectangle(previousBounds.Height, previousBounds.Width, destination.X, destination.Y);
            var deltaX = destination.X - previousPoint.X;
            var deltaY = destination.Y - previousPoint.Y;

            //move each vert using point of reference
            foreach (var verticy in primitive.Verticies)
            {
                var vert = verticy;
                var x = (verticy.X + deltaX);
                var y = (verticy.Y + deltaY);
                vert.X = x;
                vert.Y = y;
                
            }

            primitive.Body.Position = primitive.Body.Position + new Vector2(deltaX, deltaY);
        }

        /// <summary>
        /// Gets the primitive under point.
        /// </summary>
        /// <param name="point">The point.</param>
        public Primitive GetPrimitiveUnderPoint(Shared.Common.Point point)
        {

            var kvp = Primitives.FirstOrDefault(p => p.Value.Bounds.Contains(point));
            return kvp.Value;
        }

        /// <summary>
        /// Gets the primitive under point.
        /// </summary>
        /// <param name="point">The point.</param>
        public Guid GetPrimitiveIdUnderPoint(Shared.Common.Point point)
        {

            var kvp = Primitives.FirstOrDefault(p => p.Value.Bounds.Contains(point));
            return kvp.Key;
        }

        /// <summary>
        /// Moves the primitive by identifier
        /// </summary>
        /// <param name="primitiveId">identifier</param>
        /// <param name="point">point</param>
        public void MovePrimitiveById(Guid primitiveId, Shared.Common.Point point)
        {
            var prim = GetPrimitive(primitiveId);
            MovePrimitive(prim, point);
        }
    }
}
