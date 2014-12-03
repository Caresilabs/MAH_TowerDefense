using MAH_TowerDefense.Entity.Enemies;
using Microsoft.Xna.Framework;
using Simon.Mah.Framework.Scene2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Entity.Bullets
{
    public class Bullet : GameObject
    {
        public const int DEFAULT_RADIUS = 6;
        public const int MAX_FLY_TIME = 3;
        
        private Vector2 velocity;
        private HitModifier modifier;

        private bool alive;
        private float time;

        public Bullet(TextureRegion region, float x, float y, Vector2 direction, float speed, float radius = DEFAULT_RADIUS)
            : base(region, x, y, radius * 2, radius * 2)
        {
            this.velocity = direction * speed;
            this.alive = true;
            this.time = 0;
        }

        public override void Update(float delta)
        {
            time += delta;
            position += velocity * delta;

            if (time >= MAX_FLY_TIME)
                alive = false;

            base.Update(delta);
        }

        public void InjectTo(List<Enemy> enemies)
        {
            if (!alive) return;

            bool success = false;
            foreach (var enemy in enemies)
            {
                if (enemy.Hit(modifier.Clone())) success = true;
            }
            if (success) alive = false;
        }

        public void InjectTo(Enemy enemy)
        {
            if (!alive) return;

            if (enemy.Hit(modifier.Clone()))
            {
                alive = false;
            }
        }

        public void SetModifier(HitModifier modifier)
        {
            this.modifier = modifier;
        }

        public HitModifier GetHitModifier()
        {
            return modifier;
        }

        public bool IsAlive()
        {
            return alive;
        }
    }
}
