using MAH_TowerDefense.Entity;
using MAH_TowerDefense.Entity.Bullets;
using MAH_TowerDefense.Entity.Towers;
using MAH_TowerDefense.Visuals;
using MAH_TowerDefense.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Simon.Mah.Framework;
using Simon.Mah.Framework.Scene2D;
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

        public static RenderTarget2D RenderTarget { get; private set; }

        public static EffectManager Effects { get; private set; }

        public Camera2D Camera { get; private set; }

        private World World { get; set; }

        public WorldRenderer(World world, GraphicsDevice device)
        {
            RenderTarget = new RenderTarget2D(device, WIDTH, HEIGHT);
            Effects = new EffectManager();
            this.Camera = new Camera2D(device, WIDTH, HEIGHT);
            this.World = world;
        }

        public void Update(float delta)
        {
            Effects.Update(delta);

            float x = Camera.GetPosition().X;
            float y = Camera.GetPosition().Y;
            float offset = WIDTH / 30;
            x = MathHelper.Clamp(x, offset, (World.WIDTH * World.TILE_SIZE) - Camera.GetWidth() + UIController.PANEL_WIDTH - offset);
            y = MathHelper.Clamp(y, offset, (World.HEIGHT * World.TILE_SIZE) - Camera.GetHeight() - offset);
            Camera.SetPosition(Camera.GetPosition().X + (x - Camera.GetPosition().X) * delta * 15,
                Camera.GetPosition().Y + (y - Camera.GetPosition().Y) * delta * 15);
        }

        public void Render(SpriteBatch batch)
        {
            // Draw Render Target to texture
            DrawRenderTarget(batch.GraphicsDevice);

            {
                // Clear Screen
                batch.GraphicsDevice.Clear(Color.SkyBlue);

                // Draw World
                batch.Begin(SpriteSortMode.BackToFront,
                         BlendState.AlphaBlend,
                         SamplerState.LinearClamp,
                         null,
                         null,
                         null,
                         Camera.GetMatrix());

                DrawBackground(batch);
                DrawRoad(batch);
                DrawWorld(batch);
                DrawEffects(batch);

                batch.End();
            }

        }

        private void DrawEffects(SpriteBatch batch)
        {
            Effects.Draw(batch);
        }

        private void DrawBackground(SpriteBatch batch)
        {
            for (int j = 0; j < World.HEIGHT; j++)
            {
                for (int i = 0; i < World.WIDTH; i++)
                {
                    batch.Draw(Assets.items, new Rectangle((int)(i * World.TILE_SIZE), (int)(j * World.TILE_SIZE), (int)World.TILE_SIZE + 2, (int)World.TILE_SIZE + 2), Assets.GetRegion("Grass"), Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
                }
            }
        }

        private void DrawRoad(SpriteBatch batch)
        {
            var path = World.GetRoad();
            int width = 32;
            float delta = width * .8f;
            if (path.AntalPunkter >= 4)
            {
                for (float i = path.beginT + delta; i < path.endT; i += delta)
                {
                    Vector2 last = path.GetPos(i - delta);
                    Vector2 pos = path.GetPos(i);
                    float angle = (float)Math.Atan2(pos.X - last.X, pos.Y - last.Y);

                    Sprite sprite = new Sprite(Assets.GetRegion("Pixel"), pos.X - (float)Math.Cos(Math.PI - angle) * width,
                        pos.Y - (float)Math.Sin(Math.PI - angle)*width, 64, width);
                    sprite.ZIndex = .95f;
                    sprite.Color = Color.SlateGray;
                    sprite.Rotation = (float)Math.PI - angle;

                    sprite.Draw(batch);
                }
                //path.Draw(batch);
            }
        }

        private void DrawWorld(SpriteBatch batch)
        {
            foreach (GameObject entity in World.GetEntities())
            {
                entity.Draw(batch);
            }

            foreach (Bullet bullet in World.GetBullets())
            {
                bullet.Draw(batch);
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
                        Camera.GetMatrix());

            foreach (GameObject entity in World.GetEntities().Where(x => x.UsesRenderTarget()))
            {
                // Only draw placed towers
                if (entity is Tower) if (((Tower)entity).Placed == false) continue;

                DrawRoad(sb);

                entity.Draw(sb);
            }

            sb.End();

            device.SetRenderTarget(null);
        }
    }
}
