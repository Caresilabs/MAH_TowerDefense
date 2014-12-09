using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simon.Mah.Framework.Scene2D
{
    public class UIButton : Actor
    {
        private Rectangle bounds;
        private Vector2 textSize;
        private Vector2 startPos;
        private Color color;

        private string text;
        private float scale;

        public UIButton(string text, float x, float y, float scale = 1)
            : base(x, y, 10, 10)
        {
            this.text = text;
            this.scale = scale;
            this.color = Color.Purple;
            this.startPos = new Vector2(x, y);
            this.bounds = new Rectangle((int)x - (int)(UIConfig.DEFAULT_FONT.MeasureString(text).Length() / 2 * (scale * UIConfig.DEFAULT_FONT_SCALE)), (int)y,
                (int)(UIConfig.DEFAULT_FONT.MeasureString(text).Length() * (scale * UIConfig.DEFAULT_FONT_SCALE)), (int)(UIConfig.DEFAULT_FONT.MeasureString(text).Y * (scale * UIConfig.DEFAULT_FONT_SCALE)));
            
            this.textSize = (UIConfig.DEFAULT_FONT.MeasureString(text));
            textSize = Vector2.Multiply(textSize, (scale * UIConfig.DEFAULT_FONT_SCALE));

            SetRegion(UIConfig.DEFAULT_BUTTON);
            SetPosition(bounds.X - bounds.Width / 20, bounds.Y - bounds.Width / 10);
            SetSize(bounds.Width + bounds.Width / 10, bounds.Height + bounds.Width / 20);
        }

        public override void TouchDown(Vector2 mouse)
        {
            base.TouchDown(mouse);
            color = Color.Red;
        }

        public override void MouseHover(Vector2 mouse)
        {
            base.MouseHover(mouse);
            color = Color.YellowGreen;
        }

        public override void MouseLeave(Vector2 mouse)
        {
            base.MouseLeave(mouse);
            color = Color.Purple;
        }

        public override void TouchUp(Vector2 mouse)
        {
            base.TouchUp(mouse);
            color = Color.Purple;
        }

        public override void TouchLeave(Vector2 mouse)
        {
            base.TouchLeave(mouse);
            color = Color.Purple;
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(GetRegion(), GetBounds(), GetRegion(), color, 0, Vector2.Zero, SpriteEffects.None, .1f);
            batch.DrawString(UIConfig.DEFAULT_FONT, text, 
                new Vector2(GetX() + (GetBounds().Width - textSize.X) / 2, GetY() + (GetBounds().Height - textSize.Y) / 2),
                Color.White, 0, Vector2.Zero, scale * UIConfig.DEFAULT_FONT_SCALE, SpriteEffects.None, 0);

            base.Draw(batch);
        }

        public void SetText(string text)
        {
            this.text = text;

            this.textSize = (UIConfig.DEFAULT_FONT.MeasureString(text));
            textSize = Vector2.Multiply(textSize, (scale * UIConfig.DEFAULT_FONT_SCALE));

            this.bounds = new Rectangle((int)startPos.X - (int)(textSize.X)/2,
                (int)startPos.Y, (int)(textSize.X), (int)textSize.Y); // height = UIConfig.DEFAULT_FONT.MeasureString(text).Y

            SetPosition(bounds.X - 20, bounds.Y - 10);
            SetSize(bounds.Width + 30, bounds.Height + 30);
        }
    }
}
