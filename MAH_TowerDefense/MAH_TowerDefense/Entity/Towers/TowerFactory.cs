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
                Radius = 100,
                MaxSpeed = .5f,
                Damage = 30,
                CritChance = 20
            };

            Tower tower = new Tower(stats, typeof(BulletBank.SniperBullet), x, y);
            tower.Cost = 100;
            tower.sprite.AddAnimation("normal", new FrameAnimation(Assets.items, 0, 65, 64, 64, 1, 1)).SetAnimation("normal");
            
            return tower;
        }

        public static Tower CreateRocket(float x, float y)
        {
            StatsData stats = new StatsData()
            {
                Radius = 400,
                MaxSpeed = 3f,
                Damage = 150,
                CritChance = 35
            };

            Tower tower = new Tower(stats, typeof(BulletBank.SniperBullet), x, y);
            tower.Cost = 200;
            tower.sprite.AddAnimation("normal", new FrameAnimation(Assets.items, 0, 65, 32, 32, 1, 1)).SetAnimation("normal");

            return tower;
        }
    }
}
