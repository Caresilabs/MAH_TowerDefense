using Simon.Mah.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Visuals
{
    public interface IEffect : IGameLoop
    {
        bool Alive { get; set; }
    }
}
