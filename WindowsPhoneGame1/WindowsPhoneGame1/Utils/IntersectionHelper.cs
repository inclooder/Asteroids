using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace Asteroids
{
    class IntersectionHelper
    {
        public static bool PointInsideTriangle(Point s, Point a, Point b, Point c)
        {
            int as_x = s.X - a.X;
            int as_y = s.Y - a.Y;

            bool s_ab = (b.X - a.X) * as_y - (b.Y - a.Y) * as_x > 0;

            if ((c.X - a.X) * as_y - (c.Y - a.Y) * as_x > 0 == s_ab) return false;

            if ((c.X - b.X) * (s.Y - b.Y) - (c.Y - b.Y) * (s.X - b.X) > 0 != s_ab) return false;

            return true;
        }


        public static bool TrianglesIntersects(Point a1, Point a2, Point a3, Point b1, Point b2, Point b3)
        {
            return PointInsideTriangle(a1, b1, b2, b3) || PointInsideTriangle(a2, b1, b2, b3) || PointInsideTriangle(a3, b1, b2, b3);
        }
    }
}
