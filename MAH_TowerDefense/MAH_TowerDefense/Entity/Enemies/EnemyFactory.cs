using Simon.Mah.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Entity.Enemies
{
    public static class EnemyFactory
    {
        public static Enemy CreateSnail(float offset)
        {
            StatsData stats = new StatsData()
            {
                MaxHealth = 100,
                MaxSpeed = 100,
                Armor = 25
            };

            Enemy enemy = new Enemy(stats, offset, 48, 32);
            enemy.sprite.AddAnimation("normal", new FrameAnimation(Assets.items, 0, 128, 64, 32, 1, 1)).SetAnimation("normal");

            return enemy;
        }
    }
}
