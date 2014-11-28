using MAH_TowerDefense.Entity;
using MAH_TowerDefense.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            this.camera = new Camera2D(device, WIDTH, HEIGHT);
            this.world = world;
        }

        public void Render(SpriteBatch batch)
        {
            DrawRenderTarget(batch);

            {
                batch.Begin(SpriteSortMode.BackToFront,
                         BlendState.AlphaBlend,
                         SamplerState.LinearClamp,
                         null,
                         null,
                         null,
                         camera.GetMatrix());

                DrawBackground(batch);
                DrawWorld(batch);

                batch.End();
            }
        }

        private void DrawBackground(SpriteBatch batch)
        {
        }

        private void DrawWorld(SpriteBatch batch)
        {
            foreach (GameObject entity in world.GetEntities())
            {
                entity.Draw(batch);
            }
        }

        private void DrawRenderTarget(SpriteBatch batch)
        {
            SpriteBatch sb = new SpriteBatch(batch.GraphicsDevice);

            batch.GraphicsDevice.SetRenderTarget(RenderTarget);
            batch.GraphicsDevice.Clear(Color.Transparent);
            sb.Begin();

            foreach (GameObject entity in world.GetEntities().Where(x => x.UsesRenderTarget()))
            {
                entity.Draw(sb);
            }

            sb.End();

            batch.GraphicsDevice.SetRenderTarget(null);
        }
    }
}
