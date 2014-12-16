using MAH_TowerDefense.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Simon.Mah.Framework;
using Simon.Mah.Framework.Scene2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Entity
{
    public abstract class GameObject
    {
        public readonly Sprite sprite;
        public World world;

        protected Rectangle bounds;
        protected Vector2 position;

        public GameObject(TextureRegion region, float x, float y, float width, float height)
        {
            this.sprite = new Sprite(region, x, y, width, height);
            this.position = new Vector2(x, y);
            this.bounds = new Rectangle((int)x, (int)y, (int)width, (int)height);
        }

        public GameObject(float x, float y, float width, float height) : this(null, x, y, width, height) { }

        public virtual void Update(float delta)
        {
            sprite.SetPosition(position);
            UpdateBounds();
            sprite.Update(delta);
        }

        public virtual void Draw(SpriteBatch batch)
        {
            sprite.Draw(batch);
        }

        public virtual bool UsesRenderTarget()
        {
            return true;
        }

        public void UpdateBounds()
        {
            bounds.X = (int)(position.X - (bounds.Width / 2));
            bounds.Y = (int)(position.Y - (bounds.Height / 2));
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public void SetPosition(Vector2 pos)
        {
            position.X = pos.X;
            position.Y = pos.Y;
        }

        public void SetPosition(float x, float y)
        {
            position.X = x;
            position.Y = y;
        }

        public Rectangle GetBounds()
        {
            return bounds;
        }
    }
}
