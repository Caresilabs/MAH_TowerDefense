using MAH_TowerDefense.Views;
using MAH_TowerDefense.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Entity.Enemies
{
    public class CreepWave
    {
        public string Description { get; private set; }

        public int Number { get; private set; }

        private List<Enemy> enemies;

        public CreepWave(int num, string description, string[] enemies)
        {
            this.Number = num;
            this.enemies = new List<Enemy>();
            this.Description = description;
            this.InitWave(enemies);
        }

        private void InitWave(string[] enemies)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                string enemy = enemies[i].Replace(" ", "").Replace("\n", "");
                int num = 1;
                float offset = -World.TILE_SIZE;
                if (enemy.Contains("*"))
                {
                    if (int.TryParse(enemy.Split('*')[1], out num))
                    {
                        //num = int.Parse(enemy.Split('*')[1]); // TODO error catching
                        enemy = enemy.Split('*')[0];
                    }
                    else
                    {
                        Console.WriteLine("Bad integer after *. Error at enemy num: " + i);
                        continue;
                    }
                }

                while (num > 0)
                {
                    string nameSpace = "MAH_TowerDefense.Entity.Enemies.EnemyFactory" + "+"; 
                    string name = (enemy.Substring(0, 1).ToUpper() + enemy.Substring(1, enemy.Length - 1).ToLower()) + "Enemy";
                    var objType = Type.GetType(nameSpace + name, true);
                    Enemy e = (Enemy)Activator.CreateInstance(objType, offset);
                    
                    this.enemies.Add(e);
                    num--;
                    offset -= World.TILE_SIZE;
                }
            }
        }

        public List<Enemy> GetEnemies()
        {
            return enemies.ToList<Enemy>();
        }
    }
}
