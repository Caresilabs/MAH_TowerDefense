using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Entity.Bullets
{
    public class HitModifier
    {
        protected GameObject entity;
        protected StatsData modifier;
        protected bool alive;

        public HitModifier()
        {
            this.alive = true;
        }

        public void OnHit(GameObject entity)
        {
            this.entity = entity;
        }

        public virtual void Update(float delta)
        {
            this.alive = false;
        }

        public bool IsAlive()
        {
            return alive;
        }
    }
}
