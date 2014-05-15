using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;


namespace Asteroids
{
    
    public class GameEngine : Microsoft.Xna.Framework.Game
    {
        EntityManager entity_manager;
        AssetsManager assets_manager;
        EntitiesFactory entities_factory;
        AsteroidComponentGenerator asteroid_generator;
        List<GameSystem> graphic_systems;
        List<GameSystem> logic_systems;
        GraphicsDeviceManager graphics;
        int player_entity;
        GraphicRenderer renderer;
        public SoundEffect soundEffect;
        SpriteBatch batch;
        SpriteFont calibriFont;
        Random ran;
        Color laser_color;

        public int life
        {
            get;
            set;
        }

        public int score
        {
            get;
            set;
        }

        public GameEngine()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);
            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);

           
        }

        public GraphicsDevice getGraphicsDevice()
        {
            return GraphicsDevice;
        }

        public AssetsManager getAssetsManager()
        {
            return assets_manager;
        }

        public EntityManager getEntityManager()
        {
            return entity_manager;
        }

        public GraphicRenderer getRenderer()
        {
            return renderer;
        }

        public EntitiesFactory getEntitiesFactory()
        {
            return entities_factory;
        }

        

        protected override void Initialize()
        {
            batch = new SpriteBatch(graphics.GraphicsDevice);
            ran = new Random();
            laser_color = Color.Yellow;
            renderer = new GraphicRenderer(GraphicsDevice);
            entity_manager = new EntityManager();
            assets_manager = new AssetsManager();
            entities_factory = new EntitiesFactory(entity_manager);
            asteroid_generator = new AsteroidComponentGenerator(20, 40);
            graphic_systems = new List<GameSystem>();
          //  graphic_systems.Add(new VisualSystem(this));
            graphic_systems.Add(new AsteroidRenderSystem(this));
            graphic_systems.Add(new LaserRenderSystem(this));
            graphic_systems.Add(new ShipRenderSystem(this));
            logic_systems = new List<GameSystem>();
            logic_systems.Add(new GravitySystem(this));
            logic_systems.Add(new RotationForceSystem(this));
            logic_systems.Add(new CollisionSystem(this));
            
            Point[] spawns = new Point[] {
                new Point(-10, 120),
                new Point(-10, 360),
                new Point(-10, 60),
                new Point(-10, 420)
            };

            logic_systems.Add(new AsteroidsSpawnSystem(this, 10, spawns, true));

            int widthPane = 60;
            int heightPane = 60;
            Rectangle worldRect = new Rectangle(GraphicsDevice.Viewport.X - widthPane
                , GraphicsDevice.Viewport.Y - heightPane, 
                GraphicsDevice.Viewport.Width + widthPane * 2, 
                GraphicsDevice.Viewport.Height + heightPane * 2);
            
            logic_systems.Add(new EntitiesCleanerSystem(this, worldRect));

            life = 10;
            score = 0;

            base.Initialize();
        }


        public void updateLaserColor()
        {
            int[] asteroids = entity_manager.GetEntitiesWithComponent(typeof(AsteroidBodyComponent));
            List<Color> colors = new List<Color>();
            
            foreach (int asteroid in asteroids)
            {
                AsteroidBodyComponent[] asteroid_body_components = entity_manager.GetComponentsOfType(asteroid, typeof(AsteroidBodyComponent)).Cast<AsteroidBodyComponent>().ToArray();
                if (asteroid_body_components.Length < 0) break;

                AsteroidBodyComponent body_component = asteroid_body_components.First();

                VertexPositionColor[] component_colors = body_component.getVertices();
                for (int i = 0; i < component_colors.Length; i += 3)
                {
                    colors.Add(component_colors[i].Color);
                }
            }

            ShipBodyComponent[] player_ship_components = entity_manager.GetComponentsOfType(player_entity, typeof(ShipBodyComponent)).Cast<ShipBodyComponent>().ToArray();
            if (player_ship_components.Length > 0)
            {
                ShipBodyComponent ship_component = player_ship_components.First();
                Color[] avil_colors = colors.ToArray();
                ship_component.color = avil_colors[ran.Next(0, avil_colors.Length) % avil_colors.Length];
                laser_color = ship_component.color;
            }
        }


        protected override void LoadContent()
        {
            soundEffect = Content.Load<SoundEffect>("Windows Ding");
          
            calibriFont = Content.Load<SpriteFont>("Calibri");
            

            player_entity = entity_manager.CreateEntity();
            entity_manager.AddComponent(player_entity, new PositionComponent(770, 240));
            entity_manager.AddComponent(player_entity, new RotationComponent(90f));
            entity_manager.AddComponent(player_entity, new ShipBodyComponent(Color.Yellow));

        }

     
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

   
        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Allow the game to exit.
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back ==
                ButtonState.Pressed)
                this.Exit();

            TouchCollection touchCollection = TouchPanel.GetState();
            foreach (TouchLocation tl in touchCollection)
            {
                if (tl.State == TouchLocationState.Pressed)
                {
                    Vector2 click = new Vector2((int)(tl.Position.X), (int)(tl.Position.Y));
                    Vector2 player = new Vector2(770, 240);
                    Vector2 substracted = new Vector2(player.X - click.X, player.Y - click.Y);
                    int ent = entity_manager.CreateEntity();
                    entity_manager.AddComponent(ent, new PositionComponent((int)player.X, (int)player.Y));
                    entity_manager.AddComponent(ent, new RotationComponent(substracted)); 
                    substracted.Normalize();
                    entity_manager.AddComponent(ent, new GravityComponent(Vector2.Negate(substracted), 400f));
                    entity_manager.AddComponent(ent, new LaserVisualComponent(laser_color, 3));
                    
                }

                if (tl.State == TouchLocationState.Released)
                {
                    //...on touch up code
                }

            }


            foreach (GameSystem system in logic_systems)
            {
                system.process(deltaTime);
            }

            entities_factory.setLevel((int)(score / 2));

            base.Update(gameTime);
        }

    
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
      
       
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (GameSystem system in graphic_systems)
            {
                system.process(deltaTime);
            }

            batch.Begin();

            batch.DrawString(calibriFont, String.Format("Life {0}", life), new Vector2(770, 100), Color.Black, MathHelper.ToRadians(270), new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            batch.DrawString(calibriFont, String.Format("Score {0}", score), new Vector2(750, 100), Color.Black, MathHelper.ToRadians(270), new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            batch.DrawString(calibriFont, String.Format("Level {0}", entities_factory.getLevel()), new Vector2(730, 100), Color.Black, MathHelper.ToRadians(270), new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            batch.End();
          
            base.Draw(gameTime);
        }
    }
}
