using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Entity.Bullets
{
    public class BulletBank
    {
        public class SniperBullet : Bullet
        {
            public SniperBullet(float x, float y, Vector2 direction)
                : base(Assets.GetRegion("Bullet1"), x, y, direction, 700)
            {
                StatsData data = new StatsData() { 
                    Damage = 10,
                    Radius = 0,
                    MaxSpeed = 0,
                    CritChance = 50,
                    Armor = 2
                };

                HitModifier modifier = new HitModifier(data);
                SetModifier(modifier);
            }
        }
    }
}
