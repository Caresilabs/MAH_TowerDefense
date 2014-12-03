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
using System.Text;

namespace MAH_TowerDefense.Views
{
    public class UIController
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

        public UIController(GameScreen gameScreen)
        {
            this.gameScreen = gameScreen;
            this.world = gameScreen.GetWorld();
            this.camera = new Camera2D(gameScreen.GetGraphics(), WIDTH, HEIGHT);
            this.selection = Rectangle.Empty;
        }

        public void Update(float delta)
        {
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

            if (InputHandler.ClickedDown())
            {
                selection = new Rectangle((int)mouse.X, (int)mouse.Y, 0, 0);
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
            Vector2 size = gameScreen.GetRenderer().Camera.Unproject(selection.Width, selection.Height);
            world.Select(new Rectangle((int)start.X, (int)start.Y, (int)size.X, (int)size.Y));
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

            batch.End();

        }

        private void DrawUI(SpriteBatch batch)
        {
            batch.Draw(Assets.items, new Rectangle(WIDTH - PANEL_WIDTH, 0, PANEL_WIDTH, HEIGHT), Assets.GetRegion("Pixel"), Color.Gray, 0, Vector2.Zero, SpriteEffects.None, .95f);
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

        public static void drawCenterString(SpriteBatch batch, string text, float y, Color color, float scale = 1)
        {
            batch.DrawString(Assets.font, text,
                    new Vector2(
                         batch.GraphicsDevice.Viewport.Width / 2 - ((Assets.font.MeasureString(text).Length() / 2) * scale), y),
                         color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        public static void drawCenterString(SpriteBatch batch, string text, float y, float scale = 1)
        {
            drawCenterString(batch, text, y, Color.Black, scale);
        }
    }
}
