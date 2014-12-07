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
using MAH_TowerDefense.Levels;

namespace MAH_TowerDefense.Worlds
{
    public class World
    {
        public const int TILES_HORIZONTAL = 20;
        public const int WAVE_COOLDOWN = 15;

        public const int START_GOLD = 700;
        public const int START_LIVES = 20;
        public const int END_ROUND_GOLD = 300;

        public static float TILE_SIZE;
        public static int WIDTH;
        public static int HEIGHT;

        private List<GameObject> entities;
        private List<Unit> selected;
        private List<Bullet> bullets;

        private Road road;
        private GameState state;
        private WaveSystem waves;
        private CreepWave currentWave;

        private float nextWaveTime;
        private int lives;
        private int gold;

        public enum GameState
        {
            WAITING, COMBAT, WIN, DEAD
        }

        public World(float tileSize, int level = 1)
        {
            TILE_SIZE = tileSize;

            this.entities = new List<GameObject>();
            this.selected = new List<Unit>();
            this.bullets = new List<Bullet>();
            this.InitLevel(level);
        }

        public void InitLevel(int level)
        {
            this.lives = START_LIVES;
            this.gold = START_GOLD;
            this.nextWaveTime = WAVE_COOLDOWN;

            LevelEditor.LevelModel.SingleLevel loadedMap = LevelIO.LoadLevel(level);

            WIDTH = loadedMap.Width;
            HEIGHT = loadedMap.Height;

            waves = new WaveSystem(loadedMap.Waves);

            road = new Road(Start.graphics.GraphicsDevice);
            road.Clean();

            foreach (var pt in loadedMap.PathPoints)
            {
                 road.AddPoint(new Vector2(pt.X * TILE_SIZE, pt.Y * TILE_SIZE));
            }
            road.UpdateParts();

            EndRound();
            GC.Collect();
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
                    if (entities.Where(x => x is Enemy).Count() == 0)
                        EndRound();
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

        public void NextWave()
        {
            if (state != GameState.WAITING) return;

            nextWaveTime = WAVE_COOLDOWN;
            state = GameState.COMBAT;
            if (currentWave != null)
            {
                AddEntities(currentWave.GetEnemies());
            }
        }

        public void EndRound()
        {
            if (waves.GetCurrentWave() != 0)
                AddGold(END_ROUND_GOLD);

            if (waves.IsAllCleared())
            {
                state = GameState.WIN;
            }
            else
            {
                state = GameState.WAITING;
                CreepWave wave = waves.RequestWave();
                if (wave != null)
                    currentWave = wave;
            }
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
            List<Unit> newSelection = entities.Where(x => x.GetBounds().Intersects(selection) && x is Unit).Cast<Unit>().ToList();

            // If single Click
            if (newSelection.Count == 1)
            {
                if (newSelection[0] is Enemy)
                {
                    selected.Where(x => x is Tower).Cast<Tower>().ToList().ForEach(x => x.Target = (Enemy)newSelection[0]);
                }
            }
            else
            {
                // If more than half towers, select only towers OR NOT TODO
                if (newSelection.Where(x => x is Tower).Count() > newSelection.Count / 2)
                {
                    newSelection = newSelection.Where(x => x is Tower).ToList();
                }
            }
            
            // deselect old
            Deselect();

            selected.AddRange(newSelection);
            selected.ForEach(x => x.Selected = true);
        }

        public void Deselect()
        {
            selected.ForEach(x => x.Selected = false);

            selected.Clear();
        }

        public void WithdrawGold(int ammount)
        {
            this.gold -= ammount;
        }

        public void AddGold(int ammount)
        {
            this.gold += ammount;
        }

        public void Hurt(float damage)
        {
            lives -= (int)damage;

            if (lives <= 0)
                state = GameState.DEAD;
        }

        // ====== GETTERS AND SETTERS ===== ///

        public List<GameObject> GetEntities()
        {
            return entities;
        }

        public List<Bullet> GetBullets()
        {
            return bullets;
        }

        public CreepWave GetWave()
        {
            return currentWave;
        }

        public float GetWaitTime()
        {
            return this.nextWaveTime;
        }

        public int GetWavesLeft()
        {
            return waves.GetWavesLeft();
        }

        public int GetGold()
        {
            return gold;
        }

        public int GetLives()
        {
            return lives;
        }

        public GameState GetState()
        {
            return state;
        }

        public Road GetRoad()
        {
            return road;
        }

        public List<Unit> GetSelected()
        {
            return selected;
        }
    }
}
