using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YetiAdventure.Shared.Models;

namespace YetiAdventure.Shared.Common
{
    /// <summary>
    /// Rect
    /// </summary>
    public class Rectangle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class.
        /// </summary>
        public Rectangle() : this(0, 0, 0, 0) { }


        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class.
        /// </summary>
        /// <param name="height">The height.</param>
        /// <param name="width">The width.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public Rectangle(float height, float width, float x, float y)
        {
            Height = height;
            Width = width;
            X = x;
            Y = y;
        }


        /// <summary>
        /// Gets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public float Height { get; set; }

        /// <summary>
        /// Gets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public float Width { get; set; }
        /// <summary>
        /// Gets the x.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        public float X { get; set; }
        /// <summary>
        /// Gets the y.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        public float Y { get; set; }

        /// <summary>
        /// Gets the center.
        /// </summary>
        /// <returns></returns>
        public Point Center()
        {
            return new Point(X + Width / 2, Y + Height / 2);
        }

        /// <summary>
        /// Gets the upper left point.
        /// </summary>
        /// <returns></returns>
        public Point UpperLeft()
        {
            return new Point(X, Y);
        }

        /// <summary>
        /// Gets the upper right point.
        /// </summary>
        /// <returns></returns>
        public Point UpperRight()
        {
            return new Point(X + Width, Y);
        }

        /// <summary>
        /// Gets the lower left point
        /// </summary>
        /// <returns></returns>
        public Point LowerLeft()
        {
            return new Point(X, Y + Height);
        }

        /// <summary>
        /// Gets the lower right point
        /// </summary>
        /// <returns></returns>
        public Point LowerRight()
        {
            return new Point(X + Width, Y + Height);
        }

        /// <summary>
        /// Determines whether this [contains] [the specified point].
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified point]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(Point point)
        {
            return point.X >= X &&
                point.X <= (X + Width) &&
                point.Y >= Y &&
                point.Y <= (Y + Height);
        }


        /// <summary>
        /// Determines whether this rectangle [contains] [the specified rectangle].
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified rect]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(Rectangle rect)
        {
            return (rect.X >= this.X) && (rect.Y >= this.Y) &&
                    (rect.X + rect.Width <= this.X + this.Width) && (rect.Y + rect.Height <= this.Y + this.Height);
        }


        /// <summary>
        /// Determines whether this rectangle [Intersects] [the specified rectangle].
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <returns>
        ///     <c>true</c> if [Intersects] [the specified rect]; otherwise, <c>false</c>.
        /// </returns>
        public bool Intersects(Rectangle rect)
        {
            return !((rect.X + rect.Width <= this.X) ||
                    (rect.Y + rect.Height <= this.Y) ||
                    (rect.X >= this.X + this.Width) ||
                    (rect.Y >= this.Y + this.Height));
        }
    }
}
