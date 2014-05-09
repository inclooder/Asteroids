using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids
{
    class ShipRenderSystem : GameSystem
    {
        private GraphicsDevice graphics_device;
        private SpriteBatch batch;
        private BasicEffect effect;
        private VertexBuffer vertexBuffer;
        private GameEngine game_engine;


        public ShipRenderSystem(GameEngine game_engine)
        {
            this.game_engine = game_engine;
            graphics_device = game_engine.getGraphicsDevice();
            batch = new SpriteBatch(graphics_device);
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
      

                if (position_components.Length <= 0) break;

                PositionComponent position_comp = position_components.First();
               

               
                effect.World =  game_engine.getWorldMatrix() * Matrix.CreateTranslation(new Vector3(position_comp.x, position_comp.y, 0));

                batch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

                foreach (AsteroidBodyComponent asteroid_body in asteroid_body_components)
                {

                    List<VertexPositionColor> vertices_list = new List<VertexPositionColor>();

                    Vector2[] bones = asteroid_body.getBones();
                    int num_of_bones = bones.Length;

                    if (num_of_bones >= 2)
                    {

                      

                        Color[] colors = new Color[] {
                          
                            Color.Red,
                            Color.Green,
                            Color.Blue,
                            Color.Violet,
                            Color.Purple,
                            Color.Pink,
                            Color.PowderBlue,
                            Color.Gray,
                            Color.LightGreen,
                            Color.LightSkyBlue
                        };

                        for (int i = 0; i < num_of_bones - 1; i++)
                        {
                            vertices_list.Add(new VertexPositionColor(new Vector3(0, 0, 0), Color.Yellow));
                            vertices_list.Add(new VertexPositionColor(new Vector3(bones[i].X, bones[i].Y, 0), colors[i]));
                            vertices_list.Add(new VertexPositionColor(new Vector3(bones[i+1].X, bones[i+1].Y, 0), colors[i]));
                        }


                        vertices_list.Add(new VertexPositionColor(new Vector3(0, 0, 0), Color.Yellow));
                       
                        vertices_list.Add(new VertexPositionColor(new Vector3(bones.Last().X, bones.Last().Y, 0), Color.Black));
                        vertices_list.Add(new VertexPositionColor(new Vector3(bones.First().X, bones.First().Y, 0), Color.Black));
                        

                 
                        /*
                        VertexPositionColor[] vertices = new VertexPositionColor[3];

                        vertices[0] = new VertexPositionColor(new Vector3(-100, 0, 0), Color.Black);
                        vertices[1] = new VertexPositionColor(new Vector3(0, 100, 0), Color.Black);
                        vertices[2] = new VertexPositionColor(new Vector3(100, 0, 0), Color.Black);
                        */

                        vertexBuffer = new VertexBuffer(graphics_device, typeof(VertexPositionColor), vertices_list.Count, BufferUsage.WriteOnly);
                        vertexBuffer.SetData<VertexPositionColor>(vertices_list.ToArray());


                        graphics_device.SetVertexBuffer(vertexBuffer);

                        //RasterizerState rasterizerState = new RasterizerState();
                        //rasterizerState.CullMode = CullMode.None;
                        //GraphicsDevice.RasterizerState = rasterizerState;

                        foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                        {
                            pass.Apply();
                            graphics_device.DrawPrimitives(PrimitiveType.TriangleList, 0, (int)(vertices_list.Count/3));

                        }
                        //Vector2 sprite_pos = new Vector2(position_comp.x + visual.offset_x, position_comp.y + visual.offset_y);
                        //batch.Draw(assets_manager.getTexture(visual.spriteID), sprite_pos, Color.White);

                    }
                }

                
                batch.End();
            }
          
        }
    }
}
