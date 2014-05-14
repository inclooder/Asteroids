using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    public class AsteroidComponentGenerator
    {
        private int max_bone_length, min_bone_length;
        private Random num_gen;

        public AsteroidComponentGenerator(int min_bone_length, int max_bone_length)
        {
            this.max_bone_length = max_bone_length;
            this.min_bone_length = min_bone_length;
            num_gen = new Random();
        }

        public AsteroidBodyComponent genearate(int num_of_bones)
        {
            if (num_of_bones < 2)
            {
                throw new ArgumentException("Number of bones should be greater or equal to 2");
            }

            List<float> bones_lengths = new List<float>();

            for (int i = 0; i < num_of_bones; i++)
            {
                
               bones_lengths.Add(num_gen.Next(min_bone_length, max_bone_length));
            }

            return new AsteroidBodyComponent(bones_lengths.ToArray());
        }
    }
}
