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

        public Vector2 getPosition()
        {
            return new Vector2(x, y);
        }

        public void setPosition(Vector2 vector)
        {
            this.x = vector.X;
            this.y = vector.Y;
        }
        
      
    }
}
