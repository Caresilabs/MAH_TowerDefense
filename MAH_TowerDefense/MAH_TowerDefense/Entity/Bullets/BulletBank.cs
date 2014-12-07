using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Entity.Bullets
{
    public class BulletBank
    {
        public class CannonBullet : Bullet
        {
            public CannonBullet(float x, float y, Vector2 direction)
                : base(Assets.GetRegion("CannonBullet"), x, y, direction, 700)
            {
                StatsData data = new StatsData() { 
                    Damage = 10,
                    Radius = 0,
                    MaxSpeed = 0,
                    CritChance = 15,
                    Armor = .2f
                };

                HitModifier modifier = new HitModifier(data);
                SetModifier(modifier);
            }
        }

        public class IceBullet : Bullet
        {
            public IceBullet(float x, float y, Vector2 direction)
                : base(Assets.GetRegion("IceBullet"), x, y, direction, 600, 18)
            {
                StatsData data = new StatsData() { 
                    Damage = 5,
                    Radius = 100,
                    MaxSpeed = 200,
                    CritChance = 5,
                    Armor = 3
                };

                HitModifier modifier = new HitModifier(data);
                SetModifier(modifier);
            }
        }

        public class SunBullet : Bullet
        {
            public SunBullet(float x, float y, Vector2 direction)
                : base(Assets.GetRegion("SunBullet"), x, y, direction, 800, 18)
            {
                StatsData data = new StatsData()
                {
                    Damage = 20,
                    Radius = 100,
                    MaxSpeed = 0,
                    CritChance = 10,
                    Armor = 10
                };

                HitModifier modifier = new IntervalHitModifier(data, 1.5f, 8);
                SetModifier(modifier);
            }
        }

        public class NuclearBullet : Bullet
        {
            public NuclearBullet(float x, float y, Vector2 direction)
                : base(Assets.GetRegion("NuclearBullet"), x, y, direction, 800, 24)
            {
                StatsData data = new StatsData()
                {
                    Damage = 10,
                    Radius = 320,
                    MaxSpeed = 5,
                    CritChance = 20,
                    Armor = 1
                };

                HitModifier modifier = new HitModifier(data);
                SetModifier(modifier);
            }
        }
    }
}
