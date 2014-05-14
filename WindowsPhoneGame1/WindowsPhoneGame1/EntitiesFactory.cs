using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Asteroids
{
    public class EntitiesFactory
    {

        protected EntityManager entity_manager;
        protected int level;
        protected AsteroidComponentGenerator asteroid_component_generator;
        protected float asteroids_rotation_speed;
        protected int asteroid_parts_count;


        public EntitiesFactory(EntityManager entity_manager)
        {
            this.entity_manager = entity_manager;
            setLevel(0);

        }

        public void setLevel(int lv)
        {
            this.level = lv;

            if (level < 5)
            {
                asteroid_component_generator = new AsteroidComponentGenerator(40, 60);
                asteroids_rotation_speed = 0.4f;
                asteroid_parts_count = 3;
            }
            else if (level < 10)
            {
                asteroid_component_generator = new AsteroidComponentGenerator(30, 50);
                asteroids_rotation_speed = 0.8f;
                asteroid_parts_count = 4;
            }
            else if (level < 15)
            {
                asteroid_component_generator = new AsteroidComponentGenerator(30, 40);
                asteroids_rotation_speed = 1f;
                asteroid_parts_count = 5;
            }
            else
            {
                asteroid_component_generator = new AsteroidComponentGenerator(20, 30);
                asteroids_rotation_speed = 1.2f;
                asteroid_parts_count = 6;
            }
        }

        public int getLevel()
        {
            return level;
        }



        public int createAsteroid(Vector2 pos)
        {
            int ent = entity_manager.CreateEntity();
            entity_manager.AddComponent(ent, new PositionComponent(pos.X, pos.Y));
            entity_manager.AddComponent(ent, new RotationComponent(0f));
            entity_manager.AddComponent(ent, asteroid_component_generator.genearate(asteroid_parts_count));
            entity_manager.AddComponent(ent, new RotationForceComponent(true, asteroids_rotation_speed));
            entity_manager.AddComponent(ent, new GravityComponent(new Vector2(1, 0), 100f));

            return ent;
        }
    }
}
