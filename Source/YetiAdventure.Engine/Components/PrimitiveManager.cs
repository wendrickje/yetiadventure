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
        /// Gets the primitive.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the primitive or <c>Null</c> if not found</returns>
        public Primitive GetPrimitive(Guid id)
        {
            return Primitives.ContainsKey(id) ? Primitives[id] : null;
        }

        /// <summary>
        /// Moves the primitive.
        /// </summary>
        /// <param name="primitive">The primitive.</param>
        /// <param name="point">The point.</param>
        public void MovePrimitive(Primitive primitive, Shared.Common.Point point)
        {
            var previousBounds = primitive.Bounds;
            primitive.Bounds = new Shared.Common.Rectangle(previousBounds.Height, previousBounds.Width, point.X, point.Y);
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="gameTime">The game time.</param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
            foreach (var prim in Primitives)
            {
                var verts = prim.Value.Verticies.Select(v => v.ConvertToVector2()).ToArray();
                spriteBatch.DrawPolygon(verts, Microsoft.Xna.Framework.Color.Red);
            }
        }
    }
}
