using MAH_TowerDefense.Entity.Bullets;
using MAH_TowerDefense.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Entity.Enemies
{
    public class Enemy : Unit, IHitable
    {
        public const int HEALTH_BAR_HEIGHT = 8;

        public List<HitModifier> HitModifiers { get; set; }

        private float walkedDistance;

        public Enemy(float offset, float width, float height)
            : base(0, 0, width, height)
        {
            this.HitModifiers = new List<HitModifier>();
            this.walkedDistance = offset;
        }

        public Enemy(float offset)
            : this(offset, World.TILE_SIZE/2, World.TILE_SIZE/2)
        {}

        public override void Update(float delta)
        {
            Stats.Speed += Stats.MaxSpeed * delta;

            walkedDistance += Stats.Speed * delta;
            SetPosition(world.GetRoad().GetPos(walkedDistance));

            if (walkedDistance >= world.GetRoad().endT)
                Alive = false;

            if (Stats.Health <= 0)
                Kill();

            for (int i = 0; i < HitModifiers.Count; i++)
            {
                HitModifier modifier = HitModifiers[i];
                modifier.Update(delta);

                if (!modifier.IsAlive())
                    HitModifiers.Remove(modifier);
            }

            base.Update(delta);
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);

            float width = (float)(Stats.Health / Stats.MaxHealth) * bounds.Width;
            batch.Draw(Assets.items, new Rectangle(bounds.Left, bounds.Top - HEALTH_BAR_HEIGHT, (int)width, HEALTH_BAR_HEIGHT), Assets.GetRegion("Pixel"), Color.Red);
        }

        public bool Hit(HitModifier modifier)
        {
            HitModifiers.Add(modifier.OnHit(this));
            return true;
        }

        private void Kill()
        {
            Alive = false;
            //TODO world.addscore /gold
            world.AddGold(Stats.Gold);
        }

        public StatsData GetStats()
        {
            return Stats;
        }
    }
}
