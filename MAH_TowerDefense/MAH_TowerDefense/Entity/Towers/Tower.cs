using MAH_TowerDefense.Entity.Bullets;
using MAH_TowerDefense.Entity.Enemies;
using MAH_TowerDefense.Worlds;
using Microsoft.Xna.Framework;
using Simon.Mah.Framework.Scene2D;
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

        public Tower(StatsData stats, float x, float y, float width, float height)
            : base(x, y, 64, 64)
        {
            this.Stats = stats;
            this.Placed = false;
            this.Bullet = typeof(Bullet);
        }

        public Tower(StatsData stats, float x, float y)
            : this(stats, x, y, World.TILE_SIZE, World.TILE_SIZE)
        {
        }

        public override void Update(float delta)
        {
            UpdateShooting(delta);

            if (Placed)
                sprite.Rotation += delta;

            base.Update(delta);
        }

        private void UpdateShooting(float delta)
        {
            if (!Placed) return;

            if (Target == null)
            {
                List<Enemy> enemies = world.GetEnemies(this, Stats.ShootRadius);
                if (enemies.Count != 0)
                {
                    Target = enemies[0];
                    Shoot();
                }
            }
            else
            {
                // Check if target is still in range
                if (Vector2.DistanceSquared(position, Target.GetPosition()) < Stats.ShootRadius * Stats.ShootRadius) {
                    Shoot();
                }
                else
                    Target = null;
            }
        }

        private void Shoot()
        {
            
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
