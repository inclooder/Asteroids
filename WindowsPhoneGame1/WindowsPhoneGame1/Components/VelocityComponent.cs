using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    class VelocityComponent : EntityComponent
    {
        public int vx
        {
            get;
            set;
        }

        public int vy
        {
            get;
            set;
        }

        public VelocityComponent(int vx, int vy)
        {
            this.vx = vx;
            this.vy = vy;
        }
    }
}
