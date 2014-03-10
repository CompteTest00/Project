using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GTE
{
    public class Tiles
    {
        protected Texture2D texture;
        public Texture2D Texture { get { return texture; } set { texture = value; } }

        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get { return rectangle; }
            protected set { rectangle = value; }
        }

        private static ContentManager content;
        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle,null, Color.White,0f,Vector2.Zero,SpriteEffects.None,0f);
        }
    }

    public class CollisionTiles : Tiles
    {
        public CollisionTiles(int number, Rectangle newRectangle)
        {
            if (number == 0)
            {
                texture = Resources.texture_wood;
            }
            if (number == 1)
            {
                texture = Resources.texture_wood_down;
            }
            if (number == 2)
            {
                texture = Resources.texture_wood_up;
            }
            if (number == 3)
            {
                texture = Resources.texture_wood_right;
            }
            if (number == 4)
            {
                texture = Resources.texture_wood_left;
            }
            if (number == 5)
            {
                texture = Resources.texture_window1;
            }
            if (number == 6)
            {
                texture = Resources.texture_window2;
            }
            this.Rectangle = newRectangle;
        }
    }
}
