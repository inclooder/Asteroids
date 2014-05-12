using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Asteroids
{
    class GameGraphicSystem : GameSystem
    {
        protected GraphicsDevice graphics_device;
        protected AssetsManager assets_manager;
        protected SpriteBatch batch;
        protected BasicEffect effect;
        protected VertexBuffer vertexBuffer;

        public GameGraphicSystem(GameEngine game_engine) :base(game_engine)
        {
            this.graphics_device = game_engine.getGraphicsDevice();
            this.assets_manager = game_engine.getAssetsManager();
            this.batch = new SpriteBatch(this.graphics_device);
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
        }
        
    }
}
