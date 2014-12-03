using MAH_TowerDefense.Entity.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Entity.Bullets
{
    public class IntervalHitModifier : HitModifier
    {
        private float interval;
        private float intervalTime;

        private float time;
        private float totalTime;

        public IntervalHitModifier(StatsData data, float interval, float totalTime) : base(data)
        {
            this.intervalTime = 0;
            this.interval = interval;
            this.time = 0;
            this.totalTime = totalTime;
        }

        public override void Update(float delta)
        {
            time += delta;
            intervalTime += delta;

            if (time >= totalTime)
                alive = false;

            if (intervalTime >= interval)
            {
                intervalTime = 0;
                DoHit();
            }
        }

        public override HitModifier Clone()
        {
            return new IntervalHitModifier(data, interval, totalTime);
        }
    }
}
