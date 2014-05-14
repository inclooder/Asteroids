using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Asteroids
{
    class GravityComponent : EntityComponent
    {
        private Vector2 direction;
        private float speed;

        public GravityComponent(Vector2 direction, float speed)
        {
           // direction.Normalize();
            this.direction = direction;
            this.speed = speed;
        }

        public Vector2 getDirection()
        {
            return direction;
        }

        public float getSpeed()
        {
            return speed;
        }

    
    }
}
