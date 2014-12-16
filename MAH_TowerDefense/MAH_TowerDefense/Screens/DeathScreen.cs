using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Simon.Mah.Framework;
using Simon.Mah.Framework.Scene2D;

namespace MAH_TowerDefense.Screens
{
    public class DeathScreen : Screen
    {

        public override void Init() { }

        public override void Update(float delta)
        {
            if (InputHandler.KeyReleased(Keys.M) || InputHandler.Clicked())
                SetScreen(new MainMenuScreen());
        }

        public override void Draw(SpriteBatch batch)
        {
            GetGraphics().Clear(Color.Black);

            batch.Begin();

            // Draw title
            DrawCenterString(batch, GetGraphics().Viewport.Width, "You Failed the Kingdom!", 180, Color.YellowGreen, 1.4f);
            DrawCenterString(batch, GetGraphics().Viewport.Width, "Congratulations fool, now redo, and do it right!!", 250, Color.YellowGreen);

            batch.End();
        }

        public static void DrawCenterString(SpriteBatch batch, float width, string text, float y, Color color, float scale = 1)
        {
            batch.DrawString(Assets.font, text,
                    new Vector2(
                        width / 2 - ((Assets.font.MeasureString(text).Length() * scale) / 2), y),
                         color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        public static void DrawCenterString(SpriteBatch batch, float width, string text, float y, float scale = 1)
        {
            DrawCenterString(batch, width, text, y, Color.White, scale);
        }

        public override void Dispose() { }
    }
}
