using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    class RotationForceSystem : GameSystem
    {

        private float rotationDelta;

        public RotationForceSystem(GameEngine game_system)
            : base(game_system)
        {
            rotationDelta = 0;
        }

        public override void process(float deltaTime)
        {
            rotationDelta += deltaTime;
            bool updateRotation = false;
            if (rotationDelta > 0.02f)
            {
                updateRotation = true;
            }


             int[] entities = entity_manager.GetEntitiesWithComponent(typeof(RotationForceComponent));
             foreach (int entity in entities)
             {
                 RotationForceComponent[] rotation_force_components = entity_manager.GetComponentsOfType(entity, typeof(RotationForceComponent)).Cast<RotationForceComponent>().ToArray();

                 PositionComponent[] position_components = entity_manager.GetComponentsOfType(entity, typeof(PositionComponent)).Cast<PositionComponent>().ToArray();
                 if (position_components.Length < 0) break;
                 RotationComponent[] rotation_components = entity_manager.GetComponentsOfType(entity, typeof(RotationComponent)).Cast<RotationComponent>().ToArray();
                 if (rotation_components.Length < 0) break;


                 if (updateRotation && rotation_components.Length > 0)
                 {
                     RotationForceComponent rotation_force_comp = rotation_force_components.First();
                     RotationComponent rotation_component = rotation_components.First();
                     if (rotation_force_comp.clockwise)
                     {
                         rotation_component.rotation += 1 * rotationDelta * rotation_force_comp.speed;
                     }
                     else
                     {
                         rotation_component.rotation -= 1 * rotationDelta * rotation_force_comp.speed;
                     }
                           

                 }
             }


            if (updateRotation) rotationDelta = 0.0f;

        }
    }
}
