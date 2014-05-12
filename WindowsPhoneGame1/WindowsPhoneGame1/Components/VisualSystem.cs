using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Asteroids
{
    class VisualSystem : GameSystem
    {
        private GraphicsDevice graphics_device;
        private AssetsManager assets_manager;
        private SpriteBatch batch;
      

        public VisualSystem(GameEngine game_engine) :base(game_engine)
        {
            graphics_device = game_engine.getGraphicsDevice();
            this.assets_manager = game_engine.getAssetsManager(); ;
            batch = new SpriteBatch(graphics_device);
        }

        public override void process(float deltaTime, EntityManager entity_manager)
        {
            int[] entities = entity_manager.GetEntitiesWithComponent(typeof(VisualComponent));
            foreach(int entity in entities)
            {
                VisualComponent[] visual_components = entity_manager.GetComponentsOfType(entity, typeof(VisualComponent)).Cast<VisualComponent>().ToArray();



                PositionComponent[] position_components = entity_manager.GetComponentsOfType(entity, typeof(PositionComponent)).Cast<PositionComponent>().ToArray();
                if (position_components.Length <= 0) break;


                PositionComponent position_comp = position_components.First();

                Vector3 position = new Vector3(position_comp.x, position_comp.y, 0);
                
                position = graphics_device.Viewport.Project(position, game_engine.getProjectionMatrix(), game_engine.getViewMatrix(), game_engine.getWorldMatrix());
               

                batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

                foreach (VisualComponent visual in visual_components)
                {
                    
                    Vector2 sprite_pos = new Vector2(position.X, position.Y);
                  
                    batch.Draw(assets_manager.getTexture(visual.spriteID), sprite_pos, Color.White);
                  
                    
                }

                
                batch.End();
            }
          
        }
    }
}
