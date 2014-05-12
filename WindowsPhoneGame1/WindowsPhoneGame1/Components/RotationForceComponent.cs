using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    class RotationForceComponent : EntityComponent
    {
        public bool clockwise
        {
            get;
            set;
        }

        public float speed
        {
            get;
            set;
        }

        public RotationForceComponent(bool clockwise, float speed)
        {
            this.clockwise = clockwise;
            this.speed = speed;
        }
    }
}
