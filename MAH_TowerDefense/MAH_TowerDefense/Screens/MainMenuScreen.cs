using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Simon.Mah.Framework;
using Simon.Mah.Framework.Scene2D;
using Simon.Mah.Framework.Tools;
using MAH_TowerDefense.LevelEditor;

namespace MAH_TowerDefense.Screens
{
    /**
     * A game screen that manages the world, renderer and input and put them togheter in a convenient way
     */
    public class MainMenuScreen : Screen, EventListener
    {
        private Scene scene;

        public override void Init()
        {
            this.scene = new Scene(new Camera2D(GetGraphics(), 1280, 720), this);

            UIButton button = new UIButton("Play!", scene.GetWidth() / 2, 300, 2.5f);
            scene.Add("start", button);

            UIButton highscores = new UIButton("Level Editor", scene.GetWidth() / 2, 440, 2.5f);
            scene.Add("editor", highscores);

            UIButton exit = new UIButton("Exit", scene.GetWidth() / 2, 560, 2.5f);
            scene.Add("exit", exit);

            UIImage title = new UIImage(Assets.GetRegion("Title"), scene.GetWidth() / 2, 160, 1.5f);
            scene.Add("title", title);
        }


        public override void Update(float delta)
        {
            scene.Update(delta);
        }

        public override void Draw(SpriteBatch batch)
        {
            GetGraphics().Clear(Color.Black);

            batch.Begin();
            batch.Draw(Assets.bg,
                new Rectangle(0, 0, batch.GraphicsDevice.Viewport.Width, batch.GraphicsDevice.Viewport.Height),
                    null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
            batch.End();

            scene.Draw(batch);
        }

        public override void Dispose()
        {

        }

        public void EventCalled(Events e, Actor actor)
        {
            if (e == Events.TouchUp)
            {
                if (actor.Name == "start")
                {
                    if (LevelIO.LevelCount() == 0) { Console.WriteLine("No levels are created!"); return; }

                    SetScreen(new GameScreen());
                }
                else if (actor.Name == "editor")
                {
                    var form = (System.Windows.Forms.Form)System.Windows.Forms.Form.FromHandle(GetGame().Window.Handle);
                    form.WindowState = System.Windows.Forms.FormWindowState.Minimized;

                    new LevelEditorForm(0).Show();
                }
                else if (actor.Name == "exit")
                {
                    System.Windows.Forms.Application.Exit();
                }
            }
        }
    }
}
