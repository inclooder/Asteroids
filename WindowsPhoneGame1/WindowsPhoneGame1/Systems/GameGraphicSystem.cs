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
        protected GraphicRenderer renderer;
       

        public GameGraphicSystem(GameEngine game_engine) :base(game_engine)
        {
            this.renderer = game_engine.getRenderer();
        }

        public override void process(float deltaTime)
        {
        }
        
    }
}
