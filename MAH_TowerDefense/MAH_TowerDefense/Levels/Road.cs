using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Simon.Mah.Framework;
using Spline;
using Simon.Mah.Framework.Scene2D;

namespace MAH_TowerDefense.Levels
{
    public class Road : SimplePath
    {
        private List<Sprite> roadParts;
        private TextureRegion region;

        public Road(GraphicsDevice device, TextureRegion region = null)
            : base(device)
        {
            this.roadParts = new List<Sprite>();
            this.region = region == null ? Assets.GetRegion("Path") : region;
            UpdateParts();
        }

        public void UpdateParts()
        {
            roadParts.Clear();
            
            int width = 42;
            float delta = width * .61f;
            if (AntalPunkter >= 3)
            {
                for (float i = beginT + delta; i < endT; i += delta)
                {
                    Vector2 last = GetPos(i - delta*.2f);
                    Vector2 pos = GetPos(i);
                    float angle = (float)Math.Atan2(pos.X - last.X, pos.Y - last.Y);

                    Sprite sprite = new Sprite(region, 
                        pos.X - (float)Math.Cos(Math.PI - angle) ,
                        pos.Y - (float)Math.Sin(Math.PI - angle) ,
                            64, width);
                    sprite.ZIndex = .95f;
                    sprite.Color = Color.SlateGray;
                    sprite.Rotation = (float)Math.PI - angle;

                    roadParts.Add(sprite);
                }

            }
        }

        public void Render(SpriteBatch batch, bool debug = false)
        {
            foreach (var part in roadParts)
            {
                part.Draw(batch);
            }
            if (debug && AntalPunkter >= 3)
                Draw(batch);
        }
    }
}
