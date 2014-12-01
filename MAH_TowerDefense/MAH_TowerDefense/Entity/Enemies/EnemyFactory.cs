using Simon.Mah.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Entity.Enemies
{
    public static class EnemyFactory
    {
        public static Enemy CreateGoblin(float x, float y)
        {
            StatsData stats = new StatsData();
            stats.Speed = 100;

            Enemy enemy = new Enemy(stats, x, y);

            enemy.sprite.AddAnimation("normal", new FrameAnimation(Assets.GetRegion("Entity"), 0, 17, 32, 32, 1, 1)).SetAnimation("normal");

            return enemy;
        }
    }
}
