using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids
{
    class AsteroidBodyComponent : EntityComponent
    {
        private static Color DEFAULT_COLOR = Color.Black;
        private List<float> bones_length;
        private Color color;
        private List<VertexPositionColor> vertices_list;
        private Vector2[] bones;


        public AsteroidBodyComponent(float[] bones_length, Color color)
        {
            this.vertices_list = new List<VertexPositionColor>();
            this.bones_length = bones_length.ToList();
            this.color = color;
            updateVertices();
        }

        public AsteroidBodyComponent(float[] bones_length)
        {
            this.vertices_list = new List<VertexPositionColor>();
            this.bones_length = bones_length.ToList();
            this.color = DEFAULT_COLOR;
            updateVertices();
        }

        public Color getColor()
        {
            return color;
        }

        public void setColor(Color color)
        {
            this.color = color;
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
        

            Color[] colors = new Color[] {    
                            Color.Red,
                            Color.Green,
                            Color.Blue,
                            Color.Violet,
                            Color.Purple,
                            Color.Pink,
                            Color.PowderBlue,
                            Color.Gray,
                            Color.LightGreen,
                            Color.Black
                        };

            for (int i = 0; i < num_of_bones - 1; i++)
            {
                vertices_list.Add(new VertexPositionColor(new Vector3(0, 0, 0), Color.Yellow));
                vertices_list.Add(new VertexPositionColor(new Vector3(bones[i].X, bones[i].Y, 0), colors[i]));
                vertices_list.Add(new VertexPositionColor(new Vector3(bones[i + 1].X, bones[i + 1].Y, 0), colors[i]));
            }

            vertices_list.Add(new VertexPositionColor(new Vector3(0, 0, 0), Color.Yellow));
            vertices_list.Add(new VertexPositionColor(new Vector3(bones.Last().X, bones.Last().Y, 0), Color.LightSkyBlue));
            vertices_list.Add(new VertexPositionColor(new Vector3(bones.First().X, bones.First().Y, 0), Color.LightSkyBlue));
                        
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
