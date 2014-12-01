using MAH_TowerDefense.Entity;
using MAH_TowerDefense.Entity.Towers;
using MAH_TowerDefense.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Simon.Mah.Framework;
using Simon.Mah.Framework.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Views
{
    public class WorldRenderer
    {
        public const int WIDTH = 1280;
        public const int HEIGHT = 720;

        public static RenderTarget2D RenderTarget;

        private Camera2D camera;
        private World world;

        public WorldRenderer(World world, GraphicsDevice device)
        {
            RenderTarget = new RenderTarget2D(device, WIDTH, HEIGHT);
            this.camera = new Camera2D(device, WIDTH, HEIGHT);
            this.world = world;
        }

        public void Update(float delta)
        {
            float x = camera.GetPosition().X;
            float y = camera.GetPosition().Y;
            float offset = WIDTH / 30;
            x = MathHelper.Clamp(x, offset, (World.WIDTH * World.TILE_SIZE) - camera.GetWidth() + UIController.PANEL_WIDTH - offset);
            y = MathHelper.Clamp(y, offset, (World.HEIGHT * World.TILE_SIZE) - camera.GetHeight() - offset);
            camera.SetPosition(camera.GetPosition().X + (x - camera.GetPosition().X) * delta * 15,
                camera.GetPosition().Y + (y - camera.GetPosition().Y) * delta * 15);
        }

        public void Render(SpriteBatch batch)
        {
            // Draw Render Target to texture
            DrawRenderTarget(batch.GraphicsDevice);

            // Clear Screen
            batch.GraphicsDevice.Clear(Color.SkyBlue);

            {
                // Draw World
                batch.Begin(SpriteSortMode.BackToFront,
                         BlendState.AlphaBlend,
                         SamplerState.LinearClamp,
                         null,
                         null,
                         null,
                         camera.GetMatrix());

                DrawBackground(batch);
                DrawRoad(batch);
                DrawWorld(batch);

                batch.End();
            }

        }

        private void DrawBackground(SpriteBatch batch)
        {
        }

        private void DrawRoad(SpriteBatch batch)
        {
           var path = world.GetRoad();
           int delta = 25;
           if (path.AntalPunkter >= 4)
           {
               for (float i = path.beginT + delta; i < path.endT; i += delta)
               {
                   Vector2 last = path.GetPos(i - delta);
                   Vector2 pos = path.GetPos(i);
                   float angle = (float)Math.Atan2(pos.X - last.X, pos.Y - last.Y);

                   Sprite sprite = new Sprite(new Simon.Mah.Framework.Scene2D.TextureRegion(Assets.items, 0, 17, 22, 22), pos.X, pos.Y, 48, 32);
                   sprite.ZIndex = .95f;
                   sprite.Rotation = (float)Math.PI - angle;

                   sprite.Draw(batch);
               }
              // path.Draw(batch);
           }
        }

        private void DrawWorld(SpriteBatch batch)
        {
            foreach (GameObject entity in world.GetEntities())
            {
                entity.Draw(batch);
            }
        }

        private void DrawRenderTarget(GraphicsDevice device)
        {
            SpriteBatch sb = new SpriteBatch(device);

            device.SetRenderTarget(RenderTarget);
            device.Clear(Color.Transparent);

            //sb.Begin();

            sb.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        SamplerState.LinearClamp,
                        null,
                        null,
                        null,
                        camera.GetMatrix());

            foreach (GameObject entity in world.GetEntities().Where(x => x.UsesRenderTarget()))
            {
                // Only draw placed towers
                if (entity is Tower) if (((Tower)entity).Placed == false) continue;

                DrawRoad(sb);

                entity.Draw(sb);
            }

            sb.End();

            device.SetRenderTarget(null);
        }

        public Camera2D GetCamera()
        {
            return camera;
        }
    }
}
