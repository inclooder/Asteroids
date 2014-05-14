using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids
{
    public class GraphicRenderer
    {
        protected GraphicsDevice device;
        protected Matrix world, projection, view;
        protected BasicEffect effect;
        protected VertexBuffer vertexBufferTriangles, vertexBufferLine;
        

        public GraphicRenderer(GraphicsDevice device)
        {
            this.device = device;
            world = Matrix.CreateTranslation(0, 0, 0);
            view = Matrix.CreateLookAt(new Vector3(0, 0, 1), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            projection = Matrix.CreateOrthographic(device.Viewport.Width, device.Viewport.Height, 1.0f, 1000.0f);

            effect = new BasicEffect(device);
            effect.World = world;
            effect.View = view;
            effect.Projection = projection;
            effect.VertexColorEnabled = true;
            effect.LightingEnabled = false;
            effect.FogEnabled = false;

           RasterizerState rasterizerState = new RasterizerState();
           rasterizerState.CullMode = CullMode.None;
           device.RasterizerState = rasterizerState;

           vertexBufferTriangles = new VertexBuffer(device, typeof(VertexPositionColor), 3, BufferUsage.WriteOnly);
           vertexBufferLine  = new VertexBuffer(device, typeof(VertexPositionColor), 2, BufferUsage.WriteOnly);
        }

        ~GraphicRenderer()
        {
            vertexBufferTriangles.Dispose();
            vertexBufferLine.Dispose();
        }

   

        public void drawLine(Vector2 va, Vector2 vb, Color c)
        {
            drawLine(va, vb, c, c);
        }

        public void drawLine(Vector2 va, Vector2 vb, Color ca, Color cb)
        {
            Vector3 va_world = device.Viewport.Unproject(new Vector3(va.X, va.Y, 0), projection, view, world);
            Vector3 vb_world = device.Viewport.Unproject(new Vector3(vb.X, vb.Y, 0), projection, view, world);

            VertexPositionColor[] vertices = new VertexPositionColor[2];
            vertices[0] = new VertexPositionColor(va_world, ca);
            vertices[1] = new VertexPositionColor(vb_world, cb);
          
            
            vertexBufferLine.SetData<VertexPositionColor>(vertices);


            device.SetVertexBuffer(vertexBufferLine);

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.DrawPrimitives(PrimitiveType.LineList, 0, 1);
            }

            device.SetVertexBuffer(null);

            
            
        }

        public void drawRectangle(Rectangle rect, Color color)
        {
            drawRectangle(rect, color, color, color, color);
        }

        public void drawRectangle(Rectangle rect, Color ca, Color cb, Color cc, Color cd)
        {
            Vector2 top_left = new Vector2(rect.Left, rect.Top);
            Vector2 top_right = new Vector2(rect.Right, rect.Top);
            Vector2 bottom_left = new Vector2(rect.Left, rect.Bottom);
            Vector2 bottom_right = new Vector2(rect.Right, rect.Bottom);
            drawLine(top_left, top_right, ca);
            drawLine(top_right, bottom_right, cb);
            drawLine(bottom_right, bottom_left, cc);
            drawLine(bottom_left, top_left, cd);
        }

        public void fillTriangle(Vector2 va, Vector2 vb, Vector2 vc, Color color)
        {
            fillTriangle(va, vb, vc, color, color, color);
        }

        public void fillTriangle(Vector2 va, Vector2 vb, Vector2 vc, Color ca, Color cb, Color cc)
        {
            Vector3 va_world = device.Viewport.Unproject(new Vector3(va.X, va.Y, 0), projection, view, world);
            Vector3 vb_world = device.Viewport.Unproject(new Vector3(vb.X, vb.Y, 0), projection, view, world);
            Vector3 vc_world = device.Viewport.Unproject(new Vector3(vc.X, vc.Y, 0), projection, view, world);

            VertexPositionColor[] vertices = new VertexPositionColor[3];
            vertices[0] = new VertexPositionColor(va_world, ca);
            vertices[1] = new VertexPositionColor(vb_world, cb);
            vertices[2] = new VertexPositionColor(vc_world, cc);

          
            
            vertexBufferTriangles.SetData<VertexPositionColor>(vertices);

            device.SetVertexBuffer(vertexBufferTriangles);

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);
            }
            device.SetVertexBuffer(null);
            
        }

    }
}
