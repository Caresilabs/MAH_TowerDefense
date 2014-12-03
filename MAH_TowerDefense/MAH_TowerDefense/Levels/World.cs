using MAH_TowerDefense.Entity;
using MAH_TowerDefense.Entity.Bullets;
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
        public const int WAVE_COOLDOWN = 15;

        public static float TILE_SIZE;
        public static int WIDTH;
        public static int HEIGHT;

        private List<GameObject> entities;
        private List<GameObject> selected;
        private List<Bullet> bullets;

        private SimplePath road;
        private GameState state;
        private WaveSystem waves;

        private float nextWaveTime;
        private int lives;

        public enum GameState
        {
            WAITING, COMBAT
        }

        public World(int width, int height, float tileSize)
        {
            WIDTH = width;
            HEIGHT = height;
            TILE_SIZE = tileSize;

            this.entities = new List<GameObject>();
            this.selected = new List<GameObject>();
            this.bullets = new List<Bullet>();
            this.InitLevel();
        }

        public void InitLevel(int level = 1)
        {
            this.lives = 20;
            this.nextWaveTime = WAVE_COOLDOWN;
            this.state = GameState.WAITING;
            this.waves = new WaveSystem();

            road = new SimplePath(Start.graphics.GraphicsDevice);
            road.InsertPoint(new Vector2(0, HEIGHT * TILE_SIZE / 2), 0);
            road.AddPoint(new Vector2(WIDTH * TILE_SIZE, HEIGHT * TILE_SIZE / 2));

            Tower tower = TowerFactory.CreateCannon(600, 600);
            tower.Place();
            AddEntity(tower);

            for (int i = 0; i < 10; i++)
            {
                Enemy e = EnemyFactory.CreateSnail(-MathUtils.Random(0, 400));
                AddEntity(e);
            }
         
        }

        public void Update(float delta)
        {
            switch (state)
            {
                case GameState.WAITING:
                    nextWaveTime -= delta;
                    if (nextWaveTime <= 0)
                        NextWave();
                    break;
                case GameState.COMBAT:
                    break;
                default:
                    break;
            }
            UpdateEntities(delta);
            UpdateBullets(delta);
        }

        private void UpdateBullets(float delta)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                Bullet bullet = bullets[i];
                bullet.Update(delta);

                foreach (var entity in entities.Where(x => x is Enemy))
                {
                    if (entity.GetBounds().Intersects(bullet.GetBounds()))
                    {
                        if (bullet.GetHitModifier().GetModifier().Radius == 0)
                        {
                            bullet.InjectTo((Enemy)entity);
                        }
                        else
                        {
                            List<Enemy> hits = GetEnemies(bullet, bullet.GetHitModifier().GetModifier().Radius);
                            bullet.InjectTo(hits);
                        }
                    }
                }

                if (!bullet.IsAlive()) bullets.Remove(bullet);
            }
        }

        private void NextWave()
        {
            nextWaveTime = WAVE_COOLDOWN;
            state = GameState.COMBAT;
            CreepWave wave = waves.RequestWave();
            
            if (wave!= null)
                AddEntities(wave.GetEnemies());
        }

        public void EndRound()
        {
            state = GameState.WAITING;
        }

        private void UpdateEntities(float delta)
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

        public void AddBullet(Bullet bullet)
        {
            bullet.world = this;
            bullets.Add(bullet);
        }

        public void AddEntity(GameObject entity)
        {
            entity.world = this;
            entities.Add(entity);
        }

        public void AddEntities(List<GameObject> ents)
        {
            foreach (var entity in ents)
                AddEntity(entity);
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

        public List<Bullet> GetBullets()
        {
            return bullets;
        }

        public SimplePath GetRoad()
        {
            return road;
        }
    }
}
