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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameEngine : Microsoft.Xna.Framework.Game
    {
        EntityManager entity_manager;
        AssetsManager assets_manager;
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

    
        Matrix world, view, projection;
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


        public Matrix getWorldMatrix()
        {
            return world;
        }

        public Matrix getViewMatrix()
        {
            return view;
        }

        public Matrix getProjectionMatrix()
        {
            return projection;
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

        public Rectangle getScreenRectangleInWorldCoordinates()
        {
            Rectangle viewport_rect = GraphicsDevice.Viewport.Bounds;
            Vector3 top_left = new Vector3(viewport_rect.X, viewport_rect.Y, 0);
            Vector3 bottom_right = new Vector3(viewport_rect.Width + top_left.X, viewport_rect.Height + top_left.Y, 0);

            Vector3 world_bottom_right = GraphicsDevice.Viewport.Unproject(top_left, projection, view, world);
            Vector3 world_top_left = GraphicsDevice.Viewport.Unproject(bottom_right, projection, view, world);

            float width = Math.Abs(world_bottom_right.X - world_top_left.X);
            float height = Math.Abs(world_bottom_right.Y - world_top_left.Y);

         
            return new Rectangle((int)(world_top_left.X), (int)(world_top_left.Y), (int)(width), (int)(height));
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            

            world = Matrix.CreateTranslation(0, 0, 0);
            view = Matrix.CreateLookAt(new Vector3(0, 0, -1), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            //Matrix projection = Matrix.CreateOrthographic(Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.01f, 100f);
            projection = Matrix.CreateOrthographic(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height, 1.0f, 1000.0f);


            // TODO: Add your initialization logic here
            // Create a new SpriteBatch, which can be used to draw textures.
            entity_manager = new EntityManager();
            assets_manager = new AssetsManager();
            asteroid_generator = new AsteroidComponentGenerator(20, 40);

            graphic_systems = new List<GameSystem>();
            graphic_systems.Add(new VisualSystem(this));
            graphic_systems.Add(new AsteroidRenderSystem(this));
            graphic_systems.Add(new LaserRenderSystem(this));
            graphic_systems.Add(new ShipRenderSystem(this));
            

            logic_systems = new List<GameSystem>();
            logic_systems.Add(new GravitySystem(this));
            logic_systems.Add(new RotationForceSystem(this));
            logic_systems.Add(new EntitiesCleanerSystem(this, getScreenRectangleInWorldCoordinates()));

            

            base.Initialize();


        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
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

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
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
                    Vector3 pos = GraphicsDevice.Viewport.Unproject(new Vector3(tl.Position.X, tl.Position.Y, 0), projection, view, world);
                    int ent = entity_manager.CreateEntity();
                    entity_manager.AddComponent(ent, new PositionComponent((int)(pos.X), (int)(pos.Y)));
                    //entity_manager.AddComponent(ent, asteroid_generator.genearate(5));
                    entity_manager.AddComponent(ent, new RotationComponent(new Vector2(pos.X, pos.Y)));
                    //entity_manager.AddComponent(ent, new RotationForceComponent(true, 1f));
                    entity_manager.AddComponent(ent, new GravityComponent(new Vector2(pos.X,pos.Y), 100f));
                    entity_manager.AddComponent(ent, new ShipBodyComponent(10f));
                    //entity_manager.AddComponent(ent, new LaserVisualComponent(Color.Pink, 10));
                    
                }

                if (tl.State == TouchLocationState.Released)
                {
                    //...on touch up code
                }

            }


            foreach (GameSystem system in logic_systems)
            {
                system.process(deltaTime, entity_manager);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (GameSystem system in graphic_systems)
            {
                system.process(deltaTime, entity_manager);
            }

            base.Draw(gameTime);
        }

       
    }
}
