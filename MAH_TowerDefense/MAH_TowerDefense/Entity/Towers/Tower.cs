using MAH_TowerDefense.Entity.Bullets;
using MAH_TowerDefense.Entity.Enemies;
using MAH_TowerDefense.Worlds;
using Microsoft.Xna.Framework;
using Simon.Mah.Framework.Scene2D;
using Simon.Mah.Framework.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Entity.Towers
{
    public class Tower : Unit, IPlayerUnit
    {
        private const float UPGRADE_COST_FACTOR = 1.5f;

        public StatsData Stats { get; private set; }

        public Enemy Target { get; private set; }

        public Type Bullet { get; private set; }

        public bool Placed { get; private set; }

        public int Cost { get; set; }

        private float shootTime;

        public Tower(StatsData stats, Type bullet, float x, float y, float width, float height)
            : base(x, y, 64, 64)
        {
            this.Stats = stats;
            this.Placed = false;
            this.Bullet = bullet;
            this.shootTime = 0;
        }

        public Tower(StatsData stats, Type bullet, float x, float y)
            : this(stats, bullet, x, y, World.TILE_SIZE, World.TILE_SIZE)
        {
        }

        public override void Update(float delta)
        {
            UpdateShooting(delta);

            base.Update(delta);
        }

        private void UpdateShooting(float delta)
        {
            if (!Placed) return;

            if (Target == null)
            {
                List<Enemy> enemies = world.GetEnemies(this, Stats.Radius);
                if (enemies.Count != 0)
                {
                    Target = enemies[0];
                    Shoot(delta);
                }
            }
            else
            {
                // Check if target is still in range
                if (Target.Alive && Vector2.DistanceSquared(position, Target.GetPosition()) < Stats.Radius * Stats.Radius)
                {
                    Shoot(delta);
                }
                else
                    Target = null;
            }
        }

        private void Shoot(float delta)
        {
            float angle = (float)(Math.Atan2(Target.GetPosition().X - position.X, Target.GetPosition().Y - position.Y));
            sprite.Rotation = MathUtils.AngleLerp(sprite.Rotation, (float)Math.PI - angle, 8 * delta);
            // sprite.Rotation = MathHelper.Lerp(sprite.Rotation, (float)Math.PI - angle, 8 * delta);

            if (shootTime <= 0)
            {
                // Shoot
                angle = (float)Math.PI / 2 - angle;
                shootTime = Stats.Speed;
                Vector2 velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                Bullet bullet = (Bullet)Activator.CreateInstance(this.Bullet, position.X, position.Y, new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)));
                //bullet.FixStat
                world.AddBullet(bullet);
            }
            shootTime -= delta;
        }

        public void Place()
        {
            this.Placed = true;
        }

        public virtual void Upgrade()
        {
            Update();
        }

        public virtual void Update()
        {
            this.Cost = (int)(Cost * UPGRADE_COST_FACTOR);
            Stats *= 1.2f;
        }
    }
}
