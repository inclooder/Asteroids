using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    abstract class GameSystem
    {
        protected GameEngine game_engine;

        public GameSystem(GameEngine game_engine)
        {
            this.game_engine = game_engine;
        }
       
        abstract public void process(float deltaTime, EntityManager entity_manager);
    }
}
