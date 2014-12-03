using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Simon.Mah.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Visuals
{
    public class DamageText : IEffect
    {
        public const float ALIVE_TIME = 1.6f;

        public bool Alive { get; set; }

        private Vector2 position;
        private Vector2 velocity;
        private Color color;

        private float scale;
        private string text;
        private float time;

        public DamageText(String text, float x, float y, float vx, float vy, Color color, float scale = 1)
        {
            this.Alive = true;
            this.position = new Vector2(x - (Assets.font.MeasureString(text).X * scale) / 2, y - (Assets.font.MeasureString(text).Y * scale) / 2);
            this.velocity = new Vector2(vx, vy);
            this.color = color;
            this.scale = scale;
            this.text = text;
            this.time = ALIVE_TIME;
        }

        public void Update(float delta)
        {
            time -= delta;
            position += velocity * delta;

            if (time <= 1)
                color = Color.Lerp(color, Color.Transparent, delta*10);

            if (time <= 0)
                Alive = false;
                
        }

        public void Draw(SpriteBatch batch)
        {
            batch.DrawString(Assets.font, text, position, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }
    }
}
