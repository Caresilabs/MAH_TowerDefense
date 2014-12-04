using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Entity.Enemies
{
    public class CreepWave
    {
        public string Description { get; set; }

        private List<Enemy> enemies;

        public CreepWave()
        {
            this.enemies = new List<Enemy>();
        }

        public List<GameObject> GetEnemies()
        {
            return enemies.ToList<GameObject>();
        }
    }
}
