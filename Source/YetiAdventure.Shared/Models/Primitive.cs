using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;
using YetiAdventure.Shared.Common;

namespace YetiAdventure.Shared.Models
{
    /// <summary>
    /// a 2D primitive object
    /// </summary>
    public class Primitive
    {
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Primitive"/> class.
        /// </summary>
        /// <param name="bounds">The bounds.</param>
        /// <param name="levelBody">The level body.</param>
        public Primitive(Rectangle bounds, Body levelBody)
        {
            _body = levelBody;
            Bounds = bounds;
        }

        private List<Point> _verticies;
        private Body _body;

        /// <summary>
        /// Gets the verticies.
        /// </summary>
        /// <value>
        /// The verticies.
        /// </value>
        public List<Point> Verticies
        {
            get
            {
                return _verticies ?? (_verticies = new List<Point>());
            }
        }

        /// <summary>
        /// Gets or sets the bounds.
        /// </summary>
        /// <value>
        /// The bounds.
        /// </value>
        public Rectangle Bounds { get; set; }

        /// <summary>
        /// Gets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public Body Body { get { return _body;  } }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }
    }
}
