using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Simon.Mah.Framework.Scene2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MAH_TowerDefense
{
    /**
    * Assets loads all the needed assets and a non cumbersome way to retrieve them
    */
    public class Assets
    {
        public const bool SOUND = false;

        private static Dictionary<String, TextureRegion> regions;
        private static ContentManager manager;

        public static Texture2D items;
        public static Texture2D bg;

        public static SpriteFont font;

        public static SoundEffect sound;
        public static Song music;

        public static void Load(ContentManager manager)
        {
            Assets.manager = manager;
            regions = new Dictionary<string, TextureRegion>();

            // load our sprite sheet
            items = manager.Load<Texture2D>("Graphics/items");
            bg = manager.Load<Texture2D>("Graphics/background");

            // Entities
            LoadRegion("Entity", items, 0, 64, 64, 64);
            //LoadRegion("Grass", items, 0, 0, 64, 64);
            LoadRegion("Grass", items, 320, 160, 96, 96);
            LoadRegion("Path", items, 64, 8, 96, 48);
            LoadRegion("Circle", items, 161, 0, 160, 160);

            // Particles
            LoadRegion("Pixel", items, 511, 0, 1, 1);
            LoadRegion("pCircle", items, 320, 0, 32, 32);
            LoadRegion("pBlood", items, 352, 0, 32, 32);

            // Bullets
            LoadRegion("CannonBullet", items, 64, 96, 32, 32);
            LoadRegion("IceBullet", items, 96, 96, 32, 32);
            LoadRegion("SunBullet", items, 128, 96, 32, 32);
            LoadRegion("NuclearBullet", items, 0, 64, 32, 32);

            // UI
            LoadRegion("Button", items, 320, 64, 192, 96);
            LoadRegion("Title", items, 160, 288, 352, 64);

            // Load font 
            font = manager.Load<SpriteFont>("Font/font");

            // UI Config
            UIConfig.DEFAULT_FONT = font;
            UIConfig.DEFAULT_BUTTON = GetRegion("Button");

            LoadSound();
        }

        private static void LoadSound()
        {
            // load sound
            if (SOUND)
            {
                //deathSound = manager.Load<SoundEffect>("Audio/death");
                //music = manager.Load<Song>("Audio/music");
                //MediaPlayer.Volume = .8f;
                //MediaPlayer.IsRepeating = true;
                //MediaPlayer.Play(music);
            }
        }

        private static void LoadRegion(string name, Texture2D tex, int x, int y, int width, int height)
        {
            regions.Add(name, new TextureRegion(tex, x, y, width, height));
        }

        public static TextureRegion GetRegion(string name)
        {
            return regions.Keys.Contains(name) ? regions[name] : null;
        }

        public static void Unload()
        {
            manager.Dispose();
        }
    }
}
