using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Asteroids
{
    class PositionComponent : EntityComponent
    {
        public float x
        {
            get;
            set;
        }

        public float y
        {
            get;
            set;
        }




        public PositionComponent(float x, float y)
            : base()
        {
            this.x = x;
            this.y = y;
        }

        public PositionComponent(Point pos) :base()
        {
            
            this.x = pos.X;
            this.y = pos.Y;
        }

        public Vector2 getPosition()
        {
            return new Vector2(x, y);
        }

        public void setPosition(Vector2 vector)
        {
            this.x = (float)(vector.X);
            this.y = (float)(vector.Y);
        }
        
      
    }
}
