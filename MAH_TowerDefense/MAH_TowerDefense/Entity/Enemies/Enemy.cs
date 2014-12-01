using MAH_TowerDefense.Entity.Bullets;
using MAH_TowerDefense.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Entity.Enemies
{
    public class Enemy : Unit, IHitable
    {
        public StatsData Stats { get; set; }

        public List<HitModifier> HitModifiers { get; set; }

        private float walkedDistance;

        public Enemy(StatsData stats, float x, float y, float width, float height)
            : base(x, y, 64, 64)
        {
            this.Stats = stats;
            this.HitModifiers = new List<HitModifier>();
            this.walkedDistance = 0;
        }

        public Enemy(StatsData stats, float x, float y)
            : this(stats, x, y, World.TILE_SIZE, World.TILE_SIZE)
        {}

        public override void Update(float delta)
        {
            walkedDistance += Stats.Speed * delta;
            SetPosition(world.GetRoad().GetPos(walkedDistance));

            if (walkedDistance >= world.GetRoad().endT)
                Alive = false;
                

            for (int i = 0; i < HitModifiers.Count; i++)
            {
                HitModifier modifier = HitModifiers[i];
                modifier.Update(delta);

                if (!modifier.IsAlive())
                    HitModifiers.Remove(modifier);
            }

            base.Update(delta);
        }

        public bool Hit(HitModifier modifier)
        {
            //modifier.
            return true;
        }
    }
}
