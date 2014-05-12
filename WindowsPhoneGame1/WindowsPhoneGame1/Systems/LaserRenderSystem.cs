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
            graphics_device = game_engine.getGraphicsDevice();
          
            rotationDelta = 0;
        }

        public override void process(float deltaTime, EntityManager entity_manager)
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
   
                Matrix new_world_matrix = game_engine.getWorldMatrix();
                if (rotation_components.Length > 0)
                {
                    new_world_matrix *= rotation_components.First().getRotationMatrix();
                }
                new_world_matrix = new_world_matrix * Matrix.CreateTranslation(new Vector3(position_comp.x, position_comp.y, 0));
                effect.World = new_world_matrix;



                foreach (LaserVisualComponent laser_visual in laser_visual_components)
                {

                    VertexPositionColor[] vertices = new VertexPositionColor[4];

                    vertices[0] = new VertexPositionColor(new Vector3(0, 0, 0), laser_visual.color);
                    vertices[1] = new VertexPositionColor(new Vector3(0, 1*laser_visual.length, 0), Color.Red);
                    vertices[2] = new VertexPositionColor(new Vector3(0, 0, 0), laser_visual.color);
                    vertices[3] = new VertexPositionColor(new Vector3(0, -1*laser_visual.length, 0), Color.Red);

                    vertexBuffer = new VertexBuffer(graphics_device, typeof(VertexPositionColor), vertices.Length, BufferUsage.WriteOnly);
                    vertexBuffer.SetData<VertexPositionColor>(vertices);


                    graphics_device.SetVertexBuffer(vertexBuffer);

                    //RasterizerState rasterizerState = new RasterizerState();
                    //rasterizerState.CullMode = CullMode.None;
                    //graphics_device.RasterizerState = rasterizerState;

                    foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                    {
                        pass.Apply();
                        graphics_device.DrawPrimitives(PrimitiveType.LineList, 0, 2);

                    }



                }

            }
            if (updateRotation) rotationDelta = 0.0f;

        }
    }

}
