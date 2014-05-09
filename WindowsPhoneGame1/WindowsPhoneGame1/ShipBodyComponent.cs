using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    class ShipBodyComponent : EntityComponent
    {
        private float scale;

        public ShipBodyComponent(float scale)
        {
            this.scale = scale;
        }

        public float getScale()
        {
            return scale;
        }
    }
}
