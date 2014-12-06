
using MAH_TowerDefense.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Simon.Mah.Framework.Scene2D;
using Simon.Mah.Framework.Tools;
using System.Collections.Generic;
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

        public void ScrollEvent(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            camera.SetZoom(camera.GetZoom().X + (e.Delta/900f));
        }

        public void PreviewKey(object sender, System.Windows.Forms.PreviewKeyDownEventArgs e)
        {
            // Finish Drawing
            if (e.KeyCode == System.Windows.Forms.Keys.Space)
                FinishDrawing();

            float speed = 10f;
            if (e.KeyCode == System.Windows.Forms.Keys.A)
                camera.Translate(-speed, 0);
            if (e.KeyCode == System.Windows.Forms.Keys.D)
                camera.Translate(speed, 0);
            if (e.KeyCode == System.Windows.Forms.Keys.W)
                camera.Translate(0, -speed);
            if (e.KeyCode == System.Windows.Forms.Keys.S)
                camera.Translate(0, speed);
        }

        public void MousePressedDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Focus();

            // Draw Points
            if (!drawLock && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Vector2 worldMouse = camera.Unproject(e.X, e.Y);
                AddPoint(new Vector2(worldMouse.X / tileSize, worldMouse.Y / tileSize));
            }
        }

        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.Cyan);

            batch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        SamplerState.LinearClamp,
                        null,
                        null,
                        null,
                        camera.GetMatrix());

            road.Render(batch, true);
            DrawBackground(batch);

            batch.End();
        }

        private void DrawBackground(SpriteBatch batch)
        {
            for (float j = 0; j < height * tileSize; j += tileSize)
            {
                for (float i = 0; i < width * tileSize; i += tileSize)
                {
                    batch.Draw(texture, new Rectangle((int)(i), (int)(j), (int)tileSize + 2, (int)tileSize + 2), Assets.GetRegion("Grass"), Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
                }
            }
        }

        private void AddPoint(Vector2 pos)
        {
            road.AddPoint(new Vector2(pos.X * tileSize, pos.Y * tileSize));
            points.Add(pos);
            road.UpdateParts();
        }

        public void ClearPath()
        {
            InitPath();
            drawLock = false;
        }

        public void SetSize(float width, float height, bool initPath = true)
        {
            this.width = width;
            this.height = height;

            if (initPath)
                InitPath();
        }

        private void InitPath()
        {
            road.Clean();
            points.Clear();

            if (width > height)
            {
                AddPoint(new Vector2(-1.5f, height / 2));
                AddPoint(new Vector2(-.6f, -.2f + height / 2));
                AddPoint(new Vector2(-.2f, -.1f + height / 2));
                AddPoint(new Vector2(0, height / 2));
                AddPoint(new Vector2(1, height / 2));
            }
            else
            {

            }
            road.UpdateParts();
        }

        public void InitPath(List<Vector2> pnts)
        {
            this.road.Clean();
            this.points.Clear();

            foreach (var point in pnts)
            {
                this.road.AddPoint(new Vector2(point.X * tileSize, point.Y * tileSize));
                this.points.Add(point);
            }

            this.road.UpdateParts();
            this.drawLock = true;
        }

        public void FinishDrawing()
        {
            if (drawLock) return;

            if (width > height)
                AddPoint(new Vector2(width + 2, height / 2));
            else
                AddPoint(new Vector2(width / 2, height + 2));

            drawLock = true;
            road.UpdateParts();
        }

        public List<Vector2> GetPoints()
        {
            return points;
        }
    }
}
