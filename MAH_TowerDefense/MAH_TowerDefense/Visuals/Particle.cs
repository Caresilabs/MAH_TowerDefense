using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Simon.Mah.Framework.Scene2D;
using Simon.Mah.Framework.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Visuals
{
    public class Particle : IEffect
    {
        public TextureRegion Region { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Color Color { get; set; }
        public bool Alive { get; set; }
        public bool Smooth { get; set; }
        public float Angle { get; set; }
        public float Size { get; set; }
        public float Time { get; set; }
        public float ZIndex { get; set; }

        public Particle(TextureRegion region, Vector2 position, Vector2 velocity, Color color, float size, float time, bool smooth = false)
        {
            this.Region = region;
            this.Position = position;
            this.Velocity = velocity;
            this.Angle = (float)MathUtils.Random(0, (float)Math.PI);
            this.Color = color;
            this.Size = size;
            this.Time = time;
            this.Alive = true;
            this.ZIndex = 0;
            this.Smooth = smooth;
        }

        public void Update(float delta)
        {
            Time -= delta;
            Position += Velocity * delta;

            if (Smooth)
                if (Time <= 1f)
                    Color = Color.Lerp(Color, Color.Transparent, delta * 5);

            if (Time <= 0)
                Alive = false;
        }

        public void Draw(SpriteBatch batch)
        {
            Vector2 origin = new Vector2(Region.GetSource().Width / 2, Region.GetSource().Height / 2);

            batch.Draw(Region, Position, Region, Color,
                Angle, origin, Size, SpriteEffects.None, ZIndex);
        }
    }
}
