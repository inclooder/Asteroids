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

        public override void process(float deltaTime)
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


                foreach (ShipBodyComponent ship_body in ship_body_components)
                {
                    Matrix m = Matrix.CreateRotationZ(rotation_components.First().rotation);
                    m *= Matrix.CreateTranslation(position_comp.x, position_comp.y, 0);

                    Vector2 a = new Vector2(-30, -30);
                    Vector2 b = new Vector2(0, 0);
                    Vector2 c = new Vector2(30, -30);

                    a = Vector2.Transform(a, m);
                    b = Vector2.Transform(b, m);
                    c = Vector2.Transform(c, m);

                    renderer.fillTriangle(a, b, c, Color.Black, Color.Yellow, Color.Black );
                }


               
            }

        }
    }
}
