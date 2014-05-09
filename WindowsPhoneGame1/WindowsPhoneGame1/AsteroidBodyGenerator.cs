using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Asteroids.Generators
{
    class AsteroidBodyGenerator
    {
        private Random random;

        public AsteroidBodyGenerator()
        {
            random = new Random();
        }

        public Vector2[] randomAsteroidBody(int complexity, int minBoneSize, int maxBoneSize)
        {
            if (complexity < 3)
            {
                throw new System.ArgumentException("Complexity must be greater than 2");
            }

            Vector2[] body = new Vector2[complexity];

            for (int i = 0; i < complexity; i++)
            {
                int x = random.Next(minBoneSize, maxBoneSize);
                int y = random.Next(minBoneSize, maxBoneSize);
                body[i] = new Vector2(x, y);
            }

            return body;
        }
    }
}
