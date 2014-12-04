using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Simon.Mah.Framework;
using Spline;

namespace MAH_TowerDefense.Levels
{
    public class Road : SimplePath
    {
        private List<Sprite> roadParts;

        public Road(GraphicsDevice device)
            : base(device)
        {
            this.roadParts = new List<Sprite>();
            UpdateParts();
        }

        public void UpdateParts()
        {
            roadParts.Clear();

            int width = 32;
            float delta = width * .8f;
            if (AntalPunkter >= 4)
            {
                for (float i = beginT + delta; i < endT; i += delta)
                {
                    Vector2 last = GetPos(i - delta);
                    Vector2 pos = GetPos(i);
                    float angle = (float)Math.Atan2(pos.X - last.X, pos.Y - last.Y);

                    Sprite sprite = new Sprite(Assets.GetRegion("Pixel"), pos.X - (float)Math.Cos(Math.PI - angle) * width,
                        pos.Y - (float)Math.Sin(Math.PI - angle) * width, 64, width);
                    sprite.ZIndex = .95f;
                    sprite.Color = Color.SlateGray;
                    sprite.Rotation = (float)Math.PI - angle;

                    roadParts.Add(sprite);
                }

            }
        }

        public void Render(SpriteBatch batch)
        {
            foreach (var part in roadParts)
            {
                part.Draw(batch);
            }
            //Draw(batch);
        }
    }
}
