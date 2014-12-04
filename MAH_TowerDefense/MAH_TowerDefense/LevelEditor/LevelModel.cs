using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.LevelEditor
{
    public class LevelModel
    {
        public List<SingleLevel> levels;

        public class SingleLevel
        {
            public List<WaveModel> Waves { get; set; }

            public List<Vector2> PathPoints { get; set; }

            public int LevelIndex { get; set; }

            public int Width { get; set; }

            public int Height { get; set; }
        }
    }
}
