using MAH_TowerDefense.Screens;
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
    public class HUD
    {
        public const int WIDTH = 1280;
        public const int HEIGHT = 720;

        private GameScreen gameScreen;
        private World world;
        private Camera2D camera;

        public HUD(GameScreen gameScreen)
        {
            this.gameScreen = gameScreen;
            this.world = gameScreen.GetWorld();
            this.camera = new Camera2D(gameScreen.GetGraphics(), 100, 100);
        }

        public void Update(float delta)
        {

        }

        public void Draw(SpriteBatch batch)
        {
            batch.Begin(SpriteSortMode.BackToFront,
                      BlendState.AlphaBlend,
                      SamplerState.LinearClamp,
                      null,
                      null,
                      null,
                      camera.GetMatrix());

            batch.End();

        }

        public static void drawCenterString(SpriteBatch batch, string text, float y, Color color, float scale = 1)
        {
            batch.DrawString(Assets.font, text,
                    new Vector2(
                         batch.GraphicsDevice.Viewport.Width / 2 - ((Assets.font.MeasureString(text).Length() / 2) * scale), y),
                         color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        public static void drawCenterString(SpriteBatch batch, string text, float y, float scale = 1)
        {
            drawCenterString(batch, text, y, Color.Black, scale);
        }
    }
}
