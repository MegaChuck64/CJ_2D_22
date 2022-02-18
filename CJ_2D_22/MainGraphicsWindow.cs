
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CJ_2D_22;

public class MainGraphicsWindow : BaseGraphicsWindow
{
    public PixelObject CheckerBoard;
    public Color BrushColor = Color.White;
    public MainGraphicsWindow(MainWindow _window) : base(_window) { }

    public override void OnContentLoad(GraphicsDevice graphics)
    {
        var cntr = GraphicsDevice.Viewport.Bounds.Center;
        
        int pixelSize = 16;
        int boardSize = GraphicsDevice.Viewport.Height/pixelSize;
        int size = pixelSize * boardSize;
        //var offset = new Vector2(cntr.X - (size), cntr.Y - (size));
        
        var cols = new Color[size, size];
        var flip = false;
        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                for (int j = x * pixelSize; j < (x * pixelSize) + pixelSize; j++)
                {
                    for (int k = y * pixelSize; k < (y * pixelSize) + pixelSize; k++)
                    {
                        cols[j, k] = flip ? Color.Gray : Color.DarkGray;
                    }
                }
                flip = !flip;
            }
            flip = !flip;
        }

        CheckerBoard = new PixelObject(graphics, cols)
        {
           // Position = offset
        };
    }

    public override void OnUpdate(float dt)
    {
        if (Input.MouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
        {
            var (x, y) = CheckerBoard.GetPixelLocationFromWorld(Input.MouseState.Position.ToVector2());
            CheckerBoard.SetColor(x, y, BrushColor);
        }
    }

    public override void OnDraw(SpriteBatch sb)
    {
        CheckerBoard.Draw(sb);
    }

}