using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Asteroids
{
    class GravitySystem : GameSystem
    {
        public GravitySystem(GameEngine game_engine) : base(game_engine)
        {

        }
    
        public override void process(float deltaTime)
        {
            int[] entities = entity_manager.GetEntitiesWithComponent(typeof(GravityComponent));
            foreach (int entity in entities)
            {

                GravityComponent[] gravity_components = entity_manager.GetComponentsOfType(entity, typeof(GravityComponent)).Cast<GravityComponent>().ToArray();
                if (gravity_components.Length <= 0) break;

                PositionComponent[] position_components = entity_manager.GetComponentsOfType(entity, typeof(PositionComponent)).Cast<PositionComponent>().ToArray();
                if (position_components.Length <= 0) break;

                GravityComponent gravity_component = gravity_components.First();
                PositionComponent position_component = position_components.First();

                Vector2 current_position = position_component.getPosition();

                Vector2 move_vector = gravity_component.getDirection() *gravity_component.getSpeed() * deltaTime;

                position_component.setPosition(current_position + move_vector);
               
            }
          
        }
    }
}
