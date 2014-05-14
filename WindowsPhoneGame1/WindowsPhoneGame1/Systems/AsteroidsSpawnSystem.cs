using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Asteroids
{
    class AsteroidsSpawnSystem : GameSystem
    {

        bool random_spawn
        {
            get;
            set;
        }

        Point[] spawn_points
        {
            get;
            set;
        }

        int maxAsteroids
        {
            get;
            set;
        }

        protected AsteroidComponentGenerator asteroid_generator;
        
        private Random ran;
        private float timeElapsed = 4f;

        public AsteroidsSpawnSystem(GameEngine game_engine, int maxAsteroids, Point[] spawn_points, bool random_spawn) : base(game_engine)
        {
            this.maxAsteroids = maxAsteroids;
            this.spawn_points = spawn_points;
            this.random_spawn = random_spawn;
            this.asteroid_generator = new AsteroidComponentGenerator(50, 60);
            ran = new Random();
        }

        public int[] getAsteroidsEntities()
        {
            return entity_manager.GetEntitiesWithComponent(typeof(AsteroidBodyComponent));
        }

        public override void process(float deltaTime)
        {
            timeElapsed += deltaTime;
            if (timeElapsed < 4f) return;
            timeElapsed = 0f;


            if(spawn_points.Length < 0) return;

            int[] asteroids = getAsteroidsEntities();

            if (asteroids.Length < maxAsteroids)
            {
                //Add asteroids
                Point pos = spawn_points[ran.Next(0, spawn_points.Length) % spawn_points.Length];
                entities_factory.createAsteroid(new Vector2(pos.X, pos.Y));
                /*
                int ent = entity_manager.CreateEntity();
                
                entity_manager.AddComponent(ent, new PositionComponent(pos.X, pos.Y));
                entity_manager.AddComponent(ent, new RotationComponent(0f));
                entity_manager.AddComponent(ent, asteroid_generator.genearate(5));
                entity_manager.AddComponent(ent, new RotationForceComponent(true, 1f));
                entity_manager.AddComponent(ent, new GravityComponent(new Vector2(1, 0), 100f));
                 */

            }
        }
    }
}
