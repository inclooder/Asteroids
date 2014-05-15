using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    public abstract class GameSystem
    {
        protected GameEngine game_engine;
        protected EntityManager entity_manager;
        protected EntitiesFactory entities_factory;

        public GameSystem(GameEngine game_engine)
        {
            this.game_engine = game_engine;
            this.entity_manager = game_engine.getEntityManager();
            this.entities_factory = game_engine.getEntitiesFactory();
        }
       
        abstract public void process(float deltaTime);
    }
}
