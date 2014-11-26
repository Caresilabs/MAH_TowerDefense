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

        public static SpriteFont font;

        public static SoundEffect sound;
        public static Song music;

        public static void Load(ContentManager manager)
        {
            Assets.manager = manager;
            regions = new Dictionary<string, TextureRegion>();

            // load our sprite sheet
            items = manager.Load<Texture2D>("Graphics/test");

            // Entities
            LoadRegion("Entity", items, 32, 512, 32, 32);

            // Load font 
            font = manager.Load<SpriteFont>("Font/font");

            // UI Config
            UIConfig.DEFAULT_FONT = font;
            UIConfig.DEFAULT_BUTTON = GetRegion("button");

            LoadSound();
        }

        private static void LoadSound()
        {
            // load sound
            if (SOUND)
            {
                //deathSound = manager.Load<SoundEffect>("Audio/pacman_death");
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
