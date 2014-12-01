using MAH_TowerDefense.Entity;
using MAH_TowerDefense.Entity.Enemies;
using MAH_TowerDefense.Entity.Towers;
using MAH_TowerDefense.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Simon.Mah.Framework.Scene2D;
using Simon.Mah.Framework.Tools;
using Spline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Worlds
{
    public class World
    {
        public const int TILES_HORIZONTAL = 20;

        public static float TILE_SIZE;
        public static int WIDTH;
        public static int HEIGHT;

        private List<GameObject> entities;
        private List<GameObject> selected;

        private SimplePath road;

        private int lives;

        public World(int width, int height, float tileSize)
        {
            WIDTH = width;
            HEIGHT = height;
            TILE_SIZE = tileSize;

            this.entities = new List<GameObject>();
            this.selected = new List<GameObject>();
            this.InitLevel();
        }

        public void InitLevel(int level = 1)
        {
            this.lives = 20;

            road = new SimplePath(Start.graphics.GraphicsDevice);
            road.InsertPoint(new Vector2(0, HEIGHT * TILE_SIZE / 2), 0);
            road.AddPoint(new Vector2(WIDTH * TILE_SIZE, HEIGHT * TILE_SIZE / 2));

            Tower tower = TowerFactory.CreateCannon(500, 500);
            tower.Place();
            AddEntity(tower);

            Enemy e = EnemyFactory.CreateGoblin(0, 0);
            AddEntity(e);
        }

        public void Update(float delta)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                GameObject entity = entities[i];
                entity.Update(delta);

                if (entity is Unit) // Remove unit if dead
                    if (((Unit)entity).Alive == false)
                        RemoveEntity(entity);
            }
        }

        public void AddEntity(GameObject entity)
        {
            entity.world = this;
            entities.Add(entity);
        }

        public void RemoveEntity(GameObject entity)
        {
            entities.Remove(entity);
        }

        public List<Enemy> GetEnemies(GameObject entity, float radius)
        {
            List<Enemy> enemies = new List<Enemy>();

            foreach (var enemy in entities.Where(x => x is Enemy))
            {
                if (Vector2.DistanceSquared(entity.GetPosition(), enemy.GetPosition()) < radius * radius)
                {
                    enemies.Add((Enemy)enemy);
                }
            }
            return enemies;
        }

        public void Select(Rectangle selection)
        {
            selected = entities.Where(x => x.GetBounds().Intersects(selection)).ToList();
            Console.WriteLine(selected.Count);
        }

        public List<GameObject> GetEntities()
        {
            return entities;
        }

        public SimplePath GetRoad()
        {
            return road;
        }
    }
}
