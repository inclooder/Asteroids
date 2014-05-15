using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Asteroids
{
    class EntitiesCleanerSystem : GameSystem
    {
        public Rectangle bounds
        {
            get;
            set;
        }

        public EntitiesCleanerSystem(GameEngine game_engine, Rectangle bounds):base(game_engine)
        {
            this.bounds = bounds;
        }

        public override void process(float deltaTime)
        {
            int[] entities = entity_manager.GetEntitiesWithComponent(typeof(PositionComponent));

            foreach (int entity in entities)
            {
                PositionComponent[] position_components = entity_manager.GetComponentsOfType(entity, typeof(PositionComponent)).Cast<PositionComponent>().ToArray();
                if (position_components.Length <= 0) break;

                PositionComponent position = position_components.First();

                if (!bounds.Contains(new Microsoft.Xna.Framework.Point((int)position.x, (int)position.y)))
                {


                    LaserVisualComponent[] laser_components = entity_manager.GetComponentsOfType(entity, typeof(LaserVisualComponent)).Cast<LaserVisualComponent>().ToArray();
                    if (laser_components.Length > 0)
                    {
                        
                        game_engine.life -= 1;
                    }
                    entity_manager.RemoveEntity(entity);
                }

            }

        }
    }
}
