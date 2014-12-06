using MAH_TowerDefense.Entity.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Entity.Enemies
{
    public class WaveSystem
    {
        private List<CreepWave> waves;
       
        private int maxWaves;

        public WaveSystem(List<LevelEditor.WaveModel> waves)
        {
            this.waves = new List<CreepWave>();
            this.maxWaves = waves.Count;

            // Load Creep Waves
            foreach (var wave in waves)
            {
                if (wave.Enemies == null) continue;

                string description = "The Level Designer Forgot to Add Description to This Wave...";
                if (wave.Enemies.Contains("#"))
                    description = wave.Enemies.Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries)[0];

                CreepWave creepWave = new CreepWave(description, 
                    wave.Enemies.Substring(wave.Enemies.Contains("#") == false ? wave.Enemies.Length : wave.Enemies.LastIndexOf("#") + 1)
                        .Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));
                this.waves.Add(creepWave);
            }
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

        public int GetWavesLeft()
        {
            return waves.Count;
        }

        public bool IsAllCleared()
        {
            return waves.Count == 0;
        }
    }
}
