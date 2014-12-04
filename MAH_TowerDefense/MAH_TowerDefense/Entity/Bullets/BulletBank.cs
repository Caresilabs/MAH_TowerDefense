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
                    Damage = 20,
                    Radius = 400,
                    MaxSpeed = 90,
                    CritChance = 25,
                    Armor = 2
                };

                HitModifier modifier = new HitModifier(data);
                SetModifier(modifier);
            }
        }
    }
}
