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

        public RotationComponent(float radians): base()
        {
            this.rotation = rotation;
        }

        public Matrix getRotationMatrix()
        {
            return Matrix.CreateRotationZ(rotation);
        }

    }
}
