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

        public static RenderTarget2D MiniMap { get; private set; }

        public static EffectManager Effects { get; private set; }

        public Camera2D Camera { get; private set; }

        public Camera2D MiniMapCamera { get; private set; }

        private World World { get; set; }

        public WorldRenderer(World world, GraphicsDevice device)
        {
            Effects = new EffectManager();
            RenderTarget = new RenderTarget2D(device, WIDTH, HEIGHT);

            // MiniMap
            MiniMap = new RenderTarget2D(device, 480, 320); 
           
            if (World.WIDTH > World.HEIGHT)
                this.MiniMapCamera = new Camera2D(device, World.WIDTH * World.TILE_SIZE, World.WIDTH * World.TILE_SIZE * 0.666f);
            else
                this.MiniMapCamera = new Camera2D(device, World.HEIGHT * World.TILE_SIZE * 1.5f, World.HEIGHT * World.TILE_SIZE);

            this.MiniMapCamera.SetPosition(-MiniMapCamera.GetWidth() / 2 + (World.WIDTH * World.TILE_SIZE) / 2, 0);
            
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
            DrawRenderTarget(batch, RenderTarget, Camera);
            DrawRenderTarget(batch, MiniMap, MiniMapCamera);

            {
                // Clear Screen
                batch.GraphicsDevice.Clear(Color.Green);

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
            int scale = 2;
            for (float j = Camera.GetPosition().Y - (Camera.GetPosition().Y % (World.TILE_SIZE * scale)); j < Camera.GetPosition().Y + Camera.GetHeight(); j += World.TILE_SIZE * scale)
            {
                for (float i = Camera.GetPosition().X - (Camera.GetPosition().X % (World.TILE_SIZE * scale)); i < Camera.GetPosition().X + Camera.GetWidth(); i += World.TILE_SIZE * scale)
                {
                    batch.Draw(Assets.items, new Rectangle((int)(i), (int)(j), scale * (int)World.TILE_SIZE + 2, scale * (int)World.TILE_SIZE + 2), Assets.GetRegion("Grass"), Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
                }
            }
        }

        private void DrawRoad(SpriteBatch batch)
        {
            World.GetRoad().Render(batch);
        }

        private void DrawWorld(SpriteBatch batch)
        {
            foreach (GameObject entity in World.GetEntities())
            {
                if (entity.GetPosition().X > 0 && entity.GetPosition().Y > 0)
                    entity.Draw(batch);
            }

            foreach (Bullet bullet in World.GetBullets())
            {
                bullet.Draw(batch);
            }
        }

        private void DrawRenderTarget(SpriteBatch batch, RenderTarget2D target, Camera2D cam)
        {
            batch.GraphicsDevice.SetRenderTarget(target);
            batch.GraphicsDevice.Clear(Color.Transparent);

            batch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        SamplerState.LinearClamp,
                        null,
                        null,
                        null,
                        cam.GetMatrix());

            DrawRoad(batch);

            foreach (GameObject entity in World.GetEntities().Where(x => x.UsesRenderTarget()))
            {
                // Only draw placed towers
                if (entity is Tower) if (((Tower)entity).Placed == false) continue;

                entity.Draw(batch);
            }

            batch.End();

            batch.GraphicsDevice.SetRenderTarget(null);
        }
    }
}
