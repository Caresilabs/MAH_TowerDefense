using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Entity
{
    public class StatsData
    {
        public float Health { get; set; }

        private float maxHealth;
        public float MaxHealth { get { return maxHealth; } set { maxHealth = value; Health = value; } }

        private float maxSpeed;
        public float MaxSpeed { get { return maxSpeed; } set { maxSpeed = value; Speed = value; } }

        private float speed;
        public float Speed { get { return speed; } set { speed = MathHelper.Clamp(value, 0, MaxSpeed); } }

        public float Damage { get; set; }

        public float Armor { get; set; }

        public float CritChance { get; set; }

        public float Radius { get; set; }

        public int Gold { get; set; }

        public static StatsData operator -(StatsData stats1, StatsData stats2)
        {
            stats1.Health -= stats2.Health;
            stats1.MaxHealth -= stats2.MaxHealth;
            stats1.Damage -= stats2.Damage;
            stats1.Armor -= stats2.Armor;
            stats1.CritChance -= stats2.CritChance;
            stats1.Radius -= stats2.Radius;
            stats1.Speed -= stats2.Speed;
            stats1.MaxSpeed -= stats2.MaxSpeed;
            return stats1;
        }

        public static StatsData operator +(StatsData stats1, StatsData stats2)
        {
            stats1.Health += stats2.Health;
            stats1.MaxHealth += stats2.MaxHealth;
            stats1.Damage += stats2.Damage;
            stats1.Armor += stats2.Armor;
            stats1.CritChance += stats2.CritChance;
            stats1.Radius += stats2.Radius;
            stats1.Speed += stats2.Speed;
            stats1.MaxSpeed += stats2.MaxSpeed;
            return stats1;
        }

        public static StatsData operator *(StatsData stats1, float factor)
        {
            stats1.Health *= factor;
            stats1.MaxHealth *= factor;
            stats1.Damage *= factor;
            stats1.Armor *= factor;
            stats1.CritChance *= factor;
            stats1.Radius *= factor;
            stats1.Speed *= factor;
            stats1.MaxSpeed *= factor;
            return stats1;
        }
    }
}
