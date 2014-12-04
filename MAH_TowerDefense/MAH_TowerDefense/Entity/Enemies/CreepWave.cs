using MAH_TowerDefense.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Entity.Enemies
{
    public class CreepWave
    {
        public string Description { get; private set; }

        private List<Enemy> enemies;

        public CreepWave(string description, string[] enemies)
        {
            this.enemies = new List<Enemy>();
            this.Description = UIController.WrapText(description, UIController.PANEL_WIDTH);
            InitWave(enemies);
        }

        private void InitWave(string[] enemies)
        {
            foreach (var enemy in enemies)
            {
                Enemy e = (Enemy)typeof(EnemyFactory).GetMethod("Create" + enemy).Invoke(null, new object[] {1, 0});
                this.enemies.Add(e);
            }
        }

        public List<GameObject> GetEnemies()
        {
            return enemies.ToList<GameObject>();
        }
    }
}
