using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Framework.WpfInterop;
using MonoGame.Framework.WpfInterop.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJ_2D_22
{
    public class DrawingArea : BaseGraphicsWindow
    {
        
        public PixelSprite[,] pixels;

        public Color color;

        public DrawingArea(MainWindow _window) : base(_window) { }

        public override void OnContentLoad(GraphicsDevice graphics)
        {
            pixels = new PixelSprite[32, 32];
            var cntr = GraphicsDevice.Viewport.Bounds.Center;
            var offset = new Vector2(cntr.X - (16 * 8), cntr.Y - (16 * 10));
            for (int x = 0; x < 32; x++)
            {
                for (int y = 0; y < 32; y++)
                {
                    pixels[x, y] = new PixelSprite(GraphicsDevice, new Vector2(x * 8 + offset.X, y * 8 + offset.Y), 8);
                }
            }
        }

        public override void OnUpdate(float dt)
        {
            if (Input.MouseState.LeftButton == ButtonState.Pressed)
            {
                for (int x = 0; x < pixels.GetLength(0); x++)
                {
                    for (int y = 0; y < pixels.GetLength(1); y++)
                    {
                        if (pixels[x, y].GetBounds().Contains(Input.MouseState.X, Input.MouseState.Y))
                        {
                            pixels[x, y].Tint = color;
                            break;
                        }
                    }
                }
            }
        }

        public override void OnDraw(SpriteBatch sb)
        {
            for (int x = 0; x < pixels.GetLength(0); x++)
            {
                for (int y = 0; y < pixels.GetLength(1); y++)
                {
                    pixels[x, y].Draw(sb);
                }
            }
        }
    }

}