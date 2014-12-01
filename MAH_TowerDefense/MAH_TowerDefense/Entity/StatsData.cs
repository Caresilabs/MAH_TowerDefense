using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Entity
{
    public struct StatsData
    {
        public float Health { get; set; }
        public float Damage { get; set; }
        public float Armor { get; set; }
        public float CritChance { get; set; }
        public float ShootRadius { get; set; }
        public float Speed { get; set; }

        public static StatsData operator -(StatsData stats1, StatsData stats2)
        {
            stats1.Health -= stats2.Health;
            stats1.Damage -= stats2.Damage;
            stats1.Armor -= stats2.Armor;
            stats1.CritChance -= stats2.CritChance;
            stats1.ShootRadius -= stats2.ShootRadius;
            stats1.Speed -= stats2.Speed;
            return stats1;
        }

        public static StatsData operator +(StatsData stats1, StatsData stats2)
        {
            stats1.Health += stats2.Health;
            stats1.Damage += stats2.Damage;
            stats1.Armor += stats2.Armor;
            stats1.CritChance += stats2.CritChance;
            stats1.ShootRadius += stats2.ShootRadius;
            stats1.Speed += stats2.Speed;
            return stats1;
        }

        public static StatsData operator *(StatsData stats1, float factor)
        {
            stats1.Health *= factor;
            stats1.Damage *= factor;
            stats1.Armor *= factor;
            stats1.CritChance *= factor;
            stats1.ShootRadius *= factor;
            stats1.Speed *= factor;
            return stats1;
        }
    }
}
