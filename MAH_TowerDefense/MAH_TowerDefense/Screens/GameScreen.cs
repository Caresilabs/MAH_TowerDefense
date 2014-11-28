using Microsoft.Xna.Framework;
using Simon.Mah.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spline;
using Microsoft.Xna.Framework.Graphics;
using Simon.Mah.Framework.Scene2D;
using Microsoft.Xna.Framework.Input;
using MAH_TowerDefense.Views;
using MAH_TowerDefense.Worlds;

namespace MAH_TowerDefense.Screens
{
    public class GameScreen : Screen
    {
        private WorldRenderer renderer;
        private World world;
        private HUD hud;

        private float timeModifier;

        public override void Init()
        {
            this.world = new World();
            this.renderer = new WorldRenderer(world, GetGraphics());
            this.hud = new HUD(this);
            this.timeModifier = 1;
        }

        public override void Update(float delta)
        {
            UpdateInput();

            world.Update(delta * timeModifier);
            hud.Update(delta);
        }

        private void UpdateInput()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                timeModifier = 2;
            else
                if (InputHandler.KeyReleased(Keys.Space))
                    timeModifier = 1;
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.GraphicsDevice.Clear(Color.Magenta);

            hud.Draw(batch);
            renderer.Render(batch);

            /*
             * 
             *  int delta = 25;
            if (path.AntalPunkter >= 4)
            {
                for (float i = path.beginT + delta; i < path.endT; i += delta)
                {
                    Vector2 last = path.GetPos(i - delta);
                    Vector2 pos = path.GetPos(i);
                    float angle = (float)Math.Atan2(pos.X - last.X, pos.Y - last.Y);

                    Sprite sprite = new Sprite(new Simon.Mah.Framework.Scene2D.TextureRegion(Assets.items, 0, 0, 96, 64), pos.X, pos.Y, 96, 48);
                    sprite.Rotation = (float)Math.PI - angle;

                    sprite.Draw(batch);
                }
                // TODO: Add your drawing code here
                path.Draw(batch);
            }
             */
        }

        public override void Dispose()
        {
        }

        public World GetWorld()
        {
            return world;
        }
    }
}
