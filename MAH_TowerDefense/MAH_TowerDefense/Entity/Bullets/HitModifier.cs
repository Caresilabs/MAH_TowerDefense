using MAH_TowerDefense.Entity.Enemies;
using MAH_TowerDefense.Views;
using MAH_TowerDefense.Visuals;
using Microsoft.Xna.Framework;
using Simon.Mah.Framework.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Entity.Bullets
{
    public class HitModifier
    {
        protected GameObject entity;
        protected StatsData data;
        protected bool alive;

        public HitModifier(StatsData data)
        {
            this.alive = true;
            this.data = data;
        }

        public HitModifier OnHit(GameObject entity)
        {
            this.entity = entity;
            return this;
        }

        public virtual void Update(float delta)
        {
            this.alive = false;
            DoHit();
        }

        protected void DoHit()
        {
            Enemy enemy = ((Enemy)entity);

            float damageMultiplier;
            if (MathUtils.Random(0, 100) < data.CritChance)
                damageMultiplier = MathUtils.Random(1.5f, 2f);
            else
                damageMultiplier = MathUtils.Random(.5f, 1f);

            enemy.GetStats().Armor = MathHelper.Clamp(enemy.GetStats().Armor - data.Armor, 0, data.Armor);
            enemy.GetStats().Speed -= MathUtils.Random(0, data.Speed);

            float damage = MathHelper.Clamp(((data.Damage - enemy.GetStats().Armor) * damageMultiplier), 0, float.MaxValue);
            enemy.GetStats().Health -= damage;


            // Effect
            WorldRenderer.Effects.AddEffect(new DamageText(((int)damage).ToString(),
                enemy.GetPosition().X, enemy.GetPosition().Y - enemy.GetBounds().Height,
                    0, -70, damageMultiplier > 1 ? Color.Red : Color.White, damageMultiplier > 1 ? .73f : .5f));
        }

        public bool IsAlive()
        {
            return alive;
        }

        public StatsData GetModifier()
        {
            return data;
        }

        public virtual HitModifier Clone()
        {
            return new HitModifier(data);
        }
    }
}
