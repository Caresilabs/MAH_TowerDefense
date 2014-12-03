using Microsoft.Xna.Framework.Graphics;
using Simon.Mah.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Visuals
{
    public class EffectManager : IGameLoop
    {
        private List<IEffect> effects;

        public EffectManager()
        {
            this.effects = new List<IEffect>();
        }

        public void Update(float delta)
        {
            for (int i = 0; i < effects.Count; i++)
            {
                IEffect effect = effects[i];
                effect.Update(delta);

                if (!effect.Alive)
                    effects.Remove(effect);
            }
        }

        public void Draw(SpriteBatch batch)
        {
            foreach (var effect in effects)
            {
                effect.Draw(batch);
            }
        }

        public void AddEffect(IEffect effect)
        {
            effects.Add(effect);
        }
    }
}
