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


        public AsteroidRenderSystem(GameEngine game_engine)
            : base(game_engine)
        {
            this.game_engine = game_engine;

        }

        public override void process(float deltaTime)
        {

            
            int[] entities = entity_manager.GetEntitiesWithComponent(typeof(AsteroidBodyComponent));
            foreach (int entity in entities)
            {
                AsteroidBodyComponent[] asteroid_body_components = entity_manager.GetComponentsOfType(entity, typeof(AsteroidBodyComponent)).Cast<AsteroidBodyComponent>().ToArray();

                PositionComponent[] position_components = entity_manager.GetComponentsOfType(entity, typeof(PositionComponent)).Cast<PositionComponent>().ToArray();
                RotationComponent[] rotation_components = entity_manager.GetComponentsOfType(entity, typeof(RotationComponent)).Cast<RotationComponent>().ToArray();

                if (position_components.Length <= 0) break;
                if (rotation_components.Length <= 0) break;

                PositionComponent position_comp = position_components.First();

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
                           // renderer.fillTriangle(a, b, c, vertices[i].Color, vertices[i+1].Color, vertices[i+2].Color);
                            renderer.fillTriangle(a, b, c, Color.Yellow, vertices[i + 1].Color, vertices[i + 2].Color);
                        }



                    }
                }

            }


        }
    }
}
