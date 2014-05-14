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
        Texture2D[] textures;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D texture1;
        Texture2D texture2;
        Vector2 spritePosition1;
        Vector2 spritePosition2;
        Vector2 spriteSpeed1 = new Vector2(50.0f, 50.0f);
        Vector2 spriteSpeed2 = new Vector2(100.0f, 100.0f);
        int sprite1Height;
        int sprite1Width;
        int sprite2Height;
        int sprite2Width;


        int player_entity;


        Vector2 click = new Vector2(0, 0);

    
        GraphicRenderer renderer;
        

    
        SoundEffect soundEffect;

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
                new Point(-10, 240),
                new Point(-10, 120),
                new Point(-10, 360)
            };

            logic_systems.Add(new AsteroidsSpawnSystem(this, 10, spawns, true));

            int widthPane = 60;
            int heightPane = 60;
            Rectangle worldRect = new Rectangle(GraphicsDevice.Viewport.X - widthPane
                , GraphicsDevice.Viewport.Y - heightPane, 
                GraphicsDevice.Viewport.Width + widthPane * 2, 
                GraphicsDevice.Viewport.Height + heightPane * 2);
            
            logic_systems.Add(new EntitiesCleanerSystem(this, worldRect));
            base.Initialize();
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);



            assets_manager.addTexture(Content.Load<Texture2D>("PhoneGameThumb"));
            assets_manager.addTexture(Content.Load<Texture2D>("PhoneGameThumb"));
            textures = new Texture2D[] {
                Content.Load<Texture2D>("PhoneGameThumb"),
                Content.Load<Texture2D>("PhoneGameThumb")
            };

            texture1 = textures[0];
            texture2 = textures[1];

            soundEffect = Content.Load<SoundEffect>("Windows Ding");

            spritePosition1.X = 0;
            spritePosition1.Y = 0;

            spritePosition2.X = graphics.GraphicsDevice.Viewport.Width - texture1.Width;
            spritePosition2.Y = graphics.GraphicsDevice.Viewport.Height - texture1.Height;

            sprite1Height = texture1.Bounds.Height;
            sprite1Width = texture1.Bounds.Width;

            sprite2Height = texture2.Bounds.Height;
            sprite2Width = texture2.Bounds.Width;

            player_entity = entity_manager.CreateEntity();
            entity_manager.AddComponent(player_entity, new PositionComponent(770, 240));
            entity_manager.AddComponent(player_entity, new RotationComponent(90f));
            entity_manager.AddComponent(player_entity, new ShipBodyComponent(10.0f));

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
                    click = new Vector2((int)(tl.Position.X), (int)(tl.Position.Y));
                    Vector2 player = new Vector2(770, 240);
                    Vector2 substracted = new Vector2(player.X - click.X, player.Y - click.Y);


                    int ent = entity_manager.CreateEntity();
                    entity_manager.AddComponent(ent, new PositionComponent((int)player.X, (int)player.Y));
                    //entity_manager.AddComponent(ent, asteroid_generator.genearate(5));
                    entity_manager.AddComponent(ent, new RotationComponent(substracted));
                    //entity_manager.AddComponent(ent, new RotationForceComponent(true, 1f));
                               
                    //Vector2 gravity = new Vector2(800, 240);
   
                    //gravity = Vector2.Add(gravity, new Vector2((int)(tl.Position.X), (int)(tl.Position.Y)));
                    //gravity.Normalize();
                    //gravity = Vector2.Negate(gravity);

                    substracted.Normalize();
                    entity_manager.AddComponent(ent, new GravityComponent(Vector2.Negate(substracted), 400f));
                    //entity_manager.AddComponent(ent, new ShipBodyComponent(10f));
                    entity_manager.AddComponent(ent, new LaserVisualComponent(Color.Pink, 3));
                    
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
            base.Draw(gameTime);
        }
    }
}
