﻿using MAH_TowerDefense.Entity.Bullets;
using MAH_TowerDefense.Entity.Enemies;
using MAH_TowerDefense.Worlds;
using Microsoft.Xna.Framework;
using Simon.Mah.Framework.Scene2D;
using Simon.Mah.Framework.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Simon.Mah.Framework;
using MAH_TowerDefense.Views;

namespace MAH_TowerDefense.Entity.Towers
{
    public class Tower : Unit
    {
        private const float UPGRADE_COST_FACTOR = 2.0f;

        public Enemy Target { get; set; }

        public Type Bullet { get; private set; }

        public bool Placed { get; private set; }

        public int Cost { get; protected set; }

        public int Level { get; private set; }

        private float shootTime;

        public Tower(Type bullet, float width, float height)
            : base(-1000, -1000, 64, 64)
        {
            this.Placed = false;
            this.Bullet = bullet;
            this.shootTime = 0;
            this.Level = 1;
            this.sprite.ZIndex = .05f;
        }

        public Tower(Type bullet) : this(bullet, World.TILE_SIZE, World.TILE_SIZE) { }

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

        public override void Draw(SpriteBatch batch)
        {
            // Draw circle outline
            if (!Placed || Selected)
            {
                Sprite s = new Sprite(Assets.GetRegion("Circle"), position.X, position.Y, Stats.Radius * 2, Stats.Radius * 2);
                s.Color = sprite.Color;
                s.ZIndex = .65f + (position.X / (World.TILE_SIZE * World.WIDTH) * .1f);
                s.Draw(batch);
            }

            // Draw Level
            batch.DrawString(Assets.font, Level.ToString(), GetPosition(), Color.White, 0, new Vector2(-bounds.Width / 2.3f, -bounds.Height / 2.3f), .38f, SpriteEffects.None, 0);

            base.Draw(batch);
        }

        private void Shoot(float delta)
        {
            float angle = (float)(Math.Atan2(Target.GetPosition().X - position.X, Target.GetPosition().Y - position.Y));
            sprite.Rotation = MathUtils.AngleLerp(sprite.Rotation, (float)Math.PI - angle, 8 * delta);

            if (shootTime <= 0)
            {
                // Shoot
                angle = (float)Math.PI / 2 - angle;
                shootTime = Stats.Speed;
                Vector2 velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                Bullet bullet = (Bullet)Activator.CreateInstance(this.Bullet, position.X, position.Y, new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)));

                // Add tower stats
                bullet.GetHitModifier().GetModifier().Damage += Stats.Damage;
                bullet.GetHitModifier().GetModifier().CritChance += Stats.CritChance;

                world.AddBullet(bullet);
            }
            shootTime -= delta;
        }

        public void Place()
        {
            WorldRenderer.Effects.SpawnSmoke(GetPosition());
            this.Placed = true;
        }

        public void Upgrade()
        {
            WorldRenderer.Effects.SpawnSmoke(GetPosition());
            Level++;
            OnUpdate();
        }

        protected virtual void OnUpdate()
        {
            this.Cost = (int)(Cost * UPGRADE_COST_FACTOR);
            Stats *= 1.2f;
        }
    }
}
