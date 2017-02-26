using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public Primitive(Rectangle bounds)
        {
            Bounds = bounds;
        }

        private Guid _guid;

        /// <summary>
        /// Primitive ID
        /// /// </summary>
        public Guid Guid
        {
            get
            {
                return _guid == null || _guid == Guid.Empty ? (_guid = Guid.NewGuid()) : _guid;
            }
        }
        

        private List<Point> _verticies;

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

    }
}
