using MAH_TowerDefense.Entity;
using MAH_TowerDefense.Entity.Towers;
using MAH_TowerDefense.Screens;
using MAH_TowerDefense.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Simon.Mah.Framework.Scene2D;
using Simon.Mah.Framework.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MAH_TowerDefense.Views
{
    public class UIController : EventListener
    {
        public const int WIDTH = 1280;
        public const int HEIGHT = 720;
        public const int PANEL_WIDTH = WIDTH / 4;

        public static float CAMERA_SPEED = 9;

        private GameScreen gameScreen;
        private World world;
        private Camera2D camera;
        private Rectangle selection;
        private Vector2 startSelection;
        private Scene scene;
        private UIGroup buyGroup;
        private UIGroup upgradeGroup;
        private UIGroup tabGroup;
        private UIButton upgrade;

        private int tabState;
        private string toolTip;
        private float dt = 0;

        public UIController(GameScreen gameScreen)
        {
            this.gameScreen = gameScreen;
            this.world = gameScreen.GetWorld();
            this.camera = new Camera2D(gameScreen.GetGraphics(), WIDTH, HEIGHT);
            this.selection = Rectangle.Empty;
            this.scene = new Scene(camera, this);
            this.toolTip = "";

            InitUI();
        }

        private void InitUI()
        {
            this.buyGroup = new UIGroup();
            scene.Add("buy", buyGroup);

            // 1
            UIButton buy1 = new UIButton("Cannon(" + new TowerFactory.CannonTower().Cost + ")", WIDTH - PANEL_WIDTH + 80 + 0, 500);
            buyGroup.Add("buyCannon", buy1);

            // 2
            UIButton buy2 = new UIButton("Frost(" + new TowerFactory.FrostTower().Cost + ")", buy1.GetX() + buy1.GetBounds().Width / 2 + buy1.GetBounds().Width + 10, 500);
            buyGroup.Add("buyFrost", buy2);

            // 3
            UIButton buy3 = new UIButton("Nuclear(" + new TowerFactory.NuclearTower().Cost + ")", WIDTH - PANEL_WIDTH + 85 + 0, 580);
            buyGroup.Add("buyNuclear", buy3);

            // 4
            UIButton buy4 = new UIButton("Fire(" + new TowerFactory.SunTower().Cost + ")", buy1.GetX() + buy1.GetBounds().Width / 2 + buy1.GetBounds().Width + 17, 580);
            buyGroup.Add("buySun", buy4);

            // 5
            //UIButton buy5 = new UIButton("Cannon(" + new TowerFactory.CannonTower().Cost + ")", WIDTH - PANEL_WIDTH + 80 + 0, 660);
           // buyGroup.Add("buyCannon5", buy5);

            // 6
           // UIButton buy6 = new UIButton("Rocket(" + new TowerFactory.RocketTower().Cost + ")", buy1.GetX() + buy1.GetBounds().Width / 2 + buy1.GetBounds().Width + 10, 660);
            //buyGroup.Add("buyRocket6", buy6);

            // Upgrade
            this.upgradeGroup = new UIGroup();
            scene.Add("upgradeGroup", upgradeGroup);
            upgrade = new UIButton("Upgrade", WIDTH - PANEL_WIDTH / 2, 580);
            upgradeGroup.Add("upgrade", upgrade);

            // Tab
            this.tabState = 0;
            this.tabGroup = new UIGroup();
            scene.Add("tabGroup", tabGroup);
            UIButton tab1 = new UIButton("i", WIDTH - PANEL_WIDTH + 29, 480);
            tabGroup.Add("infoTab", tab1);

            UIButton tab2 = new UIButton("u", WIDTH - PANEL_WIDTH + 72, 480);
            tabGroup.Add("upgradeTab", tab2);
        }

        public void Update(float delta)
        {
            toolTip = "";
            scene.Update(delta);
            dt = delta; //for fps

            Vector2 worldMouse = gameScreen.GetRenderer().Camera.Unproject(Mouse.GetState().X, Mouse.GetState().Y);

            if (!gameScreen.IsPlacing())
                UpdateSelection();

            UpdateCameraMovement(delta, worldMouse);
        }

        private void UpdateCameraMovement(float delta, Vector2 worldMouse)
        {
            Camera2D cam = gameScreen.GetRenderer().Camera;
            float dt = cam.GetWidth() / 40f;

            // X
            if (cam.GetPosition().X + dt > worldMouse.X || InputHandler.IsKeyDown(Keys.Left))
            {
                cam.SetPosition(cam.GetPosition().X - CAMERA_SPEED, cam.GetPosition().Y);
                startSelection.X += CAMERA_SPEED;
            }
            if (cam.GetPosition().X + cam.GetWidth() - dt < worldMouse.X || InputHandler.IsKeyDown(Keys.Right))
            {
                cam.SetPosition(cam.GetPosition().X + CAMERA_SPEED, cam.GetPosition().Y);
                startSelection.X -= CAMERA_SPEED;
            }

            // Y
            if (cam.GetPosition().Y + dt > worldMouse.Y || InputHandler.IsKeyDown(Keys.Up))
            {
                cam.SetPosition(cam.GetPosition().X, cam.GetPosition().Y - CAMERA_SPEED);
                startSelection.Y += CAMERA_SPEED;
            }
            if (cam.GetPosition().Y + cam.GetHeight() - dt < worldMouse.Y || InputHandler.IsKeyDown(Keys.Down))
            {
                cam.SetPosition(cam.GetPosition().X, cam.GetPosition().Y + CAMERA_SPEED);
                startSelection.Y -= CAMERA_SPEED;
            }
        }

        private void UpdateSelection()
        {
            Vector2 mouse = camera.Unproject(Mouse.GetState().X, Mouse.GetState().Y);

            if (InputHandler.ClickedDown() && mouse.X < camera.GetWidth() - PANEL_WIDTH)
            {
                selection = new Rectangle((int)mouse.X, (int)mouse.Y, 1, 1);
                startSelection = new Vector2(selection.X, selection.Y);
            }

            if (!selection.Equals(Rectangle.Empty))
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    if ((mouse.X - startSelection.X) >= 0)
                    {
                        selection.X = (int)startSelection.X;
                        selection.Width = (int)(mouse.X - selection.X);
                    }
                    else
                    {
                        selection.X = (int)(mouse.X);
                        selection.Width = (int)(startSelection.X - selection.X);
                    }

                    if ((mouse.Y - startSelection.Y) >= 0)
                    {
                        selection.Y = (int)startSelection.Y;
                        selection.Height = (int)(mouse.Y - selection.Y);
                    }
                    else
                    {
                        selection.Y = (int)(mouse.Y);
                        selection.Height = (int)(startSelection.Y - selection.Y);
                    }
                }
                else
                {
                    Select();
                }
            }
        }

        private void Select()
        {
            Vector2 start = gameScreen.GetRenderer().Camera.Unproject(selection.X, selection.Y);
            Vector2 end = gameScreen.GetRenderer().Camera.Unproject(selection.X + selection.Width, selection.Y + selection.Height);

            start *= camera.GetViewPortScale();
            end *= camera.GetViewPortScale();

            world.Select(new Rectangle((int)start.X, (int)start.Y, Math.Max(1, (int)(end.X - start.X)), Math.Max(1, (int)(end.Y - start.Y))));
            selection = Rectangle.Empty;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Begin(SpriteSortMode.BackToFront,
                      BlendState.AlphaBlend,
                      SamplerState.LinearClamp,
                      null,
                      null,
                      null,
                      camera.GetMatrix());

            if (selection != null)
                DrawSelection(batch, selection, 3, Color.SeaGreen);

            DrawUI(batch);

            scene.Draw(batch, false);

            // debug Draw fps
            batch.DrawString(Assets.font, "fps: " + (int)(1 + 1 / dt), new Vector2(0, -3), Color.White, 0, Vector2.Zero, .4f, SpriteEffects.None, 0);

            batch.End();

        }

        private void DrawUI(SpriteBatch batch)
        {
            // Draw ui bar
            batch.Draw(Assets.items, new Rectangle(WIDTH - PANEL_WIDTH, 0, PANEL_WIDTH, HEIGHT), Assets.GetRegion("Pixel"), Color.Gray, 0, Vector2.Zero, SpriteEffects.None, .95f);

            // Draw miniMap
            batch.Draw(Assets.items, new Rectangle(WIDTH - PANEL_WIDTH, 0, PANEL_WIDTH, (int)(PANEL_WIDTH / 1.6f)), Assets.GetRegion("Pixel"), Color.DarkOliveGreen, 0, Vector2.Zero, SpriteEffects.None, .92f);
            batch.Draw(WorldRenderer.MiniMap, new Rectangle(WIDTH - PANEL_WIDTH, 0, PANEL_WIDTH, (int)(PANEL_WIDTH / 1.6f)), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, .9f);

            // Waves stats
            DrawFromContainer(batch, "Wave Info:", 0, 220, .48f);
            DrawFromContainer(batch, world.GetWave().Description, 0, 250, .53f);
            DrawFromContainer(batch, "Waves: " + world.GetWavesLeft(), 0, 370, .55f);

            if (world.GetState() == World.GameState.WAITING)
            {
                DrawFromContainer(batch, "Next: " + (int)world.GetWaitTime(), PANEL_WIDTH / 2, 370, .55f, world.GetWaitTime() <= 5 ? Color.Red : Color.White);
                if (world.GetState() == World.GameState.WAITING)
                    DrawFromContainer(batch, "Press Enter for Next Wave", -600, 5, .5f);
            }

            // Gold and lives
            DrawFromContainer(batch, "Gold: " + world.GetGold(), 0, 400, .55f);
            DrawFromContainer(batch, "Life: " + world.GetLives(), PANEL_WIDTH / 2, 400, .55f);

            DrawActionsPanel(batch);
            DrawToolTip(batch);
        }

        private void DrawToolTip(SpriteBatch batch)
        {
            DrawFromContainer(batch, toolTip, -5, 640, .55f);
        }

        private void DrawActionsPanel(SpriteBatch batch)
        {
            // Action bg
            batch.Draw(Assets.items, new Rectangle(WIDTH - PANEL_WIDTH, HEIGHT - (int)(PANEL_WIDTH / 1.25f), PANEL_WIDTH, (int)(PANEL_WIDTH / 1.25f)), Assets.GetRegion("Pixel"), Color.DarkBlue, 0, Vector2.Zero, SpriteEffects.None, .91f);
            
            // Draw splitter
            batch.Draw(Assets.items, new Rectangle(WIDTH - PANEL_WIDTH - 3 , 0, 3, HEIGHT), Assets.GetRegion("Pixel"), Color.Black, 0, Vector2.Zero, SpriteEffects.None, .89f);


            buyGroup.Enabled = false;
            upgradeGroup.Enabled = false;
            tabGroup.Enabled = false;

            if (world.GetSelected().Count == 0)
            {
                // Draw buy menu
                if (gameScreen.IsPlacing())
                    DrawStats(batch, gameScreen.GetPlacingTower().Stats);
                else
                    buyGroup.Enabled = true;
            }
            else
            {
                int cost = world.GetSelected().Where(x => x is Tower).Cast<Tower>().Sum(x => x.Cost);
                if (cost != 0)
                    upgrade.SetText("Upgrade (" + cost + ")");

                // Draw info and upgrade
                if (world.GetSelected().Count == 1)
                {
                    // Draw single Stats
                    if (world.GetSelected()[0] is Tower)
                    {
                        tabGroup.Enabled = true;
                        if (tabState == 0)
                            DrawStats(batch, world.GetSelected()[0].Stats);
                        else
                            upgradeGroup.Enabled = true;
                    }
                    else
                    {
                        // Enemy
                        upgradeGroup.Enabled = true;
                        StatsData stats = world.GetSelected()[0].Stats;
                        if (world.GetSelected()[0].Alive)
                            DrawStats(batch, stats);
                    }
                }
                else
                {
                    // Draw upgrade all
                    upgradeGroup.Enabled = true;
                }
            }
        }

        private void DrawStats(SpriteBatch batch, StatsData stats)
        {
            DrawFromContainer(batch, "Damage: " + (int)stats.Damage + "\n" +
                "Health: " + (int)stats.Health + "\n" +
            "Speed: " + Math.Round(stats.Speed, 2) + "\n" +
            "Armor: " + (int)stats.Armor + "\n" +
            "Range: " + (int)stats.Radius + "\n" +
            "Crit Chance: " + (int)stats.CritChance + "\n", 65, 513, .55f);
        }

        private void DrawSelection(SpriteBatch batch, Rectangle rectangleToDraw, int thicknessOfBorder, Color color)
        {
            // Draw top line
            batch.Draw(Assets.GetRegion("Pixel"), new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), Assets.GetRegion("Pixel"), color, 0, Vector2.Zero, SpriteEffects.None, 1);

            // Draw left line
            batch.Draw(Assets.GetRegion("Pixel"), new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), Assets.GetRegion("Pixel"), color, 0, Vector2.Zero, SpriteEffects.None, 1);

            // Draw right line
            batch.Draw(Assets.GetRegion("Pixel"), new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder),
                                            rectangleToDraw.Y,
                                            thicknessOfBorder,
                                            rectangleToDraw.Height), Assets.GetRegion("Pixel"), color, 0, Vector2.Zero, SpriteEffects.None, 1);
            // Draw bottom line
            batch.Draw(Assets.GetRegion("Pixel"), new Rectangle(rectangleToDraw.X,
                                            rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder,
                                            rectangleToDraw.Width,
                                            thicknessOfBorder), Assets.GetRegion("Pixel"), color, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        public void DrawFromContainer(SpriteBatch batch, string text, float offset, float y, float scale = 1, Color? color = null)
        {
            text = WrapText(text, PANEL_WIDTH * .94f, scale);
            batch.DrawString(Assets.font, text,
                    new Vector2(offset + camera.GetWidth() - PANEL_WIDTH + PANEL_WIDTH * .06f, y),
                         color ?? Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        public static string WrapText(string text, float maxLineWidth, float scale = 1)
        {
            text.Replace("\n", " \n ");
            string[] words = text.Split(' ');
            StringBuilder sb = new StringBuilder();
            float lineWidth = 0f;
            float spaceWidth = Assets.font.MeasureString(" ").X * scale;

            foreach (string word in words)
            {
                Vector2 size = Assets.font.MeasureString(word) * scale;

                if (lineWidth + size.X < maxLineWidth)
                {
                    sb.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    sb.Append((word.Contains("\n") ? "" : "\n") + word + " ");
                    lineWidth = size.X + spaceWidth;
                }
            }

            return sb.ToString();
        }

        public void EventCalled(Events e, Actor actor)
        {
            if (e == Events.TouchUp)
            {
                if (actor.Name.StartsWith("buy"))
                    gameScreen.StartPlacingTower(actor.Name.Substring(3));

                if (actor.Name.Equals("upgrade"))
                {
                    int cost = world.GetSelected().Where(x => x is Tower).Cast<Tower>().Sum(x => x.Cost);
                    if (world.GetGold() >= cost)
                    {
                        // Buy!
                        world.WithdrawGold(cost);
                        world.GetSelected().Where(x => x is Tower).Cast<Tower>().ToList().ForEach(x => x.Upgrade());
                    }
                }

                // Tabs
                if (actor.Name.Equals("infoTab"))
                    tabState = 0;
                if (actor.Name.Equals("upgradeTab"))
                    tabState = 1;
            }
            else if (e == Events.MouseHover)
            {
                if (actor.Name.StartsWith("buy"))
                {
                    Type type = Type.GetType("MAH_TowerDefense.Entity.Towers.TowerFactory+" + actor.Name.Substring(3) + "Tower");
                    toolTip = type.GetField("Description", BindingFlags.Public | BindingFlags.Static).GetValue(null).ToString();
                }
            }
        }
    }
}
