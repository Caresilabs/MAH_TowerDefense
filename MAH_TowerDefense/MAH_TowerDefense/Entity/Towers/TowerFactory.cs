using Simon.Mah.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Entity.Towers
{
    public static class TowerFactory
    {
        public static Tower CreateCannon(float x, float y)
        {
            StatsData stats = new StatsData();

            Tower tower = new Tower(stats, x, y);

            tower.sprite.AddAnimation("normal", new FrameAnimation(Assets.GetRegion("Entity"), 0,17, 32, 32, 1, 1)).SetAnimation("normal");
            
            return tower;
        }
    }
}
