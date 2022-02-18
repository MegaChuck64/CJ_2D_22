using CJ_2D_22.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Framework.WpfInterop;
using MonoGame.Framework.WpfInterop.Input;

namespace CJ_2D_22
{
    public abstract class BaseGraphicsWindow : WpfGame
    {
        private WpfGraphicsDeviceService _graphicsDeviceManager;
        private SpriteBatch sb;
        //private float scale = 1f;

        public GameCamera Camera;
        public MainWindow Window { get; private set; }
        public GraphicsInput Input { get; private set; }
        public Color BackgroundColor;
        public float ZoomSpeed = 1f;
        public float CameraMoveSpeed = 400f;

        public BaseGraphicsWindow(MainWindow _window)
        {
            Window = _window;    
        }

        protected override void Initialize()
        {
            _graphicsDeviceManager = new WpfGraphicsDeviceService(this);
            
            Input = new GraphicsInput(this);
            sb = new SpriteBatch(GraphicsDevice);
            Camera = new(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            base.Initialize();
            
            OnContentLoad(GraphicsDevice);
        }

        protected override void Update(GameTime time)
        {
            var dt = (float)time.ElapsedGameTime.TotalSeconds;
            Input.Update();

            if (Input.KeyState.IsKeyDown(Keys.LeftControl))
            {
                if (Input.KeyState.IsKeyDown(Keys.OemPlus))
                {
                    //scale += ZoomSpeed * (float)time.ElapsedGameTime.TotalSeconds;
                    //Window.GameGrid.LayoutTransform = new System.Windows.Media.ScaleTransform(scale, scale);
                    //Window.GameGridScroll.ScrollToVerticalOffset(Window.GameGridScroll.ScrollableHeight / 2);
                    //Window.GameGridScroll.ScrollToHorizontalOffset(Window.GameGridScroll.ScrollableWidth / 2);

                    Camera.AdjustZoom(ZoomSpeed * dt);

                }
                else if (Input.KeyState.IsKeyDown(Keys.OemMinus))
                {
                    //scale -= ZoomSpeed * (float)time.ElapsedGameTime.TotalSeconds;
                    //Window.GameGrid.LayoutTransform = new System.Windows.Media.ScaleTransform(scale, scale);
                    //Window.GameGridScroll.ScrollToVerticalOffset(Window.GameGridScroll.ScrollableHeight / 2);
                    //Window.GameGridScroll.ScrollToHorizontalOffset(Window.GameGridScroll.ScrollableWidth / 2);
                    Camera.AdjustZoom(-ZoomSpeed * dt);

                }

                var movement = new Vector2(Input.GetAxis(InputAxis.Horizontal), -Input.GetAxis(InputAxis.Vertical));
                Camera.MoveCamera(movement * dt * CameraMoveSpeed);
                
                if (Input.KeyState.IsKeyDown(Keys.P) && Input.LastKeyState.IsKeyUp(Keys.P))
                {
                    LogManager.AddLog($"CamPos: {Camera.Position}, CamZoom: {Camera.Zoom}");
                }

            }
            OnUpdate(dt);
        }
        protected override void Draw(GameTime time)
        {
            GraphicsDevice.Clear(Color.DarkSlateGray);
            sb.Begin(
                samplerState: SamplerState.PointClamp,
                transformMatrix: Camera.TranslationMatrix,
                rasterizerState: RasterizerState.CullNone);
            OnDraw(sb);
            sb.End();
        }

        public abstract void OnContentLoad(GraphicsDevice graphics);
        public abstract void OnUpdate(float dt);
        public abstract void OnDraw(SpriteBatch sb);
    }
}
