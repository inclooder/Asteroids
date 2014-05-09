using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    class VisualComponent : EntityComponent
    {
        public int spriteID
        {
            get;
            set;
        }

    
        public float scale
        {
            get;
            set;
        }

        public int layer
        {
            get;
            set;
        }

        public int offset_x
        {
            get;
            set;
        }

        public int offset_y
        {
            get;
            set;
        }

        public VisualComponent(int spriteID, float scale, int offset_x, int offset_y, int layer)
        {
            this.spriteID = spriteID;
            this.scale = scale;
            this.offset_x = offset_x;
            this.offset_y = offset_y;
            this.layer = layer;
         
        }
    }
}
