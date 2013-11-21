using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GTE
{
    class Wall
    {
        // FIELDS
        public Rectangle Hitbox;
        Texture2D Texture;
        Color Color;

        // CONSTRUCTOR
        public Wall(int x, int y, Texture2D texture, int size, Color color)
        {
            this.Texture = texture;
            this.Hitbox = new Rectangle(x, y, size, size);
            this.Color = color;
        }

        // METHODS

        // UPDTADE & DRAW
        public void Update(MouseState mouse, KeyboardState keyboard)
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Hitbox, this.Color);
        }
    }
}
