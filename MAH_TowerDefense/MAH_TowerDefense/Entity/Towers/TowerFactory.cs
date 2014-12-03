using MAH_TowerDefense.Entity.Bullets;
using Simon.Mah.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Entity.Towers
{
    public static class TowerFactory
    {
        public static Tower CreateCannon(float x, float y)
        {
            StatsData stats = new StatsData()
            {
                Radius = 200,
                MaxSpeed = .25f
            };

            Tower tower = new Tower(stats, typeof(BulletBank.SniperBullet), x, y);

            tower.sprite.AddAnimation("normal", new FrameAnimation(Assets.GetRegion("Entity"), 0, 65, 64, 64, 1, 1)).SetAnimation("normal");
            
            return tower;
        }
    }
}
