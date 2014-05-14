using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Asteroids
{
    class RotationComponent : EntityComponent
    {
        public float rotation
        {
            get;
            set;
        }

        public RotationComponent(float degree): base()
        {
            this.rotation = MathHelper.ToRadians(degree);
        }

        public RotationComponent(Vector2 direction)
        {
            if (direction == new Vector2(0, 0))
            {
                this.rotation = 0;
            }
            else
            {
                direction.Normalize();
                float angleFromVector =
                      (float)Math.Atan2(direction.X, -direction.Y);

                this.rotation = angleFromVector;
            }


        }

        

        public Matrix getRotationMatrix()
        {
            return Matrix.CreateRotationZ(rotation);
        }

    }
}
