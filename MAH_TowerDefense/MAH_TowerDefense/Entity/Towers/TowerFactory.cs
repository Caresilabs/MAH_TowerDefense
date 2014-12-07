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
        public class CannonTower : Tower
        {
            public static string Description;

            public CannonTower() : base(typeof(BulletBank.CannonBullet))
            {
                Description = "Basic DPS, a fast turret that can deal lots of damage";

                StatsData stats = new StatsData()
                {
                    Radius = 200,
                    MaxSpeed = .5f,
                    Damage = 10,
                    CritChance = 10
                };

                Stats = stats;
                Cost = 100;
                sprite.AddAnimation("normal", new FrameAnimation(Assets.items, 0, 160, 64, 64, 1, 1)).SetAnimation("normal");
            }

            protected override void OnUpdate()
            {
                base.OnUpdate();
                if (Level == 2)
                    sprite.AddAnimation("normal", new FrameAnimation(Assets.items, 64, 160, 64, 64, 1, 1)).SetAnimation("normal");
            }
        }

        public class FrostTower : Tower
        {
            public static string Description;

            public FrostTower()
                : base(typeof(BulletBank.IceBullet))
            {
                Description = "Freeze Man! Slows enemy because its chilly.";

                StatsData stats = new StatsData()
                {
                    Radius = 250,
                    MaxSpeed = 1f,
                    Damage = 25,
                    CritChance = 10
                };

                Stats = stats;
                Cost = 200;
                sprite.AddAnimation("normal", new FrameAnimation(Assets.items, 192, 160, 64, 64, 1, 1)).SetAnimation("normal");
            }

            protected override void OnUpdate()
            {
                base.OnUpdate();
                if (Level == 2)
                    sprite.AddAnimation("normal", new FrameAnimation(Assets.items, 256, 160, 64, 64, 1, 1)).SetAnimation("normal");
            }
        }

        public class SunTower : Tower
        {
            public static string Description;

            public SunTower()
                : base(typeof(BulletBank.SunBullet))
            {
                Description = "Burn baby burn! Yeah, they're at fire! Burns Armor";

                StatsData stats = new StatsData()
                {
                    Radius = 300,
                    MaxSpeed = 3f,
                    Damage = 20,
                    CritChance = 15
                };

                Stats = stats;
                Cost = 300;
                sprite.AddAnimation("normal", new FrameAnimation(Assets.items, 0, 284, 64, 64, 1, 1)).SetAnimation("normal");
            }

            protected override void OnUpdate()
            {
                base.OnUpdate();
                if (Level == 2)
                    sprite.AddAnimation("normal", new FrameAnimation(Assets.items, 64, 284, 64, 64, 1, 1)).SetAnimation("normal");
            }
        }

        public class NuclearTower : Tower
        {
            public static string Description;

            public NuclearTower()
                : base(typeof(BulletBank.NuclearBullet))
            {
                Description = "Splash and Boom! EXPLOSIONS YEAH!";

                StatsData stats = new StatsData()
                {
                    Radius = 350,
                    MaxSpeed = 4f,
                    Damage = 50,
                    CritChance = 25
                };

                Stats = stats;
                Cost = 350;
                sprite.AddAnimation("normal", new FrameAnimation(Assets.items, 0, 224, 64, 64, 1, 1)).SetAnimation("normal");
            }
        }
    }
}
