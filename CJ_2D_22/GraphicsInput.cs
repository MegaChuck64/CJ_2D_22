using Microsoft.Xna.Framework.Input;
using MonoGame.Framework.WpfInterop.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJ_2D_22;
public class GraphicsInput
{
    public WpfMouse Mouse;
    public WpfKeyboard Keyboard;

    public MouseState MouseState;
    public KeyboardState KeyState;

    public MouseState LastMouseState;
    public KeyboardState LastKeyState;

    public GraphicsInput(BaseGraphicsWindow graphicsWindow)
    {
        Mouse = new WpfMouse(graphicsWindow);
        Keyboard = new WpfKeyboard(graphicsWindow);
        Mouse.CaptureMouseWithin = false;
    }

    public void Update()
    {
        LastMouseState = MouseState;
        LastKeyState = KeyState;

        MouseState = Mouse.GetState();
        KeyState = Keyboard.GetState();
    }

    public float GetAxis(InputAxis axis)
    {
        switch (axis)
        {
            case InputAxis.Horizontal:
                if (KeyState.IsKeyDown(Keys.A) || KeyState.IsKeyDown(Keys.Left)) return -1f;
                else if (KeyState.IsKeyDown(Keys.D) || KeyState.IsKeyDown(Keys.Right)) return 1f;
                else return 0f;

            case InputAxis.Vertical:
                if (KeyState.IsKeyDown(Keys.S) || KeyState.IsKeyDown(Keys.Down)) return -1f;
                else if (KeyState.IsKeyDown(Keys.W) || KeyState.IsKeyDown(Keys.Up)) return 1f;
                else return 0f;

            default: return 0f;
        }
    }

    public bool WasPressed(Keys key) => LastKeyState.IsKeyUp(key) && KeyState.IsKeyDown(key);

    public bool WasPressed(MouseButton mouseButton) =>
        mouseButton switch
        {
            MouseButton.Left => LastMouseState.LeftButton == ButtonState.Released && MouseState.LeftButton == ButtonState.Pressed,
            MouseButton.Right => LastMouseState.RightButton == ButtonState.Released && MouseState.RightButton == ButtonState.Pressed,
            MouseButton.Middle => LastMouseState.MiddleButton == ButtonState.Released && MouseState.MiddleButton == ButtonState.Pressed,
            _ => false,
        };

    public bool WasReleased(MouseButton mouseButton) =>
        mouseButton switch
        {
            MouseButton.Left => LastMouseState.LeftButton == ButtonState.Pressed && MouseState.LeftButton == ButtonState.Released,
            MouseButton.Right => LastMouseState.RightButton == ButtonState.Pressed && MouseState.RightButton == ButtonState.Released,
            MouseButton.Middle => LastMouseState.MiddleButton == ButtonState.Pressed && MouseState.MiddleButton == ButtonState.Released,
            _ => false,
        };

    public bool WasReleased(Keys key) => LastKeyState.IsKeyDown(key) && KeyState.IsKeyUp(key);
}

public enum MouseButton
{
    Left,
    Right,
    Middle
}
public enum InputAxis
{
    Horizontal,
    Vertical,
}

