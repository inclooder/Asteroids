using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Asteroids
{
    class PositionComponent : EntityComponent
    {
        public int x
        {
            get;
            set;
        }

        public int y
        {
            get;
            set;
        }




        public PositionComponent(int x, int y)
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
            this.x = (int)(vector.X);
            this.y = (int)(vector.Y);
        }
        
      
    }
}
