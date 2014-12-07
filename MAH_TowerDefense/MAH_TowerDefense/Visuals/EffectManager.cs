using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Simon.Mah.Framework;
using Simon.Mah.Framework.Tools;
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


        // ==== EFFECTS ==== //

        public void SpawnSmoke(Vector2 pos)
        {
            float maxSpeed = 400;
            for (int i = 0; i < 120; i++)
            {
                float vx = -maxSpeed + 2*(MathUtils.Random(maxSpeed/50f, maxSpeed));
                float vy = -maxSpeed + 2 * (MathUtils.Random(maxSpeed / 50f, maxSpeed));

                Color color = new Color(MathUtils.Random(0, 255), MathUtils.Random(0, 255), MathUtils.Random(0, 255), MathUtils.Random(0, 255));
                Particle p = new Particle(Assets.GetRegion("pCircle"), pos, new Vector2(vx, vy), color, 4, MathUtils.Random(.05f, .3f));
                AddEffect(p);
            }
        }

        public void SpawnBlood(Vector2 pos)
        {
            for (int i = 0; i < 1; i++)
            {
                Particle p = new Particle(Assets.GetRegion("pBlood"), pos, new Vector2(0, 0), Color.White, MathUtils.Random(1.4f, 3.9f), 20, true);
                p.ZIndex = .7f;
                AddEffect(p);
            }
        }
    }
}
