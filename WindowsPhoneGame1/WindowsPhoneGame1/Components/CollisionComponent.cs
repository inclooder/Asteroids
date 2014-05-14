using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Asteroids
{
    public class CollisionComponent : EntityComponent
    {

        Point a
        {
            get;
            set;
        }
        Point b
        {
            get;
            set;
        }
        Point c
        {
            get;
            set;
        }

   
        public CollisionComponent(Point a, Point b, Point c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public bool collidesWith(CollisionComponent other)
        {
            return IntersectionHelper.TrianglesIntersects(a, b, c, other.a, other.b, other.c);
        }
    }
}
