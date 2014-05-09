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

        VertexBuffer vertexBuffer;
        BasicEffect basicEffect;
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

            logic_systems = new List<GameSystem>();

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


            //Create Entities

            for (int x = 1; x < 10; x++)
            {
                int entity = entity_manager.CreateEntity();
                entity_manager.AddComponent(entity, new PositionComponent(x*10, 40));
                entity_manager.AddComponent(entity, new VisualComponent(0, 1, 0, 0, 0));
            }


        

            int ent = entity_manager.CreateEntity();
            entity_manager.AddComponent(ent, new PositionComponent(0, 0));


            entity_manager.AddComponent(ent, asteroid_generator.genearate(5));
            entity_manager.AddComponent(ent, new RotationComponent(1));


            ent = entity_manager.CreateEntity();
            entity_manager.AddComponent(ent, new PositionComponent(10, 0));


            entity_manager.AddComponent(ent, asteroid_generator.genearate(5));
            entity_manager.AddComponent(ent, new RotationComponent(1));

            ent = entity_manager.CreateEntity();
            entity_manager.AddComponent(ent, new PositionComponent(50, 50));


            entity_manager.AddComponent(ent, asteroid_generator.genearate(5));
            entity_manager.AddComponent(ent, new RotationComponent(1));

            ent = entity_manager.CreateEntity();
            entity_manager.AddComponent(ent, new PositionComponent(10, 50));


            entity_manager.AddComponent(ent, asteroid_generator.genearate(5));
            entity_manager.AddComponent(ent, new RotationComponent(1));
            


          

         
        
         //   entity = entity_manager.CreateEntity();
           // entity_manager.AddComponent(entity, new PositionComponent(0, 0));
            //entity_manager.AddComponent(entity, new VisualComponent(0, 1, 300, 0, 0));


           

            // TODO: use this.Content to load your game content here
            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.World = world;
            basicEffect.View = view;
            basicEffect.Projection = projection;
            basicEffect.VertexColorEnabled = true;
            basicEffect.LightingEnabled = false;
            basicEffect.FogEnabled = false;

            VertexPositionColor[] vertices = new VertexPositionColor[3];
            vertices[1] = new VertexPositionColor(new Vector3(0, 0, 0), Color.Black);
            vertices[2] = new VertexPositionColor(new Vector3(0, 100, 0), Color.Black);
            vertices[0] = new VertexPositionColor(new Vector3(100, 0, 0), Color.Black);
            
           // vertices[3] = new VertexPositionColor(new Vector3(0, 100, 0), Color.Black);
           // vertices[4] = new VertexPositionColor(new Vector3(0, 0, 0), Color.Black);
           // vertices[5] = new VertexPositionColor(new Vector3(0, 100, 0), Color.Black);

            VertexPositionNormalTexture[] vertices2 = new VertexPositionNormalTexture[6];
           

          

            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 3, BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(vertices);
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


            foreach(GameSystem system in logic_systems){
                system.process(deltaTime, entity_manager);
            }

            // Move the sprite around.
           // UpdateSprite(gameTime, ref spritePosition1, ref spriteSpeed1);
           // UpdateSprite(gameTime, ref spritePosition2, ref spriteSpeed2);
          //  CheckForCollision();

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

            

            // Draw the sprite.
            /*
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spriteBatch.Draw(texture1, spritePosition1, Color.White);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.Opaque);
            spriteBatch.Draw(texture2, spritePosition2, Color.Gray);
            spriteBatch.End();
             * */
            /*
            VertexPositionColor[] vertices = new VertexPositionColor[3];

            vertices[0].Position = new Vector3(-0.5f, -0.5f, 0f);
            vertices[0].Color = Color.Red;
            vertices[1].Position = new Vector3(0, 0.5f, 0f);
            vertices[1].Color = Color.Green;
            vertices[2].Position = new Vector3(0.5f, -0.5f, 0f);
            vertices[2].Color = Color.Yellow;

            graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, 1, VertexPositionColor.VertexDeclaration);
            */

           // GraphicsDevice.Clear(Color.CornflowerBlue);


            /*
            GraphicsDevice.SetVertexBuffer(vertexBuffer);

            //RasterizerState rasterizerState = new RasterizerState();
            //rasterizerState.CullMode = CullMode.None;
            //GraphicsDevice.RasterizerState = rasterizerState;

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);
                
            }
             * */

            //http://rbwhitaker.wikidot.com/drawing-triangles

            base.Draw(gameTime);

           
        }

        void UpdateSprite(GameTime gameTime, ref Vector2 spritePosition, ref Vector2 spriteSpeed)
        {
            // Move the sprite by speed, scaled by elapsed time.
            spritePosition +=
                spriteSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            int MaxX =
                graphics.GraphicsDevice.Viewport.Width - texture1.Width;
            int MinX = 0;
            int MaxY =
                graphics.GraphicsDevice.Viewport.Height - texture1.Height;
            int MinY = 0;

            // Check for bounce.
            if (spritePosition.X > MaxX)
            {
                spriteSpeed.X *= -1;
                spritePosition.X = MaxX;
            }

            else if (spritePosition.X < MinX)
            {
                spriteSpeed.X *= -1;
                spritePosition.X = MinX;
            }

            if (spritePosition.Y > MaxY)
            {
                spriteSpeed.Y *= -1;
                spritePosition.Y = MaxY;
            }

            else if (spritePosition.Y < MinY)
            {
                spriteSpeed.Y *= -1;
                spritePosition.Y = MinY;
            }

        }

        void CheckForCollision()
        {
            BoundingBox bb1 = new BoundingBox(
                new Vector3(spritePosition1.X - (sprite1Width / 2), spritePosition1.Y - (sprite1Height / 2), 0),
                new Vector3(spritePosition1.X + (sprite1Width / 2), spritePosition1.Y + (sprite1Height / 2), 0));

            BoundingBox bb2 = new BoundingBox(
                new Vector3(spritePosition2.X - (sprite2Width / 2), spritePosition2.Y - (sprite2Height / 2), 0),
                new Vector3(spritePosition2.X + (sprite2Width / 2), spritePosition2.Y + (sprite2Height / 2), 0));

            if (bb1.Intersects(bb2))
            {
                soundEffect.Play();
            }

        }
    }
}
