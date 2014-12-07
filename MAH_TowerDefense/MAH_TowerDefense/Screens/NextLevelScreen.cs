using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Simon.Mah.Framework;
using Simon.Mah.Framework.Scene2D;
using MAH_TowerDefense.Screens;
using MAH_TowerDefense;
using Simon.Mah.Framework.Tools;

namespace MAH_Platformer.Screens
{
    public class NextLevelScreen : Screen
    {
        private const float textTime = 4f;
        private float time;
        private int state;
        private int nextLevel;

        private String[] texts = {
          "Sorry, the Princess is not Here!",
          "The Adventure Continues...",
          "You Smell a Game Dev nearby, Let's Battle Him!",
          "Let's Go before the Dragon Slayer Detects us!",
          "We are Getting Close to our Goal!",
          "Decisions... You Probably Thinking to Yourself: LETS GOO!!",
          "By the Process of Elimination You can Make Determinations!"
        };

        public NextLevelScreen(int nextLevel)
        {
            this.nextLevel = nextLevel;
        }

        public override void Init()
        {
            this.state = MathUtils.Random(0, texts.Length - 1);
        }

        public override void Update(float delta)
        {
            time += delta;

            if (time > textTime )
               SetScreen(new GameScreen(nextLevel));
        }

        public override void Draw(SpriteBatch batch)
        {
            GetGraphics().Clear(Color.Black);

            batch.Begin();

            // Draw story text
            batch.DrawString(Assets.font, texts[state],
                new Vector2(
                    GetGraphics().Viewport.Width / 2 - Assets.font.MeasureString(texts[state]).Length() / 2,
                    GetGraphics().Viewport.Height / 2 - 32), Color.White);

            batch.End();
        }

        public override void Dispose()
        {
        }
    }
}
