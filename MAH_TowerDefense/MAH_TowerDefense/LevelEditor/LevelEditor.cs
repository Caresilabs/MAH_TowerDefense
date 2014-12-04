
using MAH_TowerDefense.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Simon.Mah.Framework.Scene2D;
using Simon.Mah.Framework.Tools;
using System.Collections.Generic;
//using System.Windows.Forms;
using WinFormsContentLoading;

namespace MAH_TowerDefense.Screens
{
    public class LevelEditor : GraphicsDeviceControl
    {
        private List<Vector2> points;
        private SpriteBatch batch;
        private Texture2D texture;
        private Camera2D camera;
        private Road road;

        private float width;
        private float height;
        private float tileSize;
        private bool drawLock;

        protected override void Initialize()
        {
            System.Windows.Forms.Application.Idle += delegate { Invalidate(); };
            ContentManager manager = new ContentManager(Services, "Content");

            this.batch = new SpriteBatch(GraphicsDevice);
            this.points = new List<Vector2>();
            this.texture = manager.Load<Texture2D>("Graphics/items");
            this.road = new Road(GraphicsDevice, new TextureRegion(texture, 64, 0, 96, 96));
            this.camera = new Camera2D(GraphicsDevice, 1080, 1280);
            this.tileSize = camera.GetWidth() / 20;
            this.drawLock = false;
        }

        protected override void Draw()
        {
            UpdateEditor();

            GraphicsDevice.Clear(Color.Cyan);

            batch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        SamplerState.LinearClamp,
                        null,
                        null,
                        null,
                        camera.GetMatrix());

            road.Render(batch);
            road.Draw(batch);
            DrawBackground(batch);

            batch.End();
        }

        private void DrawBackground(SpriteBatch batch)
        {
            for (float j = 0; j < camera.GetHeight(); j += tileSize)
            {
                for (float i = 0; i < camera.GetWidth(); i += tileSize)
                {
                    batch.Draw(texture, new Rectangle((int)(i), (int)(j), (int)tileSize + 2, (int)tileSize + 2), Assets.GetRegion("Grass"), Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
                }
            }
        }

        private void UpdateEditor()
        {
            camera.SetZoom(1 + Mouse.GetState().ScrollWheelValue);

            if (Mouse.GetState().ScrollWheelValue != 0)
            {

            }

            // Draw Points
            if (InputHandler.Clicked() && !drawLock)
            {
                Vector2 worldMouse = camera.Unproject(Mouse.GetState().X, Mouse.GetState().Y);
                AddPoint(worldMouse);
            }

            // Finish Drawing
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                FinishDrawing();
            }

            float speed = 10f;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                camera.Translate(-speed, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                camera.Translate(speed, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                camera.Translate(0, -speed);
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                camera.Translate(0, speed);
        }

        private void AddPoint(Vector2 pos)
        {
            road.AddPoint(new Vector2(pos.X * tileSize, pos.Y * tileSize));
            points.Add(pos);
            road.UpdateParts();
        }

        private void FinishDrawing()
        {
            if (width > height)
                AddPoint(new Vector2(width * 1.1f, height / 2));
            else
                AddPoint(new Vector2(width / 2, height * 1.1f));

            drawLock = true;
            road.UpdateParts();
        }

        public void ClearPath()
        {
            road.Clean();
            road.UpdateParts();
            drawLock = false;
        }

        public void SetSize(float width, float height)
        {
            this.width = width;
            this.height = height;

            ClearPath();

            if (width > height)
            {
                AddPoint(new Vector2(-100, height / 2));
                AddPoint(new Vector2(-70, -5 + height / 2));
                AddPoint(new Vector2(-30, height / 2));
                AddPoint(new Vector2(0, height / 2));
                AddPoint(new Vector2(100, height / 2));
            }
            else
            {

            }

            road.UpdateParts();
        }

        public List<Vector2> GetPoints()
        {
            return points;
        }
    }
}
