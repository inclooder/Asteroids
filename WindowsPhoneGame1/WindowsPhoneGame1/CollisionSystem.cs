using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids
{
    class CollisionSystem : GameSystem
    {

        public CollisionSystem(GameEngine game_engine)
            : base(game_engine)
        {

        }

        public override void process(float deltaTime)
        {

            List<int> entities_to_delete = new List<int>();

            int[] entities = entity_manager.GetEntitiesWithComponent(typeof(AsteroidBodyComponent));
            int[] lasers = entity_manager.GetEntitiesWithComponent(typeof(LaserVisualComponent));

            foreach (int entity in entities)
            {
                AsteroidBodyComponent[] asteroid_body_components = entity_manager.GetComponentsOfType(entity, typeof(AsteroidBodyComponent)).Cast<AsteroidBodyComponent>().ToArray();

                PositionComponent[] position_components = entity_manager.GetComponentsOfType(entity, typeof(PositionComponent)).Cast<PositionComponent>().ToArray();
                RotationComponent[] rotation_components = entity_manager.GetComponentsOfType(entity, typeof(RotationComponent)).Cast<RotationComponent>().ToArray();

                if (position_components.Length <= 0) break;
                if (rotation_components.Length <= 0) break;

                PositionComponent position_comp = position_components.First();


                bool skip = false;

                foreach (AsteroidBodyComponent asteroid_body in asteroid_body_components)
                {

                    Vector2[] bones = asteroid_body.getBones();
                    int num_of_bones = bones.Length;

                    if (num_of_bones >= 2)
                    {

                        VertexPositionColor[] vertices = asteroid_body.getVertices();

                        for (int i = 0; i < vertices.Length; i += 3)
                        {
                            Vector2 a = new Vector2(vertices[i].Position.X, vertices[i].Position.Y);
                            Vector2 b = new Vector2(vertices[i + 1].Position.X, vertices[i + 1].Position.Y);
                            Vector2 c = new Vector2(vertices[i + 2].Position.X, vertices[i + 2].Position.Y);

                            Matrix m = Matrix.CreateRotationZ(rotation_components.First().rotation);
                            m *= Matrix.CreateTranslation(position_comp.x, position_comp.y, 0);
                            a = Vector2.Transform(a, m);
                            b = Vector2.Transform(b, m);
                            c = Vector2.Transform(c, m);
                            //renderer.fillTriangle(a, b, c, vertices[i].Color, vertices[i + 1].Color, vertices[i + 2].Color);

                            Color triangleColor = vertices[i+2].Color;



                            foreach (int laser_ent in lasers)
                            {
                                PositionComponent[] laser_position_components = entity_manager.GetComponentsOfType(laser_ent, typeof(PositionComponent)).Cast<PositionComponent>().ToArray();
                                LaserVisualComponent[] laser_visual_components = entity_manager.GetComponentsOfType(laser_ent, typeof(LaserVisualComponent)).Cast<LaserVisualComponent>().ToArray();

                                if (laser_position_components.Length < 0) continue;
                                if (laser_visual_components.Length < 0) continue;

                                PositionComponent laser_pos = laser_position_components.First();

                                    if (IntersectionHelper.PointInsideTriangle(new Point((int)laser_pos.x, (int)laser_pos.y), new Point((int)a.X, (int)a.Y), new Point((int)b.X, (int)b.Y), new Point((int)c.X, (int)c.Y)))
                                    {
                                        entities_to_delete.Add(laser_ent);

                                        if (laser_visual_components.First().color == triangleColor)
                                        {
                                            game_engine.score += 1;
                                            game_engine.soundEffect.Play();
                                            entities_to_delete.Add(entity);
                                            skip = true;
                                            break;
                                        }
                                        else
                                        {
                                            game_engine.life -= 1;
                                        }
                                        
                                    }
                                
                            }
                            if (skip) break;

                            //Check laser - asteroid collisions
                        }

                    }
                    if (skip) break;
                }
            }

            foreach (int ent_id in entities_to_delete.ToArray())
            {
                entity_manager.RemoveEntity(ent_id);
            }

           

        


            //entity_manager.GetEntitiesWithComponent(typeof(CollisionComponent))
        }
    }
}
