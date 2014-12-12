using Simon.Mah.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Entity.Enemies
{
    public static class EnemyFactory
    {
        public class SnailEnemy : Enemy
        {
            public SnailEnemy(float offset)
                : base(offset, 48, 32)
            {
                StatsData stats = new StatsData()
                {
                    MaxHealth = 600,
                    MaxSpeed = 120,
                    Armor = 7,
                    Gold = 35,
                    Damage = 1
                };
                Stats = stats;

                sprite.AddAnimation("normal", new FrameAnimation(Assets.items, 0, 352, 64, 32, 2, .4f)).SetAnimation("normal");
            }
        }

        public class PandaEnemy : Enemy
        {
            public PandaEnemy(float offset)
                : base(offset, 96, 96)
            {
                StatsData stats = new StatsData()
                {
                    MaxHealth = 3200,
                    MaxSpeed = 110,
                    Armor = 200,
                    Gold = 150,
                    Damage = 4
                };
                Stats = stats;

                sprite.AddAnimation("normal", new FrameAnimation(Assets.items, 224, 416, 96, 96, 3, .35f)).SetAnimation("normal");
            }
        }

        public class GhostEnemy : Enemy
        {
            public GhostEnemy(float offset)
                : base(offset, 64, 64)
            {
                StatsData stats = new StatsData()
                {
                    MaxHealth = 650,
                    MaxSpeed = 150,
                    Armor = 7,
                    Gold = 40,
                    Damage = 3
                };
                Stats = stats;

                sprite.AddAnimation("normal", new FrameAnimation(Assets.items, 0, 384, 64, 64, 2, 3.5f)).SetAnimation("normal");
            }
        }

        public class BlobEnemy : Enemy
        {
            public BlobEnemy(float offset)
                : base(offset, 48, 48)
            {
                StatsData stats = new StatsData()
                {
                    MaxHealth = 350,
                    MaxSpeed = 230,
                    Armor = 50,
                    Gold = 40,
                    Damage = 1
                };
                Stats = stats;

                sprite.AddAnimation("normal", new FrameAnimation(Assets.items, 0, 448, 64, 64, 1, 1)).SetAnimation("normal");
            }
        }
    }
}
