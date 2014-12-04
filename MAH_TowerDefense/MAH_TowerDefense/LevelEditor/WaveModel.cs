using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.LevelEditor
{
    public class WaveModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Enemies { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
