using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    abstract class GameSystem
    {
       
        abstract public void process(float deltaTime, EntityManager entity_manager);
    }
}
