using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids
{
    class LaserRenderSystem : GameGraphicSystem
    {
       
        private float rotationDelta;

        public LaserRenderSystem(GameEngine game_engine) : base(game_engine)
        {
            this.game_engine = game_engine;
          
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

            int[] entities = entity_manager.GetEntitiesWithComponent(typeof(LaserVisualComponent));
            foreach (int entity in entities)
            {
                LaserVisualComponent[] laser_visual_components = entity_manager.GetComponentsOfType(entity, typeof(LaserVisualComponent)).Cast<LaserVisualComponent>().ToArray();

                PositionComponent[] position_components = entity_manager.GetComponentsOfType(entity, typeof(PositionComponent)).Cast<PositionComponent>().ToArray();
                if (position_components.Length <= 0) break;
                RotationComponent[] rotation_components = entity_manager.GetComponentsOfType(entity, typeof(RotationComponent)).Cast<RotationComponent>().ToArray();
                if (position_components.Length <= 0) break;

                PositionComponent position_comp = position_components.First();

                
              

                foreach (LaserVisualComponent laser_visual in laser_visual_components)
                {

                    Vector2 a = new Vector2(0, 0);
                    Vector2 b = new Vector2(0, -10 * laser_visual.length);

                    Matrix m = Matrix.CreateRotationZ(rotation_components.First().rotation);
                    m *= Matrix.CreateTranslation(position_comp.x, position_comp.y, 0);
                    a = Vector2.Transform(a, m);
                    b = Vector2.Transform(b, m);
                    renderer.drawLine(a, b , laser_visual.color);  
                }

            }
            if (updateRotation) rotationDelta = 0.0f;

        }
    }

}
