using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetiAdventure.Engine.Utilities
{
    class GeometryUtil
    {
        //////////////////////////////////////////////////////////////////////////
        // Taken from http://www.alienryderflex.com/polygon/
        //////////////////////////////////////////////////////////////////////////
        static bool IsPointInComplexPolygon(Vector2 inPoint, Vector2[] inPolygonVertices)
        {
            int j = inPolygonVertices.Length - 1;
            bool bOddNodes = false;

            for (int i = 0; i < inPolygonVertices.Length; i++)
            {
                if (((inPolygonVertices[i].Y < inPoint.Y && inPolygonVertices[j].Y >= inPoint.Y) || (inPolygonVertices[j].Y < inPoint.Y && inPolygonVertices[i].Y >= inPoint.Y)) && (inPolygonVertices[i].X <= inPoint.X || inPolygonVertices[j].X <= inPoint.X))
                {
                    if (inPolygonVertices[i].X + (inPoint.Y - inPolygonVertices[i].Y) / (inPolygonVertices[j].Y - inPolygonVertices[i].Y) * (inPolygonVertices[j].X - inPolygonVertices[i].X) < inPoint.X)
                    {
                        bOddNodes = !bOddNodes;
                    }
                }
                j = i;
            }

            return bOddNodes;
        }
    }
}
