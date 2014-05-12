using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids
{
    public class AssetsManager
    {
        private List<Texture2D> textures = new List<Texture2D>();

        public AssetsManager()
        {
            
        }

        public int addTexture(Texture2D texture){
            textures.Add(texture);
            return textures.Count-1;
        }

        public Texture2D getTexture(int textureID)
        {
            return textures.ElementAt(textureID);
        }
    }
}
