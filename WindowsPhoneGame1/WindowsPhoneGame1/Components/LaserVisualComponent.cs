using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Asteroids
{
    public class LaserVisualComponent : EntityComponent
    {

        public Color color
        {
            get;
            set;
        }
        public float length
        {
            get;
            set;
        }

        public LaserVisualComponent(Color color, float length)
        {
            this.color = color;
            this.length = length;
        }
    }
}
