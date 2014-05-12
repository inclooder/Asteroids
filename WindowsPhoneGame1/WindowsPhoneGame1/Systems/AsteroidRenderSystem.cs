using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids
{
    class AsteroidRenderSystem : GameGraphicSystem
    {
        
        
        public AsteroidRenderSystem(GameEngine game_engine) : base(game_engine)
        {
            this.game_engine = game_engine;
            graphics_device = game_engine.getGraphicsDevice();
            effect = new BasicEffect(graphics_device);
            effect.World = game_engine.getWorldMatrix();
            effect.View = game_engine.getViewMatrix(); ;
            effect.Projection = game_engine.getProjectionMatrix();
            effect.VertexColorEnabled = true;
            effect.LightingEnabled = false;
            effect.FogEnabled = false;


          
        }
       
        public override void process(float deltaTime, EntityManager entity_manager)
        {
           

            int[] entities = entity_manager.GetEntitiesWithComponent(typeof(AsteroidBodyComponent));
            foreach(int entity in entities)
            {
                AsteroidBodyComponent[] asteroid_body_components = entity_manager.GetComponentsOfType(entity, typeof(AsteroidBodyComponent)).Cast<AsteroidBodyComponent>().ToArray();

                PositionComponent[] position_components = entity_manager.GetComponentsOfType(entity, typeof(PositionComponent)).Cast<PositionComponent>().ToArray();
                RotationComponent[] rotation_components = entity_manager.GetComponentsOfType(entity, typeof(RotationComponent)).Cast<RotationComponent>().ToArray();
      
                if (position_components.Length <= 0) break;

                PositionComponent position_comp = position_components.First();

                Matrix new_world_matrix = game_engine.getWorldMatrix();


               
                
                if (rotation_components.Length > 0)
                {
                    new_world_matrix *= rotation_components.First().getRotationMatrix();
                }
            



                new_world_matrix = new_world_matrix * Matrix.CreateTranslation(new Vector3(position_comp.x, position_comp.y, 0));

               
               
                effect.World =  new_world_matrix;
                


                foreach (AsteroidBodyComponent asteroid_body in asteroid_body_components)
                {

                    Vector2[] bones = asteroid_body.getBones();
                    int num_of_bones = bones.Length;

                    if (num_of_bones >= 2)
                    {

                        VertexPositionColor[] vertices = asteroid_body.getVertices();
                 

                        vertexBuffer = new VertexBuffer(graphics_device, typeof(VertexPositionColor), vertices.Length, BufferUsage.WriteOnly);
                        vertexBuffer.SetData<VertexPositionColor>(vertices);


                        graphics_device.SetVertexBuffer(vertexBuffer);

                       // RasterizerState rasterizerState = new RasterizerState();
                        //rasterizerState.CullMode = CullMode.None;
                        //graphics_device.RasterizerState = rasterizerState;

                        foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                        {
                            pass.Apply();
                            graphics_device.DrawPrimitives(PrimitiveType.TriangleList, 0, (int)(vertices.Length / 3));

                        }
                      

                    }
                }
                
            }
            
          
        }
    }
}
