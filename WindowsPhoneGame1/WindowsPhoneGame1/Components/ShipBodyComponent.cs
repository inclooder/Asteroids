using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Asteroids
{
    class ShipBodyComponent : EntityComponent
    {
        public Color color
        {
            get;
            set;
        }

        public ShipBodyComponent(Color color)
        {
            this.color = color;
        }

      
    }
}
