using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace YetiAdventure.Engine.Common
{
    /// <summary>
    /// extension for converting to shared models
    /// </summary>
    public static class ModelConverterExtensions
    {
        /// <summary>
        /// Converts the point to shared point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public static Shared.Common.Point ConvertToSharedPoint(this Point point)
        {
            var p = new Shared.Common.Point(point.X, point.Y);
            return p;
        }

        /// <summary>
        /// Converts the vector2 to shared point.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns></returns>
        public static Shared.Common.Point ConvertToSharedPoint(this Vector2 vector)
        {
            var point = new Shared.Common.Point(vector.X, vector.Y);
            return point;
        }


        /// <summary>
        /// Converts the shared point to vector2.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public static Vector2 ConvertToVector2(this Shared.Common.Point point)
        {
            var vector = new Vector2(point.X, point.Y);
            return vector;

        }
        /// <summary>
        /// Converts the rectangle to shared rectangle.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <returns></returns>
        public static Shared.Common.Rectangle ConvertToSharedRectangle(this Rectangle rect)
        {
            var box = new Shared.Common.Rectangle(rect.Height, rect.Width, rect.X, rect.Y);
            return box;
        }

        /// <summary>
        /// Converts the color to shared color.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        public static Shared.Common.Color ConvertToSharedColor(this Color color)
        {
            var cc = new Shared.Common.Color(color.R, color.G, color.B, color.A);
            return cc;
        }

        /// <summary>
        /// Converts the shared color to color.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        public static Color ConvertToColor(this Shared.Common.Color color)
        {
            var cc = new Color(color.Red, color.Green, color.Blue, color.Alpha);
            return cc;

        }
    }
}
