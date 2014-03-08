using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GTE
{
    public class Button
    {
        #region FIELDS
        public Texture2D texture;
        public Vector2 position;
        public Rectangle rectangle;
        public Vector2 size;
        Color color = new Color(255, 255, 255);
        public bool overlay;
        public bool isClicked;
        #endregion

        #region CONSTRUCTOR
        public Button(Texture2D texture, GraphicsDevice graphics)
        {
            this.texture = texture;
            size = new Vector2(graphics.Viewport.Width / 8, graphics.Viewport.Height / 10);
        }
        #endregion

        #region METHODS
        public void setPosition(Vector2 newPosition)
        {
           this.position = newPosition;
        }

        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle))
            {
                if (mouse.LeftButton == ButtonState.Pressed) isClicked = true;
                if (color.A == 255) overlay = false;
                if (color.A == 0) overlay = true;
                if (overlay) color.A += 3;
                else color.A -= 3;
            }
            else if (color.A < 255)
            {
                color.A += 3;
                isClicked = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, color);
        }
        #endregion



    }
}
