using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJ_2D_22;

public struct PixelSprite
{
    public int Size { get; set; }
    public Color Tint { get; set; }
    public Vector2 Location { get; set; }
    private Texture2D Texture { get; set; }

    public PixelSprite(GraphicsDevice graphics, Vector2 location, int size = 8)
    {
        Size = size;
        Tint = Color.White;
        Location = location;
        var newText = new Texture2D(graphics, 1, 1);
        newText.SetData(new Color[] { Color.White });
        Texture = newText;
    }

    public Rectangle GetBounds()
    {
        return new Rectangle((int)Location.X, (int)Location.Y, Size, Size);
    }
    public void Draw(SpriteBatch sb)
    {
        sb.Draw(Texture, GetBounds(), Tint);
    }
}

