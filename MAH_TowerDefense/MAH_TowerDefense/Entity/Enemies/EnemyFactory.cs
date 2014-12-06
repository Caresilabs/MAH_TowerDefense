using Simon.Mah.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Entity.Enemies
{
    public static class EnemyFactory
    {
        //public static Enemy CreateSnail(float offset)
        public class SnailEnemy : Enemy
        {
            public SnailEnemy(float offset)
                : base(offset, 48, 32)
            {
                StatsData stats = new StatsData()
                {
                    MaxHealth = 400,
                    MaxSpeed = 100,
                    Armor = 5,
                    Gold = 10
                };
                Stats = stats;

                //Enemy enemy = new Enemy(stats, offset, 48, 32);
                sprite.AddAnimation("normal", new FrameAnimation(Assets.items, 0, 128, 64, 32, 1, 1)).SetAnimation("normal");
                //return enemy;
            }
        }
    }
}
