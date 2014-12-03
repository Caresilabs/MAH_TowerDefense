using MAH_TowerDefense.Entity.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense
{
    public class WaveSystem
    {
        private List<CreepWave> waves;
       
        private int maxWaves;

        public WaveSystem()
        {
            this.waves = new List<CreepWave>();
            this.maxWaves = waves.Count;
        }

        public CreepWave RequestWave()
        {
            if (IsAllCleared()) return null;

            CreepWave wave = waves[0];
            waves.Remove(wave);
            return wave;
        }

        public int GetCurrentWave()
        {
            return maxWaves - waves.Count;
        }

        public bool IsAllCleared()
        {
            return waves.Count == 0;
        }
    }
}
