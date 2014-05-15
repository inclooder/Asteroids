using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids
{
    public class AsteroidBodyComponent : EntityComponent
    {
        private static Color[] avil_colors = new Color[]{
            Color.Red,
            Color.Blue,
            Color.Green,
            Color.Black
        };
        
        private List<float> bones_length;
        private Color[] colors;
        private List<VertexPositionColor> vertices_list;
        private Vector2[] bones;


        public AsteroidBodyComponent(float[] bones_length, Color[] colors)
        {
            this.vertices_list = new List<VertexPositionColor>();
            this.bones_length = bones_length.ToList();
            this.colors = colors;
            updateVertices();
        }

        public AsteroidBodyComponent(float[] bones_length)
        {
            this.vertices_list = new List<VertexPositionColor>();
            this.bones_length = bones_length.ToList();
            randomizeColors();
            updateVertices();
        }


        public void randomizeColors()
        {
            Random rand = new Random();
            colors = new Color[bones_length.Count];
            List<int> colors_used = new List<int>();
            for (int i = 0; i < bones_length.Count; i++)
            {
                
               // while (true)
                //{
                    int ran = rand.Next(0, avil_colors.Length);
                  //  if (colors_used.Contains(ran)) continue;
                    colors[i] = avil_colors[ran];
                    colors_used.Add(ran);
                  //  break;
                //}
            }
        }
      
        private void updateVertices()
        {
            vertices_list.Clear();
            Vector2 start = new Vector2(-10, 0);
            
            bones = new Vector2[bones_length.Count];
            float radians_between_bones = (2*MathHelper.Pi) / bones_length.Count;
            for (int i = 0; i < bones.Length; i++)
            {
                Vector2 myvec = Vector2.Transform(start, Matrix.CreateRotationZ(i * radians_between_bones));
                myvec.Normalize();
                myvec = myvec * bones_length[i];
                bones[i] = myvec;

            }

            int num_of_bones = bones.Length;
        

            for (int i = 0; i < num_of_bones - 1; i++)
            {
                vertices_list.Add(new VertexPositionColor(new Vector3(0, 0, 0), colors[i + 1]));
                vertices_list.Add(new VertexPositionColor(new Vector3(bones[i].X, bones[i].Y, 0), colors[i+1]));
                vertices_list.Add(new VertexPositionColor(new Vector3(bones[i + 1].X, bones[i + 1].Y, 0), colors[i+1]));
            }

            vertices_list.Add(new VertexPositionColor(new Vector3(0, 0, 0), colors[0]));
            vertices_list.Add(new VertexPositionColor(new Vector3(bones.Last().X, bones.Last().Y, 0), colors[0]));
            vertices_list.Add(new VertexPositionColor(new Vector3(bones.First().X, bones.First().Y, 0), colors[0]));
                        
        }

        public VertexPositionColor[] getVertices()
        {
            return vertices_list.ToArray();
        }

        public Vector2[] getBones() 
        {
            return bones;
        }

      
    }
}
