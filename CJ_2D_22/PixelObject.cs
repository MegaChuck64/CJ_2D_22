
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CJ_2D_22;

public class PixelObject
{
    public Color[,] Pixels { get; private set; }
    public Vector2 Position { get; set; }
    public Color Tint { get; set; }
    private Texture2D texture;
    public PixelObject(GraphicsDevice graphics, int size, Color? color = null)
    {
        texture = new Texture2D(graphics, size, size);
        Tint = Color.White;

        var cols1D = new Color[size * size];
        Pixels = new Color[size, size];
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                Pixels[x, y] = color ?? Color.Transparent;
                cols1D[y * x + size] = Pixels[x, y];
            }
        }
        texture.SetData(cols1D);
    }

    public PixelObject(GraphicsDevice graphics, Color[,] colors)
    {
        Tint = Color.White;
        texture = new Texture2D(graphics, colors.GetLength(0), colors.GetLength(1));
        Pixels = colors;
        texture.SetData(PixelsTo1D());
    }

    public void SetColor(int x, int y, Color color)
    {
        if (x >= 0 && y >= 0 && x < Pixels.GetLength(0) && y < Pixels.GetLength(1))
        {
            Pixels[x, y] = color;
            texture.SetData(PixelsTo1D());
        }
    }

    private Color[] PixelsTo1D()
    {
        var col1D = new Color[Pixels.GetLength(0) * Pixels.GetLength(1)];
        for (int x = 0; x < Pixels.GetLength(0); x++)
        {
            for (int y = 0; y < Pixels.GetLength(1); y++)
            {
                col1D[y + x * Pixels.GetLength(0)] = Pixels[x, y]; 
            }
        }
        return col1D;
    }

    public (int x, int y) GetPixelLocationFromWorld(Vector2 worldPosition)
    {
        var offset = worldPosition - Position;
        return ((int)offset.X, (int)offset.Y);
    }

    public void Draw(SpriteBatch sb)
    {
        sb.Draw(texture, Position, Tint);
    }
}
