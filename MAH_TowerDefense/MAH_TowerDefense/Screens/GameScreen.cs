using Microsoft.Xna.Framework;
using Simon.Mah.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spline;
using Microsoft.Xna.Framework.Graphics;
using Simon.Mah.Framework.Scene2D;
using Microsoft.Xna.Framework.Input;
using MAH_TowerDefense.Views;
using MAH_TowerDefense.Worlds;
using System.IO;
using MAH_TowerDefense.Entity.Towers;
using MAH_TowerDefense.Entity;
using Simon.Mah.Framework.Tools;

namespace MAH_TowerDefense.Screens
{
    public class GameScreen : Screen
    {
        private WorldRenderer renderer;
        private World world;
        private UIController hud;

        private Tower placingTower;

        private float timeModifier;
        private bool isPlacing = true; //TODO 

        public override void Init()
        {
            this.world = new World(35, 20, WorldRenderer.WIDTH / World.TILES_HORIZONTAL);
            this.renderer = new WorldRenderer(world, GetGraphics());
            this.hud = new UIController(this);
            this.timeModifier = 1;

            //deubg
            placingTower = TowerFactory.CreateCannon(200, 200);
            world.AddEntity(placingTower);
        }

        public override void Update(float delta)
        {
            UpdateInput();
            hud.Update(delta);

            world.Update(delta * timeModifier);
            renderer.Update(delta);

            UpdatePlacingTower();
        }

        private void UpdatePlacingTower()
        {
            if (!isPlacing) return;

            Vector2 worldMouse = renderer.GetCamera().Unproject(Mouse.GetState().X, Mouse.GetState().Y);
            placingTower.SetPosition(worldMouse.X, worldMouse.Y);

            if (CanPlace(placingTower, renderer.GetCamera()))
            {
                placingTower.sprite.Color = Color.White;
                if (InputHandler.Clicked())
                    PlaceTowerAction();
            }
            else
                placingTower.sprite.Color = Color.Red;

            if (InputHandler.RightClicked()) PlaceTowerAction(false);
        }

        private void PlaceTowerAction(bool success = true)
        {
            if (success)
                placingTower.Place();
            else
                world.RemoveEntity(placingTower);
            placingTower = null;
            isPlacing = false;
        }

        private void UpdateInput()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                timeModifier = 3;
            else
                if (InputHandler.KeyReleased(Keys.Space)) timeModifier = 1;


            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            renderer.Render(batch);

            hud.Draw(batch);
        }

        /// <summary>
        /// Return if world space is occupied or not
        /// </summary>
        /// <param name="wx">World cord X</param>
        /// <param name="wy">World cord X</param>
        public bool CanPlace(GameObject g, Camera2D camera)
        {
            if (!camera.IsInside(g.GetBounds())) return false;

            Rectangle bounds = g.GetBounds();
            bounds.Offset(-(int)camera.GetPosition().X, -(int)camera.GetPosition().Y);

            int top = Math.Max(bounds.Top, 0);
            int bottom = Math.Min(bounds.Bottom, WorldRenderer.RenderTarget.Height);
            int left = Math.Max(bounds.Left, 0);
            int right = Math.Min(bounds.Right, WorldRenderer.RenderTarget.Width);

            Rectangle searchArea = new Rectangle(left, top, (right - left), (bottom - top));

            Color[] gPixels = new Color[g.sprite.Region.GetSource().Width * g.sprite.Region.GetSource().Height]; // render Target
            Color[] bPixels = new Color[searchArea.Width * searchArea.Height];


            g.sprite.Region.GetTexture().GetData<Color>(0, g.sprite.Region, gPixels, 0, gPixels.Length);

            WorldRenderer.RenderTarget.GetData<Color>(0, searchArea, bPixels, 0, bPixels.Length);

            // Pixel perfect
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Color colorA = gPixels[(int)((x - bounds.Left) / g.sprite.GetRealScale().X) +
                    (int)((int)((y - bounds.Top) / g.sprite.GetRealScale().Y) * (bounds.Width / g.sprite.GetRealScale().X))];

                    Color colorB = bPixels[(x - searchArea.Left) + (y - searchArea.Top) * (searchArea.Width)];

                    if (colorA.A != 0 && colorB.A != 0) // Collision
                        return false;
                }
            }
            return true;
        }

        public override void Dispose()
        {
        }

        public World GetWorld()
        {
            return world;
        }

        public WorldRenderer GetRenderer()
        {
            return renderer;
        }

        public bool IsPlacing()
        {
            return isPlacing;
        }
    }
}
