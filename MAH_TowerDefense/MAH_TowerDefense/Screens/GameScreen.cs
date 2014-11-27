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

namespace MAH_TowerDefense.Screens
{
    public class GameScreen : Screen
    {
        SimplePath path;
        public override void Init()
        {
            path = new SimplePath(GetGraphics());
            path.Clean();

            path.AddPoint(new Vector2(0, 0));
           // path.AddPoint(new Vector2(120, 300));
           // path.AddPoint(new Vector2(400, 100));
            //path.AddPoint(new Vector2(1200, 600));

           // tex = Content.Load<Texture2D>(@"Graphics/test");
        }

        public override void Update(float delta)
        {
            if (InputHandler.Clicked())
                path.AddPoint(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {
            batch.GraphicsDevice.Clear(Color.Magenta);

            batch.Begin();

            int delta = 25;
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
            batch.End();
        }

        public override void Dispose()
        {
        }
    }
}
