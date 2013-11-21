using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GTE
{
    class GameMain
    {
        // FIELDS
        Player LocalPlayer;
        List<Wall> Walls;

        bool ClickDown;

        // CONSTRUCTOR
        public GameMain()
        {
            LocalPlayer = new Player();
            Walls = new List<Wall>();
            ClickDown = false;
        }

        // METHODS

        // UPDTATE & DRAW
        public void Updtate(MouseState mouse, KeyboardState keyboard)
        {
            LocalPlayer.Updtate(mouse, keyboard, Walls);
            foreach (Wall wall in Walls)
            {
                wall.Update(mouse, keyboard);
            }
            if (mouse.LeftButton == ButtonState.Pressed && !ClickDown)
            {
                //Resources.Match.Play();
                Walls.Add(new Wall(mouse.X, mouse.Y, Resources.Pixel, 32, Color.Black));
                ClickDown = true;
            }
            if (mouse.LeftButton == ButtonState.Released && ClickDown)
            {
                ClickDown = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {      
            foreach (Wall wall in Walls)
            {
                wall.Draw(spriteBatch);
            }
            LocalPlayer.Draw(spriteBatch);
        }
    }
}
