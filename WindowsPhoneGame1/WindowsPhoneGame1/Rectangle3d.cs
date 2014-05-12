using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Asteroids
{
    public class Rectangle3d
    {
        private Vector3 pos;
        private float width, height;

        public Rectangle3d(Vector3 pos, float width, float height)
        {
            this.pos = pos;
            this.width = width;
            this.height = height;
        }

        public Vector3 TopLeft()
        {
            return pos;
        }

        public Vector3 BottomRight()
        {
            return new Vector3(pos.X + width, pos.Y - height, 0);
        }

        public Vector3 BottomLeft()
        {
            return new Vector3(pos.X, pos.Y - height, 0);
        }

        public Vector3 TopRight()
        {
            return new Vector3(pos.X + width, pos.Y, 0);
        }

        public float getWidth()
        {
            return width;
        }

        public float getHeight()
        {
            return height;
        }
    }
}
