using MAH_TowerDefense.Entity;
using MAH_TowerDefense.Views;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense.Worlds
{
    public class World
    {
        public static int WIDTH = 20;
        public static int HEIGHT = 20;

        public static float TILE_SIZE = 32;

        private List<GameObject> entities;

        public World()
        {
            this.entities = new List<GameObject>();
        }

        public void Update(float delta)
        {

        }


        /// <summary>
        /// Return if world space is occupied or not
        /// </summary>
        /// <param name="wx">World cord X</param>
        /// <param name="wy">World cord X</param>
        public bool CanPlace(GameObject g) {
            Color[] pixels = new Color[g.sprite.Region.GetTexture().Width * g.sprite.Region.GetTexture().Height];
            Color[] pixels2 = new Color[g.sprite.Region.GetTexture().Width * g.sprite.Region.GetTexture().Height];

            g.sprite.Region.GetTexture().GetData<Color>(pixels2);

            WorldRenderer.RenderTarget.GetData(0, g.sprite.Region.GetSource(), pixels, 0, pixels.Length);

            for (int i = 0; i < pixels.Length; ++i)
            {
                if (pixels[i].A > 0.0f && pixels2[i].A > 0.0f)
                    return false;
            }
            return true;
        }

        public List<GameObject> GetEntities()
        {
            return entities;
        }
    }
}
