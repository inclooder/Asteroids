using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids
{
    class ShipRenderSystem : GameGraphicSystem
    {



        public ShipRenderSystem(GameEngine game_engine)
            : base(game_engine)
        {

        }

        public override void process(float deltaTime, EntityManager entity_manager)
        {
            int[] entities = entity_manager.GetEntitiesWithComponent(typeof(ShipBodyComponent));
            foreach (int entity in entities)
            {
                ShipBodyComponent[] ship_body_components = entity_manager.GetComponentsOfType(entity, typeof(ShipBodyComponent)).Cast<ShipBodyComponent>().ToArray();

                PositionComponent[] position_components = entity_manager.GetComponentsOfType(entity, typeof(PositionComponent)).Cast<PositionComponent>().ToArray();

                RotationComponent[] rotation_components = entity_manager.GetComponentsOfType(entity, typeof(RotationComponent)).Cast<RotationComponent>().ToArray();


                if (position_components.Length <= 0) break;
                if (rotation_components.Length <= 0) break;


                PositionComponent position_comp = position_components.First();



                effect.World = game_engine.getWorldMatrix() * Matrix.CreateTranslation(new Vector3(position_comp.x, position_comp.y, 0));
                Matrix new_world_matrix = game_engine.getWorldMatrix();


                if (rotation_components.Length > 0)
                {
                    new_world_matrix *= rotation_components.First().getRotationMatrix();
                }


                new_world_matrix = new_world_matrix * Matrix.CreateTranslation(new Vector3(position_comp.x, position_comp.y, 0));



                effect.World = new_world_matrix;
           

                foreach (ShipBodyComponent ship_body in ship_body_components)
                {
                    List<VertexPositionColor> vertices_list = new List<VertexPositionColor>();

                    vertices_list.Add(new VertexPositionColor(new Vector3(0, 0, 0), Color.Yellow));
                    vertices_list.Add(new VertexPositionColor(new Vector3(30, 0, 0), Color.Yellow));
                    vertices_list.Add(new VertexPositionColor(new Vector3(0, 30, 0), Color.Yellow));

                  
                    vertexBuffer = new VertexBuffer(graphics_device, typeof(VertexPositionColor), vertices_list.Count, BufferUsage.WriteOnly);
                    vertexBuffer.SetData<VertexPositionColor>(vertices_list.ToArray());


                    graphics_device.SetVertexBuffer(vertexBuffer);

                    RasterizerState rasterizerState = new RasterizerState();
                    rasterizerState.CullMode = CullMode.None;
                    graphics_device.RasterizerState = rasterizerState;

                    foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                    {
                        pass.Apply();
                        graphics_device.DrawPrimitives(PrimitiveType.TriangleList, 0, (int)(vertices_list.Count / 3));

                    }
                 

                }


               
            }

        }
    }
}
