using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        
        
        public PositionComponent(int x, int y) : base()
        {
            this.x = x;
            this.y = y;
        }

        
      
    }
}
